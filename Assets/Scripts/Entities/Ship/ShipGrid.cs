using UnityEngine;
using System.Collections;

public class ShipGrid : MonoBehaviour {
    public GameObject CellPrefab;
    public float CellSize = 0.2f;
    public float GridX = 10;
    public float GridY = 10;
    public int Cols = 5;
    public int Rows = 5;

    private GameObject[,] _cells;
	// Use this for initialization
	void Start () {
        _cells = new GameObject[Cols, Rows];
        for (int i = 0; i < Cols; i++)
        {
            for (int j = 0; j < Rows; j++)
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
