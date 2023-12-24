using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    public static GameObject cameraLoc;
    public GameObject[] zombiePrefabs;
    public Transform[] zombieSpawnarea;
    public float spawnTime = 2f;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 4f;
    public int levelnumber = 1;
    private int spawnCount = 1;
    private int zombieLimit;
    public List<GameObject> zombies;
    public TMP_Text levelText;

    void Start()
    {
        zombies = new List<GameObject>();
        InvokeRepeating("SpawnZombie",2.0f,1.4f);
    }
    void Update()
    {
        zombieLimit = levelnumber*4;
        if(zombieLimit < spawnCount)
        {
            CancelInvoke("SpawnZombie");
        }
    }



    public void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)],zombieSpawnarea[Random.Range(0, zombieSpawnarea.Length)]);
        zombies.Add(zombie);
        spawnCount++;

    }



    public void ZombieKilled(GameObject zombie)
    {
        Destroy(zombie);
        zombies.Remove(zombie);
        

        if (zombies.Count == 0)
        {

            spawnCount = 0;
            levelnumber++;
            levelText.text = "Level " + levelnumber;
            InvokeRepeating("SpawnZombie",2.0f,1.4f);
        }
    }
    /*   public void TakeDamage(int damage)
    {

        if (IsDead())
        {
            SpawnZombies spawnScript = FindObjectOfType<SpawnZombies>();
            spawnScript.ZombieKilled(gameObject);
            Destroy(gameObject);
        }

        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] zombiePrefabs;
    public Transform[] spawnLocations;

    private int phase = 1;
    private float timeBetweenSpawns = 5f;
    private float timer = 0f;

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            if (timer >= timeBetweenSpawns)
            {
                SpawnZombie();
                timer = 0f;
            }
            yield return null;
        }
    }

    void SpawnZombie()
    {
        int zombieIndex = Random.Range(0, zombiePrefabs.Length);
        int spawnLocationIndex = Random.Range(0, spawnLocations.Length);

        GameObject newZombie = Instantiate(zombiePrefabs[zombieIndex], spawnLocations[spawnLocationIndex].position, Quaternion.identity);
        newZombie.transform.LookAt(Vector3.zero);
    }
}
    }
    */
}