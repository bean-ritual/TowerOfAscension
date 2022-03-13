using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnitManager : MonoBehaviour{
	private static WorldUnitManager _instance;
	private Level _level;
	private Dictionary<Unit, WorldUnit> _worldUnits;
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
		_worldUnits = new Dictionary<Unit, WorldUnit>();
		_level = DungeonMaster.GetInstance().GetLevel();
		Register<Unit> units = _level.GetUnits();
		for(int i = 0; i < units.GetCount(); i++){
			CreateWorldUnit(units.Get(i));
		}
	}
	private void CreateWorldUnit(Unit unit){
		GameObject go = Instantiate(_prefabWorldUnit, this.transform);
		WorldUnit worldUnit = go.GetComponent<WorldUnit>();
		worldUnit.Setup(unit);
		_worldUnits.Add(unit, worldUnit);
	}
	public static WorldUnitManager GetInstance(){
		return _instance;
	}
}
