using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Utils
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private float xAlign;
        [SerializeField]
        private float yAlign;
        [SerializeField]
        public int gridHeight;
        [SerializeField]
        public int gridWidth;

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
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

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
            grid = new Grid(20, 10, 1f, new Vector3(-10, -5));
            gridValues = new int[gridWidth, gridHeight];
            pathChecker();
            setGrid();
        }

        private void pathChecker()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {

                }
            }
        }
        //grid goes from -9, -5 to 8, 4
        private void setGrid()
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    Debug.Log(x + " " + y);
                    gridValues[x, y] = 0;
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                int buildPosX = ((int)GetBuildPosX()) + (gridWidth / 2);
                int buildPosY = ((int)GetBuildPosY()) + (gridHeight / 2);
                if (gridValues[buildPosX, buildPosY] == 0)
                {

                    SpawnTower(buildPosX, buildPosY);

                }
            }
        }

        private float GetBuildPosX()
        {
            Vector3 buildPosition = Testing.GetMouseWorldPosition();
            float buildPositionX = ValidateWorldGridPosXY(buildPosition, 0);
            return buildPositionX;
        }

        private float GetBuildPosY()
        {
            Vector3 buildPosition = Testing.GetMouseWorldPosition();
            float buildPositionY = ValidateWorldGridPosXY(buildPosition, 1);
            return buildPositionY;
        }

        private void SpawnTower(int buildPosX, int buildPosY)
        {
            int buildX = buildPosX;
            int buildY = buildPosY;
            gridValues[buildX, buildY] = 1;
            Vector3 spawnPosition = Testing.GetMouseWorldPosition();
            spawnPosition = ValidateWorldGridPosition(spawnPosition);
            spawnPosition += new Vector3(xAlign, yAlign, 0) * grid.GetCellSize() * 0.5f;

            Instantiate(GameAssets.i.pfTower, spawnPosition, Quaternion.identity);
        }

        private Vector3 ValidateWorldGridPosition(Vector3 position)
        {
            grid.GetXY(position, out int x, out int y);
            return grid.GetWorldPosition(x, y);

        }

        private float ValidateWorldGridPosXY(Vector3 position, int returnNum)
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


    }
}
