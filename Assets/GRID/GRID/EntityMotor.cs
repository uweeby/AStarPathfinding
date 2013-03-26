using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityMotor : MonoBehaviour {
	
	public GameObject prefabParent;
	public GameObject prefabTarget;
	
	public GRIDTerrain terrain;
	
	private bool init = false;
	
	List<GRIDNode> nodeList;
	
	public bool newPath = false;
	
	//Speed
	public float moveSpeed;
	
	//Mouse button to move
	public int mouseButtonToMove;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!init)
		{
			Initialize();
		}
		
		if(Input.GetMouseButtonDown(mouseButtonToMove))
		{
			//Get mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			
			//Set Mouse Point to INT for GRID
			prefabTarget.transform.position = new Vector3(Mathf.RoundToInt(hit.point.x), 0, Mathf.RoundToInt(hit.point.z));
		
			//Set new path
			newPath = true;
			
			GRIDPath gridPathObject = new GRIDPath(prefabParent, terrain);
			nodeList = gridPathObject.FindNewPath(prefabParent.transform.position, prefabTarget.transform.position);

		}
		
		if(newPath)
		{
			PathMovement();
		}
	}
	
	private void Initialize()
	{
		init = true;
	}
	
	public void PathMovement()
	{
		Debug.Log("PathMovement() called");
		
		if(Vector3.Distance(prefabParent.transform.position, nodeList[0].GetNodePosition()) > 0)
		{
			//Rotate towards
			prefabParent.transform.LookAt(nodeList[0].GetNodePosition());
		
			//Move towards
			prefabParent.transform.position = Vector3.MoveTowards(prefabParent.transform.position, nodeList[0].GetNodePosition(), moveSpeed * Time.deltaTime);
		}
		
		else
		{
			if(nodeList.Count > 1)
			{
				nodeList.RemoveAt(0);
			}
			
			else
			{
				newPath = false;
			}
		}
	}
}
