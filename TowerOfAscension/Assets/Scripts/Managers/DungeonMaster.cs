using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DungeonMaster : MonoBehaviour{
	public static class DUNGEONMASTER_DATA{
		private static Game _game = Game.GetNullGame();
		static DUNGEONMASTER_DATA(){
			_game = new Game();
			_game.NextLevel();
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
	private bool _tick;
	private WaitForFixedUpdate _update;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
		_local = DUNGEONMASTER_DATA.GetGame();
		//
		Play();
	}
	public IEnumerator GameLoop(){
		yield return _update;
		while(_tick){
			InputHandling();
			if(WorldUnitManager.GetInstance().GetIsAnimating()){
				yield return _update;
			}
			if(!_local.Process()){
				yield return _update;
			}
		}
	}
	public void InputHandling(){
		if(Input.GetKeyDown(KeyCode.F5)){
			Save();
		}
		if(Input.GetKeyDown(KeyCode.F9)){
			Load();
		}
	}
	public void Play(){
		_tick = true;
		StartCoroutine(GameLoop());
	}
	public void Pause(){
		_tick = false;
	}
	public void Process(){
		if(!_tick){
			_local.Process();
		}
	}
	public void Save(){
		SaveSystem.Save(_local);
	}
	public void Load(){
		if(!SaveSystem.Load(out Game file)){
			file = Game.GetNullGame();
		}
		DUNGEONMASTER_DATA.SetGame(file);
		LoadSystem.Load(LoadSystem.Scene.Game, null);
	}
	public Level GetLevel(){
		return _local.GetLevel();
	}
	public static DungeonMaster GetInstance(){
		return _instance;
	}
}
