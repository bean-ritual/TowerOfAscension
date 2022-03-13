using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Level : GridMap<Tile>{
	[Serializable]
	public class NullLevel : Level{
		private const int _NULL_WIDTH = 0;
		private const int _NULL_HEIGHT = 0;
		public NullLevel() : base(
			_NULL_WIDTH,
			_NULL_HEIGHT
		){}
		public override bool Process(){
			return false;
		}
		public override bool Set(int x, int y, Tile tile){
			return false;
		}
		public override Tile Get(int x, int y){
			return Tile.GetNullTile();
		}
		public override Register<Unit> GetUnits(){
			return UnitRegister.GetNullUnitRegister();
		}
		public override bool NextTurn(){
			return false;
		}
	}
	public override Tile GetNullGridObject(){
		return Tile.GetNullTile();
	}
	[field:NonSerialized]private static readonly NullLevel _NULL_LEVEL = new NullLevel();
	private static readonly Vector3 _LEVEL_ORIGIN_POSITION = Vector3.zero;
	private static readonly Vector3 _LEVEL_CELL_DIMENSIONS = Vector3.one;
	private const float _LEVEL_CELL_SIZE = 1f;
	private const float _LEVEL_CELL_OFFSET = 0.5f;
	//
	private int _index;
	private Register<Unit> _units;
	public Level(int width, int height) : 
	base(
		width, 
		height,
		_LEVEL_CELL_SIZE,
		_LEVEL_CELL_OFFSET,
		_LEVEL_ORIGIN_POSITION,
		_LEVEL_CELL_DIMENSIONS,
		CreateTile
	){	
		_index = 0;
		_units = new UnitRegister();
	}
	//Return True: Continues Iteration
	//Return False: Waiting for Input
	public virtual bool Process(){
		return _units.Get(_index).GetProcessable().Process(this);
	}
	public virtual Register<Unit> GetUnits(){
		return _units;
	}
	public virtual bool NextTurn(){
		int count = _units.GetCount();
		if(count <= 0){
			return false;
		}
		_index = (_index + 1) % count;
		return (_index > 0);
	}
	public static Level GetNullLevel(){
		return _NULL_LEVEL;
	}
	private static Tile CreateTile(int x, int y){
		return Tile.GetNullTile();
	}
}
