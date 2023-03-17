using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().GetComponent<Renderer>().sortingOrder = 900;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemTextController.textstatus == "off")
        {
            Destroy(gameObject);
        }
    }
}
