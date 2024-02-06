using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        { if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
                return _instance;
        }

    }


    //Auto-Properties
    public Player Player { get; private set; }
    public bool HasKeyToCastle { get; set; }
    public bool IsGameOver { get; set; }


    private void Awake()
    {
        _instance= this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    //GAME CONDITIONS
    public void BeginGame()
    {
        IsGameOver = false;
    }

    public void CheckPlayerKey()
    {
        if(HasKeyToCastle)
        {
            Debug.Log("You Win");
            UIManager.Instance.PlayerWins();
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        UIManager.Instance.GameOverMessage();
    }
    
    public void RestartGame()
    {
        if (IsGameOver)
            SceneManager.LoadScene("01_Level01");

    }
}
