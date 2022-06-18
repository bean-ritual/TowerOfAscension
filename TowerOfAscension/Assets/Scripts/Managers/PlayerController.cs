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
			() => _player.GetBlock(_game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetPosition(_game),
			() => 0, 
			true
		);
		//HUDUIManager.GetInstance().SetUnit(_player);
		InventoryUIManager.GetInstance().SetData(_player);
		MinimapUIManager.GetInstance().SetData(_player);
		PickupUIManager.GetInstance().SetData(_player);
	}
	private void Update(){
		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}
		_direction = MouseDirectionHandling();
		TileTargetManager.GetInstance().SetTile(_direction.GetTile(_game.GetMap(), _player.GetBlock(_game, 1).GetIWorldPosition().GetTile(_game)));
		//
		if(Input.GetMouseButtonDown(0)){
			if(Input.GetKey(KeyCode.A)){
				GetPlayer().GetBlock(_game, Game.TOAGame.BLOCK_ACTIVE).GetIActive().Trigger(_game, _direction);
				return;
			}
			if(Input.GetKey(KeyCode.LeftShift)){
				//_player.GetTag(_local, Tag.ID.Interactor).GetIInputDirection().Input(_local, _player, _direction);
				return;
			}
			GetPlayer().GetBlock(_game, Game.TOAGame.BLOCK_WORLD).GetIMovement().Move(_game, _direction);
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			GetPlayer().GetBlock(_game, Game.TOAGame.BLOCK_DOTURN).GetIConclude().Conclude(_game);
			return;
		}
	}
	private Direction MouseDirectionHandling(){
		Vector3 finalPosition = WorldSpaceUtils.MouseToWorldSpace(
			_camera.GetCamera()) - 
			_player.GetBlock(_game, Game.TOAGame.BLOCK_WORLD).GetIWorldPosition().GetPosition(_game) - 
			_game.GetMap().GetVector3TileOffset();
		Vector3Int intgerPosition = Vector3Int.RoundToInt(finalPosition);
		return Direction.IntToDirection(intgerPosition.x, intgerPosition.y);
	}
	public Data GetPlayer(){
		if(_world.GetCurrentDataID(_game) == _player.GetID() && !WorldDataManager.GetInstance().IsBusy()){
			return _player;
		}else{
			return Data.GetNullData();
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
