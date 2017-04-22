using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActivation : MonoBehaviour {

    public Transform NukePrefab;
    public float timeToExplode=1;
    private float  timeElapsed;
	void Start () {
        timeElapsed = 0;
	}       

	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToExplode)
        {
            var nukeTransform = Instantiate(NukePrefab) as Transform;
            nukeTransform.position = transform.position;
            Destroy(gameObject);
        }
        
	}
}
