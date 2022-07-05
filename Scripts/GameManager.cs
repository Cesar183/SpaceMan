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
    void Awake()
    {
        if(sharedInstance==null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")) //(Input.GetKeyDown(KeyCode.S)) Tecla S para iniciar el juego
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
            //Logica menu
        }
        else if(newGameState == GameState.inGame)
        {
            //Logica en Juego
        }
        else if(newGameState == GameState.gameOver)
        {
            //Logica fin de juego
        }
        this.currentGameState = newGameState;
    }
}
