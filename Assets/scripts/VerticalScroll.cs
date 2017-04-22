using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class VerticalScroll : MonoBehaviour {

    // <summary>
    /// Velocidad del Scrolling
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Direccion de movimiento
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Se debe mover la camara?
    /// </summary>
    public bool isLinkedToCamera = false;

    /// <summary>
    ///Es el background infinito?
    /// </summary>
    public bool isLooping = false;

    /// <summary>
    /// Lista de hijos con un renderer
    /// </summary>
    private List<SpriteRenderer> backgroundPart;

    //Recuperamos todos los hijos
    void Start()
    {
        // Esta parte es solo para backgrounds infinitos
        if (isLooping)
        {
            //Recuperamos todos los spriteRenderers de los backgrounds
            backgroundPart = new List<SpriteRenderer>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                SpriteRenderer r = child.GetComponent<SpriteRenderer>();

                // Add only the visible children
                if (r != null)
                {
                    backgroundPart.Add(r);
                }
            }

            // Ordenar por posicion
            // Note: Recuperamos los fondos de izquierda a derecha
            backgroundPart = backgroundPart.OrderBy(
                t => t.transform.position.y
            ).ToList();

        }
    }

    void Update()
    {
        // Movement
        Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // Move the camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // Loop
        if (isLooping)
        {
            //Recuperamos el primer fondo, basandonos en la coordenada x(de izquierda a derecha)		
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                // Verificamos si el fondo esta a la izquierda de la pantalla
                if (firstChild.transform.position.y < Camera.main.transform.position.y)
                {
                    //Verificamos si el fondo esta completamente fuera de camara
                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        // Obtenemos la posicion del ultimo fondo
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        //Posicionamos el fondo actual a la derecha del ultimo fondo
                        firstChild.transform.position = new Vector3(lastPosition.x , firstChild.transform.position.y, firstChild.transform.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}
