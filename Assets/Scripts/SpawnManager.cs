using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform circleCenter; // The center of the circle
    public float circleRadius = 5f; // The radius of the circle
    public float spawnRadius = 7f; // The minimum distance from the center to spawn enemies

    public TMP_Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        //random angle
        float angle = Random.Range(0f, Mathf.PI * 2);

        //random distance that is outside the scanner's range
        float distance = Random.Range(circleRadius, spawnRadius);

        //generate position
        Vector2 spawnPosition = new Vector2(
            circleCenter.position.x + distance * Mathf.Cos(angle),
            circleCenter.position.y + distance * Mathf.Sin(angle)
        );

        //create enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        //determine random amount of seconds to generate new enemy
        float enemyTimer = Random.Range(3f, 8f);
        yield return new WaitForSeconds(enemyTimer);
    }

    public void UpdateScore()
    {
        score++;
        scoreText.SetText("Score: " + score.ToString());
    }
}
