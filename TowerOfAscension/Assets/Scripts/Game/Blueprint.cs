using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BluePrint : 
	Map.BaseMap,
	ISpawn
	{
	public static class BLUEPRINT_DATA{
		private static readonly Map[] _BLUEPRINTS;
		static BLUEPRINT_DATA(){
			_BLUEPRINTS = new BluePrint[]{
				new BluePrint(
					1,
					new int[,]{
						{1,1,3,1,1},
						{1,4,2,2,1},
						{3,2,2,5,3},
						{1,2,5,2,1},
						{1,1,3,1,1},
					},
					PrintTile.PRINT_TILE_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,2),
						new Vector2Int(2,0),
						new Vector2Int(4,2),
						new Vector2Int(2,4),
					})
				),
				new BluePrint(
					1,
					new int[,]{
						{1,3,3,1},
						{1,2,2,1},
						{1,2,2,1},
						{3,2,2,3},
						{1,2,2,1},
						{1,2,2,1},
						{1,3,3,1},
					},
					PrintTile.PRINT_TILE_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(0,2),
						new Vector2Int(6,1),
						new Vector2Int(6,2),
					})
				),
				new BluePrint(
					1,
					new int[,]{
						{1,3,1,1},
						{1,4,2,3},
						{3,2,5,1},
						{1,1,3,1},
					},
					PrintTile.PRINT_TILE_DATA.GetPrint,
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
					1,
					new int[,]{
						{1,3,3,1},
						{1,2,2,1},
						{3,2,2,3},
						{1,2,2,1},
						{3,2,2,3},
						{1,2,2,1},
						{3,2,2,3},
						{1,2,2,1},
						{1,3,3,1},
					},
					PrintTile.PRINT_TILE_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(0,2),
						new Vector2Int(8,1),
						new Vector2Int(8,2),
					})
				),
				new BluePrint(
					1,
					new int[,]{
						{1,3,1},
						{3,6,3},
						{1,3,1},
					},
					PrintTile.PRINT_TILE_DATA.GetPrint,
					Reposition.REPOSITION_DATA.GetMultiRepositions(new Vector2Int[]{
						new Vector2Int(0,1),
						new Vector2Int(1,0),
						new Vector2Int(2,1),
						new Vector2Int(1,2),
					})
				),
			};
		}
		public static Map GetBluePrint(int index){
			if(index < 0 || index >= _BLUEPRINTS.Length){
				return Map.GetNullMap();
			}
			return _BLUEPRINTS[index];
		}
		public static Map GetRandomBluePrint(){
			return _BLUEPRINTS[GetRandomIndex()];
		}
		public static int GetRandomIndex(){
			return UnityEngine.Random.Range(0, _BLUEPRINTS.Length);
		}
		public static bool CheckBounds(int index){
			if(index < 0 || index >= _BLUEPRINTS.Length){
				return false;
			}
			return true;
		}
	}
	//
	[Serializable]
	public abstract class Reposition{
		public static class REPOSITION_DATA{
			public static Reposition[] GetMultiRepositions(Vector2Int[] positions){
				List<Reposition> repositions = new List<Reposition>(positions.Length * 2);
				for(int i = 0; i < positions.Length; i++){
					repositions.Add(new Position(positions[i].x, positions[i].y));
					repositions.Add(new Transposition(positions[i].x, positions[i].y));
				}
				return repositions.ToArray();
			}
		}
		[Serializable]
		public class NullReposition : Reposition{
			public override void Spawn(Game game, Map map, int posX, int posY){}
			public override bool Check(Game game, Map map, int posX, int posY){
				return false;
			}
		}
		[Serializable]
		public class Position : Reposition{
			private int _x;
			private int _y;
			public Position(int x, int y){
				_x = x;
				_y = y;
			}
			public override void Spawn(Game game, Map map, int posX, int posY){
				int spawnX = posX - _x;
				int spawnY = posY - _y;
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						map.Get(x, y).GetIPrintTile().Spawn(game, (x + spawnX), (y + spawnY));
					}
				}
				const int CONNECTOR_INDEX = 2;
				Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, posX, posY).Add(game);
			}
			public override bool Check(Game game, Map map, int posX, int posY){
				int spawnX = posX - _x;
				int spawnY = posY - _y;
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						if(!map.Get(x, y).GetIPrintTile().Check(game, (x + spawnX), (y + spawnY))){
							return false;
						}
					}
				}
				return true;
			}
		}
		[Serializable]
		public class Transposition : Reposition{
			private int _x;
			private int _y;
			public Transposition(int x, int y){
				_x = x;
				_y = y;
			}
			public override void Spawn(Game game, Map map, int posX, int posY){
				int spawnX = posX - _y;
				int spawnY = posY - _x;
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						map.Get(x, y).GetIPrintTile().Spawn(game, (y + spawnX), (x + spawnY));
					}
				}
				const int CONNECTOR_INDEX = 2;
				Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, posX, posY).Add(game);
			}
			public override bool Check(Game game, Map map, int posX, int posY){
				int spawnX = posX - _y;
				int spawnY = posY - _x;
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						if(!map.Get(x, y).GetIPrintTile().Check(game, (y + spawnX), (x + spawnY))){
							return false;
						}
					}
				}
				return true;
			}
		}
		[Serializable]
		public class FlippedReposition : Reposition{
			private int _x;
			private int _y;
			public FlippedReposition(int x, int y){
				_x = x;
				_y = y;
			}
			public override void Spawn(Game game, Map map, int posX, int posY){
				int flipWidth = (map.GetWidth() - 1);
				int flipHeight = (map.GetHeight() - 1);
				int spawnX = posX - (flipWidth - _x);
				int spawnY = posY - (flipHeight - _y);
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						map.Get((flipWidth - x), (flipHeight - y)).GetIPrintTile().Spawn(game, (x + spawnX), (y + spawnY));
					}
				}
				const int CONNECTOR_INDEX = 2;
				Spawner.SPAWNER_DATA.GetSpawner(CONNECTOR_INDEX, posX, posY).Add(game);
			}
			public override bool Check(Game game, Map map, int posX, int posY){
				int flipWidth = (map.GetWidth() - 1);
				int flipHeight = (map.GetHeight() - 1);
				int spawnX = posX - (flipWidth - _x);
				int spawnY = posY - (flipHeight - _y);
				for(int x = 0; x < map.GetWidth(); x++){
					for(int y = 0; y < map.GetHeight(); y++){
						if(!map.Get((flipWidth - x), (flipHeight - y)).GetIPrintTile().Check(game, (x + spawnX), (y + spawnY))){
							return false;
						}
					}
				}
				return true;
			}
		}
		//
		public abstract void Spawn(Game game, Map map, int posX, int posY);
		public abstract bool Check(Game game, Map map, int posX, int posY);
		//
		private static readonly NullReposition _NULL_REPOSITION = new NullReposition();
		public static Reposition GetNullReposition(){
			return _NULL_REPOSITION;
		}
	}
	//
	private static readonly Vector3 _BLUEPRINT_ORIGIN_POSITION = Vector3.zero;
	private static readonly Vector3 _BLUEPRINT_CELL_DIMENSIONS = Vector3.one;
	private const float _BLUEPRINT_CELL_SIZE = 1f;
	private const float _BLUEPRINT_CELL_OFFSET = 0.5f;
	//
	private int _buildCost;
	private Reposition[] _repositions;
	//
	public BluePrint(int buildCost, int[,] map, Func<int, Map.Tile> GetPrintData, Reposition[] repositions) : base(
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
		_buildCost = buildCost;
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
	public void Spawn(Game game, int posX, int posY){
		List<int> final = new List<int>();
		for(int i = 0; i < _repositions.Length; i++){
			if(_repositions[i].Check(game, this, posX, posY)){
				final.Add(i);
			}
		}
		if(final.Count > 0){
			_repositions[final[UnityEngine.Random.Range(0, final.Count)]].Spawn(game, this, posX, posY);
			game.GetGeneration().AddBuildCount(_buildCost);
		}
	}
	public override ISpawn GetISpawn(){
		return this;
	}
}