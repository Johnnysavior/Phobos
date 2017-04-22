using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeTextScript : MonoBehaviour {

    private float hp, maxHp;
    private HealthScript health;
    private Text text;

	void Start () {
        text = GetComponent<Text>();
        health= GameObject.FindWithTag("Player").GetComponent<HealthScript>();
    }
	
	// Update is called once per frame
	void Update () {
        hp = health.hp;
        maxHp = health.maxHP;
        text.text = hp + " / " + maxHp;
	}
}
