using System.Collections;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

	/// <summary>
	/// Velocidad de la nave
	/// </summary>
	public Vector2 speed = new Vector2(50, 50);    
    private WeaponManager[] weaponManagers;

	// Aca se alamecena el movimiento y componente rigidbody2D
	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;
    private WeaponScript[] weapons;// Array de armas
    private ShieldScript[] shields; //array para escudos
    private Animator animator;
    public static PlayerScript Instance;
    private ParticleSystem engineNormal;
    private ParticleSystem engineRight;
    private ParticleSystem engineLeft;
    private bool paused;




    void Awake() {       
        engineNormal = transform.Find("Turbinas(normal)").GetComponentInChildren<ParticleSystem>();
        engineRight = transform.Find("Turbinas(right)").GetComponentInChildren<ParticleSystem>();
        engineLeft = transform.Find("Turbinas(left)").GetComponentInChildren<ParticleSystem>();

        bool paused = false;

        engineRight.Stop();
        engineLeft.Stop();
        if (Instance) DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        /*
        SerializedProperty it = so.GetIterator();
        while (it.Next(true))
            Debug.Log(it.propertyPath);
            */

    }

    //maneja los sonidos de las armas
    void shootSound(int slot)
    {
        switch (slot)
        {
            case 1:
                SoundEffectsHelper.Instance.MakePlayerShotSound1();
                break;
            case 2:
                SoundEffectsHelper.Instance.MakePlayerShotSound2();
                break;
            case 3:
                SoundEffectsHelper.Instance.MakePlayerShotSound3();
                break;
            case 4:
                SoundEffectsHelper.Instance.MakePlayerShotSound4();
                break;
        }
    }

    void shootWeapons(bool shootInput, int slot) {
        if (shootInput)
        {
            weapons = GetComponentsInChildren<WeaponScript>();
            shields= GetComponentsInChildren<ShieldScript>();
            if (weapons != null)
            {
                //armas
                foreach (WeaponScript weapon in weapons)
                {
                    if (weapon != null && weapon.enabled && weapon.CanAttack && weapon.slot == slot)
                    {
                        weapon.Attack(false);                        
                        shootSound(slot);
                    }
                }
                //escudo
                foreach (ShieldScript shield in shields)
                {
                    if(shield.slot==slot)shield.switchShield();                   
                }

            }
        }
    }

    void OnDestroy()
    {
        // Game Over.
        GameManagerScript.Instance.score = 0;
        GameManagerScript.Instance.enemiesDefeated = 0;
        var gameOver = FindObjectOfType<GameOverScript>();
        if (gameOver != null) {          
            gameOver.ShowButtons();           
            Time.timeScale = 0;
        }
      
    }
    

    // Update is called once per frame
    void Update () {

		//disparos
		bool shoot1 = Input.GetButton("Fire1");
        bool shoot2 = Input.GetButton("Fire2");
        bool shoot3 = Input.GetButtonDown("Fire3");
        bool shoot4 = Input.GetButton("Fire4");
     


        shootWeapons(shoot1, 1);
        shootWeapons(shoot2, 2);
        shootWeapons(shoot3, 3);
        shootWeapons(shoot4, 4);

        // Recupera input de control/teclado
        float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

        if (animator == null) animator = GetComponent<Animator>();
        
       

        //moviendose abajo
        if (inputY < 0) {
            animator.SetBool("IsMovingUp", false);
            animator.SetBool("IsMovingDown",true);
            animator.SetBool("IsIdle",false);
        }
        
        //moviendose arriba
        if (inputY > 0){
            animator.SetBool("IsMovingUp", true);
            animator.SetBool("IsMovingDown", false);
            animator.SetBool("IsIdle", false);
        }
        
        //esta quieto
        if (inputY == 0){
            animator.SetBool("IsMovingUp", false);
            animator.SetBool("IsMovingDown", false);
            animator.SetBool("IsIdle", true);
            //particulas
            engineNormal.Play();
            engineRight.Stop();
            engineLeft.Stop();    
               
        }

        if (inputX < 0) {
            //particluas
            engineNormal.Stop();
            engineRight.Stop();
            engineLeft.Play();
        }

        if (inputX > 0)
        {
            //particulas
            engineNormal.Stop();
            engineRight.Play();
            engineLeft.Stop();

        }

        movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);
                   


        // Nos aseguramos de que la nave del jugador no sale de la camara.
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).x;


        var rightBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
          new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );

   

    }

	void FixedUpdate(){
		//Recupera el componente rigidBody2d y almacena el puntero a este.
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		//con el puntero recuperado, movemos el objeto a traves del componente.
		rigidbodyComponent.velocity = movement;
	}

	//Manejo de collisiones con otros objetos
	void OnCollisionEnter2D(Collision2D collision)
	{
		bool damagePlayer = false;

		// Collision con enemigos
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			// Muere el enemigo(chocamos)
			HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
			if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

			damagePlayer = true;
		}

		// Muere el Jugador(tambien)
		if (damagePlayer)
		{
			HealthScript playerHealth = this.GetComponent<HealthScript>();
			if (playerHealth != null) playerHealth.Damage(1);
		}
	}
}
