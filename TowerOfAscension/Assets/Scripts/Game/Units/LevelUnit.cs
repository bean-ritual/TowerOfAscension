using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelUnit : 
	Unit,
	VisualController.IVisualController,
	Unit.ISpawnable,
	Unit.ITileable,
	Unit.ITaggable
	{
	protected VisualController _controller = VisualController.GetNullVisualController();
	protected int _x = Unit.NullUnit.GetNullX();
	protected int _y = Unit.NullUnit.GetNullY();
	protected Register<Unit>.ID _id = Register<Unit>.ID.GetNullID();
	protected Dictionary<Tag.ID, Tag> _tags = new Dictionary<Tag.ID, Tag>();
	public LevelUnit(){
		_tags = new Dictionary<Tag.ID, Tag>();
	}
	public LevelUnit(Game game, VisualController controller, Tag[] array){
		_controller = controller;
		_tags = new Dictionary<Tag.ID, Tag>(array.Length);
		for(int i = 0; i < array.Length; i++){
			AddTag(game, array[i]);
		}
	}
	public virtual VisualController GetVisualController(Game game){
		return _controller;
	}
	public virtual bool GetWorldVisibility(Game game){
		return game.GetLevel().Get(_x, _y).GetLightable().GetLight() > 0;
	}
	public virtual void Spawn(Game game, int x, int y){
		Unit.Default_Spawn(this, game, x, y);
	}
	public virtual void Despawn(Game game){
		Unit.Default_Despawn(this, game);
	}
	public void SetPosition(Game game, int x, int y, int moveSpeed){
		Unit.Default_SetPosition(this, game, x, y, ref _x, ref _y, moveSpeed);
	}
	public void GetPosition(Game game, out int x, out int y){
		x = _x;
		y = _y;
	}
	public void RemovePosition(Game game){
		Unit.Default_RemovePosition(this, game, _x, _y);
	}
	public Vector3 GetPosition(Game game){
		return game.GetLevel().GetWorldPosition(_x, _y);
	}
	public Tile GetTile(Game game){
		return game.GetLevel().Get(_x, _y);
	}
	public Tile GetTileFrom(Game game, int x, int y){
		return game.GetLevel().Get((x + _x), (y + _y));
	}
	public void Add(Register<Unit> register){
		register.Add(this, ref _id);
	}
	public void Remove(Register<Unit> register){
		register.Remove(_id);
	}
	public Register<Unit>.ID GetID(){
		return _id;
	}
	public void AddTag(Game game, Tag tag){
		tag.Add(_tags);
	}
	public void RemoveTag(Game game, Tag.ID id){
		GetTag(game, id).Remove(_tags);
	}
	public Tag GetTag(Game game, Tag.ID id){
		if(!_tags.TryGetValue(id, out Tag tag)){
			tag = Tag.GetNullTag();
		}
		return tag;
	}
	public override VisualController.IVisualController GetVisualController(){
		return this;
	}
	public override Unit.ISpawnable GetSpawnable(){
		return this;
	}
	public override Unit.ITileable GetTileable(){
		return this;
	}
	public override Unit.IPositionable GetPositionable(){
		return this;
	}
	public override Register<Unit>.IRegisterable GetRegisterable(){
		return this;
	}
	public override Unit.ITaggable GetTaggable(){
		return this;
	}
}
