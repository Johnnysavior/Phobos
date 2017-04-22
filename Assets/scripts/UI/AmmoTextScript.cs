using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextScript : MonoBehaviour {

    public int slot=2;// slot
    private float ammo;
    private WeaponManager[] weaponMangagers;
    private WeaponManager actualWeaponMagager;
    private Text text;
    
    
    
	void Start () {
        
        text = GetComponent<Text>();//puntero al campo de texto
        weaponMangagers = GameObject.FindWithTag("Player").GetComponentsInChildren<WeaponManager>();//puntero a la informacion de las armas del jugador
        foreach (WeaponManager wm in weaponMangagers) {
            if (wm.WeaponSlot == slot) {
                actualWeaponMagager = wm;// se accede a los datos de arma especificada por el slot seleccionado.
            }
        }
    }
	
	void Update () {
        //Se recuperan los datos de la municion del jugador
        text.text = actualWeaponMagager.ammo.ToString();
	}
}
