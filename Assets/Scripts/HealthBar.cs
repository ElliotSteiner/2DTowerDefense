using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class HealthBar : MonoBehaviour
    {


        

        public void SetSize(float sizeNormalized)
        {
            transform.Find("Bar").localScale = new Vector3(sizeNormalized, 1f);
        }

        

        

    }
}
