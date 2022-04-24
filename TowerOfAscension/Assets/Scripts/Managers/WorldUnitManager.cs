using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnitManager : MonoBehaviour{
	private static WorldUnitManager _instance;
	private Level _level = Level.GetNullLevel();
	private Dictionary<Unit, WorldUnit> _worldUnits;
	[SerializeField]private CameraManager _mainCamera;
	[SerializeField]private GameObject _prefabWorldUnit;
	private void Awake(){
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
	}
	private void OnDestroy(){
		Register<Unit> units = _level.GetUnits();
		units.OnObjectAdded -= OnObjectAdded;
		units.OnObjectRemoved -= OnObjectRemoved;
	}
	private void Start(){
		_worldUnits = new Dictionary<Unit, WorldUnit>();
		_level = DungeonMaster.GetInstance().GetLevel();
		Register<Unit> units = _level.GetUnits();
		for(int i = 0; i < units.GetCount(); i++){
			CreateWorldUnit(units.Get(i));
		}
		units.OnObjectAdded += OnObjectAdded;
		units.OnObjectRemoved += OnObjectRemoved;
	}
	private void CreateWorldUnit(Unit unit){
		GameObject go = Instantiate(_prefabWorldUnit, this.transform);
		WorldUnit worldUnit = go.GetComponent<WorldUnit>();
		worldUnit.Setup(unit, _mainCamera.GetCamera());
		_worldUnits.Add(unit, worldUnit);
	}
	private void RemoveWorldUnit(Unit unit){
		if(unit == null){
			return;
		}
		if(!_worldUnits.TryGetValue(unit, out WorldUnit worldUnit)){
			return;
		}
		_worldUnits.Remove(unit);
		worldUnit.UnitDestroy();
	}
	public static WorldUnitManager GetInstance(){
		return _instance;
	}
	private void OnObjectAdded(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		CreateWorldUnit(e.value);
	}
	private void OnObjectRemoved(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		DungeonMaster.GetInstance().QueueAction(() => RemoveWorldUnit(e.value));
	}
}
