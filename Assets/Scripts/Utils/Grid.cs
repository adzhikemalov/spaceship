using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{

    public float Width = 30f;
    public float Height = 30f;
    public Color GridColor = new Color(); 

    void OnDrawGizmos()
    {
        Vector3 position = Camera.current.transform.position;
        Gizmos.color = GridColor;

        for (float y = position.y-400f; y < position.y+400f; y+=Height)
        {
            var smoothHeightValue = Mathf.Floor(y/Height)*Height;
            Gizmos.DrawLine(new Vector3(-10000f, smoothHeightValue, 0), 
                            new Vector3(10000f, smoothHeightValue, 0));
        }

        for (float x = position.x - 400; x < position.x + 400; x += Width)
        {
            var smoothWidthValue = Mathf.Floor(x/Width)*Width;
            Gizmos.DrawLine(new Vector3(smoothWidthValue, -10000f, 0),
                            new Vector3(smoothWidthValue, 10000f, 0));
        }

        Gizmos.DrawLine(Vector3.zero, Vector3.left * 1);
    }
}
