using Assets.Utils;

namespace Assets.World.Map
{
    public class CellModel
    {
        public Point Position { get; set; }
        
        public CellModel CellFrom { get; set; }
        
        public float gCost { get; set; }
        public float hCost { get; set; }
        public float fCost {get { return hCost + gCost; }}

        public CellModel(int column, int row)
        {
            Position = new Point(column, row);
        }
    }
}
