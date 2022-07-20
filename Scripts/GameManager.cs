using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;
    private PlayerController controller;
    void Awake()
    {
        if(sharedInstance==null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)//(Input.GetKeyDown(KeyCode.S)) Tecla S para iniciar el juego
        {
            StarGame();
        }
    }
    public void StarGame()
    {
        SetGameState(GameState.inGame);
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
        }
        else if(newGameState == GameState.inGame)
        {
            LevelManager.sharedInstance.RemoveAllBlock();
            LevelManager.sharedInstance.GenerateInitialLevelBlocks();
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
        }
        else if(newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.GameOverMenu();
        }
        this.currentGameState = newGameState;
    }
}
