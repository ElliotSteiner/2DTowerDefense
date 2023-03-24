using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    [SerializeField]
    private float offSet;
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private float upgradeOffset;

    

    private void Awake()
    {
        upgradeOffset = 0;
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "")
    {
        if (content.Contains("Upgrades"))
        {
            upgradeOffset = -1f;
        }

        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (contentLength > characterWrapLimit || headerLength > characterWrapLimit) ? true : false;
    }
    private void Update()
    {
        Vector2 position = Input.mousePosition;

        float pivotX = (position.x / Screen.width) - offSet;
        float pivotY = (position.y / Screen.height) - upgradeOffset;
        

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }
}
