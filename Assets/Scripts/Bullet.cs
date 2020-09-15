using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private int range;
    private int speed;
    private Vector2 initialPos;
    private Transform tr;
    private Rigidbody2D rb;
    private BulletManager bulletManagerScript;
    private bool isCollding;

    void Start(){
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        isCollding = false;

        initialPos.y = tr.position.y;
        bulletManagerScript = 
            GameObject.Find("BulletManager").GetComponent<BulletManager>();

        range = bulletManagerScript.range;
        speed = bulletManagerScript.speed;
    }

    void FixedUpdate(){
        float difference = Mathf.Abs(this.transform.position.y - initialPos.y);
        move();

        if( difference >= range){
            Debug.Log("DestoryBullet");
            Destroy(gameObject);
            bulletManagerScript.bulletSpawned = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(isCollding) return;

        if(other.name == "Grass(Clone)" || other.name == "Soil(Clone)"){
            bulletManagerScript.animQueue.Enqueue(other.gameObject);
        }
        isCollding = true;
    }

    void OnTriggerExit2D(Collider2D other){
        isCollding = false;
    }

    private void move(){
        rb.MovePosition(rb.position + new Vector2(rb.position.x, speed * -Time.deltaTime));
    }
}
