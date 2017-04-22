using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkerScript : MonoBehaviour {

    public float movementDuration = 1;
    private MoveScript moveScript;
    private Animator animator;
    private float timeElapsed;
    
   

	// Use this for initialization
	void Start () {       
        moveScript = GetComponent<MoveScript>();       
        timeElapsed = 0;       
	}
       

	// Update is called once per frame
	void Update () {

        if (animator == null) animator = GetComponent<Animator>();

        if (moveScript.direction.x > 0)
        {
            animator.SetBool("movingRight", true);
            animator.SetBool("movingLeft", false);
        }
        else {
            animator.SetBool("movingRight", false);
            animator.SetBool("movingLeft", true);
        }


        //tiempo de dar la vuelta
        if (timeElapsed >= movementDuration) {
            moveScript.direction.x *= -1;
            timeElapsed = 0;
        }
        timeElapsed += Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D otherCollider) {
        //El enemigo atacara al jugador al cambiar de direccion
        if (otherCollider.tag == "border") {
            moveScript.direction.x *= -1;     
        }
    }
}
