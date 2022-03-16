using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Tile{
	public interface IHasUnits{
		void AddUnit(Register<Unit>.ID id);
		bool RemoveUnit(Register<Unit>.ID id);
	}
	public interface IPrintable{
		void Print(Level level, Unit master, BluePrint.Print print, Tile tile, int x, int y);
		bool CanPrint(Level level, Unit master, int x, int y);
	}
	public interface IConnectable{
		bool CanConnect(Level level, Unit master, int x, int y);
	}
	public interface IWalkable{
		void Walk(Level level, Unit unit);
		bool CanWalk(Level level, Unit unit);
	}
	[Serializable]
	public class NullTile : 
		Tile,
		LevelMeshManager.ITileMeshData,
		Tile.IHasUnits,
		Tile.IPrintable,
		Tile.IConnectable,
		Tile.IWalkable
		{
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		public NullTile(){}
		public override void GetXY(out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public int GetAtlasIndex(){
			const int NULL_INDEX = -1;
			return NULL_INDEX;
		}
		public int GetUVFactor(){
			const int NULL_FACTOR = 0;
			return NULL_FACTOR;
		}
		public void AddUnit(Register<Unit>.ID id){}
		public bool RemoveUnit(Register<Unit>.ID id){
			return false;
		}
		public void Print(Level level, Unit master, BluePrint.Print print, Tile tile, int x, int y){
			level.Set(x, y, tile);
			print.OnSpawn(level, master, tile, x, y);
		}
		public bool CanPrint(Level level, Unit master, int x, int y){
			return level.CheckBounds(x, y);
		}
		public bool CanConnect(Level level, Unit master, int x, int y){
			return false;
		}
		public void Walk(Level level, Unit unit){}
		public bool CanWalk(Level level, Unit unit){
			return false;
		}
	}
	[field:NonSerialized]private static readonly NullTile _NULL_TILE = new NullTile();
	protected int _x;
	protected int _y;
	public Tile(int x, int y){
		_x = x;
		_y = y;
	}
	public Tile(){}
	public virtual void GetXY(out int x, out int y){
		x = _x;
		y = _y;
	}
	public virtual LevelMeshManager.ITileMeshData GetTileMeshData(){
		return _NULL_TILE;
	}
	public virtual IHasUnits GetHasUnits(){
		return _NULL_TILE;
	}
	public virtual IPrintable GetPrintable(){
		return _NULL_TILE;
	}
	public virtual IConnectable GetConnectable(){
		return _NULL_TILE;
	}
	public virtual IWalkable GetWalkable(){
		return _NULL_TILE;
	}
	public static Tile GetNullTile(){
		return _NULL_TILE;
	}
}