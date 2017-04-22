using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour {


    public AudioClip SoundEffect;
    public float lifeTime = 5;
    public float playsPerSecond = 1;
    private float timeElapsed;
    private ParticleSystem particleEmiter;
    private bool played = false;


   IEnumerator PlaySound() {

        while (true) {
            AudioSource.PlayClipAtPoint(SoundEffect,transform.position);
            played = true;
            yield return new WaitForSeconds(1/playsPerSecond);
        }
                     
    }
    	
	
	// Update is called once per frame
	void Update () {
        if(played==false) StartCoroutine("PlaySound");

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= lifeTime) Destroy(gameObject);
	}
}
