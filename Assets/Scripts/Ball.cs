using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 direction;
    public float velocity;
    public GameObject blockParticle;
    public GameObject leafParticle;
    public LineRenderer lineHelper;
    public int lineHelperMaxPoints = 3;

    // Start is called before the first frame update
    void Start()
    {
        direction.Normalize(); //equals direction = direction.normalized;
        lineHelper.positionCount = lineHelperMaxPoints;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * velocity * Time.deltaTime;
        UpdateLineRenderer();
    }

    void UpdateLineRenderer()
    {
        int currentPoint = 1;
        Vector3 currentDirection = direction;
        Vector3 lastPosition = transform.position;
        lineHelper.SetPosition(0, lastPosition);
        while (currentPoint < lineHelperMaxPoints)
        {
            RaycastHit2D hit = Physics2D.Raycast(lastPosition, currentDirection);
            lastPosition = hit.point;
            lineHelper.SetPosition(currentPoint, lastPosition);
            currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            lastPosition += currentDirection * 0.05F;
            currentPoint++;
        }
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
            }else
            {
                GameObject particles = (GameObject)Instantiate(leafParticle, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z) , Quaternion.identity);
                ParticleSystem particleComponent = particles.GetComponent<ParticleSystem>();
                Destroy(particles, particleComponent.main.duration + particleComponent.main.startLifetimeMultiplier);
            }
        } else if (edgeGen != null)
        {
            if (normal == Vector2.up)
            {
                invalidCollision = true;
            }
        }
        else //block collision
        {
            invalidCollision = false;
            Bounds colliderBounds = collision.transform.GetComponent<SpriteRenderer>().bounds;
            Vector3 newPosition = new Vector3(collision.transform.position.x + colliderBounds.extents.x, collision.transform.position.y - colliderBounds.extents.y, collision.transform.position.z);
            GameObject particles = (GameObject)Instantiate(blockParticle, newPosition, Quaternion.identity);
            ParticleSystem particleComponent = particles.GetComponent<ParticleSystem>();
            Destroy(particles, particleComponent.main.duration + particleComponent.main.startLifetimeMultiplier);
            Destroy(collision.gameObject);
            GameController.totalDestroyedBlocks++;
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
            GameController.instance.GameOver();
        }
    }

}
