using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState       //se crea una clase gamestate con un enumerado (variable) toma una serie de valores
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;      //Empezar el juego en el menu

    public static GameManager sharedInstance;       //sharedinstance es el nombre que le da por se un singleton

    PlayerController controller;        //Se crea una variable para poder referenciar al player controller


    void Awake()
    {
        if (sharedInstance == null)     //si no se ha inicializado ningun sharedinstance, se asignara a ese script
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("player").GetComponent<PlayerController>();    //Se debe extraer la componente Playercontroller de un objeto de juego (player)
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && currentGameState!= GameState.inGame)    //Al presionar la tecla enter (start) y si el estado de la partida es in game
                                                                                     //se inicia el juego
        {
            StartGame();
        }
        
    }

    public void StartGame ()
    {
        SetGameState(GameState.inGame); //se encarga de cambiar el estado del juego a play
    }

    public void GameOver ()
    {
        SetGameState(GameState.gameOver);   //se encarga de cambiar el estado del juego a game over
    }

    public void BackToMenu ()
    {
        SetGameState(GameState.menu);   //se encarga de cambiar el estado del juego a menu
    }

    void SetGameState (GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {

        }
        else if (newGameState == GameState.inGame)
        {
            controller.StartGame();
        }
        else if (newGameState == GameState.gameOver)
        {

        }

        this.currentGameState = newGameState;   //this enfatiza que es una variable de la propia clase utilizada
    }
}
