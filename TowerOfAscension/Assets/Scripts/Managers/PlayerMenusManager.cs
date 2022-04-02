using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMenusManager : MonoBehaviour{
	private static PlayerMenusManager _INSTANCE;
	private bool _state;
	private List<UIWindowManager> _menus;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
		_menus = new List<UIWindowManager>();
	}
	private void Update(){
		if(Input.GetMouseButtonDown(1)){
			ToggleState();
		}
	}
	public void ToggleState(){
		_state ^= true;
		for(int i = 0; i < _menus.Count; i++){
			_menus[i].SetActive(_state);
		}
	}
	public void SetState(bool state){
		_state = state;
		for(int i = 0; i < _menus.Count; i++){
			_menus[i].SetActive(_state);
		}
	}
	public void AddMenu(UIWindowManager menu){
		_menus.Add(menu);
		menu.SetActive(_state);
	}
	public bool RemoveMenu(UIWindowManager menu){
		return _menus.Remove(menu);
	}
	public static PlayerMenusManager GetInstance(){
		return _INSTANCE;
	}
}
