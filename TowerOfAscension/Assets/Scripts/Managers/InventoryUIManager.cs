using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryUIManager : MonoBehaviour{
	/*
	public interface IInventoryUIManager{
		void SetUnit(Unit unit);
	}
	public class NullInventoryUIManager : IInventoryUIManager{
		public void SetUnit(Unit unit){}
	}
	private static NullInventoryUIManager _NULL_INVENTORY_UI = new NullInventoryUIManager();
	private static InventoryUIManager _INSTANCE;
	private static Tag.ID[] _EQUIP_SLOTS = {
		Tag.ID.Attack_Slot,
		Tag.ID.Light,
		Tag.ID.Chestplate,
		Tag.ID.Boots,
	};
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	private Register<Unit>.IRegisterEvents _inventory = Inventory.GetNullInventory();
	private Tag _weapon = Tag.GetNullTag();
	//
	private Dictionary<Tag.ID, UIUnit> _tagUnits;
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
		SetupTags();
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
		GameObject go2 = Instantiate(_prefabUIInventory, _uiWindow.GetContent().transform);
		_content = go2.GetComponent<ContentManager>().GetContent();
	}
	public void SetUnit(Unit unit){
		UnsubcribeFromEvents();
		Clear();
		_unit = unit;
		RefreshTags();
		_inventory = unit.GetTag(_local, Tag.ID.Inventory).GetIGetRegisterEvents().GetRegisterEvents(_local, unit);
		List<Unit> items = _inventory.GetAll();
		for(int i = 0; i < items.Count; i++){
			CreateUIUnit(items[i]);
		}
		_inventory.OnObjectAdded += OnObjectAdded;
		_inventory.OnObjectRemoved += OnObjectRemoved;
		_unit.GetTag(_local, Tag.ID.Attack_Slot).OnTagUpdate += OnWeaponTagUpdate;
		_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate += OnLightTagUpdate;
	}
	public void SetupTags(){
		_tagUnits = new Dictionary<Tag.ID, UIUnit>();
		for(int i = 0; i < _EQUIP_SLOTS.Length; i++){
			GameObject go = Instantiate(_prefabUIItem, _content);
			UIUnit uiUnit = go.GetComponent<UIUnit>();
			uiUnit.Setup(_unit.GetTag(_local, _EQUIP_SLOTS[i]).GetIGetUnit().GetUnit(_local, _unit), EquipInteract);
			_tagUnits.Add(_EQUIP_SLOTS[i], uiUnit);
		}
	}
	public void RefreshTags(){
		for(int i = 0; i < _EQUIP_SLOTS.Length; i++){
			SetTagUnit(_EQUIP_SLOTS[i]);
		}
	}
	public void SetTagUnit(Tag.ID id){
		if(_tagUnits.TryGetValue(id, out UIUnit uiUnit)){
			uiUnit.Setup(_unit.GetTag(_local, id).GetIGetUnit().GetUnit(_local, _unit), EquipInteract);
		}
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
		_unit.GetTag(_local, Tag.ID.Attack_Slot).OnTagUpdate -= OnWeaponTagUpdate;
		_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate -= OnLightTagUpdate;
	}
	public void Clear(){
		if(_uiUnits != null){
			foreach(KeyValuePair<Unit, UIUnit> keyValue in _uiUnits){
				keyValue.Value.UnitDestroy();
			}
		}
		_uiUnits = new Dictionary<Unit, UIUnit>();
	}
	public void InventoryInteract(Unit item){
		Unit player = PlayerController.GetInstance().GetPlayer();
		if(Input.GetKey(KeyCode.LeftShift)){
			item.GetTag(_local, Tag.ID.Pickup).GetIRemoveUnit().Remove(_local, item, player);
		}else{
			item.GetTag(_local, Tag.ID.Equippable).GetIAddUnit().Add(_local, item, player);
		}
	}
	public void EquipInteract(Unit item){
		Unit player = PlayerController.GetInstance().GetPlayer();
		item.GetTag(_local, Tag.ID.Equippable).GetIRemoveUnit().Remove(_local, item, player);
		if(Input.GetKey(KeyCode.LeftShift)){
			item.GetTag(_local, Tag.ID.Pickup).GetIRemoveUnit().Remove(_local, item, player);
		}
	}
	private void OnObjectAdded(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		CreateUIUnit(e.value);
	}
	private void OnObjectRemoved(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		RemoveUIUnit(e.value);
	}
	private void OnWeaponTagUpdate(object sender, EventArgs e){
		SetTagUnit(Tag.ID.Attack_Slot);
	}
	private void OnLightTagUpdate(object sender, EventArgs e){
		SetTagUnit(Tag.ID.Light);
	}
	private void OnChestplateTagUpdate(object sender, EventArgs e){
		SetTagUnit(Tag.ID.Chestplate);
	}
	private void OnBootsTagUpdate(object sender, EventArgs e){
		SetTagUnit(Tag.ID.Boots);
	}
	public static IInventoryUIManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_INVENTORY_UI;
		}else{
			return _INSTANCE;
		}
	}
	*/
}
