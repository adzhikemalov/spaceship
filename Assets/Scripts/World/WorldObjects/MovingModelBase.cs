using Assets.Utils;
using UnityEngine;

namespace Assets.World.WorldObjects
{
    public class MovingModelBase : ObjectModelBase
    {
        public float Speed = 0.05f;
        private Point _targertPoint = Point.Empty;

        public MovingModelBase(WorldModel world, float x, float y)
            : base(world, x, y)
        {

        }

        public MovingModelBase(WorldModel world, Point point) : base(world, point.x, point.y)
        {

        }

        public void MoveToPoint(Point target)
        {
            _targertPoint = target;
        }

        override public void Update()
        {
            if (!_targertPoint.isEmpty)
            {
                Move(_targertPoint);
                if (_targertPoint == Position)
                    _targertPoint = Point.Empty;
            }
        }

        private void Move(Point target)
        {
            if (Position.Distance(target) > Speed)
            {
                Position = Position + (target - Position).Normalize(Speed);
            }
            else
            {
                Position = target;
            }
        }
    }
}