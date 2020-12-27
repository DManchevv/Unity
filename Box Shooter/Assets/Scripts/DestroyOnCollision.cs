using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {
    // Use this for initialization
    public GameObject explosionPrefab;
	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Environment")
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(this.gameObject, 0);
        }
	} 
}
