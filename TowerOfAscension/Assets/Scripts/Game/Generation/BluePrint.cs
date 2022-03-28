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
						{3,2,2,5,3},
						{1,2,5,2,1},
						{1,1,3,1,1},
					},
					Print.PRINT_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,2),
						new Vector2Int(2,0),
						new Vector2Int(4,2),
						new Vector2Int(2,4),
					})
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
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(6,1),
					})
				),
				new BluePrint(
					new int[,]{
						{1,3,1,1},
						{1,4,2,3},
						{3,2,5,1},
						{1,1,3,1},
					},
					Print.PRINT_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(0,2),
						new Vector2Int(1,0),
						new Vector2Int(2,0),
						new Vector2Int(3,1),
						new Vector2Int(3,2),
						new Vector2Int(1,3),
						new Vector2Int(2,3),
					})
				),
				new BluePrint(
					new int[,]{
						{1,3,1},
						{1,2,1},
						{3,2,3},
						{1,2,1},
						{3,2,3},
						{1,2,1},
						{3,2,3},
						{1,2,1},
						{1,3,1},
					},
					Print.PRINT_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(8,1),
					})
				),
				new BluePrint(
					new int[,]{
						{1,3,1},
						{3,6,3},
						{1,3,1},
					},
					Print.PRINT_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(1,0),
						new Vector2Int(2,1),
						new Vector2Int(1,2),
					})
				),
			};
		}
		public static BluePrint GetBluePrint(int index){
			if(!CheckBounds(index)){
				return BluePrint.GetNullBluePrint();
			}
			return _BLUEPRINTS[index];
		}
		public static int GetBluePrintIndex(BluePrintID id){
			switch(id){
				default: return -1;
				case BluePrintID.Null: return -1;
				case BluePrintID.BasicRoom: return 0;
				case BluePrintID.BasicHallway: return 1;
				case BluePrintID.SmallRoom: return 2;
				case BluePrintID.LongHallway: return 3;
				case BluePrintID.TinyRoom: return 4;
			}
		}
		public static BluePrint GetBluePrint(BluePrintID id){
			return GetBluePrint(GetBluePrintIndex(id));
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
	public enum BluePrintID{
		Null,
		BasicRoom,
		BasicHallway,
		SmallRoom,
		LongHallway,
		TinyRoom,
	};
	// PRINT_DATA
	[Serializable]
	public abstract class Print : GridMap<Print>.Node{
		public static class PRINT_DATA{
			private static readonly Print[] _PRINTS;
			static PRINT_DATA(){
				_PRINTS = new Print[]{
					new NullPrint(),
					new WallPrint(),
					new PathPrint(),
					new WallPrint(new int[]{0,1}),
					new PathPrint(new int[]{3}),
					new PathPrint(new int[]{4}),
					new PathPrint(new int[]{5}),
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
			public override void Spawn(Game game, Unit master, int x, int y){}
			public override bool Check(Game game, Unit master, int x, int y){
				return true;
			}
			public override void OnSpawn(Game game, Unit master, Tile tile, int x, int y){}
		}
		[field:NonSerialized]private static readonly NullPrint _NULL_PRINT = new NullPrint();
		public Print(){}
		public abstract void Spawn(Game game, Unit master, int x, int y);
		public abstract bool Check(Game game, Unit master, int x, int y);
		public abstract void OnSpawn(Game game, Unit master, Tile tile, int x, int y);
		public static bool Default_Check(Game game, Unit master, int x, int y){
			return game.GetLevel().Get(x, y).GetPrintable().CanPrint(game, master, x, y);
		}
		public static void Default_OnSpawn(Game game, Unit master, Tile tile, int x, int y, int[] spawns){
			for(int  i = 0; i < spawns.Length; i++){
				ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(spawns[i], x, y).AddToMaster(game, master);
			}
		}
		public static BluePrint.Print GetNullPrint(){
			return _NULL_PRINT;
		}
	}
	[Serializable]
	public class BlankPrint : BluePrint.Print{
		public BlankPrint(){}
		public override void Spawn(Game game, Unit master, int x, int y){
			game.GetLevel().Get(x, y).GetPrintable().Print(game, master, this, Tile.GetNullTile(), x, y);
		}
		public override bool Check(Game game, Unit master, int x, int y){
			return game.GetLevel().Get(x, y).GetPrintable().CanPrint(game, master, x, y);
		}
		public override void OnSpawn(Game game, Unit master, Tile tile, int x, int y){}
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
		public override void Spawn(Game game, Unit master, int x, int y){
			game.GetLevel().Get(x, y).GetPrintable().Print(game, master, this, new PathTile(x, y), x, y);
		}
		public override bool Check(Game game, Unit master, int x, int y){
			return Print.Default_Check(game, master, x, y);
		}
		public override void OnSpawn(Game game, Unit master, Tile tile, int x, int y){
			Print.Default_OnSpawn(game, master, tile, x, y, _spawns);
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
		public override void Spawn(Game game, Unit master, int x, int y){
			game.GetLevel().Get(x, y).GetPrintable().Print(game, master, this, new WallTile(x, y), x, y);
		}
		public override bool Check(Game game, Unit master, int x, int y){
			return Print.Default_Check(game, master, x, y);
		}
		public override void OnSpawn(Game game, Unit master, Tile tile, int x, int y){
			Print.Default_OnSpawn(game, master, tile, x, y, _spawns);
		}
	}
	//
	[Serializable]
	public class Reposition{
		public static class REPOSITION_DATA{
			public static Reposition[] GetMultiRepositions(Vector2Int[] positions){
				List<Reposition> repositions = new List<Reposition>(positions.Length * 2);
				for(int i = 0; i < positions.Length; i++){
					repositions.Add(new Reposition(positions[i].x, positions[i].y));
					repositions.Add(new Transposition(positions[i].x, positions[i].y));
				}
				return repositions.ToArray();
			}
		}
		[Serializable]
		public class NullReposition : Reposition{
			public NullReposition(){}
			public override void Spawn(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){}
			public override bool Check(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
				return false;
			}
		}
		[field:NonSerialized]private static readonly NullReposition _NULL_REPOSITION = new NullReposition();
		protected int _x;
		protected int _y;
		public Reposition(int x, int y){
			_x = x;
			_y = y;
		}
		public Reposition(){}
		public virtual void Spawn(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int spawnX = positionX - _x;
			int spawnY = positionY - _y;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					bluePrint.Get(x, y).Spawn(game, master, (x + spawnX), (y + spawnY));
				}
			}
			const int CONNECTOR_INDEX = 2;
			ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, positionX, positionY).AddToMaster(game, master);
		}
		public virtual bool Check(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int spawnX = positionX - _x;
			int spawnY = positionY - _y;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					if(!bluePrint.Get(x, y).Check(game, master, (x + spawnX), (y + spawnY))){
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
	[Serializable]
	public class Transposition : Reposition{
		public Transposition(int x, int y) : base(x, y){}
		public override void Spawn(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int spawnX = positionX - _y;
			int spawnY = positionY - _x;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					bluePrint.Get(x, y).Spawn(game, master, (y + spawnX), (x + spawnY));
				}
			}
			const int CONNECTOR_INDEX = 2;
			ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, positionX, positionY).AddToMaster(game, master);
		}
		public override bool Check(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int spawnX = positionX - _y;
			int spawnY = positionY - _x;
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					if(!bluePrint.Get(x, y).Check(game, master, (y + spawnX), (x + spawnY))){
						return false;
					}
				}
			}
			return true;
		}
	}
	[Serializable]
	public class FlippedReposition : Reposition{
		public FlippedReposition(int x, int y) : base(x, y){}
		public override void Spawn(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int flipWidth = (bluePrint.GetWidth() - 1);
			int flipHeight = (bluePrint.GetHeight() - 1);
			int spawnX = positionX - (flipWidth - _x);
			int spawnY = positionY - (flipHeight - _y);
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					bluePrint.Get((flipWidth - x), (flipHeight - y)).Spawn(game, master, (x + spawnX), (y + spawnY));
				}
			}
			const int CONNECTOR_INDEX = 2;
			ClassicGen.Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, positionX, positionY).AddToMaster(game, master);
		}
		public override bool Check(Game game, BluePrint bluePrint, Unit master, int positionX, int positionY){
			int flipWidth = (bluePrint.GetWidth() - 1);
			int flipHeight = (bluePrint.GetHeight() - 1);
			int spawnX = positionX - (flipWidth - _x);
			int spawnY = positionY - (flipHeight - _y);
			for(int x = 0; x < bluePrint.GetWidth(); x++){
				for(int y = 0; y < bluePrint.GetHeight(); y++){
					if(!bluePrint.Get((flipWidth - x), (flipHeight - y)).Check(game, master, (x + spawnX), (y + spawnY))){
						return false;
					}
				}
			}
			return true;
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
		public override void Spawn(Game game, Unit master, int positionX, int positionY){}
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
	public virtual void Spawn(Game game, Unit master, int positionX, int positionY){
		List<int> final = new List<int>();
		for(int i = 0; i < _repositions.Length; i++){
			if(_repositions[i].Check(game, this, master, positionX, positionY)){
				final.Add(i);
			}
		}
		if(final.Count > 0){
			_repositions[final[UnityEngine.Random.Range(0, final.Count)]].Spawn(game, this, master, positionX, positionY);
			master.GetClassicGen().AddBuild();
		}
	}
	public bool Check(Game game, int spawnX, int spawnY){
		return true;
	}
	public static BluePrint GetNullBluePrint(){
		return _NULL_BLUEPRINT;
	}
	public static BluePrint GetNullBluePrint(int x, int y){
		return _NULL_BLUEPRINT;
	}
}