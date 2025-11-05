using UnityEngine;
using System.Linq;

public class Generation : MonoBehaviour
{
    [Header("Road Segment Prefab")]
    public GameObject roadSegmentPrefab;

    [Header("Obstacle Prefabs")]
    public GameObject[] obstacles;


    [Range(0f, 1f)]
    public float obstacleSpawnChance = 0.6f;

    private float roadSegmentLength = 60f;
    private const string RoadSegmentTag = "RoadSegment";

    [SerializeField] public float speed = 5f;
    private float timer = 10f;
    private void Start()
    {
        GameObject[] existingSegments = GameObject.FindGameObjectsWithTag(RoadSegmentTag);
        foreach (GameObject segment in existingSegments)
        {
            SpawnObstacles(segment);
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && speed < 15f)
        {
            speed += 1f;
            timer = 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GenRoad"))
        {
            GameObject[] roadSegments = GameObject.FindGameObjectsWithTag(RoadSegmentTag);
            GameObject lastSegment = roadSegments.OrderByDescending(s => s.transform.position.z).FirstOrDefault();

            if (lastSegment != null)
            {
                Vector3 spawnPosition = new Vector3(0, 0, lastSegment.transform.position.z + roadSegmentLength);
                GameObject newSegment = Instantiate(roadSegmentPrefab, spawnPosition, Quaternion.identity);
                newSegment.tag = RoadSegmentTag;

                SpawnObstacles(newSegment);
            }
        }
    }

    private void SpawnObstacles(GameObject road)
    {
        Transform spawnParent = road.transform.Find("SpawnPoints");
        if (spawnParent == null) return;

        foreach (Transform point in spawnParent)
        {
            if (Random.value < obstacleSpawnChance && obstacles.Length > 0)
            {
                GameObject prefab = obstacles[Random.Range(0, obstacles.Length)];
                Instantiate(prefab, point.position, Quaternion.Euler(0, 90, 0), road.transform);
            }
        }
    }

    
}
