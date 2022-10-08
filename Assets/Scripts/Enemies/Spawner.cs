using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Minion;

    [SerializeField] public List<GameObject> powerUps;
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;
    private int posX;
    private int posZ;
    public int enemyCount;
    public int powerupCount;



    void Start()
    {
        StartCoroutine(SpawnDrop());
    }


    IEnumerator SpawnDrop()
    {
        while(enemyCount < 5)
        {
            // Gets a random position for spawning
            posX = Random.Range(minX, maxX);
            posZ = Random.Range(minZ, maxZ);
            // Spawns the enemy in that spot
           Instantiate(Minion, new Vector3(posX, 1, posZ), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            enemyCount += 1;
        }

        while (powerupCount < 3)
        {
            // Gets a random position for spawning
            posX = Random.Range(minX, maxX);
            posZ = Random.Range(minZ, maxZ);
            // Spawns a randomn powerup in that spot
            Instantiate(powerUps[Random.Range(0, powerUps.Count)], new Vector3(posX, 1, posZ), Quaternion.identity);
            yield return new WaitForSeconds(1f);
            powerupCount += 1;
        }

        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnDrop());
    }
}
