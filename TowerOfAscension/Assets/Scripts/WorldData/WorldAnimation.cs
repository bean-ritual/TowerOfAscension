using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class WorldAnimation{
	[Serializable]
	public class NullWorldAnimation : WorldAnimation{
		public override void Animate(WorldData worldData){}
		public override int GetID(){
			return -1;
		}
	}
	[Serializable]
	public class MoveWorldAnimation : WorldAnimation{
		private int _id;
		private Vector3 _position;
		private int _moveSpeed;
		public MoveWorldAnimation(int id, Vector3 position, int moveSpeed){
			_id = id;
			_position = position;
			_moveSpeed = moveSpeed;
		}
		public MoveWorldAnimation(Game game, int id, Map.Tile tile, int moveSpeed){
			_id = id;
			_moveSpeed = moveSpeed;
			Map map = game.GetMap();
			_position = tile.GetPosition(map) + map.GetVector3TileOffset();
		}
		public override void Animate(WorldData worldData){
			worldData.PlayCoroutine(MoveAnimation(worldData));
		}
		private IEnumerator MoveAnimation(WorldData worldData){
			float duration = _moveSpeed * 0.1f;
			float time = 0f;
			Vector3 start = worldData.GetPosition();
			while(time < duration){
				worldData.SetPosition(Vector3.Lerp(start, _position, time / duration));
				time += Time.deltaTime;
				WorldDataManager.GetInstance().BusyFrame();
				yield return null;
			}
			worldData.SetPosition(_position);
			yield return null;
		}
		public override int GetID(){
			return _id;
		}
	}
	[Serializable]
	public class MeleeAttackAnimation : WorldAnimation{
		private int _id;
		private Vector3 _position;
		private int _attackSpeed;
		public MeleeAttackAnimation(int id, Vector3 position, int attackSpeed){
			_id = id;
			_position = position;
			_attackSpeed = attackSpeed;
		}
		public MeleeAttackAnimation(Game game, int id, Direction direction, Vector3 position, int attackSpeed){
			_id = id;
			_attackSpeed = attackSpeed;
			_position = (direction.GetWorldDirection(game.GetMap()) * 0.5f) + position;
		}
		public override void Animate(WorldData worldData){
			worldData.PlayCoroutine(AttackAnimation(worldData));
		}
		private IEnumerator AttackAnimation(WorldData worldData){
			float duration = _attackSpeed * 0.1f;
			float time = 0f;
			Vector3 start = worldData.GetPosition();
			while(time < duration){
				worldData.SetPosition(Vector3.Lerp(start, _position, time / duration));
				time += Time.deltaTime;
				WorldDataManager.GetInstance().BusyFrame();
				yield return null;
			}
			time = 0f;
			while(time < duration){
				worldData.SetPosition(Vector3.Lerp(_position, start, time / duration));
				time += Time.deltaTime;
				WorldDataManager.GetInstance().BusyFrame();
				yield return null;
			}
			worldData.SetPosition(start);
			yield return null;
		}
		public override int GetID(){
			return _id;
		}
	}
	[Serializable]
	public class PositionUpdateAnimation : WorldAnimation{
		private int _id;
		private Vector3 _position;
		private bool _render;
		public PositionUpdateAnimation(int id, Vector3 position, bool render){
			_id = id;
			_position = position;
			_render = render;
		}
		public PositionUpdateAnimation(Game game, Data data){
			_id = data.GetID();
			_position = data.GetBlock(game, 1).GetIWorldPosition().GetPosition(game) + game.GetMap().GetVector3TileOffset();
			_render = data.GetBlock(game, Game.TOAGame.BLOCK_VISUAL).GetIVisual().GetRender(game);
		}
		public override void Animate(WorldData worldData){
			worldData.SetPosition(_position);
			worldData.SetVisible(_render);
		}
		public override int GetID(){
			return _id;
		}
	}
	[Serializable]
	public class LightUpdateAnimation : WorldAnimation{
		private int _id;
		private bool _render;
		public LightUpdateAnimation(int id, bool render){
			_id = id;
			_render = render;
		}
		public LightUpdateAnimation(Game game, Data data){
			_id = data.GetID();
			_render = data.GetBlock(game, Game.TOAGame.BLOCK_VISUAL).GetIVisual().GetRender(game);
		}
		public override void Animate(WorldData worldData){
			worldData.SetVisible(_render);
		}
		public override int GetID(){
			return _id;
		}
	}
	[Serializable]
	public class SetupAnimation : WorldAnimation{
		private int _id;
		private Vector3 _position;
		private int _sprite;
		private int _sortingOrder;
		private bool _render;
		public SetupAnimation(int id, Vector3 position, int sprite, int sortingOrder, bool render){
			_id = id;
			_position = position;
			_sprite = sprite;
			_sortingOrder = sortingOrder;
			_render = render;
		}
		public SetupAnimation(Game game, Data data){
			_id = data.GetID();
			_position = data.GetBlock(game, 1).GetIWorldPosition().GetPosition(game) + game.GetMap().GetVector3TileOffset();
			_sprite = data.GetBlock(game, 2).GetIVisual().GetSprite(game);
			_sortingOrder = data.GetBlock(game, 2).GetIVisual().GetSortingOrder(game);
			_render = data.GetBlock(game, 2).GetIVisual().GetRender(game);
		}
		public override void Animate(WorldData worldData){
			worldData.SetPosition(_position);
			worldData.SetSprite(_sprite);
			worldData.SetSortingOrder(_sortingOrder);
			worldData.SetVisible(_render);
		}
		public override int GetID(){
			return _id;
		}
	}
	public abstract void Animate(WorldData worldData);
	public abstract int GetID();
	//
	private static NullWorldAnimation _NULL_WORLD_ANIMATION = new NullWorldAnimation();
	public static WorldAnimation GetNullWorldAnimation(){
		return _NULL_WORLD_ANIMATION;
	}
}
