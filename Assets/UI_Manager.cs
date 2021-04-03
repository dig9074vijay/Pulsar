using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
  
    [SerializeField]
    private Text game_over_text;
    [SerializeField]
    private Text restart_level_text;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score : " + 0;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
            Debug.LogError("Game Manager is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreUpdate(string _score)
    {
        _scoreText.text = "Score : " + _score; 
    }

    public void UpdateLives(int lives)
    {
        _livesImg.sprite = _livesSprites[lives];
    }

    public void GameOver()
    {
        game_over_text.gameObject.SetActive(true);
        restart_level_text.gameObject.SetActive(true);
        _gameManager.GameOverRestart();
        StartCoroutine(GameOverFlicker());
    }


    IEnumerator GameOverFlicker()
    {
        while (true) {
            game_over_text.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            game_over_text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
