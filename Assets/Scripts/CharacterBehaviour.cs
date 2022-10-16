using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public float groundCheckRadius = 0.15f;
    public Transform groundCheckPos;
    public LayerMask whatIsGround;
    private bool isGrounded = true;
    [SerializeField]
    private GameObject flashlight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
        Jump();
    }

     public void Run()
    {
       transform.Translate(Vector2.right * (Time.deltaTime * speed));
        isGrounded = GroundCheck();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight.activeSelf)
                flashlight.SetActive(false);
            else
                flashlight.SetActive(true);
        }
    }

    public void Jump()
    {
        if (isGrounded && Input.GetAxis("Jump")>0)
        {
            rb.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private bool GroundCheck() {

        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
        
    }
}
