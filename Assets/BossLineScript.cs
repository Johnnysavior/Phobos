using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLineScript : MonoBehaviour {
    

    void OnTriggerEnter2D(Collider2D otherCollider) {      
        if (otherCollider.GetComponent<PlayerScript>() != null) {
            EventManager.Instance.spawnBoss(transform.position);
            Destroy(gameObject);
            GameObject.Find("Music").GetComponent<MusicSwaperScript>().playBossMusic = true; 
        }
    } 
   
	
}
