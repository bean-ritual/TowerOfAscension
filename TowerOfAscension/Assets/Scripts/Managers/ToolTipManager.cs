using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ToolTipManager : 
	MonoBehaviour,
	ToolTipManager.IToolTipManager
	{
	public interface IToolTipManager{
		void ShowToolTip(string text);
		void HideToolTip();
	}
	[Serializable]
	public class NullToolTip : IToolTipManager{
		public NullToolTip(){}
		public void ShowToolTip(string text){}
		public void HideToolTip(){}
	}
	private static ToolTipManager _INSTANCE;
	private static NullToolTip _NULL_TOOLTIP = new NullToolTip();
	private Vector2 _anchor;
	[SerializeField]private RectTransform _canvasRect;
	[SerializeField]private TMP_Text _text;
	[SerializeField]private GameObject _control;
	[SerializeField]private RectTransform _toolTipRect;
	[SerializeField]private Vector2 _padding;
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
		HideToolTip();
	}
	private void Update(){
		_anchor = (Input.mousePosition / _canvasRect.localScale.x);
		if((_anchor.x + _toolTipRect.rect.width) > _canvasRect.rect.width){
			_anchor.x = _canvasRect.rect.width - _toolTipRect.rect.width;
		}
		if((_anchor.y + _toolTipRect.rect.height) > _canvasRect.rect.height){
			_anchor.y = _canvasRect.rect.height - _toolTipRect.rect.height;
		}
		_toolTipRect.anchoredPosition = _anchor;
	}
	public void ShowToolTip(string text){
		if(string.IsNullOrEmpty(text)){
			return;
		}
		_control.SetActive(true);
		_text.SetText(text);
		_text.ForceMeshUpdate();
		_toolTipRect.sizeDelta = _text.GetRenderedValues(false) + _padding;
	}
	public void HideToolTip(){
		_control.SetActive(false);
	}
	public static IToolTipManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_TOOLTIP;
		}
		return _INSTANCE;
	}
}
