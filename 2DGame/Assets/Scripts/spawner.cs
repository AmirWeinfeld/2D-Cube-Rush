using System.Collections;
using UnityEngine;
using TMPro;

public class spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstaclePrefab;

    [SerializeField]
    private float timeBetweenSpawn, randY, maxSize, minSize, maxRotSpeed, decreaseTimeAmmount, decreaseEverySecs, minTime, objSpeed, IncreaseSpeed;

    [SerializeField]
    private int maxTimePassed;

    private float spawn;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Use this for initialization
    void Start()
    {
        GameObject temp = Instantiate(obstaclePrefab[0]);
        spawn = temp.transform.position.x;
        temp.GetComponent<ObsticleScript>().scoreText = scoreText;
        StartCoroutine(MainSpawn());
        StartCoroutine(SpawnTimeDecreaser());
    }

    private IEnumerator MainSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            int obsIndex = Random.Range(0, 3);
            GameObject tempCube = Instantiate(obstaclePrefab[obsIndex]);

            float randy = Random.Range(-randY, randY);
            tempCube.transform.position = new Vector3(spawn, randy, 0f);

            float randSize = Random.Range(maxSize, minSize);
            tempCube.transform.localScale = new Vector3(randSize, randSize, tempCube.transform.localScale.z);
            
            float randRot = Random.Range(-maxRotSpeed, maxRotSpeed);
            ObsticleScript tempScript = tempCube.GetComponent<ObsticleScript>();
            tempScript.rotSpeed = randRot;

            tempScript.scoreText = scoreText;

            tempScript.speed = objSpeed;

            tempScript.maxTimePassedEnd = maxTimePassed;
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
