using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLongRotation : MonoBehaviour
{
    private Transform player;
    private Transform Enemy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        player = PlayerObject.transform;
        GameObject EnemyObject = GameObject.FindWithTag("Enemy");
        Enemy = EnemyObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > Enemy.position.x) {
            Vector3 AttackRotasi = transform.eulerAngles;
            AttackRotasi.y = 180f;
            transform.eulerAngles = AttackRotasi;
        }else if(player.position.x <= Enemy.position.x) {
            Vector3 AttackRotasi = transform.eulerAngles;
            AttackRotasi.y = 0f;
            transform.eulerAngles = AttackRotasi;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Shield") {
            Debug.Log("kena");
            Destroy(gameObject);
        }
    }
    
}
