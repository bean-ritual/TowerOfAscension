using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIData : 
	MonoBehaviour,
	IPointerEnterHandler,
	IPointerExitHandler
	{
	public delegate void ButtonInteract(Data data);
	private static ButtonInteract _NULL_INTERACT = (Data data) => {};
	private Game _game = Game.GetNullGame();
	private Data _data = Data.GetNullData();
	private ButtonInteract Interact = _NULL_INTERACT;
	private bool _highlighted = false;
	[SerializeField]private Button _button;
	[SerializeField]private Image _image;
	private void OnDestroy(){
		//UnsubcribeFromEvents();
	}
	private void OnDisable(){
		if(_highlighted){
			ToolTipManager.GetInstance().HideToolTip();
			_highlighted = false;
		}
	}
	public void Setup(Data data, ButtonInteract Interact){
		_data = data;
		_game = DungeonMaster.GetInstance().GetGame();
		_image.sprite = SpriteSheet.SPRITESHEET_DATA.GetSprite(_data.GetBlock(_game, 2).GetIVisual().GetSprite(_game));
		this.gameObject.SetActive(!_data.IsNull());
		this.Interact = Interact;
		_button.onClick.AddListener(OnClick);
	}
	public void OnClick(){
		Interact(_data); 
	}
	public void Disassemble(){
		ToolTipManager.GetInstance().HideToolTip();
		Destroy(gameObject);
	}
	public void OnPointerEnter(PointerEventData eventData){
		//ToolTipManager.GetInstance().ShowToolTip(_unit.GetTag(_local, Tag.ID.Tooltip).GetIGetStringValue1().GetStringValue1(_local, _unit));
		_highlighted = true;
	}
	public void OnPointerExit(PointerEventData eventData){
		ToolTipManager.GetInstance().HideToolTip();
		_highlighted = false;
	}
	public static ButtonInteract GetNullInteract(){
		return _NULL_INTERACT;
	}
}
