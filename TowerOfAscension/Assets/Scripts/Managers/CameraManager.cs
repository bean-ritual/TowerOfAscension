using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour{
	private Func<Vector3> CameraPosition;
	private Func<float> CameraZoom;
	[SerializeField]private Camera _camera;
	[SerializeField]private Vector3 _defaultPosition;
	[SerializeField]private float _defaultZoom;
	[SerializeField]private float _followSpeed;
	private void Awake(){
		const float ZERO_ZOOM = 0f;
		Setup(Vector3.zero, ZERO_ZOOM, true);
	}
	private void Update(){
		Movement();
		Zoom();
	}
	private void Movement(){
		Vector3 follow = CameraPosition() + _defaultPosition;
		Vector3 smooth = Vector3.Lerp(_camera.transform.position, follow, (_followSpeed * Time.deltaTime));
		transform.position = smooth;
	}
	private void Zoom(){
		//const float MIN_ZOOM = 1f;
		//const float MAX_ZOOM = 100f;
		//_camera.orthographicSize = Mathf.Clamp(CameraZoom(), MIN_ZOOM, MAX_ZOOM);
		_camera.orthographicSize = CameraZoom() + _defaultZoom;
	}
	public void Setup(Vector3 position, float zoom, bool snapTo = false){
		Setup(() => position, () => zoom, snapTo);
	}
	public void Setup(Func<Vector3> CameraPosition, Func<float> CameraZoom, bool snapTo = false){
		this.CameraPosition = CameraPosition;
		this.CameraZoom = CameraZoom;
		if(snapTo){
			transform.position = CameraPosition();
		}
	}
	public Camera GetCamera(){
		return _camera;
	}
}