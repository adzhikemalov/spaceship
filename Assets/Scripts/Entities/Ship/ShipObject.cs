using UnityEngine;
using System.Collections;
using Assets.World.WorldObjects;
using Assets.World;
using Assets.Utils;

public class ShipObject : MonoBehaviour {
	private MovingModelBase _model;
	// Use this for initialization
	void Start () {

	}

	public void Init(WorldModel world)
	{
		var point = world.WorldMap.GetRandomPoint ();
		Debug.Log ("start point "+point);
		_model = new MovingModelBase (world, point);
	}

	// Update is called once per frame
	int count = 0;
	void Update () {
		transform.position = new Vector3 (_model.X, _model.Y);
		if (_model.TargetPoint.isEmpty && count == 0) {
			count++;
			_model.SetPath (_model.World.WorldMap.GetPath(_model.World.WorldMap.GetCellByPosition(_model.Position), _model.World.WorldMap.GetRandomCell()));
		}
		_model.Update ();
	}
}
