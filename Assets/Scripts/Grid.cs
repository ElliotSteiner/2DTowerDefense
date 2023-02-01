using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;
using UnityEngine.EventSystems;

namespace Utils
{
    public class Grid : MonoBehaviour
    {

        private MapManager mapManager;




        [SerializeField]
        private float xAlign;
        [SerializeField]
        private float yAlign;
        [SerializeField]
        public int gridHeight;
        [SerializeField]
        public int gridWidth;


        private string ClickedButtonName;

        private Tilemap map;


        private int TowerAdj;
        private int TowerValue;
        private int width;
        private int height;
        private float cellSize;
        private Vector3 originPosition;
        private int[,] gridArray;
        private TextMesh[,] debugTextArray;
        private int[,] gridValues;

        private Grid grid;
        private int[,] pathPos;

        public const int sortingOrderDefault = 5000;
        private Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new int[width, height];

            debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    //debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 3, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y+1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);


        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellSize + originPosition;
        }
        private void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

        }
        private void Awake()
        {
            mapManager = FindObjectOfType<MapManager>();

            grid = new Grid(20, 10, 1f, new Vector3(-10, -5));
            gridValues = new int[gridWidth, gridHeight];
            //pathChecker();
            //setGrid();

        }

        //private void pathChecker()
        //{
        //   for (int y = 0; y < gridHeight; y++)
        //  {
        //     for (int x = 0; x < gridWidth; x++)
        //    {

        //}
        //}
        //}
        //grid goes from -9, -5 to 8, 4
        // private void setGrid()
        //{
        //   for (int y = 0; y < gridHeight; y++)
        //  {
        //     for (int x = 0; x < gridWidth; x++)
        //    {
        //       Debug.Log(x + " " + y);
        //      gridValues[x, y] = 0;
        // }
        //}
        //}

        private void Update()
        {

            

            if (Input.GetMouseButtonDown(1))
            {

                if (mapManager.GetTileType() == false)
                {
                    if(ClickedButtonName.Equals("Wizard"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerWizard();
                    }
                    if (ClickedButtonName.Equals("Archer"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerArcher();
                    }
                    if (ClickedButtonName.Equals("Cannon"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerCannon();
                    }
                    if (ClickedButtonName.Equals("Druid"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerDruid();
                    }
                    if (ClickedButtonName.Equals("Boulder"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerBoulder();
                    }
                    if (ClickedButtonName.Equals("Lookout"))
                    {
                        Debug.Log(ClickedButtonName + " is clicked!");
                        SpawnTowerLookout();
                    }
                    //Debug.Log("SPAWN");
                    //SpawnTower();

                }

            }
        }

        public void ifBuild(bool isPath)
        {
            if (isPath == false)
            {
                Debug.Log("GRASS");
               // SpawnTower();

            }
        }

        public Vector3 GetBuildPos()
            {

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);
            print(buildPosX + "xy " + buildPosY);
            Vector3 buildPosition = TowerPosition.GetMouseWorldPosition();
            buildPosition = ValidateWorldGridPosition(buildPosition);
            Debug.Log(buildPosition);
            return buildPosition;
        }

            public float GetBuildPosX()
            {
                Vector3 buildPosition = TowerPosition.GetMouseWorldPosition();
                float buildPositionX = ValidateWorldGridPosXY(buildPosition, 0);
                return buildPositionX;
            }

            public float GetBuildPosY()
            {
                Vector3 buildPosition = TowerPosition.GetMouseWorldPosition();
                float buildPositionY = ValidateWorldGridPosXY(buildPosition, 1);
                return buildPositionY;
            }

            //private void SpawnTower()
            //{
            //int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            //int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            //if (gridValues[buildPosX, buildPosY] == 0)
            //{
            //    gridValues[buildPosX, buildPosY] = 1;
            //    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
            //    spawnPosition = ValidateWorldGridPosition(spawnPosition);
            //    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

            //    Instantiate(GameAssets.i.pfTower, spawnPosition, Quaternion.identity);
            //}
            //else
            //{
            //    Debug.Log("NO BUILD");
            //}
            //}

            private Vector3 ValidateWorldGridPosition(Vector3 position)
            {
                grid.GetXY(position, out int x, out int y);
                return grid.GetWorldPosition(x, y);

            }

            public float ValidateWorldGridPosXY(Vector3 position, int returnNum)
            {
                grid.GetXY(position, out int x, out int y);
                Vector3 WorldPos = grid.GetWorldPosition(x, y);
                float worldPosX = WorldPos.x;
                float worldPosY = WorldPos.y;
                if (returnNum == 0)
                {
                    return worldPosX;
                }
                else
                {
                    return worldPosY;
                }

            }

            public float GetCellSize()
            {
                return cellSize;
            }


        public void TowerChoose()
        {
            ClickedButtonName = EventSystem.current.currentSelectedGameObject.name;
            //Debug.Log(ClickedButtonName + " is clicked!");
        }
        
          
            
           
        


        private void SpawnTowerWizard()
        {
            CanBuild();
            
            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {
                if (TowerAdj == 8)
                {
                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerWizard, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void SpawnTowerArcher()
        {
            CanBuild();

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {
                if (TowerAdj == 8)
                {
                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;


                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerArcher, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void SpawnTowerCannon()
        {
            CanBuild();

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {
                if (TowerAdj == 8)
                {


                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerCannon, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void SpawnTowerDruid()
        {
            CanBuild();

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {
                if (TowerAdj == 8)
                {
                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;


                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerDruid, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void SpawnTowerLookout()
        {
            CanBuild();

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {
                if (TowerAdj == 8)
                {
                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerLookout, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void SpawnTowerBoulder()
        {
            CanBuild();
            
            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);

            if (gridValues[buildPosX, buildPosY] == 0)
            {

                
                if (TowerAdj == 8) {
                    gridValues[buildPosX, buildPosY] = 1;
                    Vector3 spawnPosition = TowerPosition.GetMouseWorldPosition();
                    spawnPosition = ValidateWorldGridPosition(spawnPosition);
                    spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

                    Debug.Log("BUILD");
                    Instantiate(GameAssets.i.pfTowerBoulder, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("NO BUILD");
            }
        }

        private void CanBuild()
        {
            TowerAdj = 0;

            int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
            Debug.Log(buildPosX);
            int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);
            Debug.Log(buildPosY);

            if (buildPosX == 0 || gridValues[buildPosX - 1, buildPosY] == 0)
            {
                TowerAdj++;
                Debug.Log("Left Good");
            }
            if (buildPosY == 10 || gridValues[buildPosX, buildPosY + 1] == 0)
            {
                TowerAdj++;
                Debug.Log("Top Good");
            }
            if (buildPosX == 18 || gridValues[buildPosX + 1, buildPosY] == 0)
            {
                TowerAdj++;
                Debug.Log("Right Good");
            }
            if (buildPosY == 0 || gridValues[buildPosX, buildPosY - 1] == 0)
            {
                TowerAdj++;
                Debug.Log("Bottom Good");
            }
            if (buildPosX == 18 || buildPosY == 10 || gridValues[buildPosX + 1, buildPosY + 1] == 0 || buildPosX == 18 && buildPosY == 10)
            {
                TowerAdj++;
                Debug.Log("Top Right Good");
            }
            if (buildPosX == 18 || buildPosY == 0 || gridValues[buildPosX + 1, buildPosY - 1] == 0 || buildPosX == 18 && buildPosY == 0)
            {
                TowerAdj++;
                Debug.Log("Bottom Right Good");
            }
            if (buildPosX == 0 || buildPosY == 0 || gridValues[buildPosX - 1, buildPosY - 1] == 0 || buildPosX == 0 && buildPosY == 0)
            {
                TowerAdj++;
                Debug.Log("Bottom Left Good");
            }
            if (buildPosX == 0 || buildPosY == 10 || gridValues[buildPosX - 1, buildPosY + 1] == 0 || buildPosX == 0 && buildPosY == 10)
            {
                TowerAdj++;
                Debug.Log("Top Left Good");
            }
        }



















    }
}
