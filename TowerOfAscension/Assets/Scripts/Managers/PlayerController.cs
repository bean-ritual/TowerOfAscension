using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour{
	private static PlayerController _instance;
	private Level _level;
	private Unit _player;
	private Unit _previous;
	[SerializeField]private float _cameraZoom = 7.5f;
	[SerializeField]private float _cameraSpeed = 0.1f;
	[SerializeField]private Vector3 _cameraOffset = new Vector3(0, 0, -10);
	[SerializeField]private CameraManager _camera;
	private void OnDestroy(){
		PlayerControl.OnPlayerControl -= OnPlayerControl;
		_level.OnNextTurn -= OnNextTurn;
	}
	private void Awake(){
		if(_instance != null){
			Destroy(gameObject);
		}
		_instance = this;
	}
	private void Start(){
		_level = DungeonMaster.GetInstance().GetLevel();
		_player = Unit.GetNullUnit();
		_previous = Unit.GetNullUnit();
		_level.OnNextTurn += OnNextTurn;
		PlayerControl.OnPlayerControl += OnPlayerControl;
	}
	private void Update(){
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		Unit.Direction dir = MouseDirectionHandling();
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.A)){
				return;
			}
			if(Input.GetKey(KeyCode.LeftShift)){
				return;
			}
			_player.GetMoveable().Move(_level, dir);
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			_player.GetControllable().GetAI().GetTurnControl().EndTurn(_level, _player);
			return;
		}
	}
	private Unit.Direction MouseDirectionHandling(){
		Vector3 finalPosition = WorldSpaceUtils.MouseToWorldSpace(_camera.GetCamera()) - _player.GetPositionable().GetPosition(_level) - _level.GetVector3CellOffset();
		Vector3Int intgerPosition = Vector3Int.RoundToInt(finalPosition);
		return Unit.IntToDirection(intgerPosition.x, intgerPosition.y);
	}
	public void SetPlayer(Unit player){
		_player = player;
		if(_previous == _player){
			return;
		}
		_previous = _player;
		_camera.Setup(() => _previous.GetPositionable().GetPosition(_level), () => _cameraZoom, _cameraOffset, _cameraSpeed, true);
		//Setup
	}
	public void ClearPlayer(){
		_player = Unit.GetNullUnit();
	}
	public Unit GetPlayer(){
		return _player;
	}
	private void OnPlayerControl(object sender, PlayerControl.OnPlayerControlEventArgs e){
		SetPlayer(e.player);
	}
	private void OnNextTurn(object sender, EventArgs e){
		ClearPlayer();
	}
	public static PlayerController GetPlayerController(){
		return _instance;
	}
}
