using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer sprite;
	private int option;// opcion del cursor
    private float level=0f;
    private Color newColor;
	

	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        newColor = new Color(level,level,level,1f);
	
    }
	
	// Update is called once per frame
	void Update () {
        level = level + 0.01f;
        newColor= new Color(level, level, level, 1f);
        sprite.color = newColor;                       
    }	

}
