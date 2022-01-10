using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TempCodes
{
	public interface IEvent : System.IDisposable
	{

	}

	public class DamageEvent : IEvent
	{
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}

	public class MoveEvent : IEvent
	{
		public void Dispose()
		{
			throw new System.NotImplementedException();
		}
	}


	public class temp
	{


	}



	public interface IEventHandler
	{
		public abstract void OnNotify(IEvent e);

	}

	//옵저버 구현클래스1(이벤트 종류1)
	public class EventHandler1 : IEventHandler
	{
		//대상타입의 클래스에서 이 메소드를 실행시킴
		public virtual void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} 이벤트1 호출");
		}

	}

	//옵저버 구현클래스2(이벤트 종류2)
	public class EventHandler2 : IEventHandler
	{
		public void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} 이벤트2 호출");
		}
	}



	//대상클래스 == 직접적으로 이벤트를 호출할 클래스
	//: 대상 인터페이스를 구현할 클래스
	public class TempMessageSystem : MonoBehaviour
	{

		//이벤트들을 리스트로 관리
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();
		List<IEvent> eventList = new List<IEvent>();


		//약속(인터페이스) 목록  == 반드시 구현해야 되는 것
		public void Subscribe<T>(IEventHandler e)
		{
			//EventListenerDic.Add(typeof(T),  new List<IEventHandler>());  // 구독할때마다 새로운 리스트가 계속 생겨남
			//EventListenerDic[typeof(T)].Add(e);


			//타입이 있는지 찾아보고 없으면 새로운 리스트를 생성해서 추가.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(e);


			}
			else
			{
				//eventList.Add((TempCodes.IEvent)typeof(T));  //구독할때 이벤트리스트에 추가해주기
				EventListenerDic.Add(typeof(T), new List<IEventHandler>());
				EventListenerDic[typeof(T)].Add(e);
			}


		}
		public void Unsubscribe<T>(IEventHandler e)
		{

			if (EventListenerDic[typeof(T)].Contains(e)) EventListenerDic[typeof(T)].Remove(e);  //사전에서도 지워준다.



		}
		public void Notify()
		{
			//for 이용
			//for(int i=0;i<EventList.Count; i++)
			//{
			//	EventList[i].Notify();
			//}

			//디버깅
			Debug.Log(eventList.Count);

			//foreach 이용
			foreach (var e in eventList)
			{

				var listners = EventListenerDic[e.GetType()];

				foreach (var listner in listners)
				{

					listner.OnNotify(e);
				}
			}

			//eventList.Clear();

			//foreach(var e in eventList)
			//{
			//	e.Dispose();//Dispose 사용
			//}


		}

		// Start is called before the first frame update
		void Start()
		{

			eventList.Add(new MoveEvent());
			eventList.Add(new DamageEvent());

			IEventHandler eve1 = new EventHandler1();
			IEventHandler eve2 = new EventHandler2();




			Subscribe<MoveEvent>(eve1);
			Subscribe<DamageEvent>(eve2);


			Notify();

			Unsubscribe<DamageEvent>(eve2);

			Notify();



			eventList.Clear();


		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}