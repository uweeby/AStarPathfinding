using UnityEngine;
using System.Collections;
using System.IO;

public class GRIDPrefabTester : MonoBehaviour {
	
	public GameObject prefab;
	private GameObject prefabHolder;
	public Camera MainCamera;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		//Instantiate Controls
		GUI.Box(new Rect(10,10,100,300), "Game Object");

		if(GUI.Button(new Rect(20,40,80,20), "Instantiate")) {
			prefabHolder = GameObject.Instantiate(prefab) as GameObject;
			prefabHolder.transform.parent = this.transform;
		}

		if(GUI.Button(new Rect(20,70,80,20), "Destroy")) {
			Destroy(prefabHolder);
		}

		//Plus 90
		if(GUI.Button(new Rect(20,130,35,20), "+90")) {
			RotatePrefab(prefabHolder, 90);
		}

		//Minus 90
		if(GUI.Button(new Rect(70,130,35,20), "-90")) {
			RotatePrefab(prefabHolder, -90);
		}
		
		//Plus 180
		if(GUI.Button(new Rect(20,150,80,20), "+180")) {
			RotatePrefab(prefabHolder, 180);
		}

		//Camera Low
		if(GUI.Button(new Rect(20,200,80,20), "Camera Low")) {
			MainCamera.transform.position = new Vector3(0, 10, -5);
		}
		
		//Camera High
		if(GUI.Button(new Rect(20,230,80,20), "Camera High")) {
			MainCamera.transform.position = new Vector3(0, 25, -12);
		}
	}
	
	void RotatePrefab(GameObject _object, int _rotation)
	{
		_rotation += (int)_object.transform.eulerAngles.y;
		_object.transform.eulerAngles = new Vector3(0, _rotation, 0);
	}
}
