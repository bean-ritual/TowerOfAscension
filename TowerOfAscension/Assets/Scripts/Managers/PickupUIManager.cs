using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickupUIManager : 
	MonoBehaviour,
	PickupUIManager.IPickupUIManager
	{
	public interface IPickupUIManager{
		void SetData(Data data);
	}
	public class NullPickupUIManager : IPickupUIManager{
		public void SetData(Data data){}
	}
	private static PickupUIManager _INSTANCE;
	private Game _game	= Game.GetNullGame();
	private Data _player = Data.GetNullData();
	private Data _tile = Data.GetNullData();
	private Dictionary<int, UIData> _floor;
	private UIWindowManager _uiWindow;
	private RectTransform _content;
	[SerializeField]private GameObject _prefabUIWindow;
	[SerializeField]private GameObject _prefabUIInventory;
	[SerializeField]private GameObject _prefabUIItem;
	[SerializeField]private Canvas _canvas;
	private void OnDisable(){
		SettingsSystem.GetConfig().ground = _uiWindow.GetUISizeData();
		UnsubcribeFromEvents();
		_player.OnBlockUpdate -= OnPlayerBlockUpdate;
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
	}
	public void BuildUI(){
		GameObject go = Instantiate(_prefabUIWindow, this.transform);
		_uiWindow = go.GetComponent<UIWindowManager>();
		_uiWindow.Setup(
			"Ground",
			false,
			_canvas,
			SettingsSystem.GetConfig().ground
		);
		GameObject go2 =  Instantiate(_prefabUIInventory, _uiWindow.GetContent().transform);
		_content = go2.GetComponent<ContentManager>().GetContent();
	}
	public void SetData(Data player){
		_player.OnBlockUpdate -= OnPlayerBlockUpdate;
		_player = player;
		_player.OnBlockUpdate += OnPlayerBlockUpdate;
		RefeshTile();
	}
	public void RefeshTile(){
		_tile = _player.GetBlock(_game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetTile(_game).GetIDataTile().GetData(_game);
		UnsubcribeFromEvents();
		_floor = new Dictionary<int, UIData>();
		GameObjectUtils.DestroyAllChildren(_content);
		IListData listData = _tile.GetBlock(_game, Game.TOAGame.BLOCK_TILE).GetIListData();
		for(int i = 0; i < listData.GetDataCount(); i++){
			CreateUIData(listData.GetData(_game, i));
		}
		_tile.OnBlockUpdate += OnBlockUpdate;
		_tile.OnBlockDataAdd += OnBlockDataAdd;
		_tile.OnBlockDataRemove += OnBlockDataRemove;
		CheckExistance();
	}
	public void CreateUIData(Data data){
		if(!data.GetBlock(_game, Game.TOAGame.BLOCK_ITEM).IsNull()){
			GameObject go = Instantiate(_prefabUIItem, _content);
			UIData uiData = go.GetComponent<UIData>();
			uiData.Setup(data, PickupInteract);
			_floor.Add(data.GetID(), uiData);
		}
	}
	public void RemoveUIData(int dataID){
		if(!_floor.TryGetValue(dataID, out UIData uiData)){
			return;
		}
		_floor.Remove(dataID);
		uiData.Disassemble();
	}
	public void CheckExistance(){
		_uiWindow.SetActive(_floor.Count > 0);
	}
	public void UnsubcribeFromEvents(){
		_tile.OnBlockUpdate -= OnBlockUpdate;
		_tile.OnBlockDataAdd -= OnBlockDataAdd;
		_tile.OnBlockDataRemove -= OnBlockDataRemove;
	}
	public void PickupInteract(Data item){
		item.GetBlock(_game, Game.TOAGame.BLOCK_ITEM).GetIPickup().Pickup(_game, PlayerController.GetInstance().GetPlayer());
		//item.GetTag(_local, Tag.ID.Pickup).GetIAddUnit().Add(_local, item, PlayerController.GetInstance().GetPlayer());
		//item.GetPickupable().TryPickup(_local, PlayerController.GetInstance().GetPlayer());
	}
	private void OnPlayerBlockUpdate(object sender, Data.BlockUpdateEventArgs e){
		if(e.blockID == Game.TOAGame.BLOCK_WORLD){
			RefeshTile();
		}
	}
	private void OnBlockUpdate(object sender, Data.BlockUpdateEventArgs e){
		//RefeshTile();
	}
	private void OnBlockDataAdd(object sender, Data.BlockDataUpdateEventArgs e){
		CreateUIData(_game.GetGameData().Get(e.newDataID));
	}
	private void OnBlockDataRemove(object sender, Data.BlockDataUpdateEventArgs e){
		RemoveUIData(e.newDataID);
	}
	//
	private static NullPickupUIManager _NULL_PICKUP_UI_MANAGER = new NullPickupUIManager();
	public static IPickupUIManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_PICKUP_UI_MANAGER;
		}else{
			return _INSTANCE;
		}
	}
}
