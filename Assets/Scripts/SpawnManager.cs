using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public GameObject obstaclePrefab;
    public List<GameObject> obstaclesPrefab;
    public Vector3 spawnPos = new Vector3(25, 0, 0);

    public float startDelay = 2;
    public float repeatRate = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclesPrefab.Count);
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclesPrefab[index], spawnPos, obstaclesPrefab[index].transform.rotation);
        }
    }
}
