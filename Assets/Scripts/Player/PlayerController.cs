using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is not be used anymore.
public class PlayerController : MonoBehaviour {

	public Rigidbody rigidbody;
	public GameObject player;
	public float speed = 10.0f;
	
	public bool isTeleported = false;
	public string portalTag;
	public float portalDistance = 5.0f;

	void Start()
	{
		rigidbody = player.GetComponent<Rigidbody>();	
		DontDestroyOnLoad(player);
	}

	void FixedUpdate()
	{
		if(rigidbody.velocity.y <= 1.0f)
		{
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			// Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			// movement *= Camera.main.transform.forward;
			// TODO:Speed is not properly accelerated after change the camera direction
			Vector3 movement = Camera.main.transform.TransformDirection(moveHorizontal, 0.0f, moveVertical);
			rigidbody.AddForce(movement * speed);
		}
		if(isTeleported)
		{
			var portal = GameObject.FindGameObjectWithTag(portalTag);
			var distance = Vector3.Distance(player.transform.position, portal.transform.position);
			if(distance > portalDistance) isTeleported = false;
		}
	}
	private void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("PickupCube"))
		{
			other.gameObject.SetActive(false);
		}

		// Portal Teleportation
		// TODO:Set camera behind the portal after teleporting

		if(!isTeleported)
		{
			if(other.gameObject.CompareTag("Portal_A")){
			// MainScene->PortalA to OrangeScene->PortalOrange
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_Orange"), SceneManager.GetSceneByName("OrangeScene"));
				portalTag = "Portal_Orange";
			}else if(other.gameObject.CompareTag("Portal_B")){
			// MainScene->PortalB to BlueScene->PortalBlue
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_Blue"), SceneManager.GetSceneByName("BlueScene"));
				portalTag = "Portal_Blue";
			}else if(other.gameObject.CompareTag("Portal_Orange")){
			// OrangeScene->PortalOrange to MainScene->PortalA 
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_A"), SceneManager.GetSceneByName("MainScene"));
				portalTag = "Portal_A";
			}else if(other.gameObject.CompareTag("Portal_Blue")){
			// BlueScene->PortalBlue to MainScene->PortalB 
				Teleport(player, GameObject.FindGameObjectWithTag("Portal_B"), SceneManager.GetSceneByName("MainScene"));
				portalTag = "Portal_B";
			}
		}
		
	}

	private void Teleport(GameObject go, GameObject dest, Scene scene)
	{ // Teleport a GameObject(usually is player) to a destination
		if(scene != null){
			SceneManager.MoveGameObjectToScene(go, scene);
		}
		player.transform.position = dest.transform.position;

		// TODO:Might be better to detect the speed direction of the player with portal by Mathf.cos
		isTeleported = true;
	}
}
