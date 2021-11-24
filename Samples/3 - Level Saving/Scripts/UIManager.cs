using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ZSerializer;

public class UIManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;

    public void DestroyAllButtons()
    {
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateButton(string text, Transform levelParent, UnityAction action)
    {
        GameObject button = Instantiate(buttonPrefab, buttonParent);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        button.GetComponent<Button>().onClick.AddListener(action);
        var buttonInfo = button.AddComponent<ButtonInfo>();
        buttonInfo.levelName= text;
        buttonInfo.levelParent = levelParent;
    }
}