using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MessageBoxManager : 
	MonoBehaviour,
	MessageBoxManager.IMessageBoxManager
	{
	public interface IMessageBoxManager{
		void ShowMessage(string text);
		bool IsNull();
	}
	public class NullMessageBoxManager : 
		MessageBoxManager.IMessageBoxManager
		{
		public void ShowMessage(string text){}
		public bool IsNull(){
			const bool NULL = true;
			return NULL;
		}
	}
	private static NullMessageBoxManager _NULL_MESSAGE_BOX = new NullMessageBoxManager();
	private static IMessageBoxManager _INSTANCE = _NULL_MESSAGE_BOX;
	private Queue<TextMessage> _messages;
	[SerializeField]private RectTransform _contentRect;
	[SerializeField]private GameObject _prefabMessage;
	private void OnDestroy(){
		if(_INSTANCE == this){
			_INSTANCE = _NULL_MESSAGE_BOX;
		}
	}
	private void Awake(){
		if(!_INSTANCE.IsNull()){
			Destroy(gameObject);
		}else{
			_INSTANCE = this;
			_messages = new Queue<TextMessage>();
		}
	}
	public void ShowMessage(string text){
		const int MAX_MESSAGES = 20;
		TextMessage message;
		if(_messages.Count > MAX_MESSAGES){
			message = _messages.Dequeue();
		}else{
			GameObject go = Instantiate(_prefabMessage, _contentRect);
			message = go.GetComponent<TextMessage>();
		}
		message.Setup(text);
		_messages.Enqueue(message);
	}
	public bool IsNull(){
		const bool NULL = false;
		return NULL;
	}
	public static IMessageBoxManager GetNullMessageBox(){
		return _NULL_MESSAGE_BOX;
	}
	public static IMessageBoxManager GetInstance(){
		return _INSTANCE;
	}
}
