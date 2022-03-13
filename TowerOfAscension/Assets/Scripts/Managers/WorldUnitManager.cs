using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnitManager : MonoBehaviour{
	private static WorldUnitManager _instance;
	private Level _level;
	[SerializeField]private GameObject _prefabWorldUnit;
	private void Awake(){
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
	}
	private void OnDestroy(){
		
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		Register<Unit> units = _level.GetUnits();
	}
	public static WorldUnitManager GetInstance(){
		return _instance;
	}
}
