using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour {
    public Queue<GameObject> animQueue;
    public GameObject bulletPrefab;
    public int range;
    public int speed;
    public bool bulletSpawned;

    private GameObject currentAnimatedBlock;

    void Start() {
       bulletSpawned = false; 
       animQueue = new Queue<GameObject>();
    }

    void FixedUpdate(){
        spawnBullet(bulletPrefab);
    }

    void Update(){
        playAnimation();
    }

    private void spawnBullet(GameObject bulletPrefab){
        if(Input.GetKeyDown("space") && !bulletSpawned && animQueue.Count == 0){
            Debug.Log("spawnBullet");
            bulletSpawned = true;

            Vector3 parentPos = this.transform.parent.transform.position;
            Vector3 bulletPos = new Vector3(Mathf.Round(parentPos.x),
                                            Mathf.Round(parentPos.y) - 1,
                                            0);

            Debug.Log("spawned at y -> " + Mathf.Round(parentPos.y));
            
            Instantiate(bulletPrefab,bulletPos, Quaternion.identity);
        }else if(Input.GetKeyDown("space") && animQueue.Count > 0){
            Debug.Log("CoolDown");

        }
    }

    private void playAnimation(){
        //if no animation curently played
        if(currentAnimatedBlock == null && animQueue.Count > 0){
            currentAnimatedBlock = animQueue.Dequeue();
            currentAnimatedBlock.GetComponent<Animator>().SetBool("IsGrowing", true);
        }
        if(currentAnimatedBlock == null) return;

        Animator animator = currentAnimatedBlock.GetComponent<Animator>();

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("soilGrow") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("GrassGrow")
        ){
            currentAnimatedBlock.GetComponent<Animator>().SetBool("IsGrowing", false);
            currentAnimatedBlock = null;
        }
    }
}
