using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Direction{
	[Serializable]
	public class NullDirection : Direction{
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return Map.Tile.GetNullTile();
		}
		public override Vector3 GetWorldDirection(Map map){
			return Vector3.zero;
		}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public class North : BaseDirection{
		private const int _X = 0;
		private const int _Y = 1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class South : BaseDirection{
		private const int _X = 0;
		private const int _Y = -1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class East : BaseDirection{
		private const int _X = 1;
		private const int _Y = 0;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class West : BaseDirection{
		private const int _X = -1;
		private const int _Y = 0;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class NorthEast : BaseDirection{
		private const int _X = 1;
		private const int _Y = 1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class NorthWest : BaseDirection{
		private const int _X = -1;
		private const int _Y = 1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class SouthEast : BaseDirection{
		private const int _X = 1;
		private const int _Y = -1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public class SouthWest : BaseDirection{
		private const int _X = -1;
		private const int _Y = -1;
		public override Map.Tile GetTile(Map map, Map.Tile tile){
			return tile.GetTileFrom(map, _X, _Y);
		}
		public override Vector3 GetWorldDirection(Map map){
			return map.GetWorldPosition(_X, _Y);
		}
	}
	[Serializable]
	public abstract class BaseDirection : Direction{
		public override bool IsNull(){
			return false;
		}
	}
	private static Direction[] _DIRECTIONS = {
		new NullDirection(),
		new North(),
		new South(),
		new East(),
		new West(),
		new NorthEast(),
		new NorthWest(),
		new SouthEast(),
		new SouthWest(),
	};
	private const int _NULL_DIRECTION = 0;
	private const int _NORTH = 1;
	private const int _SOUTH = 2;
	private const int _EAST = 3;
	private const int _WEST = 4;
	private const int _NORTH_EAST = 5;
	private const int _NORTH_WEST = 6;
	private const int _SOUTH_EAST = 7;
	private const int _SOUTH_WEST = 8;
	//
	public abstract Map.Tile GetTile(Map map, Map.Tile tile);
	public abstract Vector3 GetWorldDirection(Map map);
	public abstract bool IsNull();
	//
	public static Direction IntToDirection(int x, int y){
		if(y > 0){
			if(x > 0){
				return _DIRECTIONS[_NORTH_EAST];
			}
			if(x < 0){
				return _DIRECTIONS[_NORTH_WEST];
			}
			return _DIRECTIONS[_NORTH];
		}
		if(y < 0){
			if(x > 0){
				return _DIRECTIONS[_SOUTH_EAST];
			}
			if(x < 0){
				return _DIRECTIONS[_SOUTH_WEST];
			}
			return _DIRECTIONS[_SOUTH];
		}
		if(x > 0){
			if(y > 0){
				return _DIRECTIONS[_NORTH_EAST];
			}
			if(y < 0){
				return _DIRECTIONS[_SOUTH_EAST];
			}
			return _DIRECTIONS[_EAST];
		}
		if(x < 0){
			if(y > 0){
				return _DIRECTIONS[_NORTH_WEST];
			}
			if(y < 0){
				return _DIRECTIONS[_SOUTH_WEST];
			}
			return _DIRECTIONS[_WEST];
		}
		return _DIRECTIONS[_NULL_DIRECTION];
	}
	public static Direction IntToDirection(int posX, int posY, int targetX, int targetY){
		return IntToDirection(targetX - posX, targetY - posY);
	}
	public static Direction GetNullDirection(){
		return _DIRECTIONS[_NULL_DIRECTION];
	}
	public static Direction GetNorth(){
		return _DIRECTIONS[_NORTH];
	}
	public static Direction GetSouth(){
		return _DIRECTIONS[_SOUTH];
	}
	public static Direction GetEast(){
		return _DIRECTIONS[_EAST];
	}
	public static Direction GetWest(){
		return _DIRECTIONS[_WEST];
	}
	public static Direction GetNorthEast(){
		return _DIRECTIONS[_NORTH_EAST];
	}
	public static Direction GetNorthWest(){
		return _DIRECTIONS[_NORTH_WEST];
	}
	public static Direction GetSouthEast(){
		return _DIRECTIONS[_SOUTH_EAST];
	}
	public static Direction GetSouthWest(){
		return _DIRECTIONS[_SOUTH_WEST];
	}
	public static Direction GetIntDirection(int index){
		if(index < 0 || index >= _DIRECTIONS.Length){
			return _DIRECTIONS[0];
		}else{
			return _DIRECTIONS[index];
		}
	}
	public static Direction GetRandomDirection(){
		return GetIntDirection(UnityEngine.Random.Range(1, 9));
	}
}
