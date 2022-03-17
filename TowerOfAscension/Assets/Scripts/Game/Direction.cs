using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Direction{
	[Serializable]
	public class NullDirection : Direction{
		public override Tile GetTile(Level level, int x, int y){
			return Tile.GetNullTile();
		}
	}
	[Serializable]
	public class North : Direction{
		private const int _X = 0;
		private const int _Y = 1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class South : Direction{
		private const int _X = 0;
		private const int _Y = -1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class East : Direction{
		private const int _X = 1;
		private const int _Y = 0;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class West : Direction{
		private const int _X = -1;
		private const int _Y = 0;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class NorthEast : Direction{
		private const int _X = 1;
		private const int _Y = 1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class NorthWest : Direction{
		private const int _X = -1;
		private const int _Y = 1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class SouthEast : Direction{
		private const int _X = 1;
		private const int _Y = -1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[Serializable]
	public class SouthWest : Direction{
		private const int _X = -1;
		private const int _Y = -1;
		public override Tile GetTile(Level level, int x, int y){
			return level.Get((x + _X), (y + _Y));
		}
	}
	[field:NonSerialized]private static readonly NullDirection _NULL_DIRECTION = new NullDirection();
	[field:NonSerialized]private static readonly North _NORTH = new North();
	[field:NonSerialized]private static readonly South _SOUTH = new South();
	[field:NonSerialized]private static readonly East _EAST = new East();
	[field:NonSerialized]private static readonly West _WEST = new West();
	[field:NonSerialized]private static readonly NorthEast _NORTH_EAST = new NorthEast();
	[field:NonSerialized]private static readonly NorthWest _NORTH_WEST = new NorthWest();
	[field:NonSerialized]private static readonly SouthEast _SOUTH_EAST = new SouthEast();
	[field:NonSerialized]private static readonly SouthWest _SOUTH_WEST = new SouthWest();
	//
	public abstract Tile GetTile(Level level, int x, int y);
	//
	public static Direction IntToDirection(int x, int y){
		if(y > 0){
			if(x > 0){
				return _NORTH_EAST;
			}
			if(x < 0){
				return _NORTH_WEST;
			}
			return _NORTH;
		}
		if(y < 0){
			if(x > 0){
				return _SOUTH_EAST;
			}
			if(x < 0){
				return _SOUTH_WEST;
			}
			return _SOUTH;
		}
		if(x > 0){
			if(y > 0){
				return _NORTH_EAST;
			}
			if(y < 0){
				return _SOUTH_EAST;
			}
			return _EAST;
		}
		if(x < 0){
			if(y > 0){
				return _NORTH_WEST;
			}
			if(y < 0){
				return _SOUTH_WEST;
			}
			return _WEST;
		}
		return _NULL_DIRECTION;
	}
	public static Direction IntToDirection(int unitX, int unitY, int targetX, int targetY){
		return IntToDirection(targetX - unitX, targetY - unitY);
	}
	public static Direction GetNullDirection(){
		return _NULL_DIRECTION;
	}
	public static Direction GetNorth(){
		return _NORTH;
	}
	public static Direction GetSouth(){
		return _SOUTH;
	}
	public static Direction GetEast(){
		return _EAST;
	}
	public static Direction GetWest(){
		return _WEST;
	}
	public static Direction GetNorthEast(){
		return _NORTH_EAST;
	}
	public static Direction GetNorthWest(){
		return _NORTH_WEST;
	}
	public static Direction GetSouthEast(){
		return _SOUTH_EAST;
	}
	public static Direction GetSouthWest(){
		return _SOUTH_WEST;
	}
}
