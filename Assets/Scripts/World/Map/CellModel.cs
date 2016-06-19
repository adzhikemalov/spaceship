using Assets.Utils;

namespace Assets.World.Map
{
	public enum WallPosition
	{
		Left,
		Top,
		Right,
		Bottom
	}

    public class CellModel
    {
        public Point Position { get; set; }
        public CellModel CellFrom { get; set; }
        
        public float gCost { get; set; }
        public float hCost { get; set; }
        public float fCost {get { return hCost + gCost; }}


		public bool[] Walls = new bool[4]; 

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
