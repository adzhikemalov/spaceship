﻿using Assets.Utils;
using UnityEngine;
using System.Collections.Generic;
using Assets.World.Map;

namespace Assets.World.WorldObjects
{
    public class MovingModelBase : ObjectModelBase
    {
        public float Speed = 0.05f;
		public Point TargetPoint {get { return _targetPoint; }}

		private List<CellModel> _path;
        private Point _targetPoint = Point.Empty;

        public MovingModelBase(WorldModel world, Point point) : base(world, point.x, point.y)
        {

        }

		public void SetPath(List<CellModel> path)
		{
			_path = path;
			var nextCell = _path [0];
			_path.RemoveAt (0);
			_targetPoint = nextCell.Position;
		}

        public void MoveToPoint(Point target)
        {
            _targetPoint = target;
        }

        override public void Update()
        {
			if (!_targetPoint.isEmpty) {
				Move (_targetPoint);
				if (_targetPoint == Position) {
					if (_path == null || _path.Count == 0) {
						_targetPoint = Point.Empty;
					} else {
						var nextCell = _path [0];
						_path.RemoveAt (0);
						_targetPoint = nextCell.Position;
					}
				}
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