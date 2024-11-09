using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScanFireButtons : MonoBehaviour
{
    public Button scanButton;
    public Button fireButton;
    public TMP_Text fireTimerText;
    public GameObject bullet;
    public GameObject player;

    private List<GameObject> visibleEnemies = new List<GameObject>();
    private float fireCooldown = 3f;
    private float lastFireTime = -3f;
    private float fadeDuration = 2f; //how long it takes for an enemy to fade after being scanned

    void Start()
    {
        scanButton.onClick.AddListener(OnScan);
        fireButton.onClick.AddListener(OnFire);
    }

    void Update()
    {
        UpdateFireCooldownText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!visibleEnemies.Contains(collision.gameObject))
            {
                visibleEnemies.Add(collision.gameObject);
                SetInvisible(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!visibleEnemies.Contains(collision.gameObject))
            {
                visibleEnemies.Add(collision.gameObject);
                SetInvisible(collision.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //remove the enemy from visible enemies if it leaves the field of view. player must re-scan the enemy to target it
        if (collision.CompareTag("Enemy"))
        {
            visibleEnemies.Remove(collision.gameObject);
        }
    }

    void OnScan()
    {
        foreach (GameObject enemy in visibleEnemies)
        {
            if (enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                StartCoroutine(FadeEnemy(enemy));
            }
        }
    }

    IEnumerator FadeEnemy(GameObject enemy)
    {
        if (enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            float fadeSpeed = 1f / fadeDuration;
            for (float t = 0; t < 1; t += Time.deltaTime * fadeSpeed)
            {
                if (sr != null) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(1, 0, t));
                yield return null;
            }
            if (sr != null) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f); //fully invisible
        }
    }

    void SetInvisible(GameObject enemy)
    {
        if (enemy.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        }
    }

    void OnFire()
    {
        if (Time.time >= lastFireTime + fireCooldown)
        {
            foreach (GameObject enemy in visibleEnemies)
            {
                FireAtEnemy(enemy);
            }
            lastFireTime = Time.time;
        }
    }

    void FireAtEnemy(GameObject enemy)
    {
        Vector2 direction = (enemy.transform.position - player.transform.position).normalized;

        GameObject newBullet = Instantiate(bullet, player.transform.position, Quaternion.identity);

        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = direction * 10f;
        }

        newBullet.transform.right = direction;
    }

    void UpdateFireCooldownText()
    {
        float remainingCooldown = Mathf.Max(0, fireCooldown - (Time.time - lastFireTime));
        fireTimerText.text = $"Cooldown: {remainingCooldown:F1}s";
    }
}
