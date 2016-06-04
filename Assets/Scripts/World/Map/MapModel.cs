using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Assets.World.Map
{
    [Serializable]
    public class MapModel
    {
        public int MapWidth;
        public int MapHeight;
        public int CellSize;

        [SerializeField]
        public CellModel[,] MapCells;

        public MapModel()
        {
            
        }

        public void Init(int width, int height, int cells)
        {
            MapHeight = height;
            MapWidth = width;
            CellSize = cells;

            MapCells = CreateCellsMap(MapWidth/CellSize, MapHeight/CellSize);
            FillMapWithCells(MapCells);
        }


        public List<CellModel> GetPath(CellModel start, CellModel end)
        {
            return GetPath(MapCells, start, end);
        }

        private static void FillMapWithCells(CellModel[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i,j] = new CellModel(i, j);
                }
            }
        }

        private static List<CellModel> GetPath(CellModel[,] cells, CellModel start, CellModel end)
        {
            var openSet = new Collection<CellModel>();
            var closedSet = new Collection<CellModel>();
            start.hCost = GetDistance(start, end);
            start.gCost = 0;
            start.CellFrom = null;
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                var currentCell = openSet.OrderBy(cell => cell.fCost).First();
                if (currentCell == end)
                    return GetPathForCell(currentCell);
                openSet.Remove(currentCell);
                closedSet.Add(currentCell);
                var neighbours = GetNeighbours(currentCell, cells);
                foreach (var neighbourCell in neighbours)
                {
                    var gCost = currentCell.gCost + GetDistance(currentCell, neighbourCell);
                    var hCost = GetDistance(neighbourCell, end);
                    var closedCell = closedSet.FirstOrDefault(node => node.Position == neighbourCell.Position);
                    if (closedCell != null && gCost >= neighbourCell.gCost) continue;
                    
                    var openCell = openSet.FirstOrDefault(node => node.Position == neighbourCell.Position);
                    if (openCell == null || gCost < openCell.gCost )
                    {
                        neighbourCell.gCost = gCost;
                        neighbourCell.hCost = hCost;
                        neighbourCell.CellFrom = currentCell;
                        openSet.Add(neighbourCell);
                    }
                }
            }

            return new List<CellModel>();
        }

        private static List<CellModel> GetNeighbours(CellModel currentCell, CellModel[,] cells)
        {
            var result = new List<CellModel>();

            for (int i = 0; i < 9; i++)
            {
                if (i == 4) continue;
                var dx = (i%3) - 1;
                var dy = (i/3) - 1;
                var px = currentCell.Position.x + dx;
                var py = currentCell.Position.y + dy;

                if (px < 0 || px >= cells.GetLength(0))
                    continue;
                if (py < 0 || py >= cells.GetLength(1))
                    continue;

                var cell = cells[(int)px, (int)py];
                result.Add(cell);
            }

            return result;
        }

        private static List<CellModel> GetPathForCell(CellModel cellModel)
        {
            var result = new List<CellModel>();
            var currentCell = cellModel;
            var counter = 0;
            while (currentCell != null && counter < 1000)
            {
                result.Add(currentCell);
                currentCell = currentCell.CellFrom;
                counter++;
            }
            return result;
        }

        private static float GetDistance(CellModel firstCell, CellModel secondCell)
        {
            var dx = secondCell.Position.x - firstCell.Position.x;
            var dy = secondCell.Position.y - firstCell.Position.y;
            return (float)Math.Sqrt(dx*dx + dy*dy);
        }

        private static CellModel[,] CreateCellsMap(int width, int heigth)
        {
            return new CellModel[width,heigth];
        }
    }
}
    