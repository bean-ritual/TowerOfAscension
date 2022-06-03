using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryUIManager : 
	MonoBehaviour,
	InventoryUIManager.IInventoryUIManager
	{
	public interface IInventoryUIManager{
		void SetData(Data data);
	}
	public class NullInventoryUIManager : IInventoryUIManager{
		public void SetData(Data data){}
	}
	private static InventoryUIManager _INSTANCE;
	
	private Game _game = Game.GetNullGame();
	private Data _player = Data.GetNullData();
	//
	private Dictionary<int, UIData> _equipment;
	private Dictionary<int, UIData> _inventory;
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
		if(_INSTANCE == null){
			_INSTANCE = this;
		}else{
			Destroy(gameObject);
		}
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		BuildUI();
		//SetupTags();
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
	public void SetData(Data data){
		UnsubcribeFromEvents();
		_player = data;
		_inventory = new Dictionary<int, UIData>();
		//RefreshTags();
		//_inventory = unit.GetTag(_local, Tag.ID.Inventory).GetIGetRegisterEvents().GetRegisterEvents(_local, unit);
		IListData listData = data.GetBlock(_game, Game.TOAGame.BLOCK_INVENTORY).GetIListData();
		//List<Unit> items = _inventory.GetAll();
		for(int i = 0; i < listData.GetDataCount(); i++){
			CreateUIData(listData.GetData(_game, i));
		}
		_player.OnBlockUpdate += OnBlockUpdate;
		_player.OnBlockDataAdd += OnBlockDataAdd;
		_player.OnBlockDataRemove += OnBlockDataRemove;
		//_inventory.OnObjectAdded += OnObjectAdded;
		//_inventory.OnObjectRemoved += OnObjectRemoved;
		//_unit.GetTag(_local, Tag.ID.Attack_Slot).OnTagUpdate += OnWeaponTagUpdate;
		//_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate += OnLightTagUpdate;
	}
	/*
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
	*/
	public void CreateUIData(Data data){
		GameObject go = Instantiate(_prefabUIItem, _content);
		UIData uiData = go.GetComponent<UIData>();
		uiData.Setup(data, InventoryInteract);
		_inventory.Add(data.GetID(), uiData);
	}
	public void RemoveUIData(int dataID){
		if(!_inventory.TryGetValue(dataID, out UIData uiData)){
			return;
		}
		_inventory.Remove(dataID);
		uiData.Disassemble();
	}
	public void UnsubcribeFromEvents(){
		_player.OnBlockUpdate -= OnBlockUpdate;
		_player.OnBlockDataAdd -= OnBlockDataAdd;
		_player.OnBlockDataRemove -= OnBlockDataRemove;
		/*
		_inventory.OnObjectAdded -= OnObjectAdded;
		_inventory.OnObjectRemoved -= OnObjectRemoved;
		_unit.GetTag(_local, Tag.ID.Attack_Slot).OnTagUpdate -= OnWeaponTagUpdate;
		_unit.GetTag(_local, Tag.ID.Light).OnTagUpdate -= OnLightTagUpdate;
		*/
	}
	/*
	public void Clear(){
		if(_uiUnits != null){
			foreach(KeyValuePair<Unit, UIUnit> keyValue in _uiUnits){
				keyValue.Value.UnitDestroy();
			}
		}
		_uiUnits = new Dictionary<Unit, UIUnit>();
	}
	*/
	public void InventoryInteract(Data item){
		/*
		Unit player = PlayerController.GetInstance().GetPlayer();
		if(Input.GetKey(KeyCode.LeftShift)){
			item.GetTag(_local, Tag.ID.Pickup).GetIRemoveUnit().Remove(_local, item, player);
		}else{
			item.GetTag(_local, Tag.ID.Equippable).GetIAddUnit().Add(_local, item, player);
		}
		*/
	}
	public void EquipInteract(Data item){
		/*
		Unit player = PlayerController.GetInstance().GetPlayer();
		item.GetTag(_local, Tag.ID.Equippable).GetIRemoveUnit().Remove(_local, item, player);
		if(Input.GetKey(KeyCode.LeftShift)){
			item.GetTag(_local, Tag.ID.Pickup).GetIRemoveUnit().Remove(_local, item, player);
		}
		*/
	}
	private void OnBlockUpdate(object sender, Data.BlockUpdateEventArgs e){
		//
	}
	private void OnBlockDataAdd(object sender, Data.BlockDataUpdateEventArgs e){
		CreateUIData(_game.GetGameData().Get(e.newDataID));
	}
	private void OnBlockDataRemove(object sender, Data.BlockDataUpdateEventArgs e){
		RemoveUIData(e.newDataID);
	}
	/*
	private void OnObjectAdded(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		CreateUIUnit(e.value);
	}
	private void OnObjectRemoved(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		RemoveUIUnit(e.value);
	}
	*/
	/*
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
	*/
	private static NullInventoryUIManager _NULL_INVENTORY_UI = new NullInventoryUIManager();
	public static IInventoryUIManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_INVENTORY_UI;
		}else{
			return _INSTANCE;
		}
	}
}
