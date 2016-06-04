using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class CameraHelper : MonoBehaviour {
    public float DefaultPixelPerUnit = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var orthographicSize = Camera.main.pixelHeight / 2 / DefaultPixelPerUnit; 
	    if (orthographicSize != Camera.main.orthographicSize)
        {
            Camera.main.orthographicSize = orthographicSize;
        }
	}
}
