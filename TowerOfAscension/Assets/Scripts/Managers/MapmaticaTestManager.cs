using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapmaticaTestManager : MonoBehaviour{
	private enum Mode{
		Null,
		Pathfinding,
		Raycast,
		Fov,
	};
	private const string _READOUT = "Time: {0} Tiles: {1}";
	private const float _SPEED_SCALE = 0.1f;
	private const int _TILE_UV = 4;
	private Game _game = Game.GetNullGame();
	private Map _map = Map.GetNullMap();
	private Stopwatch _watch;
	private Vector3 _mousePosition;
	private Vector2Int _mouseVector;
	private Camera _camera;
	[SerializeField]private CameraManager _cameraManager;
	[SerializeField]private Vector2Int _start;
	[SerializeField]private Vector2Int _end;
	[SerializeField]private int _range;
	[SerializeField]private int _speed;
	[SerializeField]private Mode _mode;
	private void Start(){
		_game = new Game.TestGame();
		_map = _game.GetMap();
		_watch = new Stopwatch();
		_camera = _cameraManager.GetCamera();
		//MapMeshManager.GetInstance().Setup(_game);
	}
	private void Update(){
		_mousePosition = WorldSpaceUtils.MouseToWorldSpace(_camera);
		_mouseVector.x = (int)_mousePosition.x;
		_mouseVector.y = (int)_mousePosition.y;
		if(Input.GetMouseButton(0)){
			if(Input.GetKey(KeyCode.LeftShift)){
				_end = _mouseVector;
				return;
			}
			if(Input.GetKey(KeyCode.LeftControl)){
				_map.Get(_mouseVector).GetITestTile().SetPath(_map, false);
				return;
			}
			_start = _mouseVector;
			return;
		}
		if(Input.GetMouseButton(1)){
			_map.Get(_mouseVector).GetITestTile().SetPath(_map, true);
			return;
		}
		if(Input.GetKeyDown(KeyCode.T)){
			Test();
			return;
		}
		if(Input.GetKeyDown(KeyCode.C)){
			Clear();
			return;
		}
	}
	private void Test(){
		Clear();
		switch(_mode){
			default: return;
			case Mode.Null: return;
			case Mode.Pathfinding:{
				StartCoroutine(Test_Pathfinding());
				return;
			};
			case Mode.Raycast:{
				StartCoroutine(Test_RayCast());
				return;
			}
			case Mode.Fov:{
				StartCoroutine(Test_FOV());
				return;
			}
		}
	}
	private IEnumerator Test_Pathfinding(){
		_watch.Restart();
		List<Map.Tile> path = _map.GetIMapmatics().FindPath(_start.x, _start.y, _end.x, _end.y, IsWalkable);
		_watch.Stop();
		UnityEngine.Debug.Log(string.Format(_READOUT, _watch.ElapsedMilliseconds, path.Count));
		for(int i = 0; i < path.Count; i++){
			path[i].GetITestTile().SetAltasIndex(_map, _TILE_UV);
			yield return new WaitForSeconds(_speed * _SPEED_SCALE);
		}
		yield return null;
	}
	private IEnumerator Test_RayCast(){
		_watch.Restart();
		List<Map.Tile> ray = _map.GetIMapmatics().Raycast(_start.x, _start.y, _end.x, _end.y, IsWalkable);
		_watch.Stop();
		UnityEngine.Debug.Log(string.Format(_READOUT, _watch.ElapsedMilliseconds, ray.Count));
		for(int i = 0; i < ray.Count; i++){
			ray[i].GetITestTile().SetAltasIndex(_map, _TILE_UV);
			yield return new WaitForSeconds(_speed * _SPEED_SCALE);
		}
		yield return null;
	}
	private IEnumerator Test_FOV(){
		_watch.Restart();
		List<Map.Tile> fov = _map.GetIMapmatics().CalculateFov(_start.x, _start.y, _range, IsRayable);
		UnityEngine.Debug.Log(string.Format(_READOUT, _watch.ElapsedMilliseconds, fov.Count));
		for(int i = 0; i < fov.Count; i++){
			fov[i].GetITestTile().SetAltasIndex(_map, _TILE_UV);
			yield return new WaitForSeconds(_speed * _SPEED_SCALE);
		}
		yield return null;
	}
	private void Clear(){
		StopAllCoroutines();
		for(int x = 0; x < _map.GetWidth(); x++){
			for(int y = 0; y < _map.GetHeight(); y++){
				_map.Get(x, y).GetITestTile().SetAltasIndex(_map, 1);
			}
		}
	}
	private bool IsWalkable(Map.Tile tile){
		return tile.GetITestTile().GetPath();
	}
	private bool IsRayable(int range, Map.Tile tile){
		return tile.GetITestTile().GetPath();
	}
}
