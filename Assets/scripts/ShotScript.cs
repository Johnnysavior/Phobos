using UnityEngine;

/// <summary>
/// Comportamiento de un proyectil
/// </summary>
public class ShotScript : MonoBehaviour {

	/// <summary>
	/// Dano infligido
	/// </summary>
	public float damage=1;
    public bool isDestructible = false;
    public bool ignoreSolidObjects = false;
    private SpriteRenderer rendererComponent;// Componente que Dibuja en pantalla
    private ShotScript otherShot;
    private SolidObjectScript solidObject;

	/// <summary>
	/// El proyectil es enemigo?
	/// </summary>
	public bool isEnemyShot = false;

	void Start(){

        rendererComponent = GetComponent<SpriteRenderer>();

		//Despues de una limitado tiempo, destruimos este objeto para evitar una sobrecarga de memoria.
		Destroy(gameObject, 8); // 8 segundos
	}

    void OnTriggerEnter2D(Collider2D otherCollider) {
        
        //esto es otro disparo?
        otherShot = otherCollider.GetComponent<ShotScript>();
        solidObject = otherCollider.GetComponent<SolidObjectScript>();

        //Si lo es...
        if (otherShot != null) {         
            //Si esta bala es destructible y es del bando contrario...
            if (isDestructible ) Destroy(gameObject);// se destruye
        }

        //esto es un objeto solido
        if (solidObject != null && ignoreSolidObjects==false) {
            Destroy(gameObject);
        }
        


    }

    void Update() {
        if (rendererComponent.IsVisibleFrom(Camera.main) == false)
        {
            Destroy(gameObject);
        }
    }
    
}
