using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private bool grounded;
    private Rigidbody2D rb;
    private float moveSpeed = 18f;
    public float healthPlayer = 100;
    private float JumpForce = 30f;
    public Animator PlayerAnimasion;
    public GameObject GameLose;
    public PlayerAttack PA;
    public bool Dash;
    public Transform Enemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        if(!Dash) {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }

        if(Input.GetKey(KeyCode.Space) && grounded) {
            
            jump();
            //Debug.Log(grounded);
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            Dash = true;
            if(transform.eulerAngles.y == 0f) {
                rb.velocity = new Vector2(20, rb.velocity.y);
            }else if(transform.eulerAngles.y == 180f) {
                rb.velocity = new Vector2(-20, rb.velocity.y);
            }
            StartCoroutine(ResetDash());
        }

        animator.SetBool("Run", moveX != 0);
        animator.SetBool("Ground", grounded);

        

    }


    
    private void jump() {
        grounded = false;
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        Debug.Log("Jumping with force: " + JumpForce);
    }

    

    private void OnCollisionStay2D(Collision2D collision) {
        //SDebug.Log("Collision detected with: " + collision.gameObject.name);
        if(collision.gameObject.CompareTag("Ground")) {
            grounded = true;
            //Debug.Log("Player menyentuh tanah");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "EnemyLongAttack" && !PA.isBlock) {
            healthPlayer -= 15f;
            if(healthPlayer <= 0) {
                PlayerAnimasion.SetTrigger("Death");
                StartCoroutine(PlayerTimeDie());
                StartCoroutine(TimeToGameOver());
            }
            Destroy(collision.gameObject);
            Debug.Log("Player terkena serangan jarak jauh dan HP tersisa : " + healthPlayer);

        }

    }

    IEnumerator PlayerTimeDie() {
        yield return new WaitForSeconds(1f);
        GameLose.gameObject.SetActive(true);
    }

    IEnumerator TimeToGameOver() {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0;
        
    }


    IEnumerator ResetDash() {
        yield return new WaitForSeconds(0.5f);
        Dash = false;
    }
}
