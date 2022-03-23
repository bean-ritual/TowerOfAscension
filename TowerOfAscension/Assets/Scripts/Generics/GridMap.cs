using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GridMap<TGridObject> where TGridObject : GridMap<TGridObject>.Node
	{
	[Serializable()]
	public abstract class Node{
		protected int _x;
		protected int _y;
		[field:NonSerialized]public int gCost;
		[field:NonSerialized]public int hCost;
		[field:NonSerialized]public int fCost;
		[field:NonSerialized]public TGridObject cameFrom;
		public Node(int x, int y){
			_x = x;
			_y = y;
		}
		public Node(){}
		public void CalculateFCost(){
			fCost = gCost + hCost;
		}
		public virtual int GetX(){
			return _x;
		}
		public virtual int GetY(){
			return _y;
		}
		public virtual void GetXY(out int x, out int y){
			x = _x;
			y = _y;
		}
	}
	[field:NonSerialized()]public event EventHandler<OnGridMapChangedEventArgs> OnGridMapChanged;
	public class OnGridMapChangedEventArgs : EventArgs{
		public int x;
		public int y;
		public OnGridMapChangedEventArgs(int x, int y){
			this.x = x;
			this.y = y;
		}
	}
	private static readonly Vector2Int[] _CARDINALS = {
		new Vector2Int(1, 0),
		new Vector2Int(0, 1),
		new Vector2Int(-1, 0),
		new Vector2Int(0, -1),
	};
	private static readonly Vector2Int[] _NEIGHBOURS = {
		new Vector2Int(1, 1),
		new Vector2Int(1, 0),
		new Vector2Int(0, 1),
		new Vector2Int(-1, -1),
		new Vector2Int(-1, 0),
		new Vector2Int(0, -1),
		new Vector2Int(-1, 1),
		new Vector2Int(1, -1),
	};
	public const int MOVE_STRAIGHT_COST = 10;
	public const int MOVE_DIAGONAL_COST = 14;
	public const float RAY_LENGTH = 0.5f;
	public const float RADIAL_DEGREE = 0.01745f;
	//
	[field:NonSerialized()]private List<TGridObject> _open;
	[field:NonSerialized()]private List<TGridObject> _closed;
	//
	private int _width;
	private int _height;
	private float _cellSize;
	private float _cellOffset;
	private Vector3 _origin;
	private Vector3 _dimensions;
	private TGridObject[,] _map;
	public GridMap(int width, int height, float cellSize, float cellOffset, Vector3 origin, Vector3 dimensions){
		_width = width;
		_height = height;
		_cellSize = cellSize;
		_cellOffset = cellOffset;
		_origin = origin;
		_dimensions = dimensions;
		_map = new TGridObject[width, height];
	}
	public GridMap(int width, int height, float cellSize, float cellOffset, Vector3 origin, Vector3 dimensions, Func<int, int, TGridObject> SetGridObject){
		_width = width;
		_height = height;
		_cellSize = cellSize;
		_cellOffset = cellOffset;
		_origin = origin;
		_dimensions = dimensions;
		_map = new TGridObject[width, height];
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				_map[x, y] = SetGridObject(x, y);
			}
		}
	}
	public abstract TGridObject GetNullGridObject();
	public virtual bool Set(int x, int y, TGridObject value){
		if(!CheckBounds(x, y)){
			return false;
		}
		_map[x, y] = value;
		OnGridMapChanged?.Invoke(this, new OnGridMapChangedEventArgs(x, y));
		return true;
	}
	public virtual TGridObject Get(int x, int y){
		if(!CheckBounds(x, y)){
			return GetNullGridObject();
		}
		return _map[x, y];
	}
	public TGridObject Get(Vector2Int position){
		return Get(position.x, position.y);
	}
	public TGridObject Get(Vector3 position){
		GetMapPosition(position, out int x, out int y);
		return Get(x, y);
	}
	public Vector3 GetWorldPosition(int x, int y){
		return new Vector3(x, y) * _cellSize + _origin;
	}
	public Vector3 GetOriginPosition(){
		return _origin;
	}
	public void GetMapPosition(Vector3 position, out int x, out int y){
		x =  Mathf.FloorToInt((position - _origin).x / _cellSize);
		y =  Mathf.FloorToInt((position - _origin).y / _cellSize);
	}
	public int GetWidth(){
		return _width;
	}
	public int GetHeight(){
		return _height;
	}
	public int GetArea(){
		return _height * _width;
	}
	public float GetCellSize(){
		return _cellSize;
	}
	public float GetCellOffset(){
		return _cellOffset;
	}
	public Vector3 GetVector3CellSize(){
		return _dimensions * _cellSize;
	}
	public Vector3 GetVector3CellOffset(){
		return GetVector3CellSize() * _cellOffset;
	}
	public Vector3 GetCentrePosition(){
		Vector3 position = new Vector3((_width / 2), (_height / 2)) * _cellSize;
		return position + _origin;
	}
	public void GetMidPoint(out int x, out int y){
		x = Mathf.FloorToInt(_width / 2);
		y = Mathf.FloorToInt(_height / 2);
	}
	//
	public List<TGridObject> CalculateFov(int x, int y, int range, Func<int, TGridObject, bool> IsRayable){
		List<TGridObject> nodes = new List<TGridObject>{Get(x, y)};
		float angleX, angleY;
		for(int i = 0; i < 360; i++){
			angleX = Mathf.Cos((float) i * RADIAL_DEGREE); 
			angleY = Mathf.Sin((float) i * RADIAL_DEGREE);
			RaycastAngle(ref nodes, x, y, angleX, angleY, range, IsRayable);
		}
		return nodes;
	}
	public void RaycastAngle(ref List<TGridObject> nodes, int x, int y, float angleX, float angleY, int range, Func<int, TGridObject, bool> IsRayable){
		float rayX, rayY;
		rayX = (float)x + RAY_LENGTH;
		rayY = (float)y + RAY_LENGTH;
		for(int j = 0; j < range; j++){
			rayX += angleX;
			rayY += angleY;	
			int nodeX, nodeY;
			nodeX = (int)rayX;
			nodeY = (int)rayY;
			TGridObject current = Get(nodeX, nodeY);
			if(!nodes.Contains(current)){
				nodes.Add(current);
			}
			if(!IsRayable(j, current)){
				return;
			}		
		}
	}
	public List<TGridObject> GetCardinals(int x, int y){
		List<TGridObject> cardinals = new List<TGridObject>();
		for(int i = 0; i < _CARDINALS.Length; i++){
			Vector2Int cardinal = _CARDINALS[i];
			cardinals.Add(Get((x + cardinal.x), (y + cardinal.y)));
		}
		return cardinals;
	}
	public List<TGridObject> GetCardinals(int x, int y, Func<TGridObject, bool> IsAvailable){
		List<TGridObject> cardinals = new List<TGridObject>();
		for(int i = 0; i < _CARDINALS.Length; i++){
			Vector2Int cardinal = _CARDINALS[i];
			TGridObject value = Get((x + cardinal.x), (y + cardinal.y));
			if(IsAvailable(value)){
				cardinals.Add(value);
			}
		}
		return cardinals;
	}
	public List<TGridObject> GetNeighbours(int x, int y){
		List<TGridObject> neighbours = new List<TGridObject>();
		for(int i = 0; i < _NEIGHBOURS.Length; i++){
			Vector2Int neighbour = _NEIGHBOURS[i];
			neighbours.Add(Get((x + neighbour.x), (y + neighbour.y)));
		}
		return neighbours;
	}
	public List<TGridObject> GetNeighbours(int x, int y, Func<TGridObject, bool> IsAvailable){
		List<TGridObject> neighbours = new List<TGridObject>();
		for(int i = 0; i < _NEIGHBOURS.Length; i++){
			Vector2Int neighbour = _NEIGHBOURS[i];
			TGridObject value = Get((x + neighbour.x), (y + neighbour.y));
			if(IsAvailable(value)){
				neighbours.Add(value);
			}
		}
		return neighbours;
	}
	public int CalculateDistanceCost(int startX, int startY, int endX, int endY){
		int xDistance = Mathf.Abs(startX - endX);
		int yDistance = Mathf.Abs(startY - endY);
		int remaining = Mathf.Abs(xDistance - yDistance);
		return (MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance)) + (MOVE_STRAIGHT_COST * remaining);
	}
	//
	public List<TGridObject> FindPath(int startX, int startY, int endX, int endY, Func<TGridObject, bool> IsWalkable){
		TGridObject start = Get(startX, startY);
		TGridObject end = Get(endX, endY);
		_open = new List<TGridObject>{start};
		_closed = new List<TGridObject>();
		for(int x = 0; x < GetWidth(); x++){
			for(int y = 0; y < GetHeight(); y++){
				TGridObject pathNode = Get(x, y);
				pathNode.gCost = int.MaxValue;
				pathNode.CalculateFCost();
				pathNode.cameFrom = null;
			}
		}
		start.gCost = 0;
		start.hCost = CalculateDistanceCost(start, end);
		start.CalculateFCost();
		while(_open.Count > 0){
			TGridObject current = GetLowestFCostNode(_open);
			if(current == end){
				return CalculatePath(end);
			}
			_open.Remove(current);
			_closed.Add(current);
			current.GetXY(out int posX, out int posY);
			foreach(TGridObject neighbour in GetNeighbours(posX, posY)){
				if(_closed.Contains(neighbour)){
					continue;
				}
				if(!IsWalkable(neighbour) && neighbour != end){
					_closed.Add(neighbour);
					continue;
				}
				int tentativeGCost = current.gCost + CalculateDistanceCost(current, neighbour);
				if(tentativeGCost < neighbour.gCost){
					neighbour.cameFrom = current;
					neighbour.gCost = tentativeGCost;
					neighbour.hCost = CalculateDistanceCost(neighbour, end);
					neighbour.CalculateFCost();
					if(!_open.Contains(neighbour)){
						_open.Add(neighbour);
					}
				}
			}
		}
		//ResetMap();
		return new List<TGridObject>();
	}
	private List<TGridObject> CalculatePath(TGridObject end){
		List<TGridObject> path = new List<TGridObject>();
		path.Add(end);
		TGridObject current = end;
		while(current.cameFrom != null){
			path.Add(current.cameFrom);
			current = current.cameFrom;
		}
		path.Reverse();
		//ResetMap();
		return path;
	}
	private TGridObject GetLowestFCostNode(List<TGridObject> nodes){
		TGridObject lowestFCostNode = nodes[0];
		for(int i = 1; i < nodes.Count; i++){
			if(nodes[i].fCost < lowestFCostNode.fCost){
				lowestFCostNode = nodes[i];
			}
		}
		return lowestFCostNode;
	}
	private void ResetMap(){
		for(int i = 0; i < _open.Count; i++){
			TGridObject openNode = _open[i];
			openNode.gCost = int.MaxValue;
			openNode.CalculateFCost();
			openNode.cameFrom = null;
		}
		for(int j = 0; j < _closed.Count; j++){
			TGridObject closedNode = _closed[j];
			closedNode.gCost = int.MaxValue;
			closedNode.CalculateFCost();
			closedNode.cameFrom = null;
		}
	}
	private int CalculateDistanceCost(TGridObject start, TGridObject end){
		int xDistance = Mathf.Abs(start.GetX() - end.GetX());
		int yDistance = Mathf.Abs(start.GetY() - end.GetY());
		int remaining = Mathf.Abs(xDistance - yDistance);
		return (MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance)) + (MOVE_STRAIGHT_COST * remaining);
	}
	//
	public bool CheckBounds(int x, int y){
		if(x < 0){
			return false;
		}
		if(y < 0){
			return false;
		}
		if(x >= _width){
			return false;
		}
		if(y >= _height){
			return false;
		}
		return true;
	}
}