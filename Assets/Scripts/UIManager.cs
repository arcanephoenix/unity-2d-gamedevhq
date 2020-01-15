using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Image livesImage;
    [SerializeField]
    private Sprite[] livesSprites;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject restartText;

    private bool isVisible = false;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreText.text = "Score: 0";
        gameOverText.SetActive(isVisible);
        restartText.SetActive(isVisible);
    }

    private void Update()
    {
        
    }

    public void SetPlayerScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void SetLivesSprite(int lives)
    {
        livesImage.sprite = livesSprites[lives];
    }
    public void SetGameOver()
    {
        isVisible = true;
        restartText.SetActive(isVisible);
        gameManager.GameOver();
        StartCoroutine(GameOverBlink());
    }
    
    IEnumerator GameOverBlink()
    {
        while (true)
        {
            
            gameOverText.SetActive(isVisible);
            yield return new WaitForSeconds(0.5f);
            isVisible = !isVisible;
        }
    }

}
