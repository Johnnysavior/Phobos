using UnityEngine;

/// <summary>
/// COmportamiento Generico para enemigos
/// </summary>
public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn; //Aparece en pantalla?
    private MoveScript moveScript;// Para llamar al script de movimiento
    private WeaponScript[] weapons;// Array de armas
    private Collider2D coliderComponent;// Hitbox
    private SpriteRenderer rendererComponent;// Componente que Dibuja en pantalla
    private MonoBehaviour[] otherComponents;
    
    
    void Awake()
    {
        try
        {
            weapons = GetComponentsInChildren<WeaponScript>();

            moveScript = GetComponent<MoveScript>();

            coliderComponent = GetComponent<Collider2D>();

            rendererComponent = GetComponent<SpriteRenderer>();

            otherComponents = GetComponentsInChildren<MonoBehaviour>();
        }
        catch (System.Exception)
        {
            Debug.Log("Uno o mas componentes no encontrado.");
          
        }
        // Recupera el listado de armas de los hijos
      

        
    }
    

    //Desactivamos todos(No esta en pantalla, no se hace nada)
    void Start()
    {
        hasSpawn = false;

        try
        {
            // Desactivamos todo
            // -- Hitbox
            coliderComponent.enabled = false;
            // -- Movimiento
            moveScript.enabled = false;
            // -- Disparos

            foreach (WeaponScript weapon in weapons)
            {
                weapon.enabled = false;
            }

            foreach (MonoBehaviour T in otherComponents)
            {
                T.enabled = false;
            }
        }
        catch (System.Exception)
        {

            Debug.Log("Uno o mas componentes no encontrado.");
        }
        

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
            //Disparamos
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                }
            }

            //El objeto volvio a salir de pantalla? lo borramos.
            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    
    //Funcion que activa este enemigo(entra en camara)
    private void Spawn()
    {
       
        hasSpawn = true;
        try
        {
            // Activamos todo
            // -- Hitbox
            coliderComponent.enabled = true;
            // -- Movimiento
            moveScript.enabled = true;
            // -- Disparos
            foreach (WeaponScript weapon in weapons)
            {
                weapon.enabled = true;
            }
        }
        catch (System.Exception)
        {

                Debug.Log("Uno o mas componentes no encontrado.");
        }
     
        
    }
   
}