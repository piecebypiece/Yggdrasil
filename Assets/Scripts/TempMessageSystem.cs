using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace TempCodes
{
	public interface IEvent : System.IDisposable
	{

	}


	public interface IEventHandler
	{
		//여기서 특정이벤트를 관리.
		public abstract void OnNotify(IEvent e);

	}

	//옵저버 구현클래스1(이벤트 종류1)
	public class MoveEventHandler : IEventHandler
	{
		//대상타입의 클래스에서 이 메소드를 실행시킴
		public virtual void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} 무브 이벤트 호출");
		}

	}

	//옵저버 구현클래스2(이벤트 종류2)
	public class DamageEventHandler : IEventHandler
	{
		public void OnNotify(IEvent e)
		{
			Debug.Log($"{e.ToString()} 대미지 이벤트 호출");
		}
	}



	//대상클래스 == 직접적으로 이벤트를 호출할 클래스
	//: 대상 인터페이스를 구현할 클래스
	public class TempMessageSystem : MonoBehaviour
	{

		//이벤트들을 리스트로 관리
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();
		List<IEvent> eventList = new List<IEvent>();

		#region 이벤트리스트 관련
		public void AddEvent(TempCodes.IEvent e)
		{
			eventList.Add(e);
		}

		public void RemoveEvent(TempCodes.IEvent e)
		{
			eventList.Remove(e);
		}
		#endregion



		#region 구독(Subscribe) 관련
		//구독
		public void Subscribe<T>(IEventHandler handler)
		{
			//타입(T)이 있는지 찾아보고 있으면 이벤트(e) 추가.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(handler);
			}
			else //없으면 해당 타입(T)에 관련된 목록을 만들고 이벤트(e) 추가
			{
				EventListenerDic.Add(typeof(T), new List<IEventHandler>());
				EventListenerDic[typeof(T)].Add(handler);
			}
		}
		//구독 - 복수
		public void Subscribe<T>(params IEventHandler[] handlerList)
		{
			foreach (var element in handlerList)
			{
				if (EventListenerDic.ContainsKey(typeof(T)))
				{
					EventListenerDic[typeof(T)].Add(element);

				}
				else
				{
					EventListenerDic.Add(typeof(T), new List<IEventHandler>());
					EventListenerDic[typeof(T)].Add(element);
				}
			}


		}

		//해당 이벤트를 구독한 멤버들의 목록을 보여준다.(1개의 이벤트만)
		public void SubscribeMember<T>()
		{
			var listners = EventListenerDic[typeof(T)];

			Debug.Log($"--------------{typeof(T)}를 구독하고있는 핸들러 목록--------------");

			foreach (var listner in listners)
			{
				Debug.Log($"{listner.ToString()}");

			}
			Debug.Log("-----------------------------------------------------------------");

		}

		//해당 이벤트들을 구독한 멤버들의 목록들을 보여준다.(복수의 이벤트 가능)
		public void SubscribeMember(params IEvent[] eList)
		{
			foreach (var element in eList)
			{
				var listners = EventListenerDic[element.GetType()];

				Debug.Log($"--------------{element.GetType()}를 구독하고있는 핸들러 목록--------------");

				foreach (var listner in listners)
				{
					Debug.Log($"{listner.ToString()}");
				}
				Debug.Log("-----------------------------------------------------------------");
			}

		}

		#endregion


		#region 해지(Unsubscribe) 관련
		//해지
		public void Unsubscribe<T>(IEventHandler handler)
		{
			if (EventListenerDic[typeof(T)].Contains(handler)) EventListenerDic[typeof(T)].Remove(handler);  //사전에서 지워준다.
		}

		//해지 - 복수
		public void Unsubscribe<T>(params IEventHandler[] handlereList)
		{
			foreach (var element in handlereList)
			{
				if (EventListenerDic[typeof(T)].Contains(element)) EventListenerDic[typeof(T)].Remove(element);
			}
		}
		#endregion


		#region 알림(Notify) 관련

		public void Notify<T>(params TempCodes.IEventHandler[] handlerList)
		{

			var listners = EventListenerDic[typeof(T)];



			//제외 알림이 현재 두번씩 호출됨 한번씩만 호출되도록 변경하기.
			foreach (var listner in listners)
			{
				foreach (var handler in handlerList)
				{
					if (!listner.Equals(handler))
					{
						Debug.Log($"{handler.ToString()}은 알림에서 제외합니다.");
					}

				}
			}

		}


		//개인적인(지정한(T)이벤트(복수도 가능)의 구독자)알림
		public void Notify(params TempCodes.IEvent[] eList)
		{

			foreach (var element in eList)
			{

				if (eventList.Contains(element))
				{
					var listners = EventListenerDic[element.GetType()];

					foreach (var lister in listners)
					{
						lister.OnNotify(element);
					}
				}
				else
				{
					Debug.Log($"{element.ToString()}는 이벤트 리스트에 존재하지 않습니다...<!먼저 이벤트 리스트에 추가해주세요!>");
				}


			}

		}


		//전체(이벤트 리스트에 있는 전 이벤트의 구독자)에게 알림
		public void Notify()
		{

			//foreach 이용
			foreach (var e in eventList)
			{

				var listners = EventListenerDic[e.GetType()];

				foreach (var listner in listners)
				{
					listner.OnNotify(e);
				}
			}

		}
		#endregion


		// Start is called before the first frame update
		void Start()
		{


		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}