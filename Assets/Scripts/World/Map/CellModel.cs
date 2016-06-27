using Assets.Utils;
using System;
using System.Text;

namespace Assets.World.Map
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

		public Point Position;
        public CellModel CellFrom { get; set; }
        
        public float gCost { get; set; }
        public float hCost { get; set; }
        public float fCost {get { return hCost + gCost; }}

		public bool Enabled = true;
		public bool[] Walls = new bool[4]; 

        public CellModel(int column, int row)
        {
            Position = new Point(column, row);
        }

		public override string ToString ()
		{
			return string.Format ("[CellModel: Position={0}, CellFrom={1}, gCost={2}, hCost={3}, fCost={4}]", Position, CellFrom, gCost, hCost, fCost);
		}

		public string Serialize()
		{
			StringBuilder result = new StringBuilder();
			result.Append ("{");
			result.Append (string.Format ("\"position\":[{0},{1}],", Position.x, Position.y));
			result.Append (string.Format("\"walls\":[{0},{1},{2},{3}]", Convert.ToInt32(Walls[0]), Convert.ToInt32(Walls[1]), Convert.ToInt32(Walls[2]), Convert.ToInt32(Walls[3])));
			result.Append("}");
			return result.ToString();
		}
    }
}
