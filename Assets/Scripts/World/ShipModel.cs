using System.Collections.Generic;
using Assets.Utils;
using Assets.World.WorldObjects;
using Assets.World.Map;

namespace Assets.World
{
    public class ShipModel
    {
        public List<ObjectModelBase> ShipObjects;
        public MapModel ShipMap;
        public event ParameterHandler<MovingModelBase> MovingObjectCreated;

        public ShipModel()
        {
            ShipObjects = new List<ObjectModelBase>();    
        }

        public void CreateMovingObject(Point startPoint, Point endPoint)
        {
            var obj = new MovingModelBase(this, startPoint);
            obj.MoveToPoint(endPoint);
            ShipObjects.Add(obj);
            if (MovingObjectCreated != null) MovingObjectCreated(obj);
        }

        public void Update()
        {
            foreach (var shipObjectBase in ShipObjects)
            {
                shipObjectBase.Update();
            }
        }   
    }
}

