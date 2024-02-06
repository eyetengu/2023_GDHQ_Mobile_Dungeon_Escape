using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private AudioSource _audioSource;
    private Animator _animator;
    private bool _hasOpened;

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
        _animator= GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && _hasOpened == false)
        {
            _hasOpened = true;
            _audioSource.Play();
            GameManager.Instance.HasKeyToCastle = true;
            _animator.SetTrigger("OpenChest");
        }
    }
}
