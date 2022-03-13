using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Tile{
	[Serializable]
	public class NullTile : Tile{
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullTile(){}
		public override void GetXY(out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public override void SetTerrain(Terrain terrain){}
		public override Terrain GetTerrain(){
			const Terrain NULL_TERRAIN = Terrain.Null;
			return NULL_TERRAIN;
		}
		public override void AddUnit(Register<Unit>.ID id){}
		public override bool RemoveUnit(Register<Unit>.ID id){
			return false;
		}
		public override int GetAtlasIndex(){
			return -1;
		}
	}
	public enum Terrain{
		Null,
		Wall,
		Path,
	};
	[field:NonSerialized]private static readonly NullTile _NULL_TILE = new NullTile();
	private int _x;
	private int _y;
	private Terrain _terrain;
	private List<Register<Unit>.ID> _units;
	public Tile(int x, int y, Terrain terrain){
		_x = x;
		_y = y;
		_terrain = terrain;
		_units = new List<Register<Unit>.ID>();
	}
	public Tile(){}
	public virtual void GetXY(out int x, out int y){
		x = _x;
		y = _y;
	}
	public virtual void SetTerrain(Terrain terrain){
		_terrain = terrain;
	}
	public virtual Terrain GetTerrain(){
		return _terrain;
	}
	public virtual void AddUnit(Register<Unit>.ID id){
		_units.Add(id);
	}
	public virtual bool RemoveUnit(Register<Unit>.ID id){
		return _units.Remove(id);
	}
	public virtual int GetAtlasIndex(){
		switch(_terrain){
			default: return -1;
			case Terrain.Null: return -1;
			case Terrain.Path: return 1;
			case Terrain.Wall: return 2;
		}
	}
	public static Tile GetNullTile(){
		return _NULL_TILE;
	}
}
