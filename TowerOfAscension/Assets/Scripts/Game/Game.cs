using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Game{
	[Serializable]
	public class NullGame : Game{
		public override bool Tick(){
			return true;
		}
		public override GameData GetGameData(){
			return GameData.GetNullGameData();
		}
		public override GameBlocks GetGameBlocks(int index){
			return GameBlocks.GetNullGameBlocks();
		}
		public override GameWorld GetGameWorld(){
			return GameWorld.GetNullGameWorld();
		}
		public override Map GetMap(){
			return Map.GetNullMap();
		}
		public override Generation GetGeneration(){
			return Generation.GetNullGeneration();
		}
		public override Data GetPlayer(){
			return Data.GetNullData();
		}
		public override int GetGameBlocksCount(){
			return 0;
		}
		public override int GetFloor(){
			return -1;
		}
		public override void NextFloor(){}
		public override void GameOver(){}
		public override bool IsNull(){
			return true;
		}
	}
	[Serializable]
	public class TestGame : Game{
		private Map _map;
		public TestGame(){
			_map = new TestMap(100, 100);
		}
		public override bool Tick(){
			return true;
		}
		public override GameData GetGameData(){
			return GameData.GetNullGameData();
		}
		public override GameBlocks GetGameBlocks(int index){
			return GameBlocks.GetNullGameBlocks();
		}
		public override GameWorld GetGameWorld(){
			return GameWorld.GetNullGameWorld();
		}
		public override Map GetMap(){
			return _map;
		}
		public override Generation GetGeneration(){
			return Generation.GetNullGeneration();
		}
		public override Data GetPlayer(){
			return Data.GetNullData();
		}
		public override int GetGameBlocksCount(){
			return 0;
		}
		public override int GetFloor(){
			return -1;
		}
		public override void NextFloor(){}
		public override void GameOver(){}
		public override bool IsNull(){
			return false;
		}
	}
	[Serializable]
	public class TOAGame : Game{
		/*
			GameLoop
			
			Basic
			Progress through increasing difficulty dungeon
			Gain experiance -> levels and skillpoints to use better equipment and spells
			Gain gold -> spent at merchants for supplies
			Gain loot -> weapons, armour and consumables
			Merchants at inbetween floors, elements of inventory management
			
			Advancing events mechanic
			As you move through the dungeon your actions impact random events that can happen to you
			Hard enemies spawn on you
			Loot spawns on you
			You gain a temporary buff
			Being seen by enemies increases chances of bad events
			Events trigger by number of random steps walked
			
			Enemy traits
			All enemies will have a trait to decercne them
			i.e Unlucky Skeleton
			These traits will buff/nerf/modify their stats or give them new gameplay mechanics
			
			Tooltip
			Most of the game info will be shown through a tooltip system
			For enemy health and stats this wil be shown in a contextual way
			i.e Health: Dire, Fatigue: Exhausted, Morale: Good
			
			Combat
			Fatigue mechanic lowering the damage/effectiveness of the weapon or spell propotianally
			No cap for damage reduction but many differant types of damage
			
			Morale mechanic
			The casting certain spells will reduce morale
			On low morale events can trigger
			i.e self fear, self paralyze
			Certain weapons and spells can also lower targets morale
			
		*/
		/*
			Priority
			//[0] Movement, turns, ai and control
			//[1] Gameover, floor up and light
			[2] Equipment inventory and stackables
			[3] Effects: temporary/equipment
			[4] Health defensive stats attacks melee and ranged
			[5] Merchant floors
			//
			[6] Traits and Spawners
			[7] Boss floors
			[8] Advancing mechanic
		*/
		private int _floor;
		private GameData _data;
		private GameBlocks[] _blocks;
		private GameWorld _world;
		private Map _map;
		private Generation _master;
		//
		public const int BLOCK_TILE = 0;
		public const int BLOCK_WORLD = 1;
		public const int BLOCK_VISUAL = 2;
		public const int BLOCK_DOTURN = 3;
		public const int BLOCK_LIGHT = 4;
		public const int BLOCK_INVENTORY = 5;
		public const int BLOCK_ITEM = 6;
		public const int BLOCK_EQUIPMENT = 7;
		public const int BLOCK_ACTIVE = 8;
		public const int BLOCK_STATS = 9;
		public const int BLOCK_EFFECTS = 10;
		public const int BLOCK_TRIP = 11;
		//
		public const int BLOCK_COUNT = 12;
		//
		public TOAGame(){
			_floor = 0;
			_data = new GameData.UnlimitedGameData(100);
			//
			_blocks = new GameBlocks[BLOCK_COUNT];
			_blocks[BLOCK_TILE] = new GameBlocks.RealGameBlocks(BLOCK_TILE);
			_blocks[BLOCK_WORLD] = new GameBlocks.RealGameBlocks(BLOCK_WORLD);
			_blocks[BLOCK_VISUAL] = new GameBlocks.RealGameBlocks(BLOCK_VISUAL);
			_blocks[BLOCK_DOTURN] = new GameBlocks.RealGameBlocks(BLOCK_DOTURN);
			_blocks[BLOCK_LIGHT] = new ListedBlocks(BLOCK_LIGHT);
			_blocks[BLOCK_INVENTORY] = new GameBlocks.RealGameBlocks(BLOCK_INVENTORY);
			_blocks[BLOCK_ITEM] = new GameBlocks.RealGameBlocks(BLOCK_ITEM);
			_blocks[BLOCK_EQUIPMENT] = new GameBlocks.RealGameBlocks(BLOCK_EQUIPMENT);
			_blocks[BLOCK_ACTIVE] = new GameBlocks.RealGameBlocks(BLOCK_ACTIVE);
			_blocks[BLOCK_STATS] = new GameBlocks.RealGameBlocks(BLOCK_STATS);
			_blocks[BLOCK_EFFECTS] = new GameBlocks.RealGameBlocks(BLOCK_EFFECTS);
			_blocks[BLOCK_TRIP] = new GameBlocks.RealGameBlocks(BLOCK_TRIP);
			//
			_world = new GameWorld.TOAGameWorld();
			_map = new LevelMap(100, 100);
			_master = Generation.GetNullGeneration();
			Data.DATA.CreatePlayer(this);
			Generate();
		}
		private void Generate(){
			_blocks[BLOCK_TILE].Disassemble(this);
			_world.Disassemble(this);
			_master = new Generation.ClassicGeneration(10);
			while(!_master.IsFinished() && !_master.IsFailed()){
				_master.Process(this);
			}
			_master = Generation.GetNullGeneration();
		}
		public override bool Tick(){
			return _world.Tick(this);
		}
		public override GameData GetGameData(){
			return _data;
		}
		public override GameBlocks GetGameBlocks(int index){
			if(index < 0 || index >= _blocks.Length){
				return GameBlocks.GetNullGameBlocks();
			}else{
				return _blocks[index];
			}
		}
		public override GameWorld GetGameWorld(){
			return _world;
		}
		public override Map GetMap(){
			return _map;
		}
		public override Generation GetGeneration(){
			return _master;
		}
		public override Data GetPlayer(){
			return _data.Get(0);
		}
		public override int GetGameBlocksCount(){
			return BLOCK_COUNT;
		}
		public override int GetFloor(){
			return _floor;
		}
		public override void NextFloor(){
			DungeonMaster.GetInstance().QueueAction(() => LoadSystem.Load(LoadSystem.Scene.Game, Generate));
		}
		public override void GameOver(){
			DungeonMaster.GetInstance().QueueAction(() => LoadSystem.Load(LoadSystem.Scene.GameOver, () => SaveSystem.Save(this)));
		}
		public override bool IsNull(){
			return false;
		}
	}
	public abstract bool Tick();
	public abstract GameData GetGameData();
	public abstract GameBlocks GetGameBlocks(int index);
	public abstract GameWorld GetGameWorld();
	public abstract Map GetMap();
	public abstract Generation GetGeneration();
	public abstract Data GetPlayer();
	public abstract int GetGameBlocksCount();
	public abstract int GetFloor();
	public abstract void NextFloor();
	public abstract void GameOver();
	public abstract bool IsNull();
	//
	private static NullGame _NULL_GAME = new NullGame();
	public static Game GetNullGame(){
		return _NULL_GAME;
	}
}
