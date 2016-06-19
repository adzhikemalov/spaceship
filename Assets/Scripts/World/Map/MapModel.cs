using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Assets.Utils;

namespace Assets.World.Map
{
    [Serializable]
    public class MapModel
    {
		public static bool EnableDiagonalMovement;
        public int Cols;
        public int Rows;
		public float CellSize;

        [SerializeField]
        public CellModel[,] MapCells;

		public void Init(int cols, int rows, float cellSize)
		{
			Cols = cols;
			Rows = rows;
			CellSize = cellSize;

			MapCells = CreateCellsMap (Cols, Rows);
			FillMapWithCells (MapCells);
		}

		public CellModel GetCellByPosition(Point position)
		{
			var cid = (int)(position.x / CellSize);
			var rid = (int)(position.y / CellSize);
			return MapCells [cid, rid];
		}


		public CellModel GetRandomCell()
		{
			var rnd = new System.Random();
			var cid = rnd.Next (MapCells.GetLength (0));
			var rid = rnd.Next (MapCells.GetLength (1));
			return MapCells[cid, rid];
		}

		public Point GetRandomPoint()
		{
			var rnd = new System.Random();
			var cid = rnd.Next (MapCells.GetLength (0));
			var rid = rnd.Next (MapCells.GetLength (1));
			return MapCells[cid, rid].Position;
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
			int dx, dy;
			int px, py;
			for (int i = 0; i < 9; i++) {
				if (i == 4)
					continue;
				if (!EnableDiagonalMovement) {
					if (i == 0 || i == 2 || i == 6 || i == 8)
						continue;
				}
				dx = (i % 3) - 1;
				dy = (i / 3) - 1;
				px = (int)currentCell.Position.x + dx;
				py = (int)currentCell.Position.y + dy;

				if (px < 0 || px >= cells.GetLength (0))
					continue;
				if (py < 0 || py >= cells.GetLength (1))
					continue;

				var cell = cells [px, py];
				result.Add (cell);
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
			result.Reverse ();
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
    