using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Hace que la opacidad de un sprite alterne entre 1 y 0 en cada frame.
public class FlickerScript : MonoBehaviour {


    private SpriteRenderer sprite;
    private Color newColor;
    private int opacity=0;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        newColor = sprite.color;
        newColor.a = opacity;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        sprite.color = newColor;
        if (opacity < 1) opacity = 1;
        else opacity = 0;
        newColor = sprite.color;
        newColor.a = opacity;	
        */
	}
}
