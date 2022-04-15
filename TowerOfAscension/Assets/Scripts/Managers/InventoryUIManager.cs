using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryUIManager : MonoBehaviour{
	private static InventoryUIManager _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Register<Unit>.IRegisterEvents _inventory = Inventory.GetNullInventory();
	private Dictionary<Unit, UIUnit> _uiUnits;
	private UIWindowManager _uiWindow;
	private RectTransform _content;
	[SerializeField]private GameObject _prefabUIWindow;
	[SerializeField]private GameObject _prefabUIInventory;
	[SerializeField]private GameObject _prefabUIItem;
	[SerializeField]private Canvas _canvas;
	private void OnDisable(){
		SettingsSystem.GetConfig().inventory = _uiWindow.GetUISizeData();
		UnsubcribeFromEvents();
	}
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
	}
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
		BuildUI();
	}
	public void BuildUI(){
		GameObject go = Instantiate(_prefabUIWindow, this.transform);
		_uiWindow = go.GetComponent<UIWindowManager>();
		_uiWindow.Setup(
			"Inventory",
			true,
			_canvas,
			SettingsSystem.GetConfig().inventory
		);
		PlayerMenusManager.GetInstance().AddMenu(_uiWindow);
		GameObject go2 =  Instantiate(_prefabUIInventory, _uiWindow.GetContent().transform);
		_content = go2.GetComponent<ContentManager>().GetContent();
	}
	public void SetInventory(Register<Unit>.IRegisterEvents inventory){
		UnsubcribeFromEvents();
		_uiUnits = new Dictionary<Unit, UIUnit>();
		GameObjectUtils.DestroyAllChildren(_content);
		_inventory = inventory;
		List<Unit> items = _inventory.GetAll();
		for(int i = 0; i < items.Count; i++){
			CreateUIUnit(items[i]);
		}
		_inventory.OnObjectAdded += OnObjectAdded;
		_inventory.OnObjectRemoved += OnObjectRemoved;
	}
	public void CreateUIUnit(Unit unit){
		GameObject go = Instantiate(_prefabUIItem, _content);
		UIUnit uiUnit = go.GetComponent<UIUnit>();
		uiUnit.Setup(unit, InventoryInteract);
		_uiUnits.Add(unit, uiUnit);
	}
	public void RemoveUIUnit(Unit unit){
		if(!_uiUnits.TryGetValue(unit, out UIUnit uiUnit)){
			return;
		}
		_uiUnits.Remove(unit);
		uiUnit.UnitDestroy();
	}
	public void UnsubcribeFromEvents(){
		_inventory.OnObjectAdded -= OnObjectAdded;
		_inventory.OnObjectRemoved -= OnObjectRemoved;
	}
	public void InventoryInteract(Unit item){
		Unit player = PlayerController.GetInstance().GetPlayer();
		if(Input.GetKey(KeyCode.LeftShift)){
			player.GetTaggable().GetTag(_local, Tag.ID.Inventory).GetIRemoveUnitID().Remove(_local, player, item.GetRegisterable().GetID());
			//item.GetDroppable().TryDrop(_local, player);
			return;
		}
		item.GetUseable().TryUse(_local, player);
	}
	private void OnObjectAdded(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		CreateUIUnit(e.value);
	}
	private void OnObjectRemoved(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		RemoveUIUnit(e.value);
	}
	public static InventoryUIManager GetInstance(){
		return _INSTANCE;
	}
}
