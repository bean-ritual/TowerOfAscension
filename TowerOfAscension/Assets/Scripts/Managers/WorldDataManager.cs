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
	public class NullWorldData : IWorldData{
		public void Setup(int id){}
		public void SetData(Data data){}
		public void PlayAnimation(WorldAnimation animation){}
		public void SetPosition(Vector3 position){}
		public Vector3 GetPosition(){
			return Vector3.zero;
		}
		public void SetSprite(int sprite){}
		public void SetSortingOrder(int sortingOrder){}
		public void SetVisible(bool visible){}
		public void PlayCoroutine(IEnumerator coroutine){}
		public void Disassemble(){}
		public int GetID(){
			return -1;
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
	private List<WorldData> _worldData;
	private Queue<int> _pool;
	private Dictionary<int, int> _links;
	private Queue<WorldAnimation> _animations;
	[SerializeField]private int _init;
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
	}
	private void Start(){
		_game = DungeonMaster.GetInstance().GetGame();
		_world = _game.GetGameWorld();
		//
		_state = State.Idle;
		_worldData = new List<WorldData>(_init);
		_pool = new Queue<int>(_init);
		_links = new Dictionary<int, int>();
		_animations = new Queue<WorldAnimation>();
		for(int i = 0; i < (_init - 1); i++){
			WorldData worldData = Instantiate(_prefabWorldData, this.transform).GetComponent<WorldData>();
			_worldData.Add(worldData);
			worldData.Setup(i);
			_pool.Enqueue(i);
		}
		for(int i = 0; i < _world.GetCount(); i++){
			LinkWorldData(_world.GetData(_game, i));
		}
		_world.OnSpawn += OnSpawn;
		_world.OnDespawn += OnDespawn;
	}
	//
	private void LateUpdate(){
		FlushAnimations();
		BusyFrameDecay();
	}
	private void FlushAnimations(){
		while(!IsBusy() && _animations.Count > 0){
			WorldAnimation animation = _animations.Dequeue();
			if(_links.TryGetValue(animation.GetID(), out int worldID)){
				_worldData[worldID].PlayAnimation(animation);
			}
		}
	}
	private void BusyFrameDecay(){
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
	//
	private void LinkWorldData(Data data){
		WorldData worldData;
		if(_pool.Count > 0){
			worldData = _worldData[_pool.Dequeue()];
		}else{
			worldData = CreateWorldData();
		}
		_links.Add(data.GetID(), worldData.GetID());
		worldData.SetData(data);
	}
	private void UnlinkWorldData(int dataID){
		if(!_links.TryGetValue(dataID, out int worldID)){
			return;
		}
		_links.Remove(dataID);
		_worldData[worldID].Disassemble();
		_pool.Enqueue(worldID);
	}
	private WorldData CreateWorldData(){
		WorldData worldData = Instantiate(_prefabWorldData, this.transform).GetComponent<WorldData>();
		int worldID = _worldData.Count;
		_worldData.Add(worldData);
		worldData.Setup(worldID);
		return worldData;
	}
	private IWorldData GetWorldData(int worldID){
		if(worldID < 0 || worldID >= _worldData.Count){
			return _NULL_WORLD_DATA;
		}else{
			return _worldData[worldID];
		}
	}
	//
	private void OnSpawn(object sender, Data.DataUpdateEventArgs e){
		LinkWorldData(_game.GetGameData().Get(e.dataID));
	}
	private void OnDespawn(object sender, Data.DataUpdateEventArgs e){
		UnlinkWorldData(e.dataID);
	}
	//
	public void PlayAnimation(WorldAnimation animation){
		if(IsBusy()){
			_animations.Enqueue(animation);
		}else{
			if(_links.TryGetValue(animation.GetID(), out int worldID)){
				_worldData[worldID].PlayAnimation(animation);
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
	private static NullWorldData _NULL_WORLD_DATA = new NullWorldData();
	public static IWorldData GetNullWorldData(){
		return _NULL_WORLD_DATA;
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
