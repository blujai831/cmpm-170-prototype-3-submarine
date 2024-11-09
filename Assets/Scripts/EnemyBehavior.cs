using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public GameObject player;

    public GameObject spawnManager;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnManager = GameObject.FindWithTag("SpawnManager");
        gameOver = GameObject.FindWithTag("UI");

        SetInvisible();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameOver.GetComponent<GameOver>().GameEnded();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spawnManager.GetComponent<SpawnManager>().UpdateScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void SetInvisible()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f); // Set alpha to 0 (fully invisible)
        }
    }
}
