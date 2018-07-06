using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Hazards
{
    public GameObject[] asteroids;


}

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public float hazardSpeedIncreaseFactor;
    //public Hazards hazards;
    public Vector3 spawnValue;
    public int hazardCount;
    public int hazardCountIncrementPerWave;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public TextMeshPro scoreText;
    private int score;
    private bool isFirstHazardInstance;
    private float curHazardSpeed;

    void Start()
    {
        isFirstHazardInstance = true;
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        GameObject curHazard = new GameObject();
        
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;

                // Instantiate the hazard and set its speed
                curHazard = Instantiate(hazards[Random.Range(0, hazards.Length)], spawnPosition, spawnRotation);

                // Get current hazard speed (for calculating faster speeds for future waves)
                if (isFirstHazardInstance)
                {
                    curHazardSpeed = curHazard.GetComponent<Mover>().speed;
                    isFirstHazardInstance = false;
                }
                else
                {
                    curHazard.GetComponent<Mover>().speed = curHazardSpeed;
                }
                yield return new WaitForSeconds(spawnWait);
            }

            hazardCount += hazardCountIncrementPerWave;
            curHazardSpeed *= hazardSpeedIncreaseFactor;
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore (int scoreIncrement)
    {
        score += scoreIncrement;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
