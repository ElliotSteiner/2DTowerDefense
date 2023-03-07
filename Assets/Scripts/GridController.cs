using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.UIElements;
namespace Utils
{
    public class GridController : MonoBehaviour
    {
        public Sprite build;
        public Sprite noBuild;

        private Grid grid;
        [SerializeField] private Tilemap Tiles = null;
        [SerializeField] private Tilemap interactiveMap = null;
        [SerializeField] private Tile hoverTile = null;
        [SerializeField] private Tile redHoverTile = null;


        [SerializeField]
        private List<TileData> tileDatas;

        private Dictionary<TileBase, TileData> dataFromTiles;


        private Vector3Int previousMousePos = new Vector3Int();

        void Start()
        {
            grid = gameObject.GetComponent<Grid>();
        }

        void Update()
        {
            Vector3Int mousePos = GetMousePosition();

            

            if (!mousePos.Equals(previousMousePos))
            {
                GetTileType();
                if(GetTileType() == false)
                {
                    interactiveMap.SetTile(previousMousePos, null);
                    interactiveMap.SetTile(mousePos, hoverTile);
                }
                if(GetTileType() == true)
                {
                    interactiveMap.SetTile(previousMousePos, null);
                    interactiveMap.SetTile(mousePos, redHoverTile);
                }
                
                //interactiveMap.SetTile(previousMousePos, null);
                //interactiveMap.SetTile(mousePos, hoverTile);
                previousMousePos = mousePos;
                //Tiles.GetTile(mousePos);
            }

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

        Vector3Int GetMousePosition()
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return Tiles.WorldToCell(mouseWorldPos);
            
        }

        public bool GetTileType()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = Tiles.WorldToCell(mousePosition);

            TileBase tile = Tiles.GetTile(gridPosition);

            bool path = dataFromTiles[tile].path;


            return path;
        }

    }
}