using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text coinsText, scoreText, maxScoreText;
    private PlayerController controller;
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0f);

            coinsText.text = coins.ToString();
            scoreText.text = "Your Score: " + score.ToString("f1");
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
        }
    }
}
