using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb;
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
