using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TextureNavigationManager : 
	MonoBehaviour, 
	IDragHandler, 
	IScrollHandler, 
	IPointerDownHandler
	{
	private CameraManager _camera;
	private UIWindowManager _window;
	//
	private float _zoom;
	private Vector3 _delta;
	private Level _level = Level.GetNullLevel();
	private Unit _target = Unit.GetNullUnit();
	[SerializeField]private RawImage _image;
	[SerializeField]private Button _resetButton;
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
	}
	public void Setup(CameraManager camera, UIWindowManager window){
		Reset();
		_camera = camera;
		_window = window;
		_resetButton.onClick.AddListener(Reset);
		_camera.Setup(GetPosition, GetZoom, true);
	}
	public void SetUnit(Unit target){
		_target = target;
	}
	public void OnDrag(PointerEventData eventData){
		const int DRAG_SENSITIVITY = 10;
		_delta.x -= (eventData.delta.x / DRAG_SENSITIVITY);
		_delta.y -= (eventData.delta.y / DRAG_SENSITIVITY);
	}
	public void OnScroll(PointerEventData eventData){
		const int SCROLL_SENSITIVITY = 2;
		_zoom += (eventData.scrollDelta.x / SCROLL_SENSITIVITY);
		_zoom += (eventData.scrollDelta.y / SCROLL_SENSITIVITY);
	}
	public void OnPointerDown(PointerEventData eventData){
		_window.SendWindowToFront();
	}
	public Vector3 GetPosition(){
		return _target.GetPositionable().GetPosition(_level) + _delta;
	}
	public float GetZoom(){
		return _zoom;
	}
	public void Reset(){
		const float DEFAULT_ZOOM = 10f;
		_zoom = DEFAULT_ZOOM;
		_delta = Vector3.zero;
	}
	public UIWindowManager GetWindow(){
		return _window;
	}
}
