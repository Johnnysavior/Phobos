using UnityEngine;

/// <summary>
/// Mueve en zigzag el objeto al cual esta enlazado
/// </summary>
public class ZigzagMove : MonoBehaviour
{
	
	/// <summary>
	/// Velocidad del objeto
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);

	/// <summary>
	/// Direccion del movimiento
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Valor inicial para definir el angulo de movimiento vertical
    /// </summary>
    public float frecuency = 0;
    public float magnitude = 1;


	private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;
    private float timeElapsed;

    void Start() {
        timeElapsed = 0;
    }

	void Update()
	{
        direction.y = Mathf.Sin(timeElapsed*frecuency)*magnitude;
		/// Movimiento
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
        timeElapsed += Time.deltaTime;  
	}

   	void FixedUpdate()
	{
		//ecuperacion del rigidbody2D
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		// aplicamos movimiento en el rigidbody
		rigidbodyComponent.velocity = movement;
	}
}