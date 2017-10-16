using UnityEngine;
using System.Collections;

public class ShipObject : MonoBehaviour {
	private MovingModelBase _model;
	private MapModel _map;
	// Use this for initialization

	public void Init(ShipModel world)
	{
		_map = world.ShipMap;
		var point = world.ShipMap.GetRandomPoint ();
		_model = new MovingModelBase (world, point);
	}

	// Update is called once per frame
	void Update () {
        if (_model == null) return;

		transform.position = new Vector3 (_model.X, _model.Y);
		if (_model.TargetPoint.isEmpty) {
			var currentCell = _map.GetCellByPosition (_model.Position);
			var randomCell =_map.GetRandomCell ();
			_model.SetPath (_map.GetPath(currentCell, randomCell));
		}
		_model.Update ();
	}
}
