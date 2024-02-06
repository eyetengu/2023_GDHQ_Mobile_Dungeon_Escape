using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    private bool _hasPlayedDeathAnim;
    [SerializeField] private GameObject _spiderAcidPrefab;
    private Animator _animator;


    //Use For Initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;

        _animator = GetComponentInChildren<Animator>();
    }

    public void Damage()
    {
        if (isDead) return;

        _audioSource.PlayOneShot(_audioClips[2]);
        Health--;

        Debug.Log("Spider Damage");
       if(Health < 1)
        {
            isDead= true;
            anim.SetTrigger("Die");
            _audioSource.PlayOneShot(_audioClips[1]);
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = gems;
        }
    }

    public override void Update()
    {

    }

    public override void Movement()
    {
        //sit still
    }

    public void Attack()
    {
        if(GameManager.Instance.IsGameOver == false)
        {
            Instantiate(_spiderAcidPrefab, transform.position, Quaternion.identity);
            _audioSource.PlayOneShot(_audioClips[0]);
        }
        else
            _animator.SetTrigger("Idle");

    }

    public void KillCharacter()
    {     
    }
}
