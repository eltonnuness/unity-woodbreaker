using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed;
    public float limitX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseHorizontalDirection = Input.GetAxis("Mouse X"); // -1 = left; 0 = idle, 1 = right
        GetComponent<Transform>().position += Vector3.right * mouseHorizontalDirection * speed * Time.deltaTime;
        float xCurrent = transform.position.x;
        xCurrent = Mathf.Clamp(xCurrent, -limitX, limitX);
        transform.position = new Vector3(xCurrent, transform.position.y, transform.position.z);
    }
}
