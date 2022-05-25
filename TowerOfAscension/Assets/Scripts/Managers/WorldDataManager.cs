using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldDataManager : 
	MonoBehaviour,
	WorldDataManager.IAnimationManager
	{
	public interface IAnimationManager{
		void PlayAnimation(WorldAnimation animation);
		void BusyFrame();
		bool IsBusy();
	}
	public class NullAnimationManager : IAnimationManager{
		public void PlayAnimation(WorldAnimation animation){}
		public void BusyFrame(){}
		public bool IsBusy(){
			return false;
		}
	}
	private enum State{
		Idle,
		Await,
		Busy,
	};
	private static WorldDataManager _INSTANCE;
	//
	private Game _game = Game.GetNullGame();
	private GameWorld _world = GameWorld.GetNullGameWorld();
	//
	private State _state;
	private Queue<WorldData> _pool;
	private Queue<WorldAnimation> _animations;
	private Dictionary<int, WorldData> _worldData;
	[SerializeField]private GameObject _prefabWorldData;
	private void Awake(){
		if(_INSTANCE == null){
			_INSTANCE = this;
		}else{
			Destroy(gameObject);
		}
	}
	private void OnDestroy(){
		_world.OnSpawn -= OnSpawn;
		_world.OnDespawn -= OnDespawn;
		while(_pool.Count > 0){
			_pool.Dequeue();
		}
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		_world = _game.GetGameWorld();
		//
		_state = State.Idle;
		_pool = new Queue<WorldData>();
		_animations = new Queue<WorldAnimation>();
		_worldData = new Dictionary<int, WorldData>();
		for(int i = 0; i < _world.GetCount(); i++){
			CreateWorldData(_world.GetData(_game, i));
		}
		_world.OnSpawn += OnSpawn;
		_world.OnDespawn += OnDespawn;
	}
	private void LateUpdate(){
		while(!IsBusy() && _animations.Count > 0){
			WorldAnimation animation = _animations.Dequeue();
			if(_worldData.TryGetValue(animation.GetID(), out WorldData worldData)){
				worldData.PlayAnimation(animation);
			}
		}
		switch(_state){
			default: return;
			case State.Await:{
				_state = State.Idle;
				return;
			}
			case State.Busy:{
				_state = State.Await;
				return;
			}
		}
	}
	private void CreateWorldData(Data data){
		WorldData worldData;
		if(_pool.Count > 0){
			worldData = _pool.Dequeue();
		}else{
			worldData = Instantiate(_prefabWorldData, this.transform).GetComponent<WorldData>();
		}
		_worldData.Add(data.GetID(), worldData);
		worldData.Setup(data);
	}
	private void RemoveWorldData(int id){
		if(!_worldData.TryGetValue(id, out WorldData worldData)){
			return;
		}
		_worldData.Remove(id);
		worldData.Disassemble();
		_pool.Enqueue(worldData);
	}
	private void OnSpawn(object sender, Data.DataUpdateEventArgs e){
		CreateWorldData(_game.GetGameData().Get(e.dataID));
	}
	private void OnDespawn(object sender, Data.DataUpdateEventArgs e){
		RemoveWorldData(e.dataID);
	}
	//
	public void PlayAnimation(WorldAnimation animation){
		if(IsBusy()){
			_animations.Enqueue(animation);
		}else{
			if(_worldData.TryGetValue(animation.GetID(), out WorldData worldData)){
				worldData.PlayAnimation(animation);
			}
		}
	}
	public void BusyFrame(){
		_state = State.Busy;
	}
	public bool IsBusy(){
		return _state != State.Idle; 
	}
	//
	private static NullAnimationManager _NULL_ANIMATION_MANAGER = new NullAnimationManager();
	public static IAnimationManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_ANIMATION_MANAGER;
		}else{
			return _INSTANCE;
		}
	}
}
