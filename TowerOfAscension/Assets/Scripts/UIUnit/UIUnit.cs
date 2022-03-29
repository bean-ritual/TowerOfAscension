using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUnit : MonoBehaviour{
	public delegate void ButtonInteract(Unit unit);
	private static ButtonInteract _NULL_INTERACT = (Unit unit) => {};
	private Game _local = Game.GetNullGame();
	private Unit _unit = Unit.GetNullUnit();
	private VisualController _controller = VisualController.GetNullVisualController();
	private ButtonInteract Interact = _NULL_INTERACT;
	[SerializeField]private Button _button;
	[SerializeField]private Image _image;
	private void OnDestroy(){
		UnsubcribeFromEvents();
	}
	public void Setup(Unit unit, ButtonInteract Interact){
		UnsubcribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_controller = unit.GetVisualController().GetVisualController(_local);
		this.Interact = Interact;
		_button.onClick.AddListener(OnClick);
		Refresh();
	}
	public void Refresh(){
		_image.sprite = _controller.GetSprite();
	}
	public void OnClick(){
		Interact(_unit);
	}
	public void UnsubcribeFromEvents(){
		
	}
	public void UnitDestroy(){
		Destroy(gameObject);
	}
	public static ButtonInteract GetNullInteract(){
		return _NULL_INTERACT;
	}
}
