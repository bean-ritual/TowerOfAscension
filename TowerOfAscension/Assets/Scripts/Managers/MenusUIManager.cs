using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenusUIManager : MonoBehaviour{
	private static MenusUIManager _INSTANCE;
	private Unit _unit = Unit.GetNullUnit();
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
		Refresh();
	}
	public void Refresh(){
		
	}
	public static MenusUIManager GetInstance(){
		return _INSTANCE;
	}
}
