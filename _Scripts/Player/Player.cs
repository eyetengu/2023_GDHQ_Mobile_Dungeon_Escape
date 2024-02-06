using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; 

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rb;
    private bool _grounded;
    [SerializeField] private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField] private float _speed = 3.5f;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _swordAttackRenderer;
    //public int diamonds;

    [SerializeField] private MobileAdventure_Inputs _inputs;
    private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip[] _audioClips;
    private BoxCollider2D _boxCollider2D;
    private bool _characterIsDead;


    //PROPERTIES
    public int Health { get; set; }
    public int Diamonds { get; set; }

    //INITIALIZATION
    void Start()
    {
        _inputs = new MobileAdventure_Inputs();
        _inputs.Enable();
        _inputs.Player.Jump.performed += Jump;
        _inputs.Player.Attack.performed += Attack;
        
        _rb = GetComponent<Rigidbody2D>();  
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordAttackRenderer= transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;
        UIManager.Instance.UpdateGemCount(Diamonds);
        _playerAudioSource= GetComponent<AudioSource>();
    }


    //MAIN FLOW
    void FixedUpdate()
    {
        if (Health < 1)
            return;
        Movement();
        //Debug.Log("Diamonds: " + Diamonds);
    }


    //MAIN PLAYER BEHAVIORS
    private void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
        _grounded = IsGrounded();

        Flip(move);
        
        _rb.velocity = new Vector2(move * _speed, _rb.velocity.y);
        if (_playerAnim == null)
            Debug.Log("Animator null");
        else
            _playerAnim.Move(move);    
    }    
    
    private void Jump(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Entered");
        if(IsGrounded() == true && Health > 0)
        {
            //Debug.Log("Jumping");
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
    }
    
    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(Health> 0)
        {
            _playerAnim.Attack();
            _playerAudioSource.PlayOneShot(_audioClips[0]);
        }
    }
    
    private bool IsGrounded()
    {
        if(Health<1) return false;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.7f, 1 << 8);        

        if (hitInfo.collider !=null)
        {
            //Debug.Log("IS GROUNDED");
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        //Debug.Log("Is NOT Grounded");
        return false;
    }  
    
    void Flip(float move) 
    {          
        if (move < 0)
        {
            _playerSpriteRenderer.flipX = true;
            _swordAttackRenderer.flipY = true;
            _swordAttackRenderer.flipX = true;

            Vector3 newPos = _swordAttackRenderer.transform.localPosition;
            newPos.x = -1.01f;
            _swordAttackRenderer.transform.localPosition = newPos;
        }
        else if (move > 0)
        {
            _playerSpriteRenderer.flipX = false;
            _swordAttackRenderer.flipY = false;
            _swordAttackRenderer.flipX = false;

            Vector3 newPos = _swordAttackRenderer.transform.localPosition;
            newPos.x = 1.01f;
            _swordAttackRenderer.transform.localPosition = newPos;
        }
    }
    
    public void AddGems(int gems)
    {
        //Debug.Log("AddGems(): " + gems);
        Diamonds += gems;
        UIManager.Instance.UpdateGemCount(Diamonds);
    }
    

    //INTERFACES
    public void Damage()
    {
        if (Health < 1 && _characterIsDead == false)
        { 
            _characterIsDead = true;
            KillCharacter();
        }
        _playerAudioSource.PlayOneShot(_audioClips[2]);
        Debug.Log("Damaged player");
        //_playerAnim.PlayerHit();
        Health--;
        UIManager.Instance.UpdateLives(Health);               
    }


    //DEBUG
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -.85f, 0));
    }


    //COROUTINES
    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump= false;
    }

    public void KillCharacter()
    {
        //Player Values
        Health = 0;
        _speed= 0;
        //Audio
        _playerAudioSource.PlayOneShot(_audioClips[1]);
        //Animation
        _playerAnim.Death();
        //Inform Game Manager
        GameManager.Instance.IsGameOver = true;
        //UpdateUI
        UIManager.Instance.UpdateLives(Health);
        UIManager.Instance.GameOverMessage();
        //Smart-Ass Comment
        Debug.Log("I am dying, Horatio");
    }
}
