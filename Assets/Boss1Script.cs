using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour {

    private EnemyCannonScript mainCannon; //referencia para el arma central del Boss.
    private GameObject engine1;//referencia para las turbinas del boss
    private GameObject engine2;

    public Transform explosionPrefab;

    private MoveBetweenCheckPoints movement;
   

    private GameObject[] upperParticles;
    private GameObject[] bottomParticles;

    void OnDestroy() {
        var explosionTransform = Instantiate(explosionPrefab) as Transform;
        explosionTransform.position = transform.position;

        var gameOver = FindObjectOfType<StageEndScript>();
        if (gameOver != null) gameOver.ShowElements();

    }
       
    void Start () {

        //recuperar el script del cañon y deshabilitarlo inicialmente.
        mainCannon = GameObject.Find("weapon(core)").GetComponent<EnemyCannonScript>();
        mainCannon.enabled = false;

        upperParticles = GameObject.FindGameObjectsWithTag("topDamage");
        bottomParticles = GameObject.FindGameObjectsWithTag("bottomDamage");

        foreach (GameObject particle in upperParticles) {
            particle.GetComponent<ParticleSystem>().Pause();
        }

        foreach (GameObject particle in bottomParticles)
        {
            particle.GetComponent<ParticleSystem>().Pause();
        }

        engine1 = GameObject.Find("Engine 1");
        engine2 = GameObject.Find("Engine 2");

        movement = GetComponent<MoveBetweenCheckPoints>();
    }
	
	
	void Update () {
        // Si las 2 turbinas son destruidas, la el Boss activa el cañon principal.
        if (engine1 == null && engine2 == null) {
            mainCannon.enabled = true;
            movement.speed = 1.5f;
        }

        if (engine1 == null) {
            foreach (GameObject particle in upperParticles)
            {
                particle.GetComponent<ParticleSystem>().Play();
             
                
            }
        }

        if (engine2 == null)
        {
            foreach (GameObject particle in bottomParticles)
            {
                particle.GetComponent<ParticleSystem>().Play();
            }
        }

    }
}
