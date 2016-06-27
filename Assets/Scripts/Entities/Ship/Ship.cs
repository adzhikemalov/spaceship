using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.World;

public class Ship : MonoBehaviour {
	public GameObject HumanoidPrefab;

	private ShipGrid _grid;
    private List<ShipObject> _crew;
	private ShipModel _model;
	// Use this for initialization
	void Start () {
		_model = new ShipModel ();
        _grid = transform.GetComponent<ShipGrid>();
		_grid.InitGrid(_model);
        _crew = new List<ShipObject>();
		CreateDummyCrew ();
	}

	private void CreateDummyCrew()
	{
		var go = GameObject.Instantiate(HumanoidPrefab);
		go.transform.parent = transform;
		var so = go.GetComponent<ShipObject> ();
		so.Init (_model);
		_crew.Add (so);
	}

	void Update()
	{
		_model.Update ();
	}
}
