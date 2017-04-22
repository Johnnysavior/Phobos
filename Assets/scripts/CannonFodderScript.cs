using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFodderScript : MonoBehaviour {

    //Etiqueta del Objetivo 
    public string targetTag="Player";
    public bool deleteOffScreen = true;

    private bool hasSpawn; //Aparece en pantalla?
    private MonoBehaviour moveScript;// Para llamar al script de movimiento
    private Collider2D coliderComponent;// Hitbox
    private SpriteRenderer rendererComponent;// Componente que Dibuja en pantalla

    private WeaponScript weapon;
    private Vector2 targetPos; //Posicion del objetivo
    private Transform WeaponTransformComponent; // Componente Transform del arma hija.
    private Vector2 selfPos; //Posicion propia
    private MonoBehaviour[] otherComponents;
    private Rigidbody2D rigidbodyComponent;

    void Awake () {

        // Recupera el listado de armas de los hijos
    
        moveScript = GetComponent<MoveScript>();

        if (moveScript == null) moveScript = GetComponent<ZigzagMove>();

        coliderComponent = GetComponent<Collider2D>();

        rendererComponent = GetComponent<SpriteRenderer>();

        rigidbodyComponent = GetComponent<Rigidbody2D>();
                
        //Recuperar Script de Arma y Componente Transform para cambiar de direccion al disparar.
        weapon = GetComponentInChildren<WeaponScript>();
        WeaponTransformComponent = transform.GetChild(0);

    }

    //Desactivamos todos(No esta en pantalla, no se hace nada)
    void Start()
    {
        hasSpawn = false;
        // Desactivamos todo
        // -- Hitbox
        coliderComponent.enabled = false;
        // -- Movimiento
        moveScript.enabled = false;
        // -- Disparos

        weapon.enabled = false;

        rigidbodyComponent.gravityScale = 0;

    }

    void Update()
    {

        //En cada frame revisamos si el enemigo es visible por la camara.
        if (hasSpawn == false)
        {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {

            //Calcular la direccion en la que se encuentra acutalmente el objetivo.
            try
            {
                targetPos = GameObject.FindGameObjectWithTag(targetTag).transform.position;
            }
            catch (System.Exception)
            {
                
                if (weapon != null && weapon.CanAttack)
                {                    
                    WeaponTransformComponent.eulerAngles = new Vector3(0, 0, 180);
                    weapon.Attack(true);

                }

            }
           
            
            selfPos = GetComponent<Transform>().position;
            float angle = Mathf.Atan2(targetPos.y - selfPos.y, targetPos.x - selfPos.x) * Mathf.Rad2Deg;

            //Una vez se tiene el angulo relativo al objetivo, se rota el arma en torno al eje z.
            WeaponTransformComponent.eulerAngles = new Vector3(0, 0, angle);

            //Disparo automatico.
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(true);
            }

            //El objeto volvio a salir de pantalla? lo borramos.
            if (rendererComponent.IsVisibleFrom(Camera.main) == false && deleteOffScreen)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Spawn()
    {
        hasSpawn = true;

        // Activamos todo
        // -- Hitbox
        coliderComponent.enabled = true;
        // -- Movimiento
        moveScript.enabled = true;
        // -- Disparos        
        weapon.enabled = true;

        rigidbodyComponent.gravityScale = 0;

    }
}
