using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using ZSerializer;

public class NodeSpawner : MonoBehaviour
{
    private Camera cam;
    public GameObject roadPrefab;
    public GameObject anchorPrefab;
    public Transform currentInstancedRoad;
    public UIManager uiManager;

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
    
    private List<TransformData> initialTransformDatas = new List<TransformData>();

    private void Start()
    {
        cam = Camera.main;
    }

    void CreateNode(Transform anchor)
    {
        currentInstancedRoad = Instantiate(roadPrefab, anchor.position, Quaternion.identity, transform).transform;
        var dj = currentInstancedRoad.GetComponent<PersistentGameObject>().AddComponent<HingeJoint2D>();
        dj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        var pc = anchor.GetComponent<ParentConstraint>();
        if (pc)
        {
            dj.connectedBody = pc.GetSource(0).sourceTransform.GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (!currentInstancedRoad)
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
            var localScale = currentInstancedRoad.localScale;
            localScale.x = Mathf.Clamp((v - (Vector2)currentInstancedRoad.position).magnitude, 0, 2);
            currentInstancedRoad.localScale = localScale;

            var quaternion = currentInstancedRoad.rotation;
            quaternion.eulerAngles = new Vector3(0, 0, Mathf.Atan2(v.y - currentInstancedRoad.position.y, v.x - currentInstancedRoad.position.x) * Mathf.Rad2Deg);
            currentInstancedRoad.rotation = quaternion;

            if (Input.GetMouseButtonDown(0))
            {
                var pc = Instantiate(anchorPrefab, currentInstancedRoad.position + currentInstancedRoad.right * currentInstancedRoad.localScale.x, Quaternion.identity, transform).GetComponent<PersistentGameObject>().AddComponent<ParentConstraint>();
                pc.AddSource(new ConstraintSource { sourceTransform = currentInstancedRoad, weight = 1 });
                pc.SetTranslationOffset(0, new Vector3(currentInstancedRoad.localScale.x, 0, 0));
                pc.constraintActive = true;
                CreateNode(pc.transform);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(currentInstancedRoad.gameObject);
            }
        }
    }

    private void ToggleCanvas()
    {
        uiManager.gameObject.SetActive(!uiManager.gameObject.activeSelf);
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