using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBarScript : MonoBehaviour {
       
    private float fillAmount;
    private float maxHP;
    private float currentHP;
    private HealthScript health;
    public Image ActualHealth;

	// Use this for initialization
	void Start () {
        health = GameObject.FindWithTag("Player").GetComponent<HealthScript>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}

    private void HandleBar() {
        maxHP = health.maxHP;
        currentHP = health.hp;
        fillAmount = currentHP / maxHP;
        ActualHealth.fillAmount = fillAmount;
    }
}