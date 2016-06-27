using Assets.Utils;
using UnityEngine;

namespace Assets.World.WorldObjects
{
    public class ObjectModelBase
    {
        public float X {
            get { return Position.x; }
        }
        public float Y {
            get { return Position.y; }
        }
		public ShipModel Ship{get { return _ship; }}

        public Point Position; 
		protected readonly ShipModel _ship;

        public ObjectModelBase(ShipModel ship, float x, float y)
        {
            _ship = ship;
            Position = new Point(x, y);
            if(_ship == null)
                Debug.Log("No ship specified");
        }

        public virtual void Update () {

        }
    }
}
