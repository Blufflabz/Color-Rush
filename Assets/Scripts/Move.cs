using System;
using UnityEngine;
public class Move : MonoBehaviour
{
    private float speed;

    void Update()
    {
        speed = GameObject.FindObjectOfType<Generation>().GetComponent<Generation>().speed;
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyRoad"))
        {
            Destroy(gameObject);
            Console.WriteLine("Road Segment Destroyed");
        }
    }
}