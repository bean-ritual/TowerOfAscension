using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDUIManager : MonoBehaviour{
	private static HUDUIManager _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	private Unit _lantern = Unit.GetNullUnit();
	[SerializeField]private Slider _healthBar;
	[SerializeField]private TextMeshProUGUI _healthText;
	[SerializeField]private GameObject _healthBarControl;
	[SerializeField]private GameObject _healthTextControl;
	[SerializeField]private Slider _lightBar;
	[SerializeField]private TextMeshProUGUI _lightText;
	[SerializeField]private GameObject _lightBarControl;
	[SerializeField]private GameObject _lightTextControl;
	private void OnDestroy(){
		UnsubscribeFromEvents();
	}
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
		RefreshAll();
	}
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
	}
	public void SetUnit(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_unit.GetTag(_local, Tag.ID.Health).OnTagUpdate += OnHealthTagUpdate;
		_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate += OnLanternTagUpdate;
		RefreshAll();
	}
	public void RefreshAll(){
		RefreshHealth();
		RefreshLantern();
	}
	public void RefreshHealth(){
		const string HP_TEXT = "HP: {0}/{1}";
		int health = _unit.GetTag(_local, Tag.ID.Health).GetIGetIntValue1().GetIntValue1(_local, _unit);
		int maxHealth = _unit.GetTag(_local, Tag.ID.Health).GetIGetIntValue2().GetIntValue2(_local, _unit);
		bool control = maxHealth > 0;
		_healthBarControl.SetActive(control);
		_healthTextControl.SetActive(control);
		_healthBar.maxValue = maxHealth;
		_healthBar.value = health;
		_healthText.text = String.Format(HP_TEXT, health, maxHealth);
	}
	public void RefreshLantern(){
		_lantern.GetTag(_local, Tag.ID.Fuel).OnTagUpdate -= OnLightTagUpdate;
		_lantern = _unit.GetTag(_local, Tag.ID.Light).GetIGetUnit().GetUnit(_local, _unit);
		/*
		if(_lantern.IsNull()){
			_lantern = _unit;
		}
		*/
		_lantern.GetTag(_local, Tag.ID.Fuel).OnTagUpdate += OnLightTagUpdate;
		RefreshLight();
	}
	public void RefreshLight(){
		const string LIGHT_TEXT = "LT: [{0}/{1}] {2}/{3}";
		int light = _lantern.GetTag(_local, Tag.ID.Light).GetIGetIntValue1().GetIntValue1(_local, _lantern);
		int maxLight = _lantern.GetTag(_local, Tag.ID.Light).GetIGetIntValue2().GetIntValue2(_local, _lantern);
		int fuel = _lantern.GetTag(_local, Tag.ID.Fuel).GetIGetIntValue1().GetIntValue1(_local, _lantern);
		int maxFuel = _lantern.GetTag(_local, Tag.ID.Fuel).GetIGetIntValue2().GetIntValue2(_local, _lantern);
		bool control = maxLight > 0;
		_lightBarControl.SetActive(control);
		_lightTextControl.SetActive(control);
		_lightBar.maxValue = maxFuel;
		_lightBar.value = fuel;
		_lightText.text = String.Format(LIGHT_TEXT, light, maxLight, fuel, maxFuel);
	}
	public void UnsubscribeFromEvents(){
		_unit.GetTag(_local, Tag.ID.Health).OnTagUpdate -= OnHealthTagUpdate;
		_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate -= OnLanternTagUpdate;
		_lantern.GetTag(_local, Tag.ID.Fuel).OnTagUpdate -= OnLightTagUpdate;
	}
	public void OnHealthTagUpdate(object sender, EventArgs e){
		RefreshHealth();
	}
	public void OnLanternTagUpdate(object sender, EventArgs e){
		RefreshLantern();
	}
	public void OnLightTagUpdate(object sender, EventArgs e){
		RefreshLight();
	}
	public static HUDUIManager GetInstance(){
		return _INSTANCE;
	}
}
