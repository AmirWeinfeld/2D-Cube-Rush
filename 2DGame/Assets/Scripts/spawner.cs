using System.Collections;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstacleObjects;

    [SerializeField]
    private ObsticleScript[] obstacleScripts;

    [SerializeField]
    private float timeBetweenSpawn, randY, maxSize, minSize, maxRotSpeed, decreaseTimeAmmount, decreaseEverySecs, minTime, objSpeed, IncreaseSpeed, spawn, maxSpeed;

    [SerializeField]
    private int maxTimePassed;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int nextObjToSpawn;

    // Use this for initialization
    void Start()
    {
        nextObjToSpawn = 0;
        StartCoroutine(MainSpawn());
        //StartCoroutine(SpawnTimeDecreaser());
    }

    private IEnumerator MainSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            obstacleObjects[nextObjToSpawn].SetActive(true);
            //int obsIndex = Random.Range(0, 3);
            //GameObject tempCube = Instantiate(obstaclePrefab[obsIndex]);

            float randy = Random.Range(-randY, randY);
            obstacleObjects[nextObjToSpawn].transform.position = new Vector3(spawn, randy, 0f); // setting starting poins

            float randSize = Random.Range(maxSize, minSize); // setting size
            obstacleObjects[nextObjToSpawn].transform.localScale = new Vector3(randSize, randSize, obstacleObjects[nextObjToSpawn].transform.localScale.z);
            
            float randRot = Random.Range(-maxRotSpeed, maxRotSpeed); // setting rotation speed
            obstacleScripts[nextObjToSpawn].rotSpeed = randRot;

            obstacleScripts[nextObjToSpawn].scoreText = scoreText;

            obstacleScripts[nextObjToSpawn].speed = objSpeed; // setting movement speed

            obstacleScripts[nextObjToSpawn].Spawn();

            obstacleScripts[nextObjToSpawn].maxTimePassedEnd = maxTimePassed; // setting max amount of times the obj can do

            if (timeBetweenSpawn > minTime) // decreasing time between spawn
                timeBetweenSpawn -= decreaseTimeAmmount;
            else
                timeBetweenSpawn = minTime;

            if (objSpeed < maxSpeed) // increasing obj speed
                objSpeed += IncreaseSpeed;
            else if (objSpeed < maxSpeed + maxSpeed/2)
            {
                objSpeed = maxSpeed;
                maxTimePassed = 1;
            }

            nextObjToSpawn++;
            if(nextObjToSpawn == obstacleObjects.Length)
            {
                nextObjToSpawn = 0;
            }
        }
    }

    private IEnumerator SpawnTimeDecreaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseEverySecs);
            if(timeBetweenSpawn > minTime)
                timeBetweenSpawn -= decreaseTimeAmmount;

            objSpeed += IncreaseSpeed;
        }
    }
}
