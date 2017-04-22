using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidObjectScript : MonoBehaviour {

    public bool collisionCausesDamageOnPlayer = true;
    public bool collisionCausesDamagesOnEnemy = false;
    public float damagePerTic = 1;
    public float ticCooldown = .01f;
    private float timeCount=0;
   // private int cont = 0;
    private HealthScript healthscript;
    	
    
    void OnCollisionStay2D(Collision2D otherCollider) {
       
        //Caso 1, el jugador choca con un objeto solido.
        
        if (otherCollider.gameObject.tag == "Player" && collisionCausesDamageOnPlayer) {
            if (CanDamage())
            {
                healthscript = otherCollider.collider.GetComponent<HealthScript>();
                if (healthscript != null) healthscript.Damage(damagePerTic);                
            }
        }

        //Caso 2, un enemigo Choca con un objeto solido.
        if (otherCollider.gameObject.tag == "Enemy" && collisionCausesDamagesOnEnemy) {
            if (CanDamage())
            {
                healthscript = otherCollider.collider.GetComponent<HealthScript>();
                healthscript.Damage(damagePerTic);
            }
        }
        
    }

    public bool CanDamage()
    {
        if (timeCount >= ticCooldown) return true;
        else return false;
    }

    void Update() {
        if (timeCount > ticCooldown) timeCount = 0;
        timeCount += Time.deltaTime;
    }
        
}
