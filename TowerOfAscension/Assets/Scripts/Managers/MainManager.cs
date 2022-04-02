using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainManager : MonoBehaviour{
	private static MainManager _INSTANCE;
	[SerializeField]private Button _continue;
	[SerializeField]private Button _newGame;
	[SerializeField]private Button _options;
	[SerializeField]private Button _exit;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
		_continue.onClick.AddListener(OnContinue);
		_newGame.onClick.AddListener(OnNewGame);
		_options.onClick.AddListener(OnOptions);
		_exit.onClick.AddListener(OnExit);
		if(SaveSystem.Load(out Game file)){
			DungeonMaster.DUNGEONMASTER_DATA.SetGame(file);
		}
		if(DungeonMaster.DUNGEONMASTER_DATA.GetGame().IsNull()){
			_continue.interactable = false;
		}
	}
	public void OnContinue(){
		LoadSystem.Load(LoadSystem.Scene.Game, null);
	}
	public void OnNewGame(){
		LoadSystem.Load(LoadSystem.Scene.Game, () => DungeonMaster.DUNGEONMASTER_DATA.SetGame(Game.GAME_DATA.GetNewGame()));
	}
	public void OnOptions(){
		
	}
	public void OnExit(){
		Application.Quit();
	}
	public static MainManager GetInstance(){
		return _INSTANCE;
	}
}
