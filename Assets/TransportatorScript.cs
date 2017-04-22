using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportatorScript : MonoBehaviour {

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D otherCollider) {
        if(otherCollider.GetComponent<PlayerScript>())
            EventManager.Instance.goToNextLevel();
    }

}
