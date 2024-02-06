using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [SerializeField] private GameObject _shopkeeperPanel;
    public int currentSelection;
    public int currentItemCost;

    private Player _player;


    //TRIGGERS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _player = collision.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.Diamonds);
            }
            OpenShop();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
            CloseUpShop();
    }
        
    
    //TRADE BEHAVIORS
    public void SelectItem(int item)
    {
        currentSelection= item;
        //0 = flameSword- 400g
        //1 = boots of flight- 200g
        //2 = key to castle- 100g

        Debug.Log("SelectItem() " + item);

        switch(item)
        {
            case 0: //Flame Sword
                UIManager.Instance.UpdateShopSelection(62);
                currentSelection = 0;
                currentItemCost = 400;
                break;
            case 1:  //Boots Of Flight
                UIManager.Instance.UpdateShopSelection(-39);
                currentSelection = 1;
                currentItemCost = 200;
                break;
            case 2:     //Key To Castle
                UIManager.Instance.UpdateShopSelection(-140);
                currentSelection= 2;
                currentItemCost = 100;
                break;  
                
            default: break;
        }
    }

    public void BuyItem()
    {
        //Diamond Check- Purchase - Exit Store
        if (_player.Diamonds >= currentItemCost)
        { 
            if(currentSelection == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
                Debug.Log("Purchasing Key");
            }
        
            _player.Diamonds -= currentItemCost;
            Debug.Log("Purchased " + currentSelection);
            Debug.Log("Remaining Gems: " + _player.Diamonds);
            _shopkeeperPanel.SetActive(false);
        }
        else
        {
            Debug.Log("You do not have enough diamonds. Closing Up Shop!");
            _shopkeeperPanel.SetActive(false);
        }
        UIManager.Instance.UpdateGemCount(_player.Diamonds);

    }


    //SHOP BEHAVIORS
    private void OpenShop()
    {
        _shopkeeperPanel.SetActive(true);
    }
    
    private void CloseUpShop()
    {
        _shopkeeperPanel.SetActive(false);
    }

}
