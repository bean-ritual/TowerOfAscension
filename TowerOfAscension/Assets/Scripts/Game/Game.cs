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
			[0] Movement, turns, ai and control
			[1] Gameover, floor up and light
			[2] Health defensive stats and attackable
			[3] Equipment and inventory
			[4] Effects: temporary/equipment
			[5] Merchant floors
			[6] Boss floors
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
		public const int BLOCK_INVENTORY = 4;
		public const int BLOCK_ITEM = 5;
		public const int BLOCK_EQUIP = 6;
		//
		public TOAGame(){
			const int BLOCKS = 10;
			_floor = 0;
			_data = new GameData.UnlimitedGameData(100);
			_blocks = new GameBlocks[BLOCKS];
			for(int i = 0; i < BLOCKS; i++){
				_blocks[i] = new GameBlocks.RealGameBlocks(i);
			}
			_world = new GameWorld.TOAGameWorld();
			_map = new LevelMap(100, 100);
			_master = Generation.GetNullGeneration();
			Data.DATA.CreatePlayer(this);
			Generate();
		}
		private void Generate(){
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
			return _blocks.Length;
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
	public abstract bool IsNull();
	//
	private static NullGame _NULL_GAME = new NullGame();
	public static Game GetNullGame(){
		return _NULL_GAME;
	}
}
