using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cambia el color del objeto que lo lleva a rojo a medida que este pierde vida.
/// </summary>
public class ColorOverLife : MonoBehaviour {

    private HealthScript health;

    private Color color;
    private SpriteRenderer sprite;
    private float currentHealth;
    private float maxHealth;
    private float lifeRating;
    
	void Start () {
        health = GetComponent<HealthScript>();
        sprite = GetComponent<SpriteRenderer>();
        maxHealth = health.maxHP;
    }

    // Update is called once per frame
    void Update () {
                    
        currentHealth = health.hp;
        lifeRating = currentHealth / maxHealth;     
        color = new Color(1,lifeRating,lifeRating,1);
        sprite.color = color;

    }
}
