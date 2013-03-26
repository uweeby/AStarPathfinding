using UnityEngine;
using System.Collections.Generic;

public class GRIDPath{

	public GameObject prefabParent;
	
	public Vector3 startVector;
	public Vector3 currentVector;
	public Vector3 targetVector;
	
	public Vector3 moveTowards;
	
	//Terain object to check Tiles
	private GRIDTerrain gridTerrain;
	
	private int pathPrecision = 100;
	
	//Used to count index for Node Objects
	public int nodeID = 0;
	
	//Nodes that have not been checked yet
	List<GRIDNode> openList = new List<GRIDNode>();
	
	//Path to Target
	List<GRIDNode> closedList = new List<GRIDNode>();
	
	//Public Contructor
	public GRIDPath(GameObject _prefabParent, GRIDTerrain terrain)
	{
		prefabParent = _prefabParent;
		currentVector = prefabParent.transform.position;
		gridTerrain = terrain;
	}
	
	//Main method to find a path
	public List<GRIDNode> FindNewPath(Vector3 _startingVector, Vector3 _targetVector)
	{
		bool targetOnClosedList = false;
	
		//Set the starting and target Vectors
		startVector = _startingVector;
		targetVector = _targetVector;
		
		//Create and add the starting node
		GRIDNode startingNode = CreateNode(currentVector, 0);
		openList.Add(startingNode);
		
		while(!targetOnClosedList)
		{
			PathRecursion();
			
			//Target location has been found in path. Exit
			if(closedList[closedList.Count - 1].GetNodePosition() == targetVector)
			{
				//Path has been found. Return true
				closedList = PathCleanup();
				targetOnClosedList = true;
				break;
			}
			
			//Fail to find the target square, and the open list is empty. In this case, there is no path.
		}
		
		return closedList;
	}
	
	private void PathRecursion()
	{
	
		//a) Look for the lowest F cost square on the open list. We refer to this as the current square.
		int lowestFNode = FindLowFCost();
		
		currentVector = openList[lowestFNode].GetNodePosition();
		
		//b) Switch it to the closed list.
		closedList.Add(openList[lowestFNode]);

		openList.RemoveAt(lowestFNode);
				
		//c) For each of the 8 squares adjacent to this current square …
		FindChildren(closedList[closedList.Count - 1].GetNodeID());
	}
	
	//For each of the 8 squares adjacent to this current square
	private void FindChildren(int _parentID)
	{
		
		//Find 8 surriounding Node and Check for walkable
		for(int x = -1; x <= 1; x++)
		{
			for(int z = -1; z <= 1; z++)
			{
				//Reset to false for loop start;
				bool onOpenList = false;
				bool onClosedList = false;
				bool walkable = false;
				
				int currentX = (int) currentVector.x + x;
				int currentZ = (int) currentVector.z + z;
				
				if(!(x == 0 && z == 0)) { 
				

					//If it is not walkable or if it is on the closed list, ignore it. Otherwise do the following. 
					if(gridTerrain.GetGRIDStatus(currentX, currentZ) == 0)
					{
						//Check ClosedList for Node
						for(int i = closedList.Count; i --> 0;)
						{
							if(closedList[i].GetNodePosition() == new Vector3(currentX, 0, currentZ))
							{
								onClosedList = true;
								break;
							}
						}
						  
						//Check OpenList for Node
						if(!onClosedList)
						{
							for(int i = openList.Count; i --> 0;)
							{
								if(openList[i].GetNodePosition() == new Vector3(currentX, 0, currentZ))
								{
									onOpenList = true;
									break;
								}
							}
						}
						
						//If not found on either list add to OpenList
						if(!onOpenList && !onClosedList)
						{
							//Create new node
							GRIDNode childNode = CreateNode(new Vector3(currentX, 0, currentZ), _parentID);
							openList.Add(childNode);					
						}
						
						else if(onOpenList && !onClosedList)
						{
							//If it is on the open list already, check to see if this path to that square is better, using G cost as the measure.
							//A lower G cost means that this is a better path. If so, change the parent of the square to the current square, 
							//and recalculate the G and F scores of the square. If you are keeping your open list sorted by F score, you may need to resort the list to account for the change.
						}
					}
				}
			}
		}
	}
	
	//Creates a new node at the passed in position. Returns the node object
	private GRIDNode CreateNode(Vector3 _nodeVector, int _parentID)
	{				
		int gCost = DistanceAsInt(_nodeVector, startVector);
		int hCost = DistanceAsInt(_nodeVector, targetVector);
		int fCost = gCost + hCost;

		GRIDNode node = new GRIDNode(nodeID, fCost, gCost, hCost, _parentID, _nodeVector);
		
		//Increment Node Count
		nodeID++;
		
		return node;
	}
	
	private List<GRIDNode> PathCleanup()
	{
		List<GRIDNode> _cleanPath = new List<GRIDNode>();
		_cleanPath.Add(closedList[closedList.Count - 1]);
		
		//Take in the closed list and work backwards to clean up path following patentIDs
		for(int i = closedList.Count; i --> 0;)
		{
			if(closedList[i].GetNodeID() == _cleanPath[0].GetParentNode())
			{
			_cleanPath.Insert(0, closedList[i]);			
			}
		}
		
		return _cleanPath;
	}
	
	
	//Distance between point A and point B. Multiplied by pathPrecision and returned as an INT
	private int DistanceAsInt(Vector3 _pointA, Vector3 _pointB)
	{
		return (int) (Vector3.Distance(_pointA, _pointB) * pathPrecision);
	}
		
	//Return index of lowest F value in Node List
	private int FindLowFCost()
	{		
		int lowestCost = int.MaxValue;
		int lowIndex = 0;
		
		for(int i = 0; i < openList.Count; i++)
		{
			if(openList[i].GetFCost() < lowestCost)
			{
				lowestCost = openList[i].GetFCost();
				lowIndex = i;
			}
		}
		
		return lowIndex;
	}
}
