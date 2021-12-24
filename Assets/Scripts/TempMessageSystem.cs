using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{

}

public interface IEventListener
{
	void OnEvent(IEvent e);
}

public class TempMessageSystem : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	Dictionary<Type, Action<Event>> dic = new Dictionary<Type, Action<Event>>();

	public void Subscribe<T>(Action<Event> e)
	{
		dic.Add(typeof(T), e);
	}

	public void PublishSync<T>(T info)
	{
		dic[typeof(T)](info as Event);
	}

	public void SendSync<T>(T info, IEventListener listener)
	{
		listener.OnEvent(info as IEvent);
	}
}
