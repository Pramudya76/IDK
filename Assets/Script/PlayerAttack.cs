using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    public Animator EnemyAnimasion;
    public GameManager GM;
    public bool isAttack;
    public Transform attackArea;
    public GameObject EnemyObject;
    private bool canAttack = true;
    public EnemyMovement EM;
    public LayerMask layer;
    public GameObject gameWon;
    public float attackRadius = 1.2f;
    public bool isBlock = false;
    public GameObject shield;
    public  bool stun = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        gameWon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)) {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y = 180f;
            transform.eulerAngles = newRotation;
        }else if(Input.GetKey(KeyCode.D)) {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y = 0f;
            transform.eulerAngles = newRotation;
        }

        if(Input.GetMouseButtonDown(0) && canAttack) {
            //animator.SetBool("Attack", canAttack);
            StartCoroutine(attack());
        }

        if(Input.GetMouseButtonDown(1)) {
            isBlock = true;
            shield.gameObject.SetActive(true);
            Debug.Log("Block Aktif");
        }else if(Input.GetMouseButtonUp(1)){
            isBlock = false;
            shield.gameObject.SetActive(false);
            Debug.Log("Block Non-Aktif");
        }
    }


    IEnumerator attack() {
        //false agar player tidak bisa membuat serangan lagi dan harus menunggu
        canAttack = false;
        animator.SetTrigger("CobaAttack");
        Collider2D attack = Physics2D.OverlapCircle(attackArea.position, 1f, layer);
        if(attack != null && attack.gameObject.CompareTag("Enemy")) {
            float CritDmg = Random.Range(1,10);
            if(CritDmg > 2) {
                //buat boolean untuk stun lalu buat pengkondisian di script enemy biar animasi stun dijalankan
                stun = true;
                EM.health -= 30;
                Debug.Log("Crit damage terjadi");
                StartCoroutine(CDStun());
            }else {
                EM.health -= 15;
                EnemyAnimasion.SetTrigger("Hit");
            }
            Debug.Log("Enemy terkena serangan dan sisa HP : " + EM.health);
            if(EM.health <= 0) {
                EnemyAnimasion.SetBool("DeathEnemy", true);
                Debug.Log("Enemy telah mati");
                StartCoroutine(EnemyTimeDie());
                StartCoroutine(TimeToGameOver());
                
            }
        }
        yield return new WaitForSeconds(1f);
        canAttack = true;
        //Debug.Log(canAttack);
    }

    IEnumerator EnemyTimeDie() {
        yield return new WaitForSeconds(1.5f);
        gameWon.gameObject.SetActive(true);
    }

    IEnumerator TimeToGameOver() {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        
    }

    public void PlayerDie() {
        animator.SetTrigger("Death");
        Time.timeScale = 0;
    }

    private void OnTrigerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "EnemyLongAttack" && isBlock) {
            Destroy(collision.gameObject);
            Debug.Log("kena");
        }
    }

    IEnumerator CDStun() {
        if(!EM.buff) {
            EnemyAnimasion.SetBool("Stuned", true);
            yield return new WaitForSeconds(3f);
            EnemyAnimasion.SetBool("Stuned", false);
            // if(EM.health <= 0) {
            //     EnemyAnimasion.SetBool("DeathEnemy", true);
            // }
            stun = false;

        }
    }

    

    
}
