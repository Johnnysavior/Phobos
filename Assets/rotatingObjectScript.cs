using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingObjectScript : MonoBehaviour {

    public float rotationVelocity=1;
	
	void Update () {

        transform.Rotate(Vector3.forward * rotationVelocity*Time.deltaTime);



    }
}
