using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	public float distance = 10.0f;
	public float rotateSensitivity = 5.0f;
	public float rotationSmoothTime = 0.1f;

	private Vector3 _offset = new Vector3(0, 10, -10);
	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private Vector2 pitchMinMax = new Vector2(1, 60);
	private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;

	// Use this for initialization
	void Start () {
		transform.position = player.transform.position + _offset;
		// transform.LookAt(player.transform);
	}
	
	// Update is called once per frame
	void Update () {
		yaw += Input.GetAxis("Mouse X") * rotateSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * rotateSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
	}

	void LateUpdate()
	{
		//TODO:Camera could be behind of some other object that cannot see player(e.g. after the player is teleported)
		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw, 0), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.eulerAngles = currentRotation;
		transform.position = player.transform.position - transform.forward * distance;
		// transform.LookAt(player.transform);
	}

}
