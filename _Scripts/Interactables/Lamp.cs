using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IDamageable
{
    private Animator _animator;


    private int health = 1;
    public int Health { get; set; }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void Damage()
    {
        _animator.SetTrigger("OpenChest");
        Destroy(gameObject, 1f);
    }

    public void KillCharacter()
    {

    }

    
}
