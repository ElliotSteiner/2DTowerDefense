using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Utils
{

    public class MapManager : MonoBehaviour
    {
        

        private string selectTile;

        //private TileData tileData;
        private bool isPath;

        [SerializeField]
        private Tilemap map;



        [SerializeField]
        private List<TileData> tileDatas;




        private Dictionary<TileBase, TileData> dataFromTiles;



        private void Start()
        {
           
        }

        private void Awake()
        {
            //tileData = FindObjectOfType<TileData>();
            dataFromTiles = new Dictionary<TileBase, TileData>();
            

            foreach (var tileData in tileDatas)
            {
                foreach (var tile in tileData.tiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }


        }






        void Update()
        {
           


            //if (Input.GetMouseButtonDown(1))
            //{
            //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    Vector3Int gridPosition = map.WorldToCell(mousePosition);

            //    TileBase clickedTile = map.GetTile(gridPosition);

            //    bool path = dataFromTiles[clickedTile].path;



            //}


        }

        

        public bool GetTileType()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            TileBase tile = map.GetTile(gridPosition);

            bool path = dataFromTiles[tile].path;


            return path;
        }

       
            
    }

}
