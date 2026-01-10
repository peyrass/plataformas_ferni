using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb;
  public int life = 5;
  [SerializeField] private float movementForce;
  [SerializeField] private float jumpForce;
  private float hInput;
  private float vInput;
  
  private Animator anim;
  
  
  private void Awake()
  {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
      if (other.gameObject.CompareTag("Enemy"))
      {
          life -= 1;
      }

      if (other.gameObject.CompareTag("Spikes"))
      {
          if (GameManager.instance != null)
          {
              GameManager.instance.GameOver();
          }
      }
  }

  private void PlayerDead()
  {
      if (life <= 0)
      {
          if (GameManager.instance != null)
          {
              GameManager.instance.GameOver();
          }
      }
  }
  void Start()
    {

    }
    void Update()
    {
        FaceTo();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        PlayerDead();
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

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(hInput, 0)*movementForce, ForceMode2D.Impulse);
    }
}
