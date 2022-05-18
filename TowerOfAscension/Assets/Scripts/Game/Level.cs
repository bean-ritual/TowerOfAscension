using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Level : GridMap<Tile>{
	public interface ILightControl{
		bool CheckTransparency(Game game);
	}
	public interface ILightSource{
		int GetLightRange(Game game);
	}
	[Serializable]
	public class NullLevel : Level{
		private const int _NULL_WIDTH = 0;
		private const int _NULL_HEIGHT = 0;
		public NullLevel() : base(
			_NULL_WIDTH,
			_NULL_HEIGHT
		){}
		public override bool Process(Game game){
			return false;
		}
		public override void LightUpdate(Game game, Unit unit){}
		public override bool Set(int x, int y, Tile tile){
			return false;
		}
		public override Tile Get(int x, int y){
			return Tile.GetNullTile();
		}
		public override bool NextTurn(Game game){
			return false;
		}
		public override void ResetTurn(){}
		public override Inventory GetUnits(){
			return Inventory.GetNullInventory();
		}
	}
	public override Tile GetNullGridObject(){
		return Tile.GetNullTile();
	}
	[field:NonSerialized]public event EventHandler<EventArgs> OnNextTurn;
	[field:NonSerialized]public event EventHandler<EventArgs> OnLightUpdate;
	[field:NonSerialized]private static readonly NullLevel _NULL_LEVEL = new NullLevel();
	private static readonly Vector3 _LEVEL_ORIGIN_POSITION = Vector3.zero;
	private static readonly Vector3 _LEVEL_CELL_DIMENSIONS = Vector3.one;
	private const float _LEVEL_CELL_SIZE = 1f;
	private const float _LEVEL_CELL_OFFSET = 0.5f;
	//
	private int _index;
	private Inventory _units = Inventory.GetNullInventory();
	//
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
		_units = new Inventory();
	}
	//
	public virtual bool Process(Game game){
		return _units.Get(_index).Process(game);
	}
	public virtual void LightUpdate(Game game, Unit unit){
		int lightRange = unit.GetTag(game, Tag.ID.Light).GetIGetIntValue1().GetIntValue1(game, unit);
		UnityEngine.Debug.Log(lightRange + " " + unit.GetTag(game, Tag.ID.Name).GetIGetStringValue1().GetStringValue1(game, unit));
		for(int x = 0; x < GetWidth(); x++){
			for(int y = 0; y < GetHeight(); y++){
				Get(x, y).GetLightable().SetLight(0);
			}
		}
		Tile origin = unit.GetTag(game, Tag.ID.Position).GetIGetTile().GetTile(game, unit);
		origin.GetXY(out int sourceX, out int sourceY);
		origin.GetLightable().SetLight(lightRange);
		Tag discover = unit.GetTag(game, Tag.ID.Discoverer);
		discover.GetIInputTile().Input(game, unit, origin);
		List<Tile> tiles = CalculateFov(sourceX, sourceY, lightRange, (int range, Tile tile) => {
			tile.GetLightable().SetLight(lightRange - (range - 1));
			discover.GetIInputTile().Input(game, unit, tile);
			return tile.GetLightControl().CheckTransparency(game);
		});
		OnLightUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual bool NextTurn(Game game){
		_units.Get(_index).EndTurn(game);
		int count = _units.GetCount();
		if(count <= 0){
			return false;
		}
		_index = (_index + 1) % count;
		OnNextTurn?.Invoke(this, EventArgs.Empty);
		return (_index > 0);
	}
	public virtual void ResetTurn(){
		_index = -1;
	}
	public virtual Inventory GetUnits(){
		return _units;
	}
	public static Level GetNullLevel(){
		return _NULL_LEVEL;
	}
	private static Tile CreateTile(int x, int y){
		return new Tile.NullTile(x, y);
	}
}
