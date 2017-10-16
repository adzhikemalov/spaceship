using System;
using System.Collections.Generic;
using Assets.Utils;
using Assets.World.Areas;
using World.Map;
using World.WorldObjects;
using WorldObjects;

namespace Assets.World
{
    public class WorldModel
    {
        public List<ObjectModelBase> Objects;
        public List<AreaBase> Areas;
        public MapModel WorldMap;

        public WorldModel()
        {
            Objects = new List<ObjectModelBase>();
        }

        public void InitMap(int width, int height)
        {
            WorldMap = new MapModel();
            WorldMap.Init(width, height, 1);
        }

        public MovingModelBase AddMovingObject(Point position)
        {
            var obj = new MovingModelBase(this, position);
            Objects.Add(obj);
            return obj;
        }
        
        public void Update()
        {
            foreach (var objectBase in Objects)
            {
                objectBase.Update();
            }
            foreach (var areaBase in Areas)
            {
                areaBase.Update();
            }
        }


        public bool IsObjectInCell(CellModel cell, ObjectModelBase obj)
        {
            return WorldMap.GetCellByPosition(obj.Position).Position == cell.Position;
        }
    }
}

