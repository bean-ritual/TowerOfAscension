using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIWindowManager : MonoBehaviour{
	public enum UIAnchor{
		BottomLeft,
		BottomMiddle,
		BottomRight,
		MiddleLeft,
		Centre,
		MiddleRight,
		TopLeft,
		TopMiddle,
		TopRight,
	};
	private static readonly Vector2[] _anchorVectors = {
		new Vector2(0, 0),
		new Vector2(0.5f, 0),
		new Vector2(1, 0),
		new Vector2(0, 0.5f),
		new Vector2(0.5f, 0.5f),
		new Vector2(1, 0.5f),
		new Vector2(0, 1),
		new Vector2(0.5f, 1),
		new Vector2(1, 1),
	};
	[Serializable()]
	public struct UISizeData{
		public bool forceAlways;
		public Vector2 position;
		public Vector2 windowSize;
		public Vector2 minSize;
		public Vector2 maxSize;
		public UISizeData(bool forceAlways, Vector2 position, Vector2 windowSize, Vector2 minSize, Vector2 maxSize){
			this.forceAlways = forceAlways;
			this.position = position;
			this.windowSize = windowSize;
			this.minSize = minSize;
			this.maxSize = maxSize;
		}
	};
	private const UIAnchor _POSITION_ANCHOR = UIAnchor.BottomLeft;
	private bool _forceAlways;
	private bool _actualActive;
	private Canvas _canvas;
	private RectTransform _canvasRect;
	private GameObject _windowObject;
	[SerializeField]private RectTransform _windowRect;
	[SerializeField]private Vector2 _minSize;
	[SerializeField]private Vector2 _maxSize;
	[SerializeField]private TextMeshProUGUI _headerText;
	[SerializeField]private GameObject _content;
	[SerializeField]private Image _background;
	[SerializeField]private Toggle _toggle;
	[SerializeField]private GameObject _toggleObject;
	private void Awake(){
		_toggle.onValueChanged.AddListener(delegate {ToggleForceAlways();});
	}
	public void Setup(string headerText, bool toggleable, Canvas canvas, UISizeData data){
		SetHeader(headerText);
		_canvas = canvas;
		_canvasRect = _canvas.GetComponent<RectTransform>();
		if(_windowRect == null){
			_windowRect = gameObject.GetComponent<RectTransform>();
		}
		_toggleObject.SetActive(toggleable);
		_windowObject = _windowRect.gameObject;
		SetForceAlways(data.forceAlways);
		_minSize = data.minSize;
		_maxSize = data.maxSize;
		ResizeWindow(data.windowSize, _POSITION_ANCHOR);
		SetPosition(data.position);
	}
	public void SetActive(bool active){
		_actualActive = active;
		if(_forceAlways){
			_windowObject.SetActive(true);
			return;
		}
		_windowObject.SetActive(active);
	}
	public void RefreshActive(){
		SetActive(_actualActive);
	}
	public void SetHeader(string header){
		_headerText.text = header;
	}
	public void SetAnchor(UIAnchor anchor){
		Vector2 local = _windowRect.localPosition;
		Vector2 vector = GetAnchorVector(anchor);
		_windowRect.anchorMin = vector;
		_windowRect.anchorMax = vector;
		_windowRect.localPosition = local;
		Vector3 delta = _windowRect.pivot - vector;
		delta.Scale(_windowRect.rect.size);
		delta.Scale(_windowRect.localScale);
		_windowRect.pivot = vector;
		_windowRect.localPosition -= delta;
	}
	public void SetPosition(Vector2 position){
		SetAnchor(_POSITION_ANCHOR);
		SetPositionFromBottomLeft(position);
	}
	public void SetDeltaPosition(Vector2 delta){
		SetAnchor(_POSITION_ANCHOR);
		SetPositionFromBottomLeft(_windowRect.anchoredPosition + (delta / _canvas.scaleFactor));
	}
	public void ResetPosition(){
		SetAnchor(_POSITION_ANCHOR);
		SetPositionFromBottomLeft(_windowRect.anchoredPosition);
	}
	public void ResizeWindow(Vector2 size, UIAnchor anchor){
		SetAnchor(anchor);
		if(size.x < _minSize.x){
			size.x = _minSize.x;
		}
		if( size.y < _minSize.y){
			size.y = _minSize.y;
		}
		if(size.x > _maxSize.x){
			size.x = _maxSize.x;
		}
		if(size.y > _maxSize.y){
			size.y = _maxSize.y;
		}
		_windowRect.sizeDelta = size;
		ResetPosition();
	}
	public void ResizeDeltaWindow(Vector2 delta, UIAnchor anchor){
		ResizeWindow((_windowRect.sizeDelta + (delta / _canvas.scaleFactor)), anchor);
	}
	public void SendWindowToFront(){
		_windowRect.SetAsLastSibling();
	}
	public void SetBackgroundTransparency(float value){
		Color colour = _background.color;
		colour.a = value;
		_background.color = colour;
	}
	public void ToggleForceAlways(){
		_forceAlways = _toggle.isOn;
		RefreshActive();
	}
	public void SetForceAlways(bool forceAlways){
		_forceAlways = forceAlways;
		_toggle.isOn = forceAlways;
		RefreshActive();
	}
	public bool GetForceAlways(){
		return _forceAlways;
	}
	public GameObject GetContent(){
		return _content;
	}
	public UISizeData GetUISizeData(){
		SetAnchor(_POSITION_ANCHOR);
		return new UISizeData(_forceAlways, _windowRect.anchoredPosition, _windowRect.sizeDelta, _minSize, _maxSize);
	}
	private void SetPositionFromBottomLeft(Vector2 position){
		const int CANVAS_EDGE = 0;
		float canvasWidth = _canvasRect.rect.width;
		float canvasHeight = _canvasRect.rect.height;
		float windowWidth = _windowRect.rect.width;
		float windowHeight = _windowRect.rect.height;
		if((position.x + windowWidth) > canvasWidth){
			position.x = (canvasWidth - windowWidth);
		}
		if((position.y + windowHeight) > canvasHeight){
			position.y = (canvasHeight - windowHeight);
		}
		if(CANVAS_EDGE > position.x){
			position.x = CANVAS_EDGE;
		}
		if(CANVAS_EDGE > position.y){
			position.y = CANVAS_EDGE;
		}
		_windowRect.anchoredPosition = position;
	}
	private Vector3 GetAnchorVector(UIAnchor anchor){
		switch(anchor){
			default: return _anchorVectors[0];
			case UIAnchor.BottomLeft: return _anchorVectors[0];
			case UIAnchor.BottomMiddle: return _anchorVectors[1];
			case UIAnchor.BottomRight: return _anchorVectors[2];
			case UIAnchor.MiddleLeft: return _anchorVectors[3];
			case UIAnchor.Centre: return _anchorVectors[4];
			case UIAnchor.MiddleRight: return _anchorVectors[5];
			case UIAnchor.TopLeft: return _anchorVectors[6];
			case UIAnchor.TopMiddle: return _anchorVectors[7];
			case UIAnchor.TopRight: return _anchorVectors[8];
		}
	}
}
