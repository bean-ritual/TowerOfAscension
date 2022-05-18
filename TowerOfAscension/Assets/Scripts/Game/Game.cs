using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class Game{
	public static class GAME_DATA{
		public static string GetRandomString(int length){
			const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			char[] chars = new char[length];
			for(int i = 0; i < chars.Length; i++){
				chars[i] = letters[UnityEngine.Random.Range(0, letters.Length)];
			}
			return new String(chars);
		}
	}
	[Serializable]
	public class NullGame : Game{
		public NullGame(){}
		public override bool Process(){
			return false;
		}
		public override void NextLevel(){}
		public override void GameOver(){}
		public override GameUnits GetGameUnits(){
			return GameUnits.GetNullGameUnits();
		}
		public override GameBlocks GetGameBlocks(int index){
			return GameBlocks.GetNullGameBlocks();
		}
		public override Unit GetPlayer(){
			return Unit.GetNullUnit();
		}
		public override Level GetLevel(){
			return Level.GetNullLevel();
		}
		public override int GetGameBlocksCount(){
			return 0;
		}
		public override int GetFloor(){
			return 0;
		}
		public override bool IsNull(){
			return true;
		}	
	}
	[Serializable]
	public class TOAGame : Game{
		private int _floor;
		private GameUnits _units;
		private GameBlocks[] _blocks;
		//private Unit _player;
		private Level _level;
		public TOAGame(){
			_floor = 0;
			//_player = new Hero(this);
			_units = new GameUnits.UnlimitedGameUnits(100);
			NewLevel();
		}
		public override bool Process(){
			return _level.Process(this);
		}
		public override void NextLevel(){
			DungeonMaster.GetTriggers().QueueAction(() => LoadSystem.Load(LoadSystem.Scene.Game, NewLevel));
		}
		public override void GameOver(){
			DungeonMaster.GetTriggers().QueueAction(() => LoadSystem.Load(LoadSystem.Scene.GameOver, () => SaveSystem.Save(this)));
		}
		public override GameUnits GetGameUnits(){
			return _units;
		}
		public override GameBlocks GetGameBlocks(int index){
			if(index < 0 || index >= _blocks.Length){
				return GameBlocks.GetNullGameBlocks();
			}else{
				return _blocks[index];
			}
		}
		public override Unit GetPlayer(){
			return _units.Get(0);
		}
		public override Level GetLevel(){
			return _level;
		}
		public override int GetFloor(){
			return _floor;
		}
		public override int GetGameBlocksCount(){
			return _blocks.Length;
		}
		public override bool IsNull(){
			return false;
		}
		private void NewLevel(){
			_floor = (_floor + 1);
			_level = new Level(100, 100);
			_level.GetMidPoint(out int x, out int y);
			ClassicGen classic = new ClassicGen(this, 10);
			classic.Spawn(this, x, y);
			const int MAX_SANITY = 1000;
			for(int sanity = 0; sanity < MAX_SANITY; sanity++){
				classic.Process(this);
				if(classic.IsFinished()){
					//UnityEngine.Debug.Log("Game :: NewLevel() :: Completed");
					SaveSystem.Save(this);
					return;
				}
			}
			//UnityEngine.Debug.Log("Game :: NewLevel() :: Failed");
			NewLevel();
		}
	}
	//
	public abstract bool Process();
	public abstract void NextLevel();
	public abstract void GameOver();
	public abstract GameUnits GetGameUnits();
	public abstract GameBlocks GetGameBlocks(int index);
	public abstract Unit GetPlayer();
	public abstract Level GetLevel();
	public abstract int GetGameBlocksCount();
	public abstract int GetFloor();
	public abstract bool IsNull();
	//
	private static readonly NullGame _NULL_GAME = new NullGame();
	public static Game GetNullGame(){
		return _NULL_GAME;
	}
}
