using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
    private ShipGrid _grid;
    private List<ShipObject> _crew;
	// Use this for initialization
	void Start () {
        _grid = transform.GetComponent<ShipGrid>();
        _crew = new List<ShipObject>();
	}
}
