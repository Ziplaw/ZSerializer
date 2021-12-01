using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using ZSerializer;

public class NodeSpawner : PersistentMonoBehaviour
{
    private Camera cam;
    public GameObject roadPrefab;
    public GameObject anchorPrefab;
    public Transform currentInstancedNode;
    public Transform lastPlacedNode;
    public Anchor lastPlacedAnchor;
    public UIManager uiManager;
    public GameObject tutorialCanvas;

    [Serializable]
    public struct TransformData
    {
        public Transform transform;
        public Vector2 position;
        public Quaternion rotation;

        public void Apply()
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
    
    public List<TransformData> initialTransformDatas = new List<TransformData>();

    private void Start()
    {
        cam = Camera.main;
    }

    void CreateNode(Transform anchor)
    {
        lastPlacedNode = currentInstancedNode;
        currentInstancedNode = Instantiate(roadPrefab, anchor.position, Quaternion.identity, transform).transform;
        var hingeJoint2D = currentInstancedNode.GetComponent<PersistentGameObject>().AddComponent<HingeJoint2D>(PersistentType.Everything);
        hingeJoint2D.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        var anchorComponent = anchor.GetComponent<Anchor>();
        if (anchorComponent.nodeTransform)
        {
            hingeJoint2D.connectedBody = anchorComponent.nodeTransform.GetComponent<Rigidbody2D>();
        }
    }
    
    

    private void Update()
    {
        if (!currentInstancedNode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 0f;

                Vector2 v = cam.ScreenToWorldPoint(mousePosition);

                Collider2D[] col = Physics2D.OverlapPointAll(v);

                if (col.Length > 0)
                {
                    foreach (Collider2D c in col)
                    {
                        if (c.GetComponent<Anchor>())
                        {
                            CreateNode(c.transform);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleCanvas();
            }
        }
        else
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0f;

            Vector2 v = cam.ScreenToWorldPoint(mousePosition);
            var localScale = currentInstancedNode.localScale;
            localScale.x = Mathf.Clamp((v - (Vector2)currentInstancedNode.position).magnitude, 0, 2);
            currentInstancedNode.localScale = localScale;

            var quaternion = currentInstancedNode.rotation;
            quaternion.eulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y - currentInstancedNode.position.y, v.x - currentInstancedNode.position.x) * Mathf.Rad2Deg);
            currentInstancedNode.rotation = quaternion;

            if (Input.GetMouseButtonDown(0))
            {
                lastPlacedAnchor = Instantiate(anchorPrefab, currentInstancedNode.position + currentInstancedNode.right * currentInstancedNode.localScale.x, Quaternion.identity, transform).GetComponent<Anchor>();
                lastPlacedAnchor.nodeTransform = currentInstancedNode;
                CreateNode(lastPlacedAnchor.transform);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var hinge = lastPlacedAnchor.GetComponent<PersistentGameObject>().AddComponent<HingeJoint2D>(PersistentType.Everything);
                hinge.GetComponent<BoxCollider2D>().isTrigger = false;
                hinge.connectedBody = lastPlacedNode.GetComponent<Rigidbody2D>();
                Destroy(currentInstancedNode.gameObject);
            }
        }
    }

    private void ToggleCanvas()
    {
        uiManager.gameObject.SetActive(!uiManager.gameObject.activeSelf);
        tutorialCanvas.SetActive(!tutorialCanvas.activeSelf);
        uiManager.DestroyAllButtons();
        var levelNames = ZSerialize.GetLevelNames();
        foreach (var levelName in levelNames)
        {
            uiManager.CreateButton(levelName, transform, () =>
            {
                ZSerialize.LoadLevel(levelName, transform, true);
            });
        }
    }

    public void ResetNodes()
    {
        initialTransformDatas.ForEach(td =>
        {
            var rb = td.transform.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
            }

            td.Apply();
        });
    }

    private void OnGUI()
    {
        if (!uiManager.gameObject.activeSelf)
        {
            if (GUILayout.Button("Play"))
            {
                foreach (var component in GetComponentsInChildren<Transform>())
                {
                    initialTransformDatas.Add(new TransformData
                    {
                        transform = component,
                        position = component.position,
                        rotation = component.rotation
                    });

                    var rb = component.GetComponent<Rigidbody2D>();
                    if (rb && rb.bodyType == RigidbodyType2D.Kinematic)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                }
            }

            if (GUILayout.Button("Reset"))
            {
                ResetNodes();
            }
        }
    }
}