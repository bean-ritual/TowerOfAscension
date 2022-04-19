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
	private VisualController _controller = VisualController.GetNullVisualController();
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
		_controller = unit.GetVisualController().GetVisualController(_local);
		_controller.OnSpriteUpdate += OnSpriteUpdate;
		_controller.OnItemBorderUpdate += OnItemBorderUpdate;
		this.Interact = Interact;
		_button.onClick.AddListener(OnClick);
		Refresh();
	}
	public void Refresh(){
		RefreshSprite();
		RefreshBorder();
	}
	public void RefreshSprite(){
		_image.sprite = _controller.GetSprite();
	}
	public void RefreshBorder(){
		_border.SetActive(_controller.GetItemBorder());
	}
	public void OnClick(){
		Interact(_unit); 
	}
	public void UnsubcribeFromEvents(){
		_controller.OnSpriteUpdate -= OnSpriteUpdate;
		_controller.OnItemBorderUpdate -= OnItemBorderUpdate;
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
	private void OnSpriteUpdate(object sender, EventArgs e){
		RefreshSprite();
	}
	private void OnItemBorderUpdate(object sender, EventArgs e){
		RefreshBorder();
	}
	public static ButtonInteract GetNullInteract(){
		return _NULL_INTERACT;
	}
}
