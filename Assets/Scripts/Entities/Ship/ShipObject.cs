using UnityEngine;
using System.Collections;
using Assets.World.WorldObjects;
using Assets.World;
using Assets.Utils;
using Assets.World.Map;

public class ShipObject : MonoBehaviour {
	private MovingModelBase _model;
	private MapModel _map;
	// Use this for initialization

	public void Init(WorldModel world)
	{
		_map = world.WorldMap;
		var point = world.WorldMap.GetRandomPoint ();
		_model = new MovingModelBase (world, point);
	}

	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (_model.X, _model.Y);
		if (_model.TargetPoint.isEmpty) {
			var currentCell = _model.World.WorldMap.GetCellByPosition (_model.Position);
			var randomCell = _model.World.WorldMap.GetRandomCell ();
			_model.SetPath (_model.World.WorldMap.GetPath(currentCell, randomCell));
		}
		_model.Update ();
	}
}
