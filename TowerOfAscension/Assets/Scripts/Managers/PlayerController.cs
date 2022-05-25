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
		Data GetPlayer();
	}
	[Serializable]
	public class NullPlayerController : IPlayerController{
		public Data GetPlayer(){
			return Data.GetNullData();
		}
	}
	//
	private static PlayerController _INSTANCE;
	//
	private Game _game = Game.GetNullGame();
	private GameWorld _world = GameWorld.GetNullGameWorld();
	private Data _player = Data.GetNullData();
	private Data _active = Data.GetNullData();
	private Direction _direction = Direction.GetNullDirection();
	[SerializeField]private CameraManager _camera;
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
		_player = _game.GetPlayer();
		_camera.Setup(
			() => _player.GetBlock(_game, 1).GetIWorldPosition().GetPosition(_game),
			() => 0, 
			true
		);
		//HUDUIManager.GetInstance().SetUnit(_player);
		//InventoryUIManager.GetInstance().SetUnit(_player);
		MinimapUIManager.GetInstance().SetData(_player);
	}
	private void Update(){
		if(_world.GetCurrentDataID(_game) == _player.GetID()){
			_active = _player;
		}else{
			_active = Data.GetNullData();
		}
		//
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		_direction = MouseDirectionHandling();
		TileTargetManager.GetInstance().SetTile(_direction.GetTile(_game.GetMap(), _player.GetBlock(_game, 1).GetIWorldPosition().GetTile(_game)));
		//
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.A)){
				//_player.GetTag(_local, Tag.ID.Attack_Slot).GetIInputDirection().Input(_local, _player, _direction);
				return;
			}
			if(Input.GetKey(KeyCode.LeftShift)){
				//_player.GetTag(_local, Tag.ID.Interactor).GetIInputDirection().Input(_local, _player, _direction);
				return;
			}
			_active.GetBlock(_game, 1).GetIMovement().Move(_game, _direction);
			//_player.GetTag(_local, Tag.ID.Move).GetIInputDirection().Input(_local, _player, _direction);
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			//_player.GetTag(_local, Tag.ID.AI).GetIClear().Clear(_local, _player);
			return;
		}
	}
	private Direction MouseDirectionHandling(){
		Vector3 finalPosition = WorldSpaceUtils.MouseToWorldSpace(
			_camera.GetCamera()) - 
			_player.GetBlock(_game, 1).GetIWorldPosition().GetPosition(_game) - 
			_game.GetMap().GetVector3TileOffset();
		Vector3Int intgerPosition = Vector3Int.RoundToInt(finalPosition);
		return Direction.IntToDirection(intgerPosition.x, intgerPosition.y);
	}
	/*
	public void SetPlayer(Data player){
		_player = player;
		//PickupUIManager.GetInstance().SetTile(_player.GetTag(_local, Tag.ID.Position).GetIGetTile().GetTile(_local, _player));
		if(_previous == _player){
			return;
		}
		_previous = _player;
	}
	*/
	public Data GetPlayer(){
		return _active;
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
