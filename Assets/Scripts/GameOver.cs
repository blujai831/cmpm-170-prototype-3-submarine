using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button replayButton;
    public TMP_Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        replayButton.onClick.AddListener(ReplayClicked);
        replayButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReplayClicked()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void GameEnded()
    {
        replayButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }
}
