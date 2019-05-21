using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;

    // Start is called before the first frame update
    void Start()
    {
        direction.Normalize(); //equals direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * velocity * Time.deltaTime;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        bool invalidCollision = false;
        Vector2 normal = collision.contacts[0].normal;
        Platform platform = collision.transform.GetComponent<Platform>();
        EdgeGenerator edgeGen = collision.transform.GetComponent<EdgeGenerator>();
        if (platform != null)
        {
            if (normal != Vector2.up)
            {
                invalidCollision = true;
            }
        } else if (edgeGen != null)
        {
            if (normal == Vector2.up)
            {
                invalidCollision = true;
            }
        }
        if (!invalidCollision)
        {
            Debug.Log($"apple object normal: {normal}");
            direction = Vector2.Reflect(direction, normal);
            Debug.Log($"apple object normal after reflection: {direction}");
            direction.Normalize();
            Debug.Log($"apple object normal after reflection with normalize: {direction}");
        }
        else
        {
            GameController.GameOver();
        }
    }

}
