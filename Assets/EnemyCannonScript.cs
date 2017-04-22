using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonScript : MonoBehaviour {


    public float attackTimeInterval = 2;
    public int shootsPerInterval = 10;
    public int shotSound=0;//sonido que hace al disparar, 0 para no hacer ningun sonido
    private int remainingShoots;
    private WeaponScript[] weapons;
    private float elapsedTime;
    public bool shootTowardsPlayer = false;
    private GameObject player;


    //maneja el sonido de los disparos, si se permite
    void shootSound(int sound) {
        switch (sound)
        {
            case 1:
                SoundEffectsHelper.Instance.MakePlayerShotSound2();
                break;

            default:
                break;
        }
    }
   
	// Use this for initialization
	void Start () {
        if (shootTowardsPlayer) {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.LookAt(player.transform.position);
        }
        weapons = GetComponentsInChildren<WeaponScript>();
        elapsedTime = 0;
        remainingShoots = shootsPerInterval;
    	}

    // Update is called once per frame
    void Update() {
        
        //si se llego al tiempo de ataque
        if (elapsedTime >= attackTimeInterval)
        {
           
            foreach (WeaponScript weapon in weapons)
            {
                //si quedan disparos disponibles
                if (weapon.CanAttack)
                {
                    weapon.Attack(true);
                    shootSound(shotSound);

                    remainingShoots -= 1;                  
                }
                if (remainingShoots <= 0)
                {
                    elapsedTime = 0;
                    remainingShoots = shootsPerInterval;
                }

            }
        }

        elapsedTime += Time.deltaTime;
    }
}
