using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenCheckPoints : MonoBehaviour {


    private GameObject[] points;    
    public float speed;
    private int length;
    public int currentPoint;
    private Transform next;

	// Use this for initialization
	void Start () {
        points = GameObject.FindGameObjectsWithTag("point");
        length = points.Length;
        currentPoint = 0;
        next = points[currentPoint].transform;

    }

    // Update is called once per frame
    void Update() {      

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,next.position,step);
        

        if (transform.position.x==next.position.x &&transform.position.y==next.position.y ) {
           
            currentPoint += 1;
            if (currentPoint == length) currentPoint = 0;
            next = points[currentPoint].transform;
        }        

	}
}
