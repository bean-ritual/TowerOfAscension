using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopup: MonoBehaviour{
	private float _timer;
	private Color _color;
	[SerializeField]private TextMeshPro _text;
	public void Setup(string text, int sortingOrder){
		const float TIMER = 0.5f;
		_text.SetText(text);
		_text.sortingOrder = sortingOrder;
		_text.color = Color.red;
		_color = _text.color;
		_timer = TIMER;
	}
	private void Update(){
		const float MOVE_SPEED = 1f;
		this.transform.localPosition += new Vector3(0, MOVE_SPEED) * Time.deltaTime;
		_timer -= Time.deltaTime;
		if(_timer < 0){
			const float FADE_SPEED = 5f;
			_color.a -= FADE_SPEED * Time.deltaTime;
			_text.color = _color;
			if(_color.a < 0){
				Destroy(gameObject);
			}
		}
	}
}
