using UnityEngine;
using System;
using System.Collections;

//Node Information:
//F Cost - G + H
//G Cost - Cost to move from starting previous node to current node
//H Cost - Estimated cost to move from current node to destination
//Parent - Parent Node
//Position - World Position

public class GRIDNode {

	private int nodeID;
	private int fCost;
	private int gCost;
	private int hCost;
	private int parentNode;
	private Vector3 nodePosition;
	
	public GRIDNode(int _nodeID, int _fCost, int _gCost, int _hCost, int _parentNode, Vector3 _position)
	{
		nodeID = _nodeID;
		fCost = _fCost;
		gCost = _gCost;
		hCost = _hCost;
		parentNode = _parentNode;
		nodePosition = _position;
	}
	
	public int GetNodeID()
	{
		return nodeID;
	}
	
	public int GetFCost()
	{
		return fCost;
	}
	
	public int GetGCost()
	{
		return gCost;	
	}
	
	public int GetHCost()
	{
		return hCost;	
	}
	
	public int GetParentNode()
	{
		return parentNode;	
	}
	
	public Vector3 GetNodePosition()
	{
		return nodePosition;	
	}
}
