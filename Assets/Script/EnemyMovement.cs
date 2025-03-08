using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Animator PlayerAnimasion;
    private Animator animator;
    public GameObject PlayerObject;
    public GameObject GameLose;
    public Transform enemyAttackArea;
    private bool enemyAtk = true;
    public LayerMask layer;
    
    public PlayerMovement PM;
    public PlayerAttack PA;
    public float health = 500;
    private Rigidbody2D rb;  
    private bool enemyCanAtk = true;
    private float stopDistance = 1.3f;
    private float moveSpeed = 2f;
    private bool jumpAtk = true;
    private bool isGrounded = true;
    public GameObject SeranganJauh;
    public Transform areaAttackLong;
    public bool buff = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        GameLose.gameObject.SetActive(false);
        //EnemyAtkArea.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(rb.transform.position, player.position);
        //float direction = rb.transform.position.x - player.position.x;
        

        

            if(distance > stopDistance && !PA.stun) {
                transform.position = Vector2.MoveTowards(rb.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                animator.SetBool("Walk",  true);
            }else {
                rb.velocity = Vector2.zero;
                jumpAtk = false;
                if(enemyCanAtk && distance <= stopDistance && !jumpAtk && !PA.stun) {
                    animator.SetBool("Walk", false);
                    StartCoroutine(EnemyAtk());
                }
            }

            if(rb.transform.position.x > player.transform.position.x) {
                
                Vector3 newRotasi = transform.eulerAngles;
                newRotasi.y = 180f;
                transform.eulerAngles = newRotasi;
                //Debug.Log("kiri");
            }else if(rb.transform.position.x < player.transform.position.x) {
                Vector3 newRotasi = transform.eulerAngles;
                newRotasi.y = 0f;
                transform.eulerAngles = newRotasi;
                //Debug.Log("kanan");
            }

            if(health <= 500 && !buff) {
                Debug.Log("Buff jalan");
                StartCoroutine(CDBuff());
            }
        

    }

    IEnumerator CDBuff() {
        buff = true;
        Debug.Log("Awal buff");
        // animator.SetTrigger("Fase2");
        animator.SetBool("Back", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Back", false);
        //buff = false;
        moveSpeed = 5f;
        Debug.Log("Akhir buff");
    }


    IEnumerator EnemyAtk() {
        enemyCanAtk = false;
        animator.SetTrigger("CobaAttack");
        yield return new WaitForSeconds(0.5f);
        Collider2D enemyAtk = Physics2D.OverlapCircle(enemyAttackArea.position, 0.8f, layer);
        if(enemyAtk != null && enemyAtk.gameObject.CompareTag("Player") && !PA.isBlock ) {
            PM.healthPlayer -= 10;
            Debug.Log("Player terkena serangan");
            if(PM.healthPlayer <= 0) {
                PlayerAnimasion.SetTrigger("Death");
                Debug.Log("Player telah mati");
                StartCoroutine(PlayerTimeDie());
                StartCoroutine(TimeToGameOver());
            }
        }
        yield return new WaitForSeconds(2f);
        enemyCanAtk = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Warna Gizmo
        Gizmos.DrawSphere(enemyAttackArea.position, 0.8f); // Menggambar bola di posisi objek
    }

    IEnumerator PlayerTimeDie() {
        yield return new WaitForSeconds(2f);
        GameLose.gameObject.SetActive(true);
    }

    IEnumerator TimeToGameOver() {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground" && enemyAtk && !PA.stun) {
            float distance = Vector2.Distance(rb.transform.position, player.position);
            

            if(distance >= 12) {
                enemyAtk = false;
                jumpAtk = true;
                int angkaRandom = Random.Range(1,10);
                if(angkaRandom > 1 && jumpAtk) {
                    StartCoroutine(CDJump());
                }else {
                    StartCoroutine(CD());
                    Debug.Log("serangan jarak jauh aktif");
                }
            }
        }
    }

    private void SeranganJauhPembelahAwan() {
        //memisahkan script instantiate dlm bentuk script yg berbeda lalu masukan ke areaLongAttack
        Debug.Log("awal " + areaAttackLong.position);
        Vector3 spawnAttack = areaAttackLong.position;
        GameObject AttackJauh = Instantiate(SeranganJauh, areaAttackLong.position, Quaternion.identity);
        Vector2 arah = (player.position - transform.position).normalized;
        AttackJauh.GetComponent<Rigidbody2D>().velocity = arah * 10f;
        Debug.Log("akhir " + areaAttackLong.position);
    }

    IEnumerator CD() {
        //SeranganJauhPembelahAwan();
        SeranganJauhPembelahAwan();
        if(health <= 500) {
            // yield return new WaitForSeconds(0.5f);
            // SeranganJauhPembelahAwan();
            // yield return new WaitForSeconds(0.5f);
            // SeranganJauhPembelahAwan();
            for(int a = 0; a < 4; a++) {
                SeranganJauhPembelahAwan();
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(2f);
        enemyAtk = true;
    }

    IEnumerator CDJump() {
        animator.SetTrigger("EnemyJump");
        float direction = rb.transform.position.x - player.position.x;
        rb.velocity = new Vector2(-direction, 4.5f);
        yield return new WaitForSeconds(2f);
        enemyAtk = true;
        Debug.Log("ada " + direction);
        Debug.Log(isGrounded);

    }
    
    
}
