using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldUnit : MonoBehaviour{
	private Unit _unit = Unit.GetNullUnit();
	private VisualController _controller = VisualController.GetNullVisualController();
	private Game _local = Game.GetNullGame();
	[SerializeField]private GameObject _offset;
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private WorldUnitUI _worldUnitUI;
	private void OnDisable(){
		UnsubscribeFromEvents();
	}
	public void Setup(Unit unit){
		UnsubscribeFromEvents();
		_unit = unit;
		_local = DungeonMaster.GetInstance().GetLocalGame();
		_controller = unit.GetVisualController().GetVisualController(_local);
		_controller.OnSpriteUpdate += OnSpriteUpdate;
		_controller.OnSortingOrderUpdate += OnSortingOrderUpdate;
		_controller.OnWorldPositionUpdate += OnWorldPositionUpdate;
		_controller.OnAttackAnimation += OnAttackAnimation;
		_local.GetLevel().OnLightUpdate += OnLightUpdate;
		_worldUnitUI.Setup(_unit);
		RefreshAll();
	}
	public void RefreshAll(){
		_offset.transform.localPosition = _local.GetLevel().GetVector3CellOffset();
		RefreshSprite();
		RefreshSortingOrder();
		this.transform.localPosition = _controller.GetWorldPosition();
		_worldUnitUI.RefreshAll();
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
		_offset.SetActive(_unit.GetVisualController().GetWorldVisibility(_local));
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
		_controller.OnSpriteUpdate -= OnSpriteUpdate;
		_controller.OnSortingOrderUpdate -= OnSortingOrderUpdate;
		_controller.OnWorldPositionUpdate -= OnWorldPositionUpdate;
		_controller.OnAttackAnimation -= OnAttackAnimation;
		_local.GetLevel().OnLightUpdate -= OnLightUpdate;
	}
	private void OnSpriteUpdate(object sender, EventArgs e){
		RefreshSprite();
	}
	private void OnSortingOrderUpdate(object sender, EventArgs e){
		RefreshSortingOrder();
	}
	private void OnWorldPositionUpdate(object sender, EventArgs e){
		RefreshWorldPosition();
	}
	private void OnAttackAnimation(object sender, VisualController.VisualAnimateEventArgs e){
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
