using System.Collections.Generic;
using Assets.Utils;
using Assets.World.WorldObjects;
using Assets.World.Map;

namespace Assets.World
{
    public class WorldModel
    {
        public List<ObjectModelBase> WorldObjects;
        public MapModel WorldMap;
        public event ParameterHandler<MovingModelBase> MovingObjectCreated;

        public WorldModel()
        {
            WorldObjects = new List<ObjectModelBase>();    
        }

        public void CreateMovingObject(Point startPoint, Point endPoint)
        {
            var obj = new MovingModelBase(this, startPoint);
            obj.MoveToPoint(endPoint);
            WorldObjects.Add(obj);
            if (MovingObjectCreated != null) MovingObjectCreated(obj);
        }

        public void Update()
        {
            foreach (var worldObjectBase in WorldObjects)
            {
                worldObjectBase.Update();
            }
        }   
    }
}

