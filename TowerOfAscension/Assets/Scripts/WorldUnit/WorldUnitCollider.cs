using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnitCollider : MonoBehaviour{
	public interface IToolTip{
		string GetToolTip();
	}
	public class NullToolTip : IToolTip{
		public string GetToolTip(){
			const string NULL = "";
			return NULL;
		}
	}
	private static NullToolTip _NULL_TOOLTIP = new NullToolTip();
	private IToolTip _toolTip = _NULL_TOOLTIP;
	private bool _highlighted = false;
	private void OnDisable(){
		if(_highlighted){
			ToolTipManager.GetInstance().HideToolTip();
			_highlighted = false;
		}
	}
	public void Setup(IToolTip toolTip){
		_toolTip = toolTip;
	}
	public void SetActive(bool active){
		this.gameObject.SetActive(active);
	}
	public void SetPosition(Vector3 position){
		this.transform.localPosition = position;
	}
	private void OnMouseEnter(){
		ToolTipManager.GetInstance().ShowToolTip(_toolTip.GetToolTip());
		_highlighted = true;
	}
	private void OnMouseExit(){
		ToolTipManager.GetInstance().HideToolTip();
		_highlighted = false;
	}
	public static IToolTip GetNullToolTip(){
		return _NULL_TOOLTIP;
	}
}
