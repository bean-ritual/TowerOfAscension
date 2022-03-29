using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	[Serializable]
	public class WorldUnitController{
		[Serializable]
		public class NullWorldUnitContoller : WorldUnitController{
			public NullWorldUnitContoller(){}
			public override void SetSpriteID(SpriteSheet.SpriteID spriteID){}
			public override void SetSpriteIndex(int spriteIndex){}
			public override void SetSprite(SpriteSheet.SpriteID spriteID, int spriteIndex){}
			public override Sprite GetSprite(){
				return SpriteSheet.NullSpriteSheet.GetNullSprite();
			}
			public override void SetSortingOrder(int sortingOrder){}
			public override int GetSortingOrder(){
				return 0;
			}
			public override void SetPosition(Vector3 worldPosition){}
			public override void SetMoveSpeed(int moveSpeed){}
			public override void SetWorldPosition(Vector3 worldPosition, int moveSpeed){}
			public override Vector3 GetWorldPosition(){
				return Vector3.zero;
			}
			public override int GetMoveSpeed(){
				return 0;
			}
		}
		[field:NonSerialized]private static readonly NullWorldUnitContoller _NULL_WORLD_UNIT_CONTROLLER = new NullWorldUnitContoller();
		[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitSpriteUpdate;
		[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitSortingOrderUpdate;
		[field:NonSerialized]public event EventHandler<EventArgs> OnWorldUnitWorldPositionUpdate;
		private SpriteSheet.SpriteID _spriteID;
		private int _spriteIndex;
		private int _sortingOrder;
		private Vector3 _worldPosition;
		private int _moveSpeed;
		public WorldUnitController(
			SpriteSheet.SpriteID spriteID, 
			int spriteIndex, 
			int sortingOrder, 
			Vector3 worldPosition, 
			int moveSpeed
			){
			_spriteID = spriteID;
			_spriteIndex = spriteIndex;
			_sortingOrder = sortingOrder;
			_worldPosition = worldPosition;
			_moveSpeed = moveSpeed;
		}
		public WorldUnitController(){}
		public virtual void SetSpriteID(SpriteSheet.SpriteID spriteID){
			_spriteID = spriteID;
			OnWorldUnitSpriteUpdate?.Invoke(this, EventArgs.Empty);
		}
		public virtual void SetSpriteIndex(int spriteIndex){
			_spriteIndex = spriteIndex;
			OnWorldUnitSpriteUpdate?.Invoke(this, EventArgs.Empty);
		}
		public virtual void SetSprite(SpriteSheet.SpriteID spriteID, int spriteIndex){
			_spriteID = spriteID;
			_spriteIndex = spriteIndex;
			OnWorldUnitSpriteUpdate?.Invoke(this, EventArgs.Empty);
		}
		public virtual Sprite GetSprite(){
			return SpriteSheet.SPRITESHEET_DATA.GetSprite(_spriteID, _spriteIndex);
		}
		public virtual void SetSortingOrder(int sortingOrder){
			_sortingOrder = sortingOrder;
		}
		public virtual int GetSortingOrder(){
			return _sortingOrder;
		}
		public virtual void SetPosition(Vector3 worldPosition){
			_worldPosition = worldPosition;
			OnWorldUnitWorldPositionUpdate?.Invoke(this, EventArgs.Empty);
		}
		public virtual void SetMoveSpeed(int moveSpeed){
			_moveSpeed = moveSpeed;
		}
		public virtual void SetWorldPosition(Vector3 worldPosition, int moveSpeed){
			_worldPosition = worldPosition;
			_moveSpeed = moveSpeed;
			OnWorldUnitWorldPositionUpdate?.Invoke(this, EventArgs.Empty);
		}
		public virtual Vector3 GetWorldPosition(){
			return _worldPosition;
		}
		public virtual int GetMoveSpeed(){
			return _moveSpeed;
		}
		public static WorldUnitController GetNullWorldUnitController(){
			return _NULL_WORLD_UNIT_CONTROLLER;
		}
	}
	public class UnitAnimateEventArgs : EventArgs{
		public Unit unit;
		public Vector3 position;
		public UnitAnimateEventArgs(Unit unit, Vector3 position){
			this.unit = unit;
			this.position = position;
		}
	}
	public interface IWorldUnit{
		WorldUnit.WorldUnitController GetWorldUnitController();
		bool GetWorldVisibility(Game game);
	}
	public interface IWorldUnitUI{
		event EventHandler<EventArgs> OnWorldUnitUIUpdate;
		Vector3 GetUIOffset();
		int GetUISortingOrder();
		bool GetHealthBar();
	}
	public interface IWorldUnitAnimations{
		event EventHandler<UnitAnimateEventArgs> OnAttackAnimation;
	}
	private Unit _unit = Unit.GetNullUnit();
	private WorldUnitController _controller = WorldUnitController.GetNullWorldUnitController();
	private Game _local = Game.GetNullGame();
	private Level _level = Level.GetNullLevel();
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private WorldUnitUI _worldUnitUI;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_controller = unit.GetWorldUnit().GetWorldUnitController();
		_controller.OnWorldUnitSpriteUpdate += OnWorldUnitSpriteUpdate;
		_controller.OnWorldUnitSortingOrderUpdate += OnWorldUnitSortingOrderUpdate;
		_controller.OnWorldUnitWorldPositionUpdate += OnWorldUnitWorldPositionUpdate;
		_unit.GetWorldUnitAnimations().OnAttackAnimation += OnAttackAnimation;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_level = _local.GetLevel();
		_level.OnLightUpdate += OnLightUpdate;
		_worldUnitUI.Setup(_unit);
		RefreshAll();
	}
	public void RefreshAll(){
		_offset.transform.localPosition = _level.GetVector3CellOffset();
		RefreshSprite();
		RefreshSortingOrder();
		this.transform.localPosition = _controller.GetWorldPosition();
		_worldUnitUI.Refresh();
		RefreshVisibility();
	}
	public void RefreshSprite(){
		_renderer.sprite = _controller.GetSprite();
	}
	public void RefreshSortingOrder(){
		_renderer.sortingOrder = _controller.GetSortingOrder();
	}
	public void RefreshWorldPosition(){
		RefreshVisibility();
		if(_offset.activeSelf && _controller.GetMoveSpeed() > 0){
			DungeonMaster.GetInstance().QueueAction(() => MoveAnimation(_controller.GetWorldPosition(), _controller.GetMoveSpeed()));
		}else{
			this.transform.localPosition = _controller.GetWorldPosition();
		}
	}
	public void RefreshVisibility(){
		_offset.SetActive(_unit.GetWorldUnit().GetWorldVisibility(_local));
	}
	public IEnumerator LerpToTarget(Vector3 target, float duration, Action OnAnimationComplete){
		float time = 0f;
		Vector3 start = this.transform.localPosition;
		while(time < duration){
			this.transform.localPosition = Vector3.Lerp(start, target, time / duration);
			time += Time.deltaTime;
			DungeonMaster.GetInstance().BusyFrame();
			yield return null;
		}
		this.transform.localPosition = target;
		OnAnimationComplete?.Invoke();
		DungeonMaster.GetInstance().BusyFrame();
	}
	public void MoveAnimation(Vector3 target, int moveSpeed){
		const float MOVE_FACTOR = 10f;
		StartCoroutine(LerpToTarget(target, moveSpeed / MOVE_FACTOR, null));
	}
	public void AttackAnimation(Vector3 target){
		const float ATTACK_SPEED = 0.1f;
		Vector3 position = this.transform.localPosition;
		StartCoroutine(LerpToTarget(target, ATTACK_SPEED, () => {
			StartCoroutine(LerpToTarget(position, ATTACK_SPEED, null));
		}));
	}
	public void UnitDestroy(){
		Destroy(gameObject);
	}
	private void UnsubscribeFromEvents(){
		_controller.OnWorldUnitSpriteUpdate -= OnWorldUnitSpriteUpdate;
		_controller.OnWorldUnitSortingOrderUpdate -= OnWorldUnitSortingOrderUpdate;
		_controller.OnWorldUnitWorldPositionUpdate -= OnWorldUnitWorldPositionUpdate;
		_unit.GetWorldUnitAnimations().OnAttackAnimation -= OnAttackAnimation;
		_level.OnLightUpdate -= OnLightUpdate;
	}
	private void OnWorldUnitSpriteUpdate(object sender, EventArgs e){
		RefreshSprite();
	}
	private void OnWorldUnitSortingOrderUpdate(object sender, EventArgs e){
		RefreshSortingOrder();
	}
	private void OnWorldUnitWorldPositionUpdate(object sender, EventArgs e){
		RefreshWorldPosition();
	}
	private void OnAttackAnimation(object sender, UnitAnimateEventArgs e){
		RefreshVisibility();
		if(!_offset.activeSelf){
			return;
		}
		DungeonMaster.GetInstance().QueueAction(() => AttackAnimation(e.position));
	}
	private void OnLightUpdate(object sender, EventArgs e){
		RefreshVisibility();
	}
}
