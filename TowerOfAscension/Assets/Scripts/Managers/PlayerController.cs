using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : 
	MonoBehaviour,
	PlayerController.IPlayerController
	{
	public interface IPlayerController{
		void SetPlayer(Data data);
		void ClearPlayer();
		Data GetPlayer();
	}
	[Serializable]
	public class NullPlayerController : IPlayerController{
		public void SetPlayer(Data data){}
		public void ClearPlayer(){}
		public Data GetPlayer(){
			return Data.GetNullData();
		}
	}
	//
	private static IPlayerController _INSTANCE;
	//
	private Game _game = Game.GetNullGame();
	private GameWorld _world = GameWorld.GetNullGameWorld();
	private Data _player = Data.GetNullData();
	private Data _previous = Data.GetNullData();
	private Direction _direction = Direction.GetNullDirection();
	private const float _CAMERA_ZOOM = 10f;
	private const float _CAMERA_SPEED = 10f;
	private static Vector3 _CAMERA_OFFSET = new Vector3(0, 0, -10);
	[SerializeField]private CameraManager _camera;
	private void OnDestroy(){
		_world.OnNextTurn -= OnNextTurn;
	}
	private void Awake(){
		if(_INSTANCE == null){
			_INSTANCE = this;
		}else{
			Destroy(gameObject);
		}
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		_world = _game.GetGameWorld();
		_world.OnNextTurn += OnNextTurn;
	}
	private void Update(){
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		_direction = MouseDirectionHandling();
		TileTargetManager.GetInstance().SetTile(_direction.GetTile(_game.GetMap(), _player.GetBlock(_game, 1).GetIWorldPosition().GetTile(_game)));
		/*
		//
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.A)){
				_player.GetTag(_local, Tag.ID.Attack_Slot).GetIInputDirection().Input(_local, _player, _direction);
				return;
			}
			if(Input.GetKey(KeyCode.LeftShift)){
				_player.GetTag(_local, Tag.ID.Interactor).GetIInputDirection().Input(_local, _player, _direction);
				return;
			}
			_player.GetTag(_local, Tag.ID.Move).GetIInputDirection().Input(_local, _player, _direction);
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			_player.GetTag(_local, Tag.ID.AI).GetIClear().Clear(_local, _player);
			return;
		}
		*/
	}
	private Direction MouseDirectionHandling(){
		Vector3 finalPosition = WorldSpaceUtils.MouseToWorldSpace(
			_camera.GetCamera()) - 
			_player.GetBlock(_game, 1).GetIWorldPosition().GetPosition(_game) - 
			_game.GetMap().GetVector3TileOffset();
		Vector3Int intgerPosition = Vector3Int.RoundToInt(finalPosition);
		return Direction.IntToDirection(intgerPosition.x, intgerPosition.y);
	}
	public void SetPlayer(Data player){
		_player = player;
		//PickupUIManager.GetInstance().SetTile(_player.GetTag(_local, Tag.ID.Position).GetIGetTile().GetTile(_local, _player));
		if(_previous == _player){
			return;
		}
		_previous = _player;
		_camera.Setup(
			() => _previous.GetBlock(_game, 1).GetIWorldPosition().GetPosition(_game),
			() => _CAMERA_ZOOM, 
			true
		);
		//HUDUIManager.GetInstance().SetUnit(_previous);
		//InventoryUIManager.GetInstance().SetUnit(_previous);
		MinimapUIManager.GetInstance().SetData(_previous);
	}
	public void ClearPlayer(){
		_player = Data.GetNullData();
	}
	public Data GetPlayer(){
		return _player;
	}
	private void OnNextTurn(object sender, Data.DataUpdateEventArgs e){
		if(e.dataID == 0){
			SetPlayer(_game.GetGameData().Get(e.dataID));
		}else{
			SetPlayer(Data.GetNullData());
		}
	}
	//
	private static NullPlayerController _NULL_PLAYER_CONTROLLER = new NullPlayerController();
	public static IPlayerController GetInstance(){
		if(_INSTANCE == null){
			return _NULL_PLAYER_CONTROLLER;
		}else{
			return _INSTANCE;
		}
	}
}
