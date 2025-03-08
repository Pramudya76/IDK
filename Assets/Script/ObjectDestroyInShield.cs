using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyInShield : MonoBehaviour
{
    public PlayerAttack PA;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTrigerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "EnemyLongAttack" && !PA.isBlock) {
            Destroy(collision.gameObject);
            Debug.Log("kena");
        }
    }
}
