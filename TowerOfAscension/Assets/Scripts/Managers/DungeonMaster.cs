using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DungeonMaster : 
	MonoBehaviour,
	DungeonMaster.IGameTriggers
	{
	public static class DUNGEONMASTER_DATA{
		private static Game _game = Game.GetNullGame();
		public static void SetGame(Game game){
			_game = game;
		}
		public static void ClearGame(){
			_game = Game.GetNullGame();
		}
		public static Game GetGame(){
			return _game;
		}
	}
	public interface IGameTriggers{
		void QueueAction(Action action);
	}
	public class NullGameTriggers :
		IGameTriggers
		{
		public void QueueAction(Action action){
			UnityEngine.Debug.Log("NullGameTriggers");
		}
	}
	private static readonly NullGameTriggers _NULL_GAME_TRIGGERS = new NullGameTriggers();
	private static DungeonMaster _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Queue<Action> _actions;
	private bool _tick;
	private bool _busy;
	private WaitForFixedUpdate _update;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
		_actions = new Queue<Action>();
		_local = DUNGEONMASTER_DATA.GetGame();
		//
		Play();
	}
	private void LateUpdate(){
		if(!_busy && _actions.Count > 0){
			_actions.Dequeue()?.Invoke();
			_busy = true;
			return;
		}
		_busy = false;
	}
	public IEnumerator GameLoop(){
		yield return _update;
		while(_tick){
			if(IsBusy()){
				yield return _update;
			}
			if(!_local.Process()){
				yield return _update;
			}
		}
	}
	public void QueueAction(Action action){
		_actions.Enqueue(action);
	}
	public void BusyFrame(){
		_busy = true;
	}
	public bool IsBusy(){
		return _busy || _actions.Count > 0;
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
	public Game GetLocalGame(){
		return _local;
	}
	public Level GetLevel(){
		return _local.GetLevel();
	}
	public static DungeonMaster GetInstance(){
		return _INSTANCE;
	}
	public static IGameTriggers GetTriggers(){
		if(_INSTANCE == null){
			return _NULL_GAME_TRIGGERS;
		}
		return _INSTANCE;
	}
}
