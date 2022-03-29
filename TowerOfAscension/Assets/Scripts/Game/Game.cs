using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Game{
	public static class GAME_DATA{
		
	}
	[Serializable]
	public class NullGame : Game{
		public NullGame(){}
		public override bool Process(){
			return false;
		}
		public override void NextLevel(){}
		public override void NewLevel(){}
		public override Level GetLevel(){
			return Level.GetNullLevel();
		}
	}
	[field:NonSerialized]private static readonly NullGame _NULL_GAME = new NullGame();
	private int _floor;
	private Unit _player = Unit.GetNullUnit();
	private Level _level = Level.GetNullLevel();
	public Game(){
		_floor = 0;
		_player = new Hero();
	}
	public virtual bool Process(){
		return _level.Process(this);
	}
	public virtual void NextLevel(){
		DungeonMaster.GetInstance().QueueAction(() => LoadSystem.Load(LoadSystem.Scene.Game, NewLevel));
	}
	public virtual void NewLevel(){
		_level = new Level(100, 100);
		_level.GetMidPoint(out int x, out int y);
		ClassicGen classic = new ClassicGen(_player, 10);
		classic.Spawn(this, x, y);
		const int MAX_SANITY = 1000;
		for(int sanity = 0; sanity < MAX_SANITY; sanity++){
			classic.Process(this);
			if(classic.IsFinished()){
				UnityEngine.Debug.Log("Game :: NewLevel() :: Completed");
				//SaveSystem.Save(this);
				return;
			}
		}
		UnityEngine.Debug.Log("Game :: NewLevel() :: Failed");
		NewLevel();
	}
	public virtual Level GetLevel(){
		return _level;
	}
	public static Game GetNullGame(){
		return _NULL_GAME;
	}
}
