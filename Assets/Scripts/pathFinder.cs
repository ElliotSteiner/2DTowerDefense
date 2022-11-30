//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Utils;
//using UnityEngine.Tilemaps;

//public class pathFinder : MonoBehaviour
//{
//    private TileBase tb;
//    private Tilemap tm;
//    private Vector3Int position;
//    private int[,] pathFind;
//    private int width;
//    private int height;
    
   

//    private void Start()
//    {

//        tm = GetComponent<Tilemap>();
//        Utils.Grid.width = Utils.Grid.gridWidth;
//        Utils.Grid.height = Utils.Grid.gridHeight;
//        pathChecker();

//    }

//    private void Awake()
//    {
//        pathFind = new int[width, height];
//    }

//    private void pathChecker()
//    {
//        for(int y = 0; y < height; y++)
//        {
//            for(int x = 0; x < width; x++)
//            {
//                position.x = x;
//                position.y = y;
//                position.z = 0;
//                Debug.Log(tb.ToString());
//                Debug.Log(x + " " + y);
//                Debug.Log(width + " " + height);
//            }
//        }
//    }
//}
