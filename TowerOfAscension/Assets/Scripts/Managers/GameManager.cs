using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour{
	private static GameManager _instance;
	private void Awake(){
		_instance = this;
		DontDestroyOnLoad(gameObject);
	}
	private void LateUpdate(){
		InputHandling();
	}
	public void RingTheDinkster(){
		Debug.Log("GameManager :: RingTheDinkster()");
	}
	private void InputHandling(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
	public static GameManager GetInstance(){
		if(_instance == null){
			string name = "[GameManager]";
			GameObject go = new GameObject(name);
			_instance = go.AddComponent<GameManager>();
		}
		return _instance;
	}
}