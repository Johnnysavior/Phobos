using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// activa y desactiva las armas de los hijos del objeto en que esta este
/// script.
/// </summary>
public class WeaponManager : MonoBehaviour {


    public int WeaponLevel = 0;
    public int WeaponSlot = 1;
    public float damage = 1;
    public float ammo = 1000;
    public bool ammoIsInfinite = false;

    //Armas stackea
    public bool levelingUpstackWeapons = false;
    public Vector2 speed = new Vector2(10,10);
    public float shootingRate = .25f;

    private WeaponScript[] weapons;// Array de armas
    private ShieldScript shield;
    private WeaponManager[] wms;
                                   
   
   void Start () {
        // Recuperacion de las armas hijas
        weapons = GetComponentsInChildren<WeaponScript>();
        wms = GetComponentsInChildren<WeaponManager>();
        //En Caso de que esta arma sea un escudo, recuperamos el Script.
        shield = GetComponent<ShieldScript>();

        //Al instanciar el arma, desactivamos todas por defecto.
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }
    }
		
    //en cada frame, se comprueba el nivel del arma, el cual podra 
    //incrementarse con power ups u otros medios.
	void Update () {

        foreach (WeaponManager wm in wms) {
            if (wm.WeaponSlot == 2 && wm.ammo <= 0) {
                wm.WeaponLevel = 0;
            }
        }


        foreach (WeaponScript weapon in weapons)
        {
            if (weapon.level <= WeaponLevel) weapon.enabled = true;
            else weapon.enabled = false; 

            if(levelingUpstackWeapons==false && weapon.level<WeaponLevel) weapon.enabled = false;
                       

            weapon.slot = WeaponSlot;
            weapon.speed = speed;
            weapon.shootingRate = shootingRate;
            weapon.damage = damage;
            if (ammoIsInfinite) weapon.ammo = 9999;
                else weapon.ammo = ammo;
        }
        //Manegamos la energia del escudo directamente desde el Weapon Manager.
        if (shield != null) shield.shieldAmount = ammo;

    }
}
