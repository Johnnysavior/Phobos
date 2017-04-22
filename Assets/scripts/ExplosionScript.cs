using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para manejar el daño que hace una explosion. El daño se hace por tics a traves del tiempo.
public class ExplosionScript : MonoBehaviour {

    
    public float damagePerTic = 1;
    public float ticCooldown = .01f;
    public float maxlifeTime = 1;
    private float actualLifeTime = 0;
    private float timeCount=0;
    public bool isEnenmyExplosion=false;
    private HealthScript healthscript;

    void Start() {
        
        SoundEffectsHelper.Instance.MakeNukeSound();
    }

    void OnTriggerStay2D(Collider2D otherCollider)
    {
       
        //Si la explosion alcanza un enemigo lo dañamos...
        if (otherCollider.gameObject.tag == "Enemy" && isEnenmyExplosion==false)
        {           
            if (CanDamage()) {
                healthscript = otherCollider.GetComponent<HealthScript>();
                healthscript.Damage(damagePerTic);
            }     
        }
        //Si es enemiga dañamos al jugador
        if (otherCollider.gameObject.tag == "Player" && isEnenmyExplosion == true)
        {
            if (CanDamage())
            {
                healthscript = otherCollider.GetComponent<HealthScript>();
                healthscript.Damage(damagePerTic);
            }
        }

        if (otherCollider.GetComponent<ShotScript>() != null && isEnenmyExplosion == false) {
            Destroy(otherCollider.gameObject);
        }

    }

    public bool CanDamage() {
        if (timeCount >= ticCooldown) return true;
        else return false;
    }

    void Update() {
        if (timeCount > ticCooldown) timeCount = 0;
        timeCount += Time.deltaTime;

        actualLifeTime += Time.deltaTime;
        if (actualLifeTime >= maxlifeTime) GameObject.Destroy(gameObject);
    }
	
}
