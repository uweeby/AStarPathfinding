using UnityEngine;
using System.Collections;

public class GRIDPrefab : MonoBehaviour {
	
	private GRIDTerrain gridTerrain;
	
	private bool init = false;
	
	// Use this for initialization
	void Start ()
	{
		
		//Assign GRIDTerrain Object:
		gridTerrain = Terrain.activeTerrain.GetComponent("GRIDTerrain") as GRIDTerrain;
		
		foreach (Transform child in transform) {
			//Disable their mesh renderers
            child.renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!init)
		{
			Initialize();	
		}
	}
	
	void Initialize()
	{
		//Find all the children cubes
		foreach (Transform child in transform) {
			//Take the Transform and find important Values
			ChildInfos(child.gameObject);
        }
		init = true;
	}
	
	void ChildInfos(GameObject _child)
	{
		//Prefab Type
		byte prefabType = 1;
		
		//Get the world position
		int _xPos = (int)_child.transform.parent.position.x;
		int _zPos = (int)_child.transform.parent.position.z;
		
		//Use the scale to determine how many total tiles are colliders
		int _xScale = (int)_child.transform.localScale.x;
		int _zScale = (int)_child.transform.localScale.z;
		
		//Find the world rotation // transform.localRotation.eulerAngles.y;
		int _yRotation = (int)gameObject.transform.parent.rotation.eulerAngles.y;

		switch (_yRotation)
		{
		case 0:
			//Prefab has 0 degree Y rotation
			for(int xAxis = 0; xAxis < _xScale; xAxis++)
			{
				for(int zAxis = 0; zAxis < _zScale; zAxis++) 
				{
					int xRotatedSum = _xPos + xAxis;
					int zRotatedSum = _zPos + zAxis;
					gridTerrain.SetGRIDStatus(xRotatedSum, zRotatedSum, prefabType);
				}
			}
			break;
			
		case 90:
			//Prefab has 90 degree Y rotation
			
			break;
			
		case 180:
			//Prefab has 180 degree Y rotation
			
			//Invert the numbers.
			_xScale = -_xScale;
			_zScale = -_zScale;
			Debug.Log("Xsc: " + _xScale + ", Zsc: " + _zScale);
			
			for(int xAxis = _xScale; xAxis < 0; xAxis++)
			{
				for(int zAxis = _zScale; zAxis < 0; zAxis++) 
				{
					int xRotatedSum = _xPos + xAxis + 1;
					int zRotatedSum = _zPos + zAxis + 1;
					gridTerrain.SetGRIDStatus(xRotatedSum, zRotatedSum, prefabType);
				}
			}
			break;
			
		case 270: 
			//Prefab has 270 degree Y rotation
			
			break;
			
		default:
			//Y Rotation does not need GRID rules.
			break;
			
			
		}
	}
}
