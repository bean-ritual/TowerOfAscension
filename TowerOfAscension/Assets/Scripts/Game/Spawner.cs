using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Spawner{
	// SPAWNER_DATA
	public static class SPAWNER_DATA{
		public static Spawner GetSpawner(int index, int x, int y){
			switch(index){
				default: return GetNullSpawner();
				case 0: return GetContextualBluePrintSpawner(x, y);
				case 1: return GetPossibleConnectorSpawner(x, y);
				case 2: return new ConnectorSpawner(x, y);
				case 3: return new ExitSpawner(x, y);
				case 4: return new MonsterSpawner(x, y);
				case 5: return new LootSpawner(x, y);
				case 10: return GetRandomBluePrintSpawner(x, y);
				case 11: return GetStructureBluePrintSpawner(x, y);
				case 12: return GetDetailBluePrintSpawner(x, y);
			}
		}
		public static Spawner GetRandomBluePrintSpawner(int x, int y){
			return new BluePrintSpawner(x, y, BluePrint.BLUEPRINT_DATA.GetRandomIndex());
		}
		public static Spawner GetStructureBluePrintSpawner(int x, int y){
			return new BluePrintSpawner(x, y, Generation.ClassicGeneration.CLASSICGEN_DATA.GetRandomStructureBluePrintIndex());
		}
		public static Spawner GetDetailBluePrintSpawner(int x, int y){
			return new BluePrintSpawner(x, y, Generation.ClassicGeneration.CLASSICGEN_DATA.GetRandomDetailBluePrintIndex());
		}
		public static Spawner GetContextualBluePrintSpawner(int x, int y){
			return new ContextualBluePrintSpawner(x, y);
		}
		public static Spawner GetPossibleConnectorSpawner(int x, int y){
			if(UnityEngine.Random.Range(0, 100) < 10){
				return new ConnectorSpawner(x, y);
			}
			return GetNullSpawner();
		}
	}
	[Serializable]
	public class NullSpawner : Spawner{
		public override void Add(Game game){}
		public override void Spawn(Game game){}
		public override void GetXY(out int x, out int y){
			x = -1;
			y = -1;
		}
		public override int GetX(){
			return -1;
		}
		public override int GetY(){
			return -1;
		}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public class BluePrintSpawner : BaseSpawner{
		private int _index;
		public BluePrintSpawner(int x, int y, int index) : base(x, y){
			_index = index;
		}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 1);
		}
		public override void Spawn(Game game){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(_index).GetISpawn().Spawn(game, GetX(), GetY());
		}
	}
	[Serializable]
	public class ContextualBluePrintSpawner : BaseSpawner{
		public ContextualBluePrintSpawner(int x, int y) : base(x, y){}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 1);
		}
		public override void Spawn(Game game){
			BluePrint.BLUEPRINT_DATA.GetBluePrint(game.GetGeneration().GetContextualBluePrintIndex()).GetISpawn().Spawn(game, GetX(), GetY());
		}
	}
	//
	[Serializable]
	public class ConnectorSpawner : BaseSpawner{
		public ConnectorSpawner(int x, int y) : base(x, y){}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 2);
		}
		public override void Spawn(Game game){
			if(!game.GetMap().Get(GetX(), GetY()).GetIDataTile().GetData(game).GetBlock(game, 0).GetICanConnect().CanConnect(game)){
				if(game.GetMap().GetIMapmatics().GetCardinals(GetX(), GetY(), (Map.Tile tile) => {return tile.GetIDataTile().GetData(game).GetBlock(game, 0).GetICanConnect().CanConnect(game);}).Count == 2){
					game.GetMap().Get(GetX(), GetY()).GetIDataTile().SetData(game, Data.DATA.CreateGroundTile(game, GetX(), GetY()));
				}
			}
		}
	}
	[Serializable]
	public class ExitSpawner : BaseSpawner{
		public ExitSpawner(int x, int y) : base(x, y){}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 3);
		}
		public override void Spawn(Game game){
			game.GetMap().Get(GetX(), GetY()).GetIDataTile().SetData(game, Data.DATA.CreateExitTile(game, GetX(), GetY()));
		}
	}
	//
	[Serializable]
	public class MonsterSpawner : BaseSpawner{
		public MonsterSpawner(int x, int y) : base(x, y){}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 2);
		}
		public override void Spawn(Game game){
			Data.DATA.CreateRat(game).GetBlock(game, 1).GetIWorldPosition().Spawn(game, GetX(), GetY());
		}

	}
	[Serializable]
	public class LootSpawner : BaseSpawner{
		public LootSpawner(int x, int y) : base(x, y){}
		public override void Add(Game game){
			game.GetGeneration().Add(this, 2);
		}
		public override void Spawn(Game game){
			//Unit.UNIT_DATA.GetLevelledItem(game, game.GetFloor()).Spawn(game, _x, _y);
		}
	}
	[Serializable]
	public abstract class BaseSpawner : Spawner{
		private int _x;
		private int _y;
		public BaseSpawner(int x, int y){
			_x = x;
			_y = y;
		}
		public override void GetXY(out int x, out int y){
			x = _x;
			y = _y;
		}
		public override int GetX(){
			return _x;
		}
		public override int GetY(){
			return _y;
		}
		public override bool IsNull(){
			return false;
		}
	}
	//
	public abstract void Add(Game game);
	public abstract void Spawn(Game game);
	public abstract void GetXY(out int x, out int y);
	public abstract int GetX();
	public abstract int GetY();
	public abstract bool IsNull();
	//
	private static readonly NullSpawner _NULL_SPAWNER = new NullSpawner();
	public static Spawner GetNullSpawner(){
		return _NULL_SPAWNER;
	}
}