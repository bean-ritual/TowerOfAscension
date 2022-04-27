using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopup: MonoBehaviour{
	public enum TextColour{
		White,
		Red,
		Blue,
		Green,
	};
	private float _timer;
	private Color _color;
	[SerializeField]private TextMeshPro _text;
	public void Setup(string text, int sortingOrder){
		Setup(text, sortingOrder, TextColour.White);
	}
	public void Setup(string text, int sortingOrder, TextColour colour){
		const float TIMER = 0.5f;
		_text.SetText(text);
		_text.sortingOrder = sortingOrder;
		_text.color = GetColour(colour);
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
	private Color GetColour(TextColour colour){
		switch(colour){
			default: return Color.white;
			case TextColour.Red: return Color.red;
			case TextColour.Blue: return Color.blue;
			case TextColour.Green: return Color.green;
		}
	}
}
