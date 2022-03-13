using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BluePrint : GridMap<BluePrint.Print>{
	public static class BLUEPRINT_DATA{
		private static readonly BluePrint[] _BLUEPRINTS;
		static BLUEPRINT_DATA(){
			_BLUEPRINTS = new BluePrint[]{
				new BluePrint(
					new int[,]{
						{1,1,1,1,1},
						{1,2,2,2,1},
						{1,2,2,2,1},
						{1,2,2,2,1},
						{1,1,1,1,1},
					},
					Print.PRINT_DATA.GetPrint,
					new Vector2Int[]{
						new Vector2Int(0,2),
						new Vector2Int(2,0),
						new Vector2Int(4,2),
						new Vector2Int(2,4),
					}
				),
			};
		}
		public static BluePrint GetBluePrint(int index){
			if(!CheckBounds(index)){
				return BluePrint.GetNullBluePrint();
			}
			return _BLUEPRINTS[index];
		}
		public static bool CheckBounds(int index){
			if(index < 0){
				return false;
			}
			if(index > _BLUEPRINTS.Length){
				return false;
			}
			return true;
		}
		public static Print GetNullPrint_Index(int index){
			return Print.GetNullPrint();
		}
	}
	//
	[Serializable]
	public abstract class Print{
		public static class PRINT_DATA{
			private static readonly Print[] _PRINTS;
			static PRINT_DATA(){
				_PRINTS = new Print[]{
					new TilePrint(Tile.Terrain.Null),
					new TilePrint(Tile.Terrain.Wall),
					new TilePrint(Tile.Terrain.Path)
				};
			}
			public static Print GetPrint(int index){
				if(!CheckBounds(index)){
					return Print.GetNullPrint();
				}
				return _PRINTS[index];;
			}
			public static bool CheckBounds(int index){
				if(index < 0){
					return false;
				}
				if(index > _PRINTS.Length){
					return false;
				}
				return true;
			}
		}
		[Serializable]
		public class NullPrint : Print{
			public NullPrint(){}
			public override void Spawn(Level level, ClassicGen master, int x, int y){}
			public override bool Check(Level level, ClassicGen master, int x, int y){
				return false;
			}
		}
		[field:NonSerialized]private static readonly NullPrint _NULL_PRINT = new NullPrint();
		public Print(){}
		public abstract void Spawn(Level level, ClassicGen master, int x, int y);
		public abstract bool Check(Level level, ClassicGen master, int x, int y);
		public static BluePrint.Print GetNullPrint(){
			return _NULL_PRINT;
		}
	}
	public class TilePrint : BluePrint.Print{
		private Tile.Terrain _terrain;
		public TilePrint(Tile.Terrain terrain){
			_terrain = terrain;
		}
		public override void Spawn(Level level, ClassicGen master, int x, int y){
			level.Set(x, y, new Tile(x, y, _terrain));
		}
		public override bool Check(Level level, ClassicGen master, int x, int y){
			switch(level.Get(x, y).GetTerrain()){
				default: return false;
				case Tile.Terrain.Null: return true;
				case Tile.Terrain.Wall: return true;
				case Tile.Terrain.Path: return false;
			}
		}
	}
	//
	[Serializable]
	public class NullBluePrint : BluePrint{
		private const int _NULL_WIDTH = 0;
		private const int _NULL_HEIGHT = 0;
		public NullBluePrint() : base(
			_NULL_WIDTH,
			_NULL_HEIGHT
		){}
		public override void Spawn(Level level, ClassicGen master, int positionX, int positionY){}
	}
	public override BluePrint.Print GetNullGridObject(){
		return BluePrint.Print.GetNullPrint();
	}
	[field:NonSerialized]private static readonly NullBluePrint _NULL_BLUEPRINT = new NullBluePrint();
	private static readonly Vector3 _BLUEPRINT_ORIGIN_POSITION = Vector3.zero;
	private static readonly Vector3 _BLUEPRINT_CELL_DIMENSIONS = Vector3.one;
	private const float _BLUEPRINT_CELL_SIZE = 1f;
	private const float _BLUEPRINT_CELL_OFFSET = 0.5f;
	//
	private Vector2Int[] _spawns;
	public BluePrint(int[,] map, Func<int, Print> GetPrintData, Vector2Int[] spawns) : base(
		map.GetLength(0), 
		map.GetLength(1),
		_BLUEPRINT_CELL_SIZE,
		_BLUEPRINT_CELL_OFFSET,
		_BLUEPRINT_ORIGIN_POSITION,
		_BLUEPRINT_CELL_DIMENSIONS,
		(int x, int y) => {
			return GetPrintData(map[x, y]);
		}
	){
		_spawns = spawns;
	}
	public BluePrint(int width, int height) : base(
		width,
		height,
		_BLUEPRINT_CELL_SIZE,
		_BLUEPRINT_CELL_OFFSET,
		_BLUEPRINT_ORIGIN_POSITION,
		_BLUEPRINT_CELL_DIMENSIONS
	){
		_spawns = new Vector2Int[0];
	}
	public virtual void Spawn(Level level, ClassicGen master, int positionX, int positionY){
		/*
		List<int> final = new List<int>();
		for(int i = 0; i < _spawns.Length; i++){
			if(Check(level, (positionX - _spawns[i].x), (positionY - _spawns[i].y))){
				final.Add(i);
			}
		}
		if(final.Count <= 0){
			return;
		}
		Vector2Int spawn = final[UnityEngine.Random.Range(0, final.Count)];
		for(int x = 0; x < GetWidth(); x++){
			for(int y = 0; y < GetHeight(); y++){
				Get(x, y).Spawn(level, master, (positionX + x), (positionY + y));
			}
		}
		*/
	}
	public bool Check(Level level, int spawnX, int spawnY){
		/*
		for(int x = 0; x < GetWidth(); x++){
			for(int y = 0; y < GetHeight(); y++){
				if(!Get(x, y).Check(level, master, (positionX + x), (positionY + y))){
					return false;
				}
			}
		}
		return true;
		*/
		return true;
	}
	public static BluePrint GetNullBluePrint(){
		return _NULL_BLUEPRINT;
	}
	public static BluePrint GetNullBluePrint(int x, int y){
		return _NULL_BLUEPRINT;
	}
}