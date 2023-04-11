using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.UIElements;
namespace Utils
{
    public class GridController : MonoBehaviour
    {
        //public Sprite build;
        public Sprite noBuild;

        private GridSet gridSet;
        [SerializeField] private Tilemap Tiles = null;
        [SerializeField] private Tilemap interactiveMap = null;
        [SerializeField] private Tile hoverTile = null;
        [SerializeField] private Tile redHoverTile = null;

        private int[,] gridValue;

        [SerializeField]
        private List<TileData> tileDatas;

        private Dictionary<TileBase, TileData> dataFromTiles;


        private Vector3Int previousMousePos = new Vector3Int();

        void Start()
        {
            
            //grid = gameObject.GetComponent<Grid>();
            //gridValue = new int[grid.gridWidth, grid.gridHeight];
            //gridValue = grid.gridValues;
            
        }

        void Update()
        {

            

            Vector3Int mousePos = GetMousePosition();
          
            

            if (!mousePos.Equals(previousMousePos))
            {
                int buildPosX = ((int)gridSet.GetBuildPosX()) + (gridSet.gridWidth / 2);
                int buildPosY = ((int)gridSet.GetBuildPosY()) + (gridSet.gridHeight / 2);
                //Debug.Log(gridValue[buildPosX, buildPosY]);
                //Debug.Log(buildPosX + ", " + buildPosY);
                //GetTileType();
                if(GetTileType() == false)
                {
                    mousePos.x += 7;
                    mousePos.y -= 7;
                    
                    interactiveMap.SetTile(previousMousePos, null);
                    interactiveMap.SetTile(mousePos, hoverTile);
                }
                if (GetTileType() == true)
                {
                    mousePos.x += 7;
                    mousePos.y -= 7;
                    interactiveMap.SetTile(previousMousePos, null);
                    interactiveMap.SetTile(mousePos, redHoverTile);
                }

                if (buildPosX < 18 && buildPosX > -1 && buildPosY > -1 && buildPosY < 10)
               
                    if (gridValue[buildPosX, buildPosY] == 1)
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
            gridSet = FindObjectOfType<GridSet>();
            gridValue = gridSet.gridValues;

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
            //Debug.Log(gridPosition);
            if (gridPosition.x < 9 && gridPosition.x > -10 && gridPosition.y > -6 && gridPosition.y < 5)
            {
                TileBase tile = Tiles.GetTile(gridPosition);
                bool path = dataFromTiles[tile].path;
                //Debug.Log(path);
                return path;
            }
            else
            {
                bool path = true;
                return path;
            }   
        }
    }
}