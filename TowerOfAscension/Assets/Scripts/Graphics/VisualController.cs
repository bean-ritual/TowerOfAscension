using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class VisualController{
	public interface IVisualController{
		VisualController GetVisualController(Game game);
		bool GetWorldVisibility(Game game);
	}
	[Serializable]
	public class NullVisualController : VisualController{
		public NullVisualController(){}
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
		public override void SetUIOffset(Vector3 uiOffset){}
		public override Vector3 GetUIOffset(){
			return Vector3.zero;
		}
		public override void SetUISortingOrder(int uiSortingOrder){}
		public override int GetUISortingOrder(){
			return 0;
		}
		public override void SetHealthBarActive(bool healthBarActive){}
		public override bool GetHealthBarActive(){
			return false;
		}
		public override void SetItemBorder(bool border){}
		public override bool GetItemBorder(){
			return false;
		}
		public override void InvokeAttackAnimation(Vector3 position){}
		public override void InvokeDamagePopup(string damage){}
		public override bool IsNull(){
			return true;
		}
	}
	public class VisualAnimateEventArgs : EventArgs{
		public Vector3 position;
		public VisualAnimateEventArgs(Vector3 position){
			this.position = position;
		}
	}
	public class DamagePopupEventArgs : EventArgs{
		public string text;
		public DamagePopupEventArgs(string text){
			this.text = text;
		}
	}
	[field:NonSerialized]private static readonly NullVisualController _NULL_VISUAL_CONTROLLER = new NullVisualController();
	[field:NonSerialized]public event EventHandler<EventArgs> OnSpriteUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnSortingOrderUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnWorldPositionUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnUIOffsetUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnUISortingOrderUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnHealthBarActiveUpdate;
	[field:NonSerialized]public event EventHandler<EventArgs> OnItemBorderUpdate;
	[field:NonSerialized]public event EventHandler<VisualAnimateEventArgs> OnAttackAnimation;
	[field:NonSerialized]public event EventHandler<DamagePopupEventArgs> OnDamagePopup;
	private SpriteSheet.SpriteID _spriteID;
	private int _spriteIndex;
	private int _sortingOrder;
	private Vector3 _worldPosition;
	private int _moveSpeed;
	private Vector3 _uiOffset;
	private int _uiSortingOrder;
	private bool _healthBarActive;
	private bool _itemBorder;
	public VisualController(){
		_spriteID = SpriteSheet.SpriteID.Null;
		_spriteIndex = 0;
		_sortingOrder = 0;
		_worldPosition = Vector3.zero;
		_moveSpeed = 0;
		_uiOffset = Vector3.zero;
		_uiSortingOrder = 0;
		_healthBarActive = false;
		_itemBorder = false;
	}
	public virtual void SetSpriteID(SpriteSheet.SpriteID spriteID){
		_spriteID = spriteID;
		OnSpriteUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual void SetSpriteIndex(int spriteIndex){
		_spriteIndex = spriteIndex;
		OnSpriteUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual void SetSprite(SpriteSheet.SpriteID spriteID, int spriteIndex){
		_spriteID = spriteID;
		_spriteIndex = spriteIndex;
		OnSpriteUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual Sprite GetSprite(){
		return SpriteSheet.SPRITESHEET_DATA.GetSprite(_spriteID, _spriteIndex);
	}
	public virtual void SetSortingOrder(int sortingOrder){
		_sortingOrder = sortingOrder;
		OnSortingOrderUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual int GetSortingOrder(){
		return _sortingOrder;
	}
	public virtual void SetPosition(Vector3 worldPosition){
		_worldPosition = worldPosition;
		OnWorldPositionUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual void SetMoveSpeed(int moveSpeed){
		_moveSpeed = moveSpeed;
	}
	public virtual void SetWorldPosition(Vector3 worldPosition, int moveSpeed){
		_worldPosition = worldPosition;
		_moveSpeed = moveSpeed;
		OnWorldPositionUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual Vector3 GetWorldPosition(){
		return _worldPosition;
	}
	public virtual int GetMoveSpeed(){
		return _moveSpeed;
	}
	public virtual void SetUIOffset(Vector3 uiOffset){
		_uiOffset = uiOffset;
		OnUIOffsetUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual Vector3 GetUIOffset(){
		return _uiOffset;
	}
	public virtual void SetUISortingOrder(int uiSortingOrder){
		_uiSortingOrder = uiSortingOrder;
		OnUISortingOrderUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual int GetUISortingOrder(){
		return _uiSortingOrder + _sortingOrder;
	}
	public virtual void SetHealthBarActive(bool healthBarActive){
		_healthBarActive = healthBarActive;
		OnHealthBarActiveUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual bool GetHealthBarActive(){
		return _healthBarActive;
	}
	public virtual void SetItemBorder(bool border){
		_itemBorder = border;
		OnItemBorderUpdate?.Invoke(this, EventArgs.Empty);
	}
	public virtual bool GetItemBorder(){
		return _itemBorder;
	}
	public virtual void InvokeAttackAnimation(Vector3 position){
		OnAttackAnimation?.Invoke(this, new VisualAnimateEventArgs(position));
	}
	public virtual void InvokeDamagePopup(string damage){
		OnDamagePopup?.Invoke(this, new DamagePopupEventArgs(damage));
	}
	//
	public virtual bool IsNull(){
		return false;
	}
	public static VisualController GetNullVisualController(){
		return _NULL_VISUAL_CONTROLLER;
	}
}
