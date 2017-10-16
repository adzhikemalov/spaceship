using Assets.Utils;
using System;
using System.Text;

namespace World.Map
{
	public enum WallPosition
	{
		Left,
		Top,
		Right,
		Bottom
	}

	[System.Serializable]
    public class CellModel
    {
        public static CellModel UndefinedCell = new CellModel(-1, -1);
		public Point Position;
        public CellModel CellFrom { get; set; }
        
        public float gCost { get; set; }
        public float hCost { get; set; }
        public float fCost {get { return hCost + gCost; }}

		public bool Enabled = true;
        public int[] Walls = new int[4]; 

        public CellModel(int column, int row)
        {
            Position = new Point(column, row);
        }

		public override string ToString ()
		{
			return string.Format ("[CellModel: Position={0}, CellFrom={1}, gCost={2}, hCost={3}, fCost={4}]", Position, CellFrom, gCost, hCost, fCost);
		}

    }
}
