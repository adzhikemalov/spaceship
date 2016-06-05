using System;
using UnityEngine;
using Assets.World;
using Assets.World.Map;

public class Game : MonoBehaviour
{
    private WorldModel _world;

	// Use this for initialization
	void Start () {
	    _world = new WorldModel {WorldMap = new MapModel()};
	}

	void Update () {
	    _world.Update();
	}
}
