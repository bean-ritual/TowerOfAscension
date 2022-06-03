using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldData : 
	MonoBehaviour,
	IWorldData
	{
	private int _id;
	private Game _game = Game.GetNullGame();
	private Data _data = Data.GetNullData();
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private BoxCollider2D _collider;
	public void Setup(int id){
		_id = id;
		SetData(Data.GetNullData());
	}
	public void SetData(Data data){
		const string NAME = "WorldData {0}";
		UnsubscribeFromEvents();
		_game = DungeonMaster.GetInstance().GetGame();
		_data = data;
		_data.OnBlockUpdate += OnBlockUpdate;
		_game.GetGameBlocks(Game.TOAGame.BLOCK_LIGHT).OnGameBlockUpdate += OnLightBlockUpdate;
		this.gameObject.name = string.Format(NAME, _data.GetID());
		//
		WorldAnimation animation = new WorldAnimation.SetupAnimation(_game, _data);
		if(_data.IsNull()){
			PlayAnimation(animation);
		}else{
			WorldDataManager.GetInstance().PlayAnimation(animation);
		}
	}
	public void PlayAnimation(WorldAnimation animation){
		animation.Animate(this);
	}
	public void SetPosition(Vector3 position){
		this.transform.localPosition = position;
	}
	public Vector3 GetPosition(){
		return this.transform.localPosition;
	}
	public void SetSprite(int sprite){
		_renderer.sprite = SpriteSheet.SPRITESHEET_DATA.GetSprite(sprite);
	}
	public void SetSortingOrder(int sortingOrder){
		_renderer.sortingOrder = sortingOrder;
	}
	public void SetVisible(bool visible){
		if(!visible){
			StopAllCoroutines();
		}
		this.gameObject.SetActive(visible);
	}
	public void PlayCoroutine(IEnumerator coroutine){
		if(this.gameObject.activeSelf){
			StartCoroutine(coroutine);
		}
	}
	public void Disassemble(){
		StopAllCoroutines();
		SetData(Data.GetNullData());
	}
	public int GetID(){
		return _id;
	}
	private void UnsubscribeFromEvents(){
		_data.OnBlockUpdate -= OnBlockUpdate;
		_game.GetGameBlocks(Game.TOAGame.BLOCK_LIGHT).OnGameBlockUpdate -= OnLightBlockUpdate;
	}
	private void OnBlockUpdate(object sender, Data.BlockUpdateEventArgs e){
		if(e.blockID == Game.TOAGame.BLOCK_WORLD){
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.PositionUpdateAnimation(_game, _data));
		}
	}
	private void OnLightBlockUpdate(object sender, GameBlocks.GameBlockUpdateEventArgs e){
		WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.LightUpdateAnimation(_game, _data));
	}
}
