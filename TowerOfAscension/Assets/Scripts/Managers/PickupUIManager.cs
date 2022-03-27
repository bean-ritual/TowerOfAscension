using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickupUIManager : MonoBehaviour{
	private static PickupUIManager _INSTANCE;
	private Level _level = Level.GetNullLevel();
	private Tile _tile = Tile.GetNullTile();
	private Dictionary<Unit, UIUnit> _uiUnits;
	private UIWindowManager _uiWindow;
	private RectTransform _content;
	[SerializeField]private GameObject _prefabUIWindow;
	[SerializeField]private GameObject _prefabUIInventory;
	[SerializeField]private GameObject _prefabUIItem;
	[SerializeField]private Canvas _canvas;
	private void OnDestroy(){
		UnsubcribeFromEvents();
	}
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
		}
		_INSTANCE = this;
		BuildUI();
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
	}
	public void BuildUI(){
		GameObject go = Instantiate(_prefabUIWindow, this.transform);
		_uiWindow = go.GetComponent<UIWindowManager>();
		_uiWindow.Setup(
			"Ground",
			true,
			_canvas,
			new UIWindowManager.UISizeData(
				false,
				new Vector2(500, 500),
				new Vector2(250, 250),
				new Vector2(100, 100),
				new Vector2(1000, 1000)
			)
		);
		_uiWindow.SetActive(true);
		GameObject go2 =  Instantiate(_prefabUIInventory, _uiWindow.GetContent().transform);
		_content = go2.GetComponent<ContentManager>().GetContent();
	}
	public void SetTile(Tile tile){
		if(_tile == tile){
			return;
		}
		UnsubcribeFromEvents();
		_uiUnits = new Dictionary<Unit, UIUnit>();
		GameObjectUtils.DestroyAllChildren(_content);
		_tile = tile;
		List<Unit> units = _tile.GetHasUnits().GetUnits(_level);
		for(int i = 0; i < units.Count; i++){
			CreateUIUnit(units[i]);
		}
		_tile.GetHasUnits().OnUnitAdded += OnUnitAdded;
		_tile.GetHasUnits().OnUnitRemoved += OnUnitRemoved;
	}
	public void CreateUIUnit(Unit unit){
		if(unit is Unit.IPickupable && !unit.IsNull()){
			GameObject go = Instantiate(_prefabUIItem, _content);
			UIUnit uiUnit = go.GetComponent<UIUnit>();
			uiUnit.Setup(unit, PickupInteract);
			_uiUnits.Add(unit, uiUnit);
		}
	}
	public void RemoveUIUnit(Unit unit){
		if(!_uiUnits.TryGetValue(unit, out UIUnit uiUnit)){
			return;
		}
		_uiUnits.Remove(unit);
		uiUnit.UnitDestroy();
	}
	public void UnsubcribeFromEvents(){
		_tile.GetHasUnits().OnUnitAdded -= OnUnitAdded;
		_tile.GetHasUnits().OnUnitRemoved -= OnUnitRemoved;
	}
	public void PickupInteract(Unit item){
		item.GetPickupable().TryPickup(_level, PlayerController.GetInstance().GetPlayer());
	}
	private void OnUnitAdded(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		CreateUIUnit(e.value);
	}
	private void OnUnitRemoved(object sender, Register<Unit>.OnObjectChangedEventArgs e){
		RemoveUIUnit(e.value);
	}
	public static PickupUIManager GetInstance(){
		return _INSTANCE;
	}
}
