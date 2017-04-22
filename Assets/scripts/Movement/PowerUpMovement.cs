using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour {


    /// <summary>
    /// Velocidad del objeto
    /// </summary>
    public Vector2 speed = new Vector2(5, 5);

    /// <summary>
    /// Direccion del movimiento
    /// </summary>
    public Vector2 direction = new Vector2(1, 1);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    
    void Update()
    {
        /// Movimiento
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);

        //calculamos coordenadas de los bordes de la pantalla
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
                     
        float PosX = transform.position.x;
        float PosY = transform.position.y;
           
        //si el objeto toca los bordes de la pantalla, cambiamos su direccion de movimiento segun el borde que toco.                    
        if (PosX < leftBorder) { direction.x=1; } 
        if (PosX > rightBorder) { direction.x = -1; }        

        if (PosY < -bottomBorder) { direction.y = 1; }
        if (PosY > -topBorder) { direction.y = -1; }
                
    }

    void FixedUpdate()
    {
        //ecuperacion del rigidbody2D
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // aplicamos movimiento en el rigidbody
        rigidbodyComponent.velocity = movement;
    }
}
