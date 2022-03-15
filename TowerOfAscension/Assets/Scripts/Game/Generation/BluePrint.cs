using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BluePrint : GridMap<BluePrint.Print>{
	// BLUEPRINT_DATA
	public static class BLUEPRINT_DATA{
		private static readonly BluePrint[] _BLUEPRINTS;
		static BLUEPRINT_DATA(){
			_BLUEPRINTS = new BluePrint[]{
				new BluePrint(
					new int[,]{
						{1,1,3,1,1},
						{1,4,2,2,1},
						{3,2,2,2,3},
						{1,2,2,2,1},
						{1,1,3,1,1},
					},
					Print.PRINT_DATA.GetPrint,
					new Reposition[]{
						new Reposition(0,2),
						new Reposition(2,0),
						new Reposition(4,2),
						new Reposition(2,4),
					}
				),
				new BluePrint(
					new int[,]{
						{1,3,1},
						{1,2,1},
						{1,2,1},
						{3,2,3},
						{1,2,1},
						{1,2,1},
						{1,3,1},
					},
					Print.PRINT_DATA.GetPrint,
					new Reposition[]{
						new Reposition(0,1),
						new Reposition(6,1),
					}
				),
				new BluePrint(
					new int[,]{
						{1,1,1,1},
						{1,4,2,1},
						{1,2,2,1},
						{1,1,1,1},
					},
					Print.PRINT_DATA.GetPrint,
					new Reposition[]{
						new Reposition(0,1),
						new Reposition(0,2),
						new Reposition(1,0),
						new Reposition(2,0),
						new Reposition(3,1),
						new Reposition(3,2),
						new Reposition(1,3),
						new Reposition(2,3),
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
		public static BluePrint GetRandomBluePrint(){
			return _BLUEPRINTS[GetRandomIndex()];
		}
		public static int GetRandomIndex(){
			return UnityEngine.Random.Range(0, _BLUEPRINTS.Length);
		}
		public static bool CheckBounds(int index){
			if(index < 0){
				return false;
			}
			if(index >= _BLUEPRINTS.Length){
				return false;
			}
			return true;
		}
		public static Print GetNullPrint_Index(int index){
			return Print.GetNullPrint();
		}
	}
	// PRINT_DATA
	[Serializable]
	public abstract class Print{
		public static class PRINT_DATA{
			private static readonly Print[] _PRINTS;
			static PRINT_DATA(){
				_PRINTS = new Print[]{
					new NullPrint(),
					new WallPrint(),
					new PathPrint(),
					new WallPrint(new int[]{0,1}),
					new PathPrint(new int[]{3}),
				};
			}
			public static Print GetPrint(int index){
				if(!CheckBounds(index)){
					return Print.GetNullPrint();
				}
				return _PRINTS[index];
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
		//
		[Serializable]
		public class NullPrint : Print{
			public NullPrint(){}
			public override void Spawn(Level level, ClassicGen master, int x, int y){}
			public override bool Check(Level level, ClassicGen master, int x, int y){
				return false;
			}
			public override void OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y){}
		}
		[field:NonSerialized]private static readonly NullPrint _NULL_PRINT = new NullPrint();
		public Print(){}
		public abstract void Spawn(Level level, ClassicGen master, int x, int y);
		public abstract bool Check(Level level, ClassicGen master, int x, int y);
		public abstract void OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y);
		public static bool Default_Check(Level level, ClassicGen master, int x, int y){
			return level.Get(x, y).GetPrintable().CanPrint(level, master, x, y);
		}
		public static void Default_OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y, int[] spawns){
			for(int  i = 0; i < spawns.Length; i++){
				ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(spawns[i], x, y).AddToMaster(level, master);
			}
		}
		public static BluePrint.Print GetNullPrint(){
			return _NULL_PRINT;
		}
	}
	[Serializable]
	public class NullPrint : BluePrint.Print{
		public NullPrint(){}
		public override void Spawn(Level level, ClassicGen master, int x, int y){
			level.Get(x, y).GetPrintable().Print(level, master, this, Tile.GetNullTile(), x, y);
		}
		public override bool Check(Level level, ClassicGen master, int x, int y){
			return level.Get(x, y).GetPrintable().CanPrint(level, master, x, y);
		}
		public override void OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y){}
	}
	[Serializable]
	public class PathPrint : BluePrint.Print{
		private int[] _spawns;
		public PathPrint(int[] spawns){
			_spawns = spawns;
		}
		public PathPrint(){
			_spawns = new int[0];
		}
		public override void Spawn(Level level, ClassicGen master, int x, int y){
			level.Get(x, y).GetPrintable().Print(level, master, this, new PathTile(x, y), x, y);
		}
		public override bool Check(Level level, ClassicGen master, int x, int y){
			return Print.Default_Check(level, master, x, y);
		}
		public override void OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y){
			Print.Default_OnSpawn(level, master, tile, x, y, _spawns);
		}
	}
	[Serializable]
	public class WallPrint : BluePrint.Print{
		private int[] _spawns;
		public WallPrint(int[] spawns){
			_spawns = spawns;
		}
		public WallPrint(){
			_spawns = new int[0];
		}
		public override void Spawn(Level level, ClassicGen master, int x, int y){
			level.Get(x, y).GetPrintable().Print(level, master, this, new WallTile(x, y), x, y);
		}
		public override bool Check(Level level, ClassicGen master, int x, int y){
			return Print.Default_Check(level, master, x, y);
		}
		public override void OnSpawn(Level level, ClassicGen master, Tile tile, int x, int y){
			Print.Default_OnSpawn(level, master, tile, x, y, _spawns);
		}
	}
	//
	[Serializable]
	public class Reposition{
		[Serializable]
		public class NullReposition : Reposition{
			public NullReposition(){}
			public override void Spawn(Level level, BluePrint bluePrint, ClassicGen master, int positionX, int positionY){}
			public override bool Check(Level level, BluePrint bluePrint, ClassicGen master, int positionX, int positionY){
				return false;
			}
		}
		[field:NonSerialized]private static readonly NullReposition _NULL_REPOSITION = new NullReposition();
		private int _x;
		private int _y;
		public Reposition(int x, int y){
			_x = x;
			_y = y;
		}
		public Reposition(){}
		public virtual void Spawn(Level level, BluePrint bluePrint, ClassicGen master, int positionX, int positionY){
			int spawnX = positionX - _x;
			int spawnY = positionY - _y;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					bluePrint.Get(x, y).Spawn(level, master, (x + spawnX), (y + spawnY));
				}
			}
			const int CONNECTOR_INDEX = 2;
			ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, positionX, positionY).AddToMaster(level, master);
		}
		public virtual bool Check(Level level, BluePrint bluePrint, ClassicGen master, int positionX, int positionY){
			int spawnX = positionX - _x;
			int spawnY = positionY - _y;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					if(!bluePrint.Get(x, y).Check(level, master, (x + spawnX), (y + spawnY))){
						return false;
					}
				}
			}
			return true;
		}
		public static Reposition GetNullReposition(){
			return _NULL_REPOSITION;
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
	private Reposition[] _repositions;
	public BluePrint(int[,] map, Func<int, Print> GetPrintData, Reposition[] repositions) : base(
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
		_repositions = repositions;
	}
	public BluePrint(int width, int height) : base(
		width,
		height,
		_BLUEPRINT_CELL_SIZE,
		_BLUEPRINT_CELL_OFFSET,
		_BLUEPRINT_ORIGIN_POSITION,
		_BLUEPRINT_CELL_DIMENSIONS
	){
		_repositions = new Reposition[0];
	}
	public virtual void Spawn(Level level, ClassicGen master, int positionX, int positionY){
		List<int> final = new List<int>();
		for(int i = 0; i < _repositions.Length; i++){
			if(_repositions[i].Check(level, this, master, positionX, positionY)){
				final.Add(i);
			}
		}
		if(final.Count > 0){
			_repositions[final[UnityEngine.Random.Range(0, final.Count)]].Spawn(level, this, master, positionX, positionY);
			master.AddBuild();
		}
	}
	public bool Check(Level level, int spawnX, int spawnY){
		return true;
	}
	public static BluePrint GetNullBluePrint(){
		return _NULL_BLUEPRINT;
	}
	public static BluePrint GetNullBluePrint(int x, int y){
		return _NULL_BLUEPRINT;
	}
}