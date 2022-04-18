using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour{
	private static PlayerController _INSTANCE;
	private Game _local = Game.GetNullGame();
	private Unit _player = Unit.GetNullUnit();
	private Unit _previous = Unit.GetNullUnit();
	private Direction _direction = Direction.GetNullDirection();
	private const float _CAMERA_ZOOM = 10f;
	private const float _CAMERA_SPEED = 10f;
	private static readonly Vector3 _CAMERA_OFFSET = new Vector3(0, 0, -10);
	[SerializeField]private CameraManager _camera;
	private void OnDestroy(){
		_local.GetLevel().OnNextTurn -= OnNextTurn;
		PlayerControl.OnPlayerControl -= OnPlayerControl;
	}
	private void Awake(){
		if(_INSTANCE != null){
			Destroy(gameObject);
			return;
		}
		_INSTANCE = this;
	}
	private void Start(){
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_local.GetLevel().OnNextTurn += OnNextTurn;
		PlayerControl.OnPlayerControl += OnPlayerControl;
	}
	private void Update(){
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		//
		_direction = MouseDirectionHandling();
		TileTargetManager.GetInstance().SetTile(_direction.GetTileFromUnit(_local, _player));
		//
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.A)){
				_player.GetTaggable().GetTag(_local, Tag.ID.Basic_Attack).GetIInputDirection().Input(_local, _player, _direction);
				//_player.GetAttacker().TryAttack(_local, _direction);
				return;
			}
			if(Input.GetKey(KeyCode.LeftShift)){
				_player.GetInteractor().Interact(_local, _direction);
				return;
			}
			_player.GetMoveable().Move(_local, _direction);
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			_player.GetControllable().GetAI(_local).GetTurnControl().EndTurn(_local, _player);
			return;
		}
	}
	private Direction MouseDirectionHandling(){
		Vector3 finalPosition = WorldSpaceUtils.MouseToWorldSpace(_camera.GetCamera()) - _player.GetPositionable().GetPosition(_local) - _local.GetLevel().GetVector3CellOffset();
		Vector3Int intgerPosition = Vector3Int.RoundToInt(finalPosition);
		return Direction.IntToDirection(intgerPosition.x, intgerPosition.y);
	}
	public void SetPlayer(Unit player){
		_player = player;
		PickupUIManager.GetInstance().SetTile(_player.GetPositionable().GetTile(_local));
		if(_previous == _player){
			return;
		}
		_previous = _player;
		_camera.Setup(
			() => _previous.GetPositionable().GetPosition(_local), 
			() => _CAMERA_ZOOM, 
			_CAMERA_OFFSET, 
			_CAMERA_SPEED, 
			true
		);
		HUDUIManager.GetInstance().SetUnit(_previous);
		InventoryUIManager.GetInstance().SetUnit(_previous);
		MinimapUIManager.GetInstance().SetUnit(_previous);
	}
	public void ClearPlayer(){
		_player = Unit.GetNullUnit();
	}
	public Unit GetPlayer(){
		return _player;
	}
	private void OnPlayerControl(object sender, PlayerControl.OnPlayerControlEventArgs e){
		if(!DungeonMaster.GetInstance().IsBusy()){
			SetPlayer(e.player);
		}
	}
	private void OnNextTurn(object sender, EventArgs e){
		ClearPlayer();
	}
	public static PlayerController GetInstance(){
		return _INSTANCE;
	}
}
