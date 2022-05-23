using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlatteningTestManager : 
	MonoBehaviour,
	FlatteningTestManager.IFlatteningTestManager
	{
	public interface IFlatteningTestManager{
		Game GetGame();
	}
	public class NullFlatteningTestManager : 
		FlatteningTestManager.IFlatteningTestManager
		{
		public Game GetGame(){
			return Game.GetNullGame();
		}
	}
	private static IFlatteningTestManager _INSTANCE;
	private Game _game = Game.GetNullGame();
	private void Awake(){
		if(_INSTANCE == null){
			_INSTANCE = this;
			_game = new Game.TOAGame();
			//MapMeshManager.GetInstance().Setup(_game);
		}else{
			Destroy(gameObject);
		}
	}
	public Game GetGame(){
		return _game;
	}
	//
	private static NullFlatteningTestManager _NULL_FLAT_MANAGER = new NullFlatteningTestManager();
	public static IFlatteningTestManager GetNullFlatteningTestManager(){
		return _NULL_FLAT_MANAGER;
	}
	public static IFlatteningTestManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_FLAT_MANAGER;
		}else{
			return _INSTANCE;
		}
	}
}
