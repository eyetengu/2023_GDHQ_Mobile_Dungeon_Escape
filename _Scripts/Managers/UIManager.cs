using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }

            return _instance; 
        }
    }

    public TMP_Text playerGemCountText;
    public Image selectionImg;
    public TMP_Text gemCountText;
    public Image[] _HealthBars;
    [SerializeField] private GameObject _gameOverMessage;
    [SerializeField] private GameObject _playerWinsScreen;


    private void Awake()
    {
        _instance= this;        
    }


    //SHOP
    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount.ToString() + " G";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int count)
    {
        gemCountText.text = count.ToString();
    }


    //LIVES
    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            { 
                _HealthBars[i].enabled = false;
            }
        }
    }
    

    //GAME
    public void PlayerWins()
    {
        _playerWinsScreen.SetActive(true);
    }
    public void RestartGame()
    {
        _gameOverMessage.SetActive(false);
    }
    public void GameOverMessage()
    {
        _gameOverMessage.SetActive(true);
    }
}
