using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

    public float shieldAmount = 100;
    public int slot = 3;
    public float maxShield;
    public bool isEnemy = false;    
    private CapsuleCollider2D hitbox;
    private SpriteRenderer sprite;
    private Color color;
    private float transparency=.2f;
    private WeaponManager parentWeaponManager;


    // Use this for initialization
    void Start () {
        hitbox = GetComponent<CapsuleCollider2D>();
        sprite= GetComponent<SpriteRenderer>();
        parentWeaponManager = GetComponentInParent<WeaponManager>();
        maxShield = shieldAmount;
        enabled = false;
        hitbox.enabled=false;
        sprite.enabled = false;

        color = new Color(1, 1, 1, transparency);
	}

    public void Damage(float damageCount)
    {
        shieldAmount -= damageCount;
        if (shieldAmount <= 0) Deactivate();
       
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Es esto un disparo?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            // El fuego amigo no causa dano
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                parentWeaponManager.ammo -= 1;
                transparency += .2f;             

                // Destruimos el disparo una vez que choca con algo
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                //Sonido de bloqueo con escudo encendido.
                SoundEffectsHelper.Instance.MakeShieldBlockSound();
            }
        }
    }

    void Update() {
        sprite.color = color;
        color = new Color(1, 1, 1, transparency);
        transparency -= .1f;
        if (transparency <= .2f) transparency = .2f;
        if (shieldAmount <= 0) Deactivate();          

    }

    public void switchShield() {

        if (enabled) enabled = false;
        else enabled=true;

        if (hitbox.enabled) hitbox.enabled = false;
        else hitbox.enabled = true;

        if (sprite.enabled) sprite.enabled = false;
        else sprite.enabled = true;

        //Hacer el sonido de activacion de escudo, el escudo
        //SIempre estara puesto en el slot 3, por ende llamamos
        //a la funcion correspondiente.
        SoundEffectsHelper.Instance.MakePlayerShotSound3();

    }

    public void Activate() {
        hitbox.enabled = true;
        enabled = true;
        sprite.enabled = true;       
    }

    public void Deactivate() {
        hitbox.enabled = false;
        enabled =false;
        sprite.enabled = false;
    }
}
