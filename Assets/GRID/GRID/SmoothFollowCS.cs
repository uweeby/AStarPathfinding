using UnityEngine;
using System.Collections;

public class SmoothFollowCS : MonoBehaviour {
	
	//Camera Object
	public Transform target;
	
	//Camera positions
	public float distance = 10.0f;
	public float height = 5.0f;
	public float xRot = 0.0f;
	public float yRot = 0.0f;
	public float zRot = 0.0f;
	
	//Smoothing
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float speed = 1f;
	
	void Update()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			yRot = yRot + speed;	
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			yRot = yRot - speed;	
		}
	}
	
	// Use this for initialization
	void LateUpdate()
	{
		if (!target)
		{
			return;
		}
		
		// Damp the height
		float currentHeight = transform.position.y;
		float wantedHeight = target.position.y + height;
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (xRot, yRot, zRot);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
	
		// Always look at the target
		transform.LookAt (target);
	}
}
