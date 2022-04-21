using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIUnit : 
	MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler
	{
	public delegate void ButtonInteract(Unit unit);
	private static ButtonInteract _NULL_INTERACT = (Unit unit) => {};
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	private ButtonInteract Interact = _NULL_INTERACT;
	[SerializeField]private Button _button;
	[SerializeField]private Image _image;
	[SerializeField]private GameObject _border;
	private void OnDestroy(){
		UnsubcribeFromEvents();
	}
	public void Setup(Unit unit, ButtonInteract Interact){
		UnsubcribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		gameObject.SetActive(!_unit.IsNull());
		_unit.GetTaggable().GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate += OnWorldUnitTagUpdate;
		_unit.GetTaggable().GetTag(_local, Tag.ID.UIUnit).OnTagUpdate += OnUIUnitTagUpdate;
		this.Interact = Interact;
		_button.onClick.AddListener(OnClick);
		Refresh();
	}
	public void Refresh(){
		RefreshSprite();
		RefreshBorder();
	}
	public void RefreshSprite(){
		_image.sprite = _unit.GetTaggable().GetTag(_local, Tag.ID.WorldUnit).GetIGetSprite().GetSprite(_local, _unit);
	}
	public void RefreshBorder(){
		_border.SetActive(_unit.GetTaggable().GetTag(_local, Tag.ID.UIUnit).GetICondition().Check(_local, _unit));
	}
	public void OnClick(){
		Interact(_unit); 
	}
	public void UnsubcribeFromEvents(){
		_unit.GetTaggable().GetTag(_local, Tag.ID.WorldUnit).OnTagUpdate -= OnWorldUnitTagUpdate;
		_unit.GetTaggable().GetTag(_local, Tag.ID.UIUnit).OnTagUpdate -= OnUIUnitTagUpdate;
	}
	public void UnitDestroy(){
		ToolTipManager.GetInstance().HideToolTip();
		Destroy(gameObject);
	}
	public void OnPointerEnter(PointerEventData eventData){
		ToolTipManager.GetInstance().ShowToolTip("bing");
	}
	public void OnPointerExit(PointerEventData eventData){
		ToolTipManager.GetInstance().HideToolTip();
	}
	private void OnWorldUnitTagUpdate(object sender, EventArgs e){
		RefreshSprite();
	}
	private void OnUIUnitTagUpdate(object sender, EventArgs e){
		RefreshBorder();
	}
	public static ButtonInteract GetNullInteract(){
		return _NULL_INTERACT;
	}
}
