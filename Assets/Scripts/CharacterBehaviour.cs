using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Animator anim;
    public Transform playerSpawnPoint;
    public GameObject player;
    public AudioSource jumping;
    public AudioSource deathSound;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
        jumping = GetComponent<AudioSource>();
        deathSound = GetComponent<AudioSource>();
        
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
            jumping.Play();
            anim.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
            isGrounded = false;
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }
 
    private bool GroundCheck() {

        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
        
    }


    public IEnumerator WaitForSceneLoad()
    {

        yield return new WaitForSeconds(0.5f);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathTriggers"))
        {
            deathSound.Play();
            anim.SetBool("isDead", true);
            StartCoroutine(WaitForSceneLoad());

            //player.transform.position = playerSpawnPoint.position;
        }
        else 
        {
            anim.SetBool("isDead", false);
            
        }
    }
}
