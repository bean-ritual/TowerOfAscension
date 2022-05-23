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
	}
	[Serializable]
	public class NullMessageBoxManager : IMessageBoxManager{
		public void ShowMessage(string text){}
	}
	private static IMessageBoxManager _INSTANCE;
	private Queue<TextMessage> _messages;
	[SerializeField]private RectTransform _contentRect;
	[SerializeField]private GameObject _prefabMessage;
	private void Awake(){
		if(_INSTANCE == null){
			_INSTANCE = this;
			_messages = new Queue<TextMessage>();
		}else{
			Destroy(gameObject);
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
	private static NullMessageBoxManager _NULL_MESSAGE_BOX = new NullMessageBoxManager();
	public static IMessageBoxManager GetInstance(){
		if(_INSTANCE == null){
			return _NULL_MESSAGE_BOX;
		}else{
			return _INSTANCE;
		}
	}
}
