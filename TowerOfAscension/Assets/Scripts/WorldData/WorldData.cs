using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WorldData : MonoBehaviour{
	private Game _game = Game.GetNullGame();
	private Data _data = Data.GetNullData();
	[SerializeField]private SpriteRenderer _renderer;
	[SerializeField]private BoxCollider2D _collider;
	public void Setup(Data data){
		const string NAME = "WorldData {0}";
		UnsubscribeFromEvents();
		_game = DungeonMaster.GetInstance().GetGame();
		_data = data;
		WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.SetupAnimation(_game, _data));
		this.gameObject.name = string.Format(NAME, _data.GetID());
		_data.OnBlockUpdate += OnBlockUpdate;
	}
	public void PlayAnimation(WorldAnimation animation){
		StartCoroutine(animation.Animate(this));
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
		this.gameObject.SetActive(visible);
	}
	public void Disassemble(){
		StopAllCoroutines();
		Setup(Data.GetNullData());
	}
	private void UnsubscribeFromEvents(){
		_data.OnBlockUpdate -= OnBlockUpdate;
	}
	private void OnBlockUpdate(object sender, Data.BlockUpdateEventArgs e){
		if(e.blockID == 1){
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.PositionUpdateAnimation(_game, _data));
			return;
		}
		if(e.blockID == 2){
			WorldDataManager.GetInstance().PlayAnimation(new WorldAnimation.LightUpdateAnimation(_game, _data));
			return;
		}
	}
}
