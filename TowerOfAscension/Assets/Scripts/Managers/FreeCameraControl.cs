using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FreeCameraControl : MonoBehaviour{
	private Vector3 _position;
	private float _zoom;
	private float _time;
	[SerializeField]private CameraManager _camera;
	[SerializeField]private float _moveSpeed;
	[SerializeField]private float _zoomSpeed;
	private void Start(){
		_camera.Setup(GetPosition, GetZoom);
	}
	private void Update(){
		const string HORIZONTAL = "Horizontal";
		const string VERTICAL = "Vertical";
		_time = Time.deltaTime;
		_zoom -= ((Input.mouseScrollDelta.y * _time) * _zoomSpeed);
		_position.x += ((Input.GetAxisRaw(HORIZONTAL) * _time) * _moveSpeed);
		_position.y += ((Input.GetAxisRaw(VERTICAL) * _time) * _moveSpeed);
		
	}
	private Vector3 GetPosition(){
		return _position;
	}
	private float GetZoom(){
		return _zoom;
	}
}
