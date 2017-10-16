using Assets.Utils;
using Assets.World;

namespace WorldObjects
{
    public class ObjectModelBase
    {
        public float X {
            get { return Position.x; }
        }
        public float Y {
            get { return Position.y; }
        }
		public WorldModel World{get { return _world; }}

        public Point Position; 
		protected readonly WorldModel _world;

        public ObjectModelBase(WorldModel world, float x, float y)
        {
            _world = world;
            Position = new Point(x, y);
        }

        public virtual void Update () {

        }
    }
}
