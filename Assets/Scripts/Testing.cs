using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{

    public class Testing : MonoBehaviour
    {
        private Grid grid;

        private void Start()
        {
            grid = new Grid(18, 10, 1f, new Vector3(-9, -5));
            //new Grid(4, 3, 5f, new Vector3(0, -20));
            //new Grid(10, 10, 20f, new Vector3(-100f, -20));

        }

        







        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }

        public static Vector3 GetMouseWorldPositionWithZ()
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        }

        public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
        {
            return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
        {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }

       
    }
}
