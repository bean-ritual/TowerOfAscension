using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverManager : MonoBehaviour{
	private static GameOverManager _INSTANCE;
	private Game _over = Game.GetNullGame();
	[SerializeField]private Button _exit;
	[SerializeField]private TextMeshProUGUI _scoreText;
	private void Awake(){
		GameManager.GetInstance().RingTheDinkster();
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
		_over = DungeonMaster.DUNGEONMASTER_DATA.GetGame();
		_exit.onClick.AddListener(OnExit);
		RefreshScore();
	}
	public void RefreshScore(){
		const string SCORE_TEXT = "Score: ";
		_scoreText.text = SCORE_TEXT + _over.GetFloor();
	}
	public void OnExit(){
		UnityEngine.Debug.Log("Exit");
		DungeonMaster.DUNGEONMASTER_DATA.NewGame();
	}
	public static GameOverManager GetInstance(){
		return _INSTANCE;
	}
}
