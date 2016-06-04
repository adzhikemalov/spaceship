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

        public Point Position; 
        private readonly WorldModel _world;

        public ObjectModelBase(WorldModel world, float x, float y)
        {
            _world = world;
            Position = new Point(x, y);
            if(_world == null)
                Debug.Log("No world specified");
        }

        public virtual void Update () {

        }
    }
}
