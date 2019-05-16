using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
        Vector2 lowerLeftEdge = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 upperLeftEdge = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        Vector2 upperRightEdge = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector2 lowerRightEdge = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        collider.points = new Vector2[5] { lowerLeftEdge, upperLeftEdge, upperRightEdge, lowerRightEdge, lowerLeftEdge };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
