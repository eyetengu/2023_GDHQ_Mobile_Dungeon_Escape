using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("TriggerWarning");

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                _audioSource.Play();
                player.AddGems(gems);                
                Destroy(this.gameObject, 1f);
            }
        }
    }
}
