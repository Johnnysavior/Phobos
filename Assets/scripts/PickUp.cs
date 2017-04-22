using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public int ammotype = 0;//Tipo de municion de este pick up
    public int quantity = 1000;//Cantidad restaurada de recurso.   
    private PowerUpMovement movement;

	// Cuando se instancia un power Up, se comenzara a mover en una direccion al azar.
	void Start () {

        movement = GetComponent<PowerUpMovement>();
        movement.direction.x = Random.Range(-1f, 1f);
        movement.direction.y = Random.Range(-1f, 1f);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //es esto el jugador?
        if (otherCollider.tag == "Player")  {
            switch (ammotype){
                case 0:
                    HealthScript health = otherCollider.GetComponent<HealthScript>();
                    health.hp += quantity;
                    break;

                default:
                    WeaponManager[] weapons = otherCollider.GetComponentsInChildren<WeaponManager>();
                    foreach (WeaponManager weapon in weapons)
                    {
                        if (weapon.WeaponSlot == ammotype){weapon.ammo += quantity;}
                        if (weapon.WeaponSlot == 2) {
                            weapon.WeaponLevel += 1;
                        }

                    }
                    break;
            }
            Destroy(gameObject);
        }
       
    }
}
