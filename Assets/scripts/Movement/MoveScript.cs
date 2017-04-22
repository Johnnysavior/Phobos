using UnityEngine;

/// <summary>
/// Mueve el objeto al cual este enlazado.
/// </summary>
public class MoveScript : MonoBehaviour
{
	
	/// <summary>
	/// Velocidad del objeto
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);

	/// <summary>
	/// Direccion del movimiento
	/// </summary>
	public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
	private Rigidbody2D rigidbodyComponent;

	void Update()
	{        
		/// Movimiento
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
	}

	void FixedUpdate()
	{
		//ecuperacion del rigidbody2D
		if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

		// aplicamos movimiento en el rigidbody
		rigidbodyComponent.velocity = movement;
	}
}