namespace GV.Utility
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Grid of generic objects
    /// </summary>
    public class Grid<TGridObject>
    {
        public int Width { get => _width; }
        private int _width;
        public int Height { get => _height; }
        private int _height;
        public float CellSize { get => _cellSize; }
        private float _cellSize;

        private Vector3 _originPosition;
        private TGridObject[,] _gridArray;


        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, TGridObject> createGridObject)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new TGridObject[height, width];

            for (int row = 0; row < _gridArray.GetLength(0); row++)
            {
                for (int column = 0; column < _gridArray.GetLength(1); column++)
                {
                    _gridArray[row, column] = createGridObject(row, column);
                }
            }
        }

        /// <summary>
        /// Returns the corresponding world position of the passed coordinates
        /// </summary>
        public Vector3 GetWorldPosition(int row, int column)
        {
            return new Vector3(column, 0f, row) * _cellSize + new Vector3(1f, 0f, 1f) * _cellSize * .5f + _originPosition; // changed column,0,row instead of column,row,0
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            int row = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
            int column = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            return new Vector2Int(row, column);
        }

        /// <summary>
        /// Returns the corresponding grid coordinates of the passed world position
        /// </summary>
        public void GetRowColumn(Vector3 worldPosition, out int row, out int column)
        {
            row = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize); // changed to z instead of y
            column = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        }

        /// <summary>
        /// Return the grid object placed at the passed coordinates
        /// </summary>
        public TGridObject GetGridObject(int row, int column)
        {
            if (row >= 0 && column >= 0 && row < _height && column < _width) return _gridArray[row, column];
            else return default;
        }

        /// <summary>
        /// Returns the grid object placed at the coordinates obtained from the past world position
        /// </summary>
        public TGridObject GetGridObject(Vector3 worldPosition)
        {
            GetRowColumn(worldPosition, out int row, out int column);
            return GetGridObject(row, column);
        }

        /// <summary>
        /// Set the grid object placed at the passed coordinates
        /// </summary>
        public void SetGridObject(int row, int column, TGridObject value)
        {
            if (row >= 0 && column >= 0 && row < _height && column < _width)
                _gridArray[row, column] = value;
        }

        /// <summary>
        /// Set the grid object placed at the coordinates obtained from the past world position
        /// </summary>
        public void SetGridObject(Vector3 worldPosition, TGridObject value)
        {
            GetRowColumn(worldPosition, out int row, out int column);
            SetGridObject(row, column, value);
        }
    }
}


