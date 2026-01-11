using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb;
  public int life = 5;
  [Header("Movement")]
  [SerializeField] private float movementForce;
  [SerializeField] private float jumpForce;
  [SerializeField] private float jumpCutMultiplier;
  private float hInput;
  private float vInput;
  private Animator anim;
  
  [Header("Ground Checker")]
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float checkRadius = 0.2f;
  [SerializeField] private LayerMask groundLayer;
  private bool isGrounded;
  
  [Header("Knockback Settings")]
  [SerializeField] private float kbForce = 10f;       
  [SerializeField] private float kbDuration = 0.2f;    
  private bool isKnockback;
  private bool canTakeDamage = true;
  
  
  private void Awake()
  {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
  }

  void Update()
  {
      if (isKnockback) return;
      
      isGrounded=Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
      
      FaceTo();
      
      if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
      {
          rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
          rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      }
      
      if (Input.GetKeyUp(KeyCode.Space))
      {
          if (rb.linearVelocity.y > 0)
          {
              //Velocidad * el cut multiplier = velocidad reducida
              rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
          }
      }
      PlayerDead();
  }
  
  private void FixedUpdate()
  {
      if (isKnockback) return;
      
      rb.AddForce(new Vector2(hInput, 0)*movementForce, ForceMode2D.Impulse);
  }
  
  private void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("Enemy")&&canTakeDamage)
      {
          EnemyLogic enemyScript = other.gameObject.GetComponent<EnemyLogic>();
          
          if (enemyScript != null && enemyScript.isDead) return;
          if (rb.linearVelocity.y < -0.1f) return;
          
          ApplyKnockback(other.transform);
      }
      
      if (other.gameObject.CompareTag("Exit"))
      {
          int ThisScene =  SceneManager.GetActiveScene().buildIndex;
          SceneManager.LoadScene(ThisScene + 1);
      }
      
     if (other.gameObject.CompareTag("Void"))
      {
          if (GameManager.instance != null)
          {
              GameManager.instance.GameOver();
          }
      }
      
  }
  
  public void Bounce()
  {
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
      rb.AddForce(Vector2.up * (jumpForce * 0.7f), ForceMode2D.Impulse);
  }

  private void ApplyKnockback (Transform enemyTransform)
  {
      life -=1;
      Vector2 dir = (transform.position - enemyTransform.position).normalized;
      StartCoroutine(KnockbackRoutine(dir));
      StartCoroutine(InvincibilityRoutine());
  }
  
  private IEnumerator KnockbackRoutine(Vector2 direction)
  {
      isKnockback = true;
      // Velocidad = 0
      rb.linearVelocity = Vector2.zero;
      
      rb.AddForce((direction + Vector2.up * 0.5f).normalized * kbForce, ForceMode2D.Impulse);

      yield return new WaitForSeconds(kbDuration);
      isKnockback = false;
  }

  private IEnumerator InvincibilityRoutine()
  {
      canTakeDamage = false;
      
      yield return new WaitForSeconds(1f);
      
      canTakeDamage = true;
  }

  private void PlayerDead()
  {
      if (life <= 0 && GameManager.instance != null)
      {
              GameManager.instance.GameOver();
      }
  }
  
    private void FaceTo()
    {
        anim.SetFloat("xSpeed", Mathf.Abs(hInput));
        anim.SetFloat("ySpeed", rb.linearVelocityY);

        hInput = Input.GetAxisRaw("Horizontal");
        if (hInput < 0 && transform.eulerAngles.y == 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (hInput > 0)
        {
            transform.localScale = Vector3.one;
        }
    }
}
