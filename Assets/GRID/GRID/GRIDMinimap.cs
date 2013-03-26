using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRIDMinimap : MonoBehaviour {
	
	Terrain _terrain;
	GRIDTerrain gridTerrain;
	byte[,] _gridArray;
	
	//Minimap texture
	Texture2D minimapTexture;
	
	private bool init = false;
	
	public GUITexture guiTexture;
	public Texture2D tex;
	
	public GameObject playerGO;
	int halfMinimapSize = 16;
	
	// Use this for initialization
	void Start () {
		minimapTexture = new Texture2D(halfMinimapSize * 2, halfMinimapSize * 2);
			
		
		gridTerrain = Terrain.activeTerrain.GetComponent("GRIDTerrain") as GRIDTerrain;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 position = playerGO.transform.position;
			
			
			for(int i = ((int)position.x - halfMinimapSize); i < ((int)position.x + halfMinimapSize); i++)
			{
				for(int j = ((int)position.z - halfMinimapSize); j < ((int)position.z + halfMinimapSize); j++)
				{
					if(gridTerrain.GetGRIDStatus(i, j) == 255)
					{
						minimapTexture.SetPixel(i, j, Color.green);
					}
					
					if(gridTerrain.GetGRIDStatus(i, j) == 0)
					{
						minimapTexture.SetPixel(i, j, Color.white);
					}
					
					if(gridTerrain.GetGRIDStatus(i, j) == 1)
					{
						minimapTexture.SetPixel(i, j, Color.black);	
					}
				}
			}
			minimapTexture.SetPixel((int)position.x, (int)position.z, Color.red);
			
			minimapTexture.Apply();
			guiTexture.texture = minimapTexture;
		}
		
		
	}
}
