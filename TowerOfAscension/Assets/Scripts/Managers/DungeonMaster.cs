using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DungeonMaster : 
	MonoBehaviour,
	DungeonMaster.IDungeonMaster
	{
	public static class DUNGEONMASTER_DATA{
		private static Game _global;
		static DUNGEONMASTER_DATA(){
			_global = Game.GetNullGame();
		}
		public static void SetGame(Game game){
			_global = game;
		}
		public static void ClearGame(){
			_global = Game.GetNullGame();
		}
		public static Game GetGame(){
			return _global;
		}
	}
	public interface IDungeonMaster{
		void QueueAction(Action action);
		Game GetGame();
	}
	public class NullDungeonMaster :
		IDungeonMaster
		{
		public void QueueAction(Action action){}
		public Game GetGame(){
			return Game.GetNullGame();
		}
	}
	private static DungeonMaster _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Queue<Action> _actions;
	private bool _tick;
	private WaitForFixedUpdate _update;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_INSTANCE == null){
			_INSTANCE = this;
			_local = DUNGEONMASTER_DATA.GetGame();
			_actions = new Queue<Action>();
			if(_local.IsNull()){
				_actions.Enqueue(() => LoadSystem.Load(LoadSystem.Scene.Main, () => SaveSystem.Save(_local)));
			}else{
				Play();
			}
		}else{
			Destroy(gameObject);
		}
	}
	private void LateUpdate(){
		if(!WorldDataManager.GetInstance().IsBusy() && _actions.Count > 0){
			_actions.Dequeue()?.Invoke();
		}
	}
	public IEnumerator GameLoop(){
		yield return _update;
		while(_tick){
			if(WorldDataManager.GetInstance().IsBusy()){
				yield return _update;
			}
			if(_local.Tick()){
				yield return _update;
			}
		}
	}
	public void QueueAction(Action action){
		_actions.Enqueue(action);
	}
	public void Play(){
		_tick = true;
		StartCoroutine(GameLoop());
	}
	public void Pause(){
		_tick = false;
	}
	public Game GetGame(){
		return _local;
	}
	//
	private static NullDungeonMaster _NULL_DUNGEON_MASTER = new NullDungeonMaster();
	public static IDungeonMaster GetInstance(){
		if(_INSTANCE == null){
			return _NULL_DUNGEON_MASTER;
		}else{
			return _INSTANCE;
		}
	}
}
