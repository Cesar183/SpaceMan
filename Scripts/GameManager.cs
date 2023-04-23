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
    public int collectedObject = 0;
    [SerializeField] int valueExtraLife = 10;
    void Awake()
    {
        if(sharedInstance == null)
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
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)//(Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
    }
    public void StartGame()
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
            LevelManager.sharedInstace.RemoveAllBlocks();
            LevelManager.sharedInstace.GenerateInitialBlocks();
            collectedObject = 0;
            controller.StartGame();
            MenuManager.sharedInstance.HideMainMenu();
        }
        else if(newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.MenuGameOver();
        }
        this.currentGameState = newGameState;
    }
    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
        if(collectedObject >= 10)
        {
            collectedObject = 0;
            controller.CollectHealth(valueExtraLife);
        }
    }
}
