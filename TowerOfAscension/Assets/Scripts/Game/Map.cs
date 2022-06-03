using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Map{
	public interface IMapmatics{
		List<Tile> CalculateFov(int x, int y, int range, Func<int, Tile, bool> IsRayable);
		List<Tile> Raycast(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable);
		bool RaycastHit(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable);
		List<Tile> GetCardinals(int x, int y);
		List<Tile> GetCardinals(int x, int y, Func<Tile, bool> IsAvailable);
		List<Tile> GetNeighbours(int x, int y);
		List<Tile> GetNeighbours(int x, int y, Func<Tile, bool> IsAvailable);
		int CalculateDistanceCost(int startX, int startY, int endX, int endY);
		List<Tile> FindPath(int startX, int startY, int endX, int endY, Func<Tile, bool> IsWalkable);
	};
	[Serializable]
	public abstract class Tile{
		[Serializable]
		public class NullTile : 
			Tile,
			TestTile.ITestTile,
			DataTile.IDataTile,
			PrintTile.IPrintTile,
			MapMeshManager.ITileMeshData
			{
			//
			//
			public void SetPath(Map map, bool path){}
			public bool GetPath(){
				return false;
			}
			public void SetAltasIndex(Map map, int index){}
			//
			public void SetData(Game game, Data data){}
			public void ClearData(Game game){}
			public Data GetData(Game game){
				return Data.GetNullData();
			}
			public Block GetBlock(Game game, int blockID){
				return Block.GetNullBlock();
			}
			//
			public void Spawn(Game game, int x, int y){}
			public bool Check(Game game, int x, int y){
				return false;
			}
			//
			public int GetAtlasIndex(Game game){
				return 0;
			}
			public int GetUVFactor(Game game){
				return 0;
			}
			//
			private const int _NULL_XY = -1;
			public override void Add(List<Tile> tiles){}
			public override int GetX(){
				return _NULL_XY;
			}
			public override int GetY(){
				return _NULL_XY;
			}
			public override void GetXY(out int x, out int y){
				x = _NULL_XY;
				y = _NULL_XY;
			}
			public override Vector3 GetPosition(Map map){
				return Vector3.zero;
			}
			public override void SetGCost(int gCost){}
			public override void SetHCost(int hCost){}
			public override void CalculateFCost(){}
			public override int GetGCost(){
				return 0;
			}
			public override int GetHCost(){
				return 0;
			}
			public override int GetFCost(){
				return 0;
			}
			public override void SetCameFrom(int x, int y){}
			public override Tile GetCameFrom(Map map){
				return Map.Tile.GetNullTile();
			}
			public override void ClearCameFrom(){}
			public override Tile GetTileFrom(Map map, int x, int y){
				return Tile.GetNullTile();
			}
			public override void FireTileUpdateEvent(Map map){}
			public override bool IsNull(){
				return true;
			}
			public static int GetNullXY(){
				return _NULL_XY;
			}
		}
		[Serializable]
		public class BlankTile : 
			Tile
			{
			private const int _NULL_XY = -1;
			public override void Add(List<Tile> tiles){
				tiles.Add(this);
			}
			public override int GetX(){
				return _NULL_XY;
			}
			public override int GetY(){
				return _NULL_XY;
			}
			public override void GetXY(out int x, out int y){
				x = _NULL_XY;
				y = _NULL_XY;
			}
			public override Vector3 GetPosition(Map map){
				return Vector3.zero;
			}
			public override void SetGCost(int gCost){}
			public override void SetHCost(int hCost){}
			public override void CalculateFCost(){}
			public override int GetGCost(){
				return 0;
			}
			public override int GetHCost(){
				return 0;
			}
			public override int GetFCost(){
				return 0;
			}
			public override void SetCameFrom(int x, int y){}
			public override Tile GetCameFrom(Map map){
				return Map.Tile.GetNullTile();
			}
			public override void ClearCameFrom(){}
			public override Tile GetTileFrom(Map map, int x, int y){
				return Tile.GetNullTile();
			}
			public override void FireTileUpdateEvent(Map map){}
			public override bool IsNull(){
				return false;
			}
		}
		[Serializable]
		public class BaseTile : Tile{
			private int _x;
			private int _y;
			private int _gCost;
			private int _hCost;
			private int _fCost;
			private int _cameX;
			private int _cameY;
			public BaseTile(int x, int y){
				_x = x;
				_y = y;
			}
			public override void Add(List<Tile> tiles){
				tiles.Add(this);
			}
			public override int GetX(){
				return _x;
			}
			public override int GetY(){
				return _y;
			}
			public override void GetXY(out int x, out int y){
				x = _x;
				y = _y;
			}
			public override Vector3 GetPosition(Map map){
				return map.GetWorldPosition(_x, _y);
			}
			public override void SetGCost(int gCost){
				_gCost = gCost;
			}
			public override void SetHCost(int hCost){
				_hCost = hCost;
			}
			public override void CalculateFCost(){
				_fCost = _gCost + _hCost;
			}
			public override int GetGCost(){
				return _gCost;
			}
			public override int GetHCost(){
				return _hCost;
			}
			public override int GetFCost(){
				return _fCost;
			}
			public override void SetCameFrom(int x, int y){
				_cameX = x;
				_cameY = y;
			}
			public override Tile GetCameFrom(Map map){
				return map.Get(_cameX, _cameY);
			}
			public override void ClearCameFrom(){
				_cameX = Map.Tile.NullTile.GetNullXY();
				_cameY = Map.Tile.NullTile.GetNullXY();
			}
			public override Tile GetTileFrom(Map map, int x, int y){
				return map.Get((_x + x), (_y + y));
			}
			public override void FireTileUpdateEvent(Map map){
				map.FireTileUpdateEvent(_x, _y);
			}
			public override bool IsNull(){
				return false;
			}
		}
		public abstract void Add(List<Tile> tiles);
		public abstract int GetX();
		public abstract int GetY();
		public abstract void GetXY(out int x, out int y);
		public abstract Vector3 GetPosition(Map map);
		public abstract void SetGCost(int gCost);
		public abstract void SetHCost(int gCost);
		public abstract void CalculateFCost();
		public abstract int GetGCost();
		public abstract int GetHCost();
		public abstract int GetFCost();
		public abstract void SetCameFrom(int x, int y);
		public abstract Tile GetCameFrom(Map map);
		public abstract void ClearCameFrom();
		public abstract Tile GetTileFrom(Map map, int x, int y);
		public abstract void FireTileUpdateEvent(Map map);
		public abstract bool IsNull();
		//
		public virtual DataTile.IDataTile GetIDataTile(){
			return _NULL_TILE;
		}
		public virtual TestTile.ITestTile GetITestTile(){
			return _NULL_TILE;
		}
		public virtual PrintTile.IPrintTile GetIPrintTile(){
			return _NULL_TILE;
		}
		public virtual MapMeshManager.ITileMeshData GetITileMeshData(){
			return _NULL_TILE;
		}
		//
		private static NullTile _NULL_TILE = new NullTile();
		public static Tile GetNullTile(){
			return _NULL_TILE;
		}
	}
	[Serializable]
	public class NullMap : 
		Map,
		Map.IMapmatics,
		ISpawn
		{
		//
		//
		private static readonly Vector3 _NULL_VECTOR_3 = new Vector3(0,0,0);
		public override bool Set(int x, int y, Tile value){
			return false;
		}
		public override Tile Get(int x, int y){
			return Map.Tile.GetNullTile();
		}
		public override Tile Get(Vector2Int position){
			return Map.Tile.GetNullTile();
		}
		public override Tile Get(Vector3 position){
			return Map.Tile.GetNullTile();
		}
		public override Vector3 GetWorldPosition(int x, int y){
			return _NULL_VECTOR_3;
		}
		public override Vector3 GetOriginPosition(){
			return _NULL_VECTOR_3;
		}
		public override void GetMapPosition(Vector3 position, out int x, out int y){
			x = 0;
			y = 0;
		}
		public override int GetWidth(){
			return 0;
		}
		public override int GetHeight(){
			return 0;
		}
		public override int GetArea(){
			return 0;
		}
		public override float GetTileSize(){
			return 0;
		}
		public override float GetTileOffset(){
			return 0;
		}
		public override Vector3 GetVector3TileSize(){
			return _NULL_VECTOR_3;
		}
		public override Vector3 GetVector3TileOffset(){
			return _NULL_VECTOR_3;
		}
		public override Vector3 GetCentrePosition(){
			return _NULL_VECTOR_3;
		}
		public override void GetMidPoint(out int x, out int y){
			x = 0;
			y = 0;
		}
		public override bool CheckBounds(int x, int y){
			return false;
		}
		//
		public override void FireMapUpdateEvent(){}
		public override void FireTileUpdateEvent(int x, int y){}
		//
		public List<Tile> CalculateFov(int x, int y, int range, Func<int, Tile, bool> IsRayable){
			return new List<Tile>();
		}
		public List<Tile> Raycast(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable){
			return new List<Tile>();
		}
		public bool RaycastHit(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable){
			return false;
		}
		public List<Tile> GetCardinals(int x, int y){
			return new List<Tile>();
		}
		public List<Tile> GetCardinals(int x, int y, Func<Tile, bool> IsAvailable){
			return new List<Tile>();
		}
		public List<Tile> GetNeighbours(int x, int y){
			return new List<Tile>();
		}
		public List<Tile> GetNeighbours(int x, int y, Func<Tile, bool> IsAvailable){
			return new List<Tile>();
		}
		public int CalculateDistanceCost(int startX, int startY, int endX, int endY){
			return 0;
		}
		public List<Tile> FindPath(int startX, int startY, int endX, int endY, Func<Tile, bool> IsWalkable){
			return new List<Tile>();
		}
		//
		public void Spawn(Game game, int x, int y){}
	}
	[Serializable]
	public class BaseMap : 
		Map,
		Map.IMapmatics
		{
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
		private const int _SANITY = 1000;
		//
		//
		private int _width;
		private int _height;
		private float _tileSize;
		private float _tileOffset;
		private Vector3 _origin;
		private Vector3 _dimensions;
		private Tile[,] _map;
		public BaseMap(int width, int height, float tileSize, float tileOffset, Vector3 origin, Vector3 dimensions){
			_width = width;
			_height = height;
			_tileSize = tileSize;
			_tileOffset = tileOffset;
			_origin = origin;
			_dimensions = dimensions;
			_map = new Tile[width, height];
		}
		public BaseMap(int width, int height, float tileSize, float tileOffset, Vector3 origin, Vector3 dimensions, Func<int, int, Tile> SetGridObject){
			_width = width;
			_height = height;
			_tileSize = tileSize;
			_tileOffset = tileOffset;
			_origin = origin;
			_dimensions = dimensions;
			_map = new Tile[width, height];
			for(int x = 0; x < width; x++){
				for(int y = 0; y < height; y++){
					_map[x, y] = SetGridObject(x, y);
				}
			}
		}
		public override bool Set(int x, int y, Tile value){
			if(!CheckBounds(x, y)){
				return false;
			}
			_map[x, y] = value;
			FireTileUpdateEvent(x, y);
			return true;
		}
		public override Tile Get(int x, int y){
			if(!CheckBounds(x, y)){
				return Map.Tile.GetNullTile();
			}
			return _map[x, y];
		}
		public override Tile Get(Vector2Int position){
			return Get(position.x, position.y);
		}
		public override Tile Get(Vector3 position){
			GetMapPosition(position, out int x, out int y);
			return Get(x, y);
		}
		public override Vector3 GetWorldPosition(int x, int y){
			return new Vector3(x, y) * _tileSize + _origin;
		}
		public override Vector3 GetOriginPosition(){
			return _origin;
		}
		public override void GetMapPosition(Vector3 position, out int x, out int y){
			x =  Mathf.FloorToInt((position - _origin).x / _tileSize);
			y =  Mathf.FloorToInt((position - _origin).y / _tileSize);
		}
		public override int GetWidth(){
			return _width;
		}
		public override int GetHeight(){
			return _height;
		}
		public override int GetArea(){
			return _height * _width;
		}
		public override float GetTileSize(){
			return _tileSize;
		}
		public override float GetTileOffset(){
			return _tileOffset;
		}
		public override Vector3 GetVector3TileSize(){
			return _dimensions * _tileSize;
		}
		public override Vector3 GetVector3TileOffset(){
			return GetVector3TileSize() * _tileOffset;
		}
		public override Vector3 GetCentrePosition(){
			Vector3 position = new Vector3((_width / 2), (_height / 2)) * _tileSize;
			return position + _origin;
		}
		public override void GetMidPoint(out int x, out int y){
			x = Mathf.FloorToInt(_width / 2);
			y = Mathf.FloorToInt(_height / 2);
		}
		//
		public List<Tile> CalculateFov(int x, int y, int range, Func<int, Tile, bool> IsRayable){
			if(IsRayable == null){
				IsRayable = NullIsRayable;
			}
			List<Tile> tiles = new List<Tile>{Get(x, y)};
			float angleX, angleY;
			for(int i = 0; i < 360; i++){
				angleX = Mathf.Cos((float) i * RADIAL_DEGREE); 
				angleY = Mathf.Sin((float) i * RADIAL_DEGREE);
				float rayX, rayY;
				rayX = (float)x + RAY_LENGTH;
				rayY = (float)y + RAY_LENGTH;
				for(int j = 0; j < range; j++){
					rayX += angleX;
					rayY += angleY;	
					int tileX, tileY;
					tileX = (int)rayX;
					tileY = (int)rayY;
					Tile current = Get(tileX, tileY);
					if(!tiles.Contains(current)){
						tiles.Add(current);
					}
					if(!IsRayable(j, current)){
						break;
					}		
				}
			}
			return tiles;
		}
		/*
		public void RaycastAngle(ref List<Tile> nodes, int x, int y, float angleX, float angleY, int range, Func<int, Tile, bool> IsRayable){
			float rayX, rayY;
			rayX = (float)x + RAY_LENGTH;
			rayY = (float)y + RAY_LENGTH;
			for(int j = 0; j < range; j++){
				rayX += angleX;
				rayY += angleY;	
				int nodeX, nodeY;
				nodeX = (int)rayX;
				nodeY = (int)rayY;
				Tile current = Get(nodeX, nodeY);
				if(!nodes.Contains(current)){
					nodes.Add(current);
				}
				if(!IsRayable(j, current)){
					return;
				}		
			}
		}
		*/
		public List<Tile> Raycast(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable){
			if(IsRayable == null){
				IsRayable = NullIsWalkable;
			}
			Tile end = Get(endX, endY);
			List<Map.Tile> tiles = new List<Map.Tile>{Get(startX, startY)};
			Vector2 angle = new Vector2Int((endX - startX), (endY - startY));
			angle.Normalize();
			float rayX, rayY;
			rayX = (float)startX + RAY_LENGTH;
			rayY = (float)startY + RAY_LENGTH;
			for(int i = 0; i < _SANITY; i++){
				rayX += angle.x;
				rayY += angle.y;	
				int tileX, tileY;
				tileX = (int)rayX;
				tileY = (int)rayY;
				Tile current = Get(tileX, tileY);
				if(!tiles.Contains(current)){
					tiles.Add(current);
				}
				if(!IsRayable(current) || current == end){
					return tiles;
				}
			}
			return tiles;
		}
		public bool RaycastHit(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable){
			if(IsRayable == null){
				IsRayable = NullIsWalkable;
			}
			Tile end = Get(endX, endY);
			Vector2 angle = new Vector2Int((endX - startX), (endY - startY));
			angle.Normalize();
			float rayX, rayY;
			rayX = (float)startX + RAY_LENGTH;
			rayY = (float)startY + RAY_LENGTH;
			for(int i = 0; i < _SANITY; i++){
				rayX += angle.x;
				rayY += angle.y;	
				int tileX, tileY;
				tileX = (int)rayX;
				tileY = (int)rayY;
				Tile current = Get(tileX, tileY);
				if(!IsRayable(current)){
					return false;
				}
				if(current == end){
					return true;
				}
			}
			return false;
		}
		/*
		public List<Tile> Raycast(int startX, int startY, int endX, int endY, Func<Tile, bool> IsRayable){
			if(IsRayable == null){
				IsRayable = NullIsWalkable;
			}
			List<Map.Tile> tiles = new List<Map.Tile>();
			Vector2 angle = new Vector2Int((endX - startX), (endY - startY));
			angle.Normalize();
			float sqrtX = Mathf.Sqrt(1 + (angle.y / angle.x) * (angle.y / angle.x));
			float sqrtY = Mathf.Sqrt(1 + (angle.x / angle.y) * (angle.x / angle.y));
			int tileX = startX;
			int tileY = startY;
			float lengthX, lengthY;
			int stepX, stepY;
			if(angle.x < 0){
				stepX = (stepX - 1);
				lengthX = (startX - (float)tileX) * sqrtX;
			}else{
				stepX = (stepX + 1);
				lengthX = ((float)(tileX + 1) - startX) * sqrtX;
			}
			if(angle.y < 0){
				stepY = (stepY - 1);
				lengthY = (startY - (float)tileY) * sqrtY;
			}else{
				stepY = (stepY + 1);
				lengthY = ((float)(tileY + 1) - startY) * sqrtY;
			}
			while(){
				
			}
		}
		*/
		public List<Tile> GetCardinals(int x, int y){
			List<Tile> cardinals = new List<Tile>();
			for(int i = 0; i < _CARDINALS.Length; i++){
				Vector2Int cardinal = _CARDINALS[i];
				Get((x + cardinal.x), (y + cardinal.y)).Add(cardinals);
			}
			return cardinals;
		}
		public List<Tile> GetCardinals(int x, int y, Func<Tile, bool> IsAvailable){
			List<Tile> cardinals = new List<Tile>();
			for(int i = 0; i < _CARDINALS.Length; i++){
				Vector2Int cardinal = _CARDINALS[i];
				Tile tile = Get((x + cardinal.x), (y + cardinal.y));
				if(IsAvailable(tile)){
					tile.Add(cardinals);
				}
			}
			return cardinals;
		}
		public List<Tile> GetNeighbours(int x, int y){
			List<Tile> neighbours = new List<Tile>();
			for(int i = 0; i < _NEIGHBOURS.Length; i++){
				Vector2Int neighbour = _NEIGHBOURS[i];
				Get((x + neighbour.x), (y + neighbour.y)).Add(neighbours);
			}
			return neighbours;
		}
		public List<Tile> GetNeighbours(int x, int y, Func<Tile, bool> IsAvailable){
			List<Tile> neighbours = new List<Tile>();
			for(int i = 0; i < _NEIGHBOURS.Length; i++){
				Vector2Int neighbour = _NEIGHBOURS[i];
				Tile tile = Get((x + neighbour.x), (y + neighbour.y));
				if(IsAvailable(tile)){
					tile.Add(neighbours);
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
		public List<Tile> FindPath(int startX, int startY, int endX, int endY, Func<Tile, bool> IsWalkable){
			Tile start = Get(startX, startY);
			Tile end = Get(endX, endY);
			if(start.IsNull() || end.IsNull()){
				return new List<Tile>();
			}
			if(IsWalkable == null){
				IsWalkable = NullIsWalkable;
			}
			List<Tile> open = new List<Tile>{start};
			List<Tile> closed = new List<Tile>{Map.Tile.GetNullTile()};
			for(int x = 0; x < GetWidth(); x++){
				for(int y = 0; y < GetHeight(); y++){
					Tile tile = Get(x, y);
					tile.SetGCost(int.MaxValue);
					tile.CalculateFCost();
					tile.ClearCameFrom();
				}
			}
			start.SetGCost(0);
			start.SetHCost(CalculateDistanceCost(start, end));
			start.CalculateFCost();
			while(open.Count > 0){
				Tile current = GetLowestFCostNode(open);
				if(current == end){
					return CalculatePath(end);
				}
				open.Remove(current);
				closed.Add(current);
				current.GetXY(out int posX, out int posY);
				for(int i = 0; i < _NEIGHBOURS.Length; i++){
					Vector2Int neighbour = _NEIGHBOURS[i];
					Tile tile = Get((posX + neighbour.x), (posY + neighbour.y));
					if(closed.Contains(tile)){
						continue;
					}
					if(!IsWalkable(tile) && tile != end){
						closed.Add(tile);
						continue;
					}
					int tentativeGCost = current.GetGCost() + CalculateDistanceCost(current, tile);
					if(tentativeGCost < tile.GetGCost()){
						current.GetXY(out int cx, out int cy);
						tile.SetCameFrom(cx, cy);
						tile.SetGCost(tentativeGCost);
						tile.SetHCost(CalculateDistanceCost(tile, end));
						tile.CalculateFCost();
						if(!open.Contains(tile)){
							tile.Add(open);
						}
					}
				}
				//
			}
			return new List<Tile>();
		}
		private List<Tile> CalculatePath(Tile end){
			List<Tile> path = new List<Tile>();
			path.Add(end);
			Tile current = end;
			Tile cameFrom = current.GetCameFrom(this);
			while(!cameFrom.IsNull()){
				path.Add(cameFrom);
				current = cameFrom;
				cameFrom = current.GetCameFrom(this);
			}
			path.Reverse();
			return path;
		}
		private Tile GetLowestFCostNode(List<Tile> nodes){
			Tile lowestFCostNode = nodes[0];
			for(int i = 1; i < nodes.Count; i++){
				if(nodes[i].GetFCost() < lowestFCostNode.GetFCost()){
					lowestFCostNode = nodes[i];
				}
			}
			return lowestFCostNode;
		}
		private int CalculateDistanceCost(Tile start, Tile end){
			int xDistance = Mathf.Abs(start.GetX() - end.GetX());
			int yDistance = Mathf.Abs(start.GetY() - end.GetY());
			int remaining = Mathf.Abs(xDistance - yDistance);
			return (MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance)) + (MOVE_STRAIGHT_COST * remaining);
		}
		private bool NullIsWalkable(Tile tile){
			return true;
		}
		private bool NullIsRayable(int range, Tile tile){
			return true;
		}
		//
		public override bool CheckBounds(int x, int y){
			if(x < 0 || y < 0 || x >= _width || y >= _height){
				return false;
			}else{
				return true;
			}
		}
		public override	Map.IMapmatics GetIMapmatics(){
			return this;
		}
	}
	[field:NonSerialized]public event EventHandler<EventArgs> OnMapUpdate;
	[field:NonSerialized]public event EventHandler<OnTileUpdateEventArgs> OnTileUpdate;
	public class OnTileUpdateEventArgs : EventArgs{
		public int x;
		public int y;
		public OnTileUpdateEventArgs(int x, int y){
			this.x = x;
			this.y = y;
		}
	}
	public virtual void FireMapUpdateEvent(){
		OnMapUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual void FireTileUpdateEvent(int x, int y){
		OnTileUpdate?.Invoke(this, new OnTileUpdateEventArgs(x, y));
	}
	//
	public abstract bool Set(int x, int y, Tile value);
	public abstract Tile Get(int x, int y);
	public abstract Tile Get(Vector2Int position);
	public abstract Tile Get(Vector3 position);
	public abstract Vector3 GetWorldPosition(int x, int y);
	public abstract Vector3 GetOriginPosition();
	public abstract void GetMapPosition(Vector3 position, out int x, out int y);
	public abstract int GetWidth();
	public abstract int GetHeight();
	public abstract int GetArea();
	public abstract float GetTileSize();
	public abstract float GetTileOffset();
	public abstract Vector3 GetVector3TileSize();
	public abstract Vector3 GetVector3TileOffset();
	public abstract Vector3 GetCentrePosition();
	public abstract void GetMidPoint(out int x, out int y);
	public abstract bool CheckBounds(int x, int y);
	//
	public virtual Map.IMapmatics GetIMapmatics(){
		return _NULL_MAP;
	}
	public virtual ISpawn GetISpawn(){
		return _NULL_MAP;
	}
	//
	private static NullMap _NULL_MAP = new NullMap();
	public static Map GetNullMap(){
		return _NULL_MAP;
	}
}