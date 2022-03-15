using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class GridMap<TGridObject>{
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