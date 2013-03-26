using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The GIRD Terrain object is in every scene that Entities can move around.
//Instead of using colliders the GRID Terrain keeps an array for th GRID tiles.
//Entities will referance and update this array as they move around.
//The static objects in the scene should already be added to the array from the GRIDTool.

public class GRIDTerrain : MonoBehaviour {
	
	//Link to the terrain for this scene:
	public Terrain terrain;
	public Component pubComponnet;
	
	//Link to the GRID array
	public byte[,] gridArray;
	private int terrainWidth;
	private int terrainHeight;
	
	//The array should be loaded from the file containing the static objects
	
	
	
	void Awake()
	{
	}
	
	// Use this for initialization
	void Start ()
	{
		terrainWidth = Terrain.activeTerrain.terrainData.heightmapWidth;
    	terrainHeight = Terrain.activeTerrain.terrainData.heightmapHeight;
		gridArray = new byte[terrainWidth,terrainHeight];
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	//Returns true when an object occupies the provided position
	public byte GetGRIDStatus(int _xPos, int _zPos)
	{
		//Valid Position
		if((_xPos >= 0 && _xPos <= terrainWidth) && (_zPos >= 0 && _zPos <= terrainHeight))
		{
			return gridArray[_xPos, _zPos];	
		}
		
		//Invalid Position
		else
		{
			return 255;	
		}
	}
	
	//Returns true when an object occupies the provided position
	public byte GetGRIDStatus(Vector3 _position)
	{
		return gridArray[(int)_position.x, (int)_position.z];	
	}
	
	//Update the array with new position information. Returns true is request is sucessful.
	public bool SetGRIDStatus(int _xPos, int _zPos, byte _status)
	{
		//Check to see if the request is valid
		
		//If so update and return true
		gridArray[_xPos, _zPos] = _status;
		return true;
	}
	
	void FreeSpaceList()
	{
		int freespace = 0;
		for(int x = 0; x < terrainWidth; x++)
		{
			for(int z = 0; z < terrainHeight; z++) 
			{
				if(GetGRIDStatus(x, z) == 0)
				{
					freespace++;
				}
			}
		}
		Debug.Log("Free Space: " + freespace);
	}
}

