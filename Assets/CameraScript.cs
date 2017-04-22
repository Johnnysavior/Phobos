using UnityEngine;

//script para mover la camara de forma vertical y seguir al jugador hasta cierto punto.

public class CameraScript : MonoBehaviour {


    public float maxY=10; //lo maximo que se le permite a la camara moverse.
    public float verticalLimit = 1.5f; //distancia vertical minima que se debe mover el jugador para mover la camara.
  

    private GameObject player;//el jugador
    private Vector3 offset;//diferencia entre la posicion del jugador y la camara.
    private MoveScript moveScript;
    private Rigidbody2D playerBody;
        

    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        playerBody = player.GetComponent<Rigidbody2D>();
        offset = transform.position - player.transform.position;
        moveScript = GetComponent<MoveScript>();
      
    }

    void Update() {

        offset = transform.position - player.transform.position;//recalculamos la posicion relativa de la camara respecto al jugador.

       if (offset.y > verticalLimit && playerBody.velocity.y < 0)
        {//si la distancia de la nave en ele eje Y es mas de x unidades hacia abajo
            Debug.Log("Mover camara abajo.");
            moveScript.speed.y = -8f;
        }

        if (offset.y < -verticalLimit && playerBody.velocity.y > 0)
        {//si la distancia de la nave en ele eje Y es mas de x unidades hacia arriba...
            Debug.Log("Mover camara arriba.");
            moveScript.speed.y = 8f;
        }

        //si el jugador no se mueve, la camara tampoco.
        if (playerBody.velocity.y == 0)
        {
            Debug.Log("quieto.");
            moveScript.speed.y = 0;
        }
        //si la camara llego al limite vertical superior, la camara se detiene.
        if (transform.position.y > maxY ) {
            moveScript.speed.y = 0;
            transform.position = new Vector3(transform.position.x,maxY,transform.position.z);
        }
        //lo mismo con el limite inferior.
        if (transform.position.y < -maxY)
        {
            moveScript.speed.y = 0;
            transform.position = new Vector3(transform.position.x, -maxY, transform.position.z);
        }

    }    
    	
}
