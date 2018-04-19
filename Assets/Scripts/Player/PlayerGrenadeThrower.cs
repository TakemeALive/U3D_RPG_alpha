using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenadeThrower : MonoBehaviour {
	
	public float force = 15f;
	public GameObject grenade;

	void Update () {
		if(Input.GetMouseButtonDown(1)){
			ThrowGrenade();
		}
	}

	void ThrowGrenade(){
		var obj = Instantiate(grenade, transform.position, transform.rotation);
		var rb = obj.GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
	}
}
