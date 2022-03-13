using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DungeonMaster : MonoBehaviour{
	public static class DUNGEONMASTER_DATA{
		private static Game _game = Game.GetNullGame();
		static DUNGEONMASTER_DATA(){
			_game = new Game();
			_game.NewLevel();
		}
		public static void SetGame(Game game){
			_game = game;
		}
		public static Game GetGame(){
			return _game;
		}
	}
	private static DungeonMaster _instance;
	private Game _local;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
		_local = DUNGEONMASTER_DATA.GetGame();
		TestUnit testUnit = new TestUnit();
		testUnit.Spawn(_local.GetLevel(), 50, 50);
	}
	public void Save(){
		SaveSystem.Save(_local);
	}
	public void Load(){
		if(!SaveSystem.Load(out Game file)){
			file = Game.GetNullGame();
		}
		DUNGEONMASTER_DATA.SetGame(file);
	}
	public Level GetLevel(){
		return _local.GetLevel();
	}
	public static DungeonMaster GetInstance(){
		return _instance;
	}
}
