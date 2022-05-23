using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextPopup: MonoBehaviour{
	private float _timer;
	private Color _colour;
	[SerializeField]private TextMeshPro _text;
	public void Setup(string text){
		Setup(text, 0);
	}
	public void Setup(string text, int colour){
		const float TIMER = 0.5f;
		_text.SetText(text);
		_text.color = GetColour(colour);
		_colour = _text.color;
		_timer = TIMER;
	}
	private void Update(){
		const float MOVE_SPEED = 1f;
		this.transform.localPosition += new Vector3(0, MOVE_SPEED) * Time.deltaTime;
		_timer -= Time.deltaTime;
		if(_timer < 0){
			const float FADE_SPEED = 5f;
			_colour.a -= FADE_SPEED * Time.deltaTime;
			_text.color = _colour;
			if(_colour.a < 0){
				Destroy(gameObject);
			}
		}
	}
	private Color GetColour(int colour){
		switch(colour){
			default: return Color.white;
			case 0: return Color.white;
			case 1: return Color.red;
			case 2: return Color.blue;
			case 3: return Color.green;
		}
	}
}
