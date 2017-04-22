using UnityEngine;

/// <summary>
/// Provoca un disparo de arma
/// </summary>
public class WeaponScript : MonoBehaviour
{
	/// <summary>
	/// prefab de Proyectil para disparar
	/// </summary>
	public Transform shotPrefab;
    public MoveScript[] moveScripts;
    public float damage;
    private WeaponManager parentWeaponManager;  


    /// <summary>
    /// retraso entre cada disparo(cooldown)
    /// </summary>
    /// 
    public int level = 0;
    public int slot = 1;    
   	public float shootingRate = 0.25f;
	private float shootCooldown;
    public float ammo = 1000;
    public Vector2 speed = new Vector2(10,10);
    
	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
      
        foreach (MoveScript moveScript in moveScripts) {
            moveScript.speed = speed;
        }

        if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	///Estas funciones se invocan desde otros objetos

	/// <summary>
	/// Creamos otros proyectil si es posible
	/// </summary>
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
            //hace el sonido correspondiente al disparar                    

            parentWeaponManager = GetComponentInParent<WeaponManager>();
            
			shootCooldown = shootingRate;
            ammo -= 1;

            parentWeaponManager.ammo -= 1;
           // moveScript.speed = speed;

			// Crea un nuevo disparo
			var shotTransform = Instantiate(shotPrefab) as Transform;

			// Asignamos su posicion.
			shotTransform.position = transform.position;

			// Si el arma es disparada por un enemigo, entonces el disparo es enemigo tambien...
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            shot.damage = damage;
                                 
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			// Nos aseguramos que el disparo se mueva hacia adelante del objeto que dispara
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
                move.speed = speed;
				move.direction = this.transform.right; // towards in 2D space is the right of the sprite
			}
		}
	}

	/// <summary>
	/// Comprobamos si podemos disparar otra vez
	/// </summary>
	public bool CanAttack
	{

		get
		{
            return shootCooldown <= 0f && ammo > 0;
		}
	}
      
   
}