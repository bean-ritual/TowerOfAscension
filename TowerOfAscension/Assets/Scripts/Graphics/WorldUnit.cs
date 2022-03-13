using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	private Unit _unit;
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		if(_unit != null){
			UnsubscribeFromEvents();
		}
		_unit = unit;
		this.transform.localPosition = unit.GetPositionable().GetPosition(DungeonMaster.GetInstance().GetLevel());
	}
	private void UnsubscribeFromEvents(){
		
	}
}
