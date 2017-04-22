using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickUpOnDeath : MonoBehaviour {

    public Transform pickUpPrefab;

    public void SpawnPickUp() {

        var pickUpTransform = Instantiate(pickUpPrefab) as Transform;
        pickUpTransform.position = transform.position;
    }
}
