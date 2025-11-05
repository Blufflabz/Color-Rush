using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{
    [Header("Explosion Settings")]
    public int cubeCount = 15;
    public float cubeSize = 0.2f;
    public float explosionForce = 8f;
    public float destroyDelay = 2f;

    [Header("Visual Effects")]
    public Material Mat1;
    public Material Mat2;
    private Material cubeMaterial;
    public Color cubeColor = Color.red;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red") || other.CompareTag("Blue"))
        {
            if (other.tag != this.tag)
            {
                if (this.tag == "Red")
                {
                    cubeMaterial = Mat1;
                }
                else
                {
                    cubeMaterial = Mat2;
                }
                MakeBallExplode(gameObject);
            }
        }
    }

    void MakeBallExplode(GameObject ball)
    {
        Vector3 ballPosition = ball.transform.position;
        // Color ballColor = GetBallColor(ball);
        float ballScale = ball.transform.localScale.x;

        ball.GetComponent<MeshRenderer>().enabled = false;
        ball.GetComponent<Collider>().enabled = false;

        for (int i = 0; i < cubeCount; i++)
        {
            CreateCubePiece(ballPosition, ballScale);
        }

        Destroy(ball, destroyDelay);
    }

    void CreateCubePiece(Vector3 position, float originalBallScale)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = position + Random.insideUnitSphere * 0.5f;

        float size = cubeSize * originalBallScale;
        cube.transform.localScale = new Vector3(size, size, size);

        Rigidbody rb = cube.AddComponent<Rigidbody>();

        Vector3 randomDirection = (Random.insideUnitSphere + Vector3.up * 0.5f).normalized;
        float force = explosionForce * Random.Range(0.8f, 1.2f);
        rb.AddForce(randomDirection * force, ForceMode.Impulse);

        rb.AddTorque(Random.insideUnitSphere * force * 0.5f, ForceMode.Impulse);

        Renderer renderer = cube.GetComponent<Renderer>();
        if (cubeMaterial != null)
        {
            renderer.material = cubeMaterial;
        }
        // renderer.material.color = color;

        Destroy(cube, destroyDelay);
    }

    /* Color GetBallColor(GameObject ball)
    {
        Renderer renderer = ball.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            return renderer.material.color;
        }
        return cubeColor; // Fallback color
    }
    */
}