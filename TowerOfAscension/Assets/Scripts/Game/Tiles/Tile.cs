using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Tile : GridMap<Tile>.Node{
	public interface IHasUnits{
		event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitAdded;
		event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitRemoved;
		void AddUnit(Game game, Register<Unit>.ID id);
		bool RemoveUnit(Game game, Register<Unit>.ID id);
		List<Unit> GetUnits(Game game);
	}
	public interface IPrintable{
		void Print(Game game, Unit master, BluePrint.Print print, Tile tile, int x, int y);
		bool CanPrint(Game game, Unit master, int x, int y);
	}
	public interface IConnectable{
		bool CanConnect(Game game, Unit master, int x, int y);
	}
	public interface IWalkable : IHasUnits{
		void Walk(Game game, Unit unit);
		bool CanWalk(Game game, Unit unit);
	}
	public interface ILightable{
		void SetLight(int light);
		int GetLight();
	}
	public interface IDiscoverable{
		void Discover(Game game, Unit unit);
	}
	public interface ITargetable{
		List<Unit> GetTargets(Game game, Unit unit);
	}
	[Serializable]
	public class NullTile : 
		Tile,
		LevelMeshManager.ITileMeshData,
		LightMeshManager.ILightMeshData,
		Tile.IHasUnits,
		Tile.IPrintable,
		Tile.IConnectable,
		Tile.IWalkable,
		Tile.ILightable,
		Level.ILightControl,
		Tile.IDiscoverable,
		Unit.IInteractable,
		Unit.IAttackable,
		Unit.IHostileTarget
		{
		[field:NonSerialized]public event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitAdded;
		[field:NonSerialized]public event EventHandler<Register<Unit>.OnObjectChangedEventArgs> OnUnitRemoved;
		private const int _NULL_X = -1;
		private const int _NULL_Y = -1;
		private const int _NULL_INDEX = -1;
		private const int _NULL_FACTOR = 0;
		public NullTile(){}
		public override void GetXY(out int x, out int y){
			x = _NULL_X;
			y = _NULL_Y;
		}
		public int GetAtlasIndex(){
			return _NULL_INDEX;
		}
		public int GetUVFactor(){
			return _NULL_FACTOR;
		}
		public int GetLightAtlasIndex(){
			return _NULL_INDEX;
		}
		public int GetLightUVFactor(){
			return _NULL_FACTOR;
		}
		public void AddUnit(Game game, Register<Unit>.ID id){}
		public bool RemoveUnit(Game game, Register<Unit>.ID id){
			return false;
		}
		public List<Unit> GetUnits(Game game){
			return new List<Unit>();
		}
		public void Print(Game game, Unit master, BluePrint.Print print, Tile tile, int x, int y){
			game.GetLevel().Set(x, y, tile);
			print.OnSpawn(game, master, tile, x, y);
		}
		public bool CanPrint(Game game, Unit master, int x, int y){
			return game.GetLevel().CheckBounds(x, y);
		}
		public bool CanConnect(Game game, Unit master, int x, int y){
			return false;
		}
		public void Walk(Game game, Unit unit){}
		public bool CanWalk(Game game, Unit unit){
			return false;
		}
		public void SetLight(int light){}
		public int GetLight(){
			return _NULL_INDEX;
		}
		public bool CheckTransparency(Game game){
			return false;
		}
		public void Discover(Game game, Unit unit){}
		public void Interact(Game game, Unit unit){}
		public void Attacked(Game game, Unit unit, int attack){}
		public void OnAttacked(){}
		public bool CheckHostility(Game game, Unit unit){
			return false;
		}
	}
	[field:NonSerialized]private static readonly NullTile _NULL_TILE = new NullTile();
	public Tile(int x, int y) : base(x, y){}
	public Tile(){}
	public virtual LevelMeshManager.ITileMeshData GetTileMeshData(){
		return _NULL_TILE;
	}
	public virtual LightMeshManager.ILightMeshData GetLightMeshData(){
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
	public virtual ILightable GetLightable(){
		return _NULL_TILE;
	}
	public virtual Level.ILightControl GetLightControl(){
		return _NULL_TILE;
	}
	public virtual IDiscoverable GetDiscoverable(){
		return _NULL_TILE;
	}
	public virtual Unit.IInteractable GetInteractable(){
		return _NULL_TILE;
	}
	public virtual Unit.IAttackable GetAttackable(){
		return _NULL_TILE;
	}
	public virtual Unit.IHostileTarget GetHostileTarget(){
		return _NULL_TILE;
	}
	public static Tile GetNullTile(){
		return _NULL_TILE;
	}
}
