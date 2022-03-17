using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraManager : MonoBehaviour{
	private Func<Vector3> _following;
	private Func<float> _zooming;
	[SerializeField]private Vector3 _offset = new Vector3(0, 0, -10);
	[SerializeField]private float _followSpeed = 0.1f;
	[SerializeField]private Camera _camera;
	private void Update(){
		Movement();
		Zoom();
	}
	private void Movement(){
		if(_following == null){
			return;
		}
		Vector3 follow = _following() + _offset;
		Vector3 smooth = Vector3.Lerp(_camera.transform.position, follow, _followSpeed);
		transform.position = smooth;
	}
	private void Zoom(){
		if(_zooming == null){
			return;
		}
		_camera.orthographicSize = _zooming();
	}
	public void Setup(Vector3 position, float zoom){
		Setup(() => position, () => zoom);
	}
	public void Setup(Func<Vector3> following, Func<float> zooming, bool snapTo = false){
		Setup(following, zooming, _offset, _followSpeed, snapTo);
	}
	public void Setup(Func<Vector3> following, Func<float> zooming, Vector3 offset, float followSpeed, bool snapTo = false){
		_following = following;
		_zooming = zooming;
		_offset = offset;
		_followSpeed = followSpeed;
		if(snapTo){
			transform.position = following();
		}
	}
	public Camera GetCamera(){
		return _camera;
	}
}