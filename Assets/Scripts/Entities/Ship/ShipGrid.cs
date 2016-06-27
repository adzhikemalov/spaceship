using UnityEngine;
using System.Collections;
using Assets.World.Map;
using Assets.World;
using Assets.Scripts.World.WorldObjects;

public class ShipGrid : MonoBehaviour {
    public GameObject CellPrefab;
    public int CellSize = 1;
    public float GridX = 10;
    public float GridY = 10;
    public int Cols = 5;
    public int Rows = 5;

    public TextAsset ShipData;
	private ShipModel _world;
    private ShipData _model; 
    private GameObject[,] _cells;
	// Use this for initialization
	void Start () {

	}

	public void InitGrid(ShipModel world)
	{
		_world = world;
        _model = JsonUtility.FromJson<ShipData>(ShipData.text);
		_world.ShipMap = _model.Map;
        CellSize = _world.ShipMap.CellSize;

		_cells = new GameObject[_model.Map.Cols, _model.Map.Rows];
		for (int i = 0; i < _model.Map.Cols; i++)
		{
			for (int j = 0; j < _model.Map.Rows; j++)
			{
				_cells[i, j] = CreateNewCell(i, j);
			}
		}
	}


	
    private GameObject CreateNewCell(int i, int j)
    {
        var go = GameObject.Instantiate(CellPrefab);
        go.transform.parent = transform;
        go.transform.position = new Vector3(GridX+i*CellSize, GridY+j*CellSize);
        return go;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
