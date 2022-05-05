using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextMessage : MonoBehaviour{
    [SerializeField]private TextMeshProUGUI _text;
	public void Setup(string text){
		gameObject.SetActive(!string.IsNullOrEmpty(text));
		transform.SetAsLastSibling();
		_text.text = text;
	}
	public void SetActive(bool active){
		gameObject.SetActive(active);
	}
}
