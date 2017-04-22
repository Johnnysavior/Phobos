using System.Collections;
using UnityEngine;

/// <summary>
/// Maneja Puntos de Salud y dano
/// </summary>
public class HealthScript : MonoBehaviour
{
	/// <summary>
	/// Puntos de salud totales(HP)
	/// </summary>
	public float hp = 100;
    public float maxHP;
    public int score = 100;//Si esto es un enemigo, dara puntaje al morir.
    private SpawnPickUpOnDeath pickUp;
    private ShieldScript shield;
    private bool damaged;   
    /// Este script esta enlazado al jugador o a un enemigo?
 
    public bool isEnemy = true;

    void Awake() {     
        maxHP = hp;
    }

    //Hace Parpadear el el objeto al recibid daño
    IEnumerator ToggleRenderer()
    {
        
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
             
        //Repeat as much as needed
    }



    public void Damage(float damageCount)
	{
		hp -= damageCount;        
        //Si hay un pickup disponible enlazado a este script, este aparecera al morir este objeto.
        if (pickUp== null)pickUp=GetComponent<SpawnPickUpOnDeath>();
        
        if (hp <= 0)
		{
            //Muerte!
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();

            GameManagerScript.Instance.score += score;       
           
            Destroy(gameObject);           
           
            if (isEnemy==true) {

                GameManagerScript.Instance.enemiesDefeated += 1;              

            }//añade +1 al conteo de enemigos muertos.
           
            if(pickUp!=null) pickUp.SpawnPickUp();
        }
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
                //recuperamos un escudo si es que el objeto lo tiene, si esta activado, no se recibe daño.
                try
                {
                    shield = GetComponentInChildren<ShieldScript>();
                    if (shield.enabled == false)
                    {
                        Damage(shot.damage);
                    }
                }
                //no hay escudo
                catch (System.Exception)
                {                                
                    Damage(shot.damage);
                }                                      
				
				// Destruimos el disparo una vez que choca con algo
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                StartCoroutine("ToggleRenderer");
                SoundEffectsHelper.Instance.MakehurtSound();
                

            }
		}
	}

    void Update() {
        if (hp > maxHP) hp = maxHP;
    }

}