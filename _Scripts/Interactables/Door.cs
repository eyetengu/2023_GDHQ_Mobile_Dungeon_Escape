using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _hasOpened;
        
    void OnTriggerEnter2D(Collider2D collision)
    {    
        if(collision.tag == "Player" && _hasOpened == false)
        {
            bool value = GameManager.Instance.HasKeyToCastle;
            if(value)
            {
                _hasOpened = true;
                GameManager.Instance.CheckPlayerKey();
            }
            else
            {
                Debug.Log("You Really Need A Key");
            }
        }
    }
}
