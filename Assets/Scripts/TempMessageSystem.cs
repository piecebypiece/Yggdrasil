using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEvent : TempCodes.IEvent
{
	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}

public class MoveEvent : TempCodes.IEvent
{

	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}

public class AttackEvent : TempCodes.IEvent
{


	public void Dispose()
	{
		throw new System.NotImplementedException();
	}
}


public class Handler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{
		Debug.Log($"{e.ToString()}");
	}

}

public class AttackHandler : TempCodes.IEventHandler
{


	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} 공격");
		//공격에관한 처리
	}

}


public class MoveHandler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} 이동");
		//이동에관한 처리
	}

}

public class DamageHandler : TempCodes.IEventHandler
{

	public void OnNotify(TempCodes.IEvent e)
	{

		Debug.Log($"{e.ToString()} 대미지");
		//대미지에관한 처리
	}

}







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



	//대상클래스 == 직접적으로 이벤트를 호출할 클래스
	//: 대상 인터페이스를 구현할 클래스
	public class TempMessageSystem : MonoBehaviour
	{

		private static TempMessageSystem instance = null;

		//이벤트와 핸들러들을 관리하는 사전
		Dictionary<System.Type, List<IEventHandler>> EventListenerDic = new Dictionary<System.Type, List<IEventHandler>>();

		//타입과 이벤트를 매치시켜주는 사전
		Dictionary<System.Type, IEvent> EventDic = new Dictionary<System.Type, IEvent>();

		//이벤트를 관리하는 리스트
		List<IEvent> eventList = new List<IEvent>();



		private void Awake()
		{
			if(null == instance)
			{
				instance = this;

				DontDestroyOnLoad(this.gameObject);
			}
			else
			{
				Destroy(this.gameObject);
			}
		}


		public static TempMessageSystem Instance
		{
			get
			{
				if (null == instance)
				{
					return null;
				}

				return instance;

			}
		}

		//타입(T) => 이벤트들.

		#region 이벤트리스트 관련


		//이벤트 리스트 와 매치사전 목록 보여주는 함수 구현


		//이벤트 리스트 목록을 보여주는 함수
		public void ShowEventList()
		{

			Debug.Log("--------------리스트에 등록되어 있는 이벤트--------------");
			foreach(var item in eventList)
			{
				Debug.Log($"{item.ToString()}");
			}
			Debug.Log("-----------------------------------------------------------------");
		}

		//이벤트 매치사전의 목록을 보여주는 함수
		public void ShowEventDic()
		{
			Debug.Log("--------------사전에 매치되어 있는 이벤트--------------");
			
			foreach(KeyValuePair<System.Type,TempCodes.IEvent> item in EventDic)
			{
				Debug.Log($"Type:{item.Key.ToString()} / Event:{item.Value.ToString()}");
			}
			Debug.Log("-----------------------------------------------------------------");

		}
		
		public void AddEvent(TempCodes.IEvent e)  //리스트에 이벤트 추가
		{

			if (eventList.Contains(e))
				Debug.Log($"{e.GetType()}은 등록되어있는 이벤트입니다.");
			else
				eventList.Add(e);
		}

		public void RemoveEvent(TempCodes.IEvent e) //리스트에 이벤트 제거
		{
			if (eventList.Contains(e))
				eventList.Remove(e);
			else
				Debug.Log($"{e.GetType()}은 리스트에 없는 이벤트입니다.");
		}



		public void AddEventdic(TempCodes.IEvent e) //사전에 타입과 이벤트 매칭
		{
			if (!EventDic.ContainsKey(e.GetType()))
			{
				EventDic.Add(e.GetType(), e);
				Debug.Log($"EventDic에 {e.ToString()} 추가");
				Debug.Log($"EventDic.Count:{EventDic.Count}");
			}
			else
			{
				Debug.Log($"{e.GetType()}은 현재 매칭되어 있는 이벤트입니다.");
			}
		}

		public void RemoveEventdic(TempCodes.IEvent e) //사전에 매칭정보 지우기
		{

			if (EventDic.ContainsKey(e.GetType())) 
			{
				EventDic.Remove(e.GetType(), out e);
				Debug.Log($"EventDic에 {e.ToString()} 삭제");
				Debug.Log($"EventDic.Count:{EventDic.Count}");
			}
			else
			{
				Debug.Log($"{e.GetType()}은 사전에 매치되어있지 않습니다.");
			}
				
			
		}

		#endregion


		#region 구독(Subscribe) 관련
		//구독
		public void Subscribe<T>(IEventHandler handler)
		{
			// 타입(T)이 있는지 찾아보고 있으면 핸들러 추가.
			if (EventListenerDic.ContainsKey(typeof(T)))
			{
				EventListenerDic[typeof(T)].Add(handler);
			}
			else //없으면 해당 타입(T)에 관련된 목록을 만들고 핸들러(handler) 추가
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
				//타입(T)이 있는지 찾아보고 있으면 핸들러들을 추가
				if (EventListenerDic.ContainsKey(typeof(T)))
				{
					EventListenerDic[typeof(T)].Add(element);

				}
				else //없으면 해당 타입(T)에 관련된 목록을 만들고 핸들러들(handlerList)을 추가
				{
					EventListenerDic.Add(typeof(T), new List<IEventHandler>());
					EventListenerDic[typeof(T)].Add(element);
				}
			}


		}

		//해당 이벤트를 구독한 멤버들의 목록을 보여준다.(1개의 타입(T)만)
		public void ShowSubscribeMember<T>()
		{
			var listners = EventListenerDic[typeof(T)];

			Debug.Log($"--------------{typeof(T)}를 구독하고있는 핸들러 목록--------------");

			foreach (var listner in listners)
			{
				Debug.Log($"{listner.ToString()}");

			}
			Debug.Log("-----------------------------------------------------------------");

		}

		//해당 이벤트들을 구독한 멤버들의 목록들을 보여준다.(복수의 타입(T)가능)
		public void ShowSubscribeMember(params IEvent[] eList)
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
			//타입(T)에 해당하는 리스트 목록중에 지정한 핸들러(handler)가 있으면 지워준다.
			if (EventListenerDic[typeof(T)].Contains(handler)) EventListenerDic[typeof(T)].Remove(handler);
		}

		//해지 - 복수
		public void Unsubscribe<T>(params IEventHandler[] handlereList)
		{
			foreach (var element in handlereList)
			{
				//타입(T)에 해당하는 리스트 목록중에 지정한 핸들러들(handlereList)이 있으면 지워준다.
				if (EventListenerDic[typeof(T)].Contains(element)) EventListenerDic[typeof(T)].Remove(element);
			}
		}
		#endregion


		#region 알림(Notify) 관련

		//알림 - 핸들러 제외 기능
		public void Notify<T>(bool Except,params TempCodes.IEventHandler[] handlerList)
		{

			if(Except)  //Except가 True일경우 뒤에오는 핸들러 목록을 알림에서 제외
			{
				//지정 타입(T)과 핸들러 리스트를 받는다.
				var listners = EventListenerDic[typeof(T)]; //해당 타입(T)에 관련된 리스트를 받아온다. 

				bool check;

				foreach (var listner in listners)  //그 리스트를 구독하는 사람들중
				{
					check = false;
					foreach (var handler in handlerList)  //핸들러 리스트에 적혀있는 핸들러가 있다면
					{
						//핸들러 리스트에 적혀있는 핸들러가 있다면...
						if (string.Compare(handler.ToString(), listner.ToString(), false) == 0)
						{
							check = true;
							Debug.Log($"{typeof(T).ToString()}의{handler.ToString()}은 알림에서 제외합니다.");
						}

					}

					//지정된 핸들러 리스트에 없다면 알림을 보내준다.
					if (!check)
						listner.OnNotify(EventDic[typeof(T)]);

				}
			}
			else  //False일 경우 뒤에 오는 핸들러 목록만 알림전송
			{

				//지정 타입(T)과 핸들러 리스트를 받는다.
				var listners = EventListenerDic[typeof(T)]; //해당 타입(T)에 관련된 리스트를 받아온다. 

				bool check;

				foreach (var listner in listners)  //그 리스트를 구독하는 사람들중
				{
					check = false;
					foreach (var handler in handlerList)  //핸들러 리스트에 적혀있는 핸들러가 있다면
					{
						//핸들러 리스트에 적혀있는 핸들러가 있다면...
						if (string.Compare(handler.ToString(), listner.ToString(), false) == 0)
						{
							check = true;
						}

					}

					//지정된 핸들러 리스트에 없다면 알림을 보내준다.
					if (check)
						listner.OnNotify(EventDic[typeof(T)]);

				}


			}

			
		}



		//알림- 선택(이벤트 리스트에서 선택한(단일,복수 가능) 이벤트를 구독하고있는 모든 핸들러들에게 
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

		

		//알림 - 전체(이벤트 리스트에 있는 이벤트를 구독하고있는 모든 핸들러들에게)
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



		public void ResetData()
		{
			eventList.Clear();
			EventDic.Clear();
			EventListenerDic.Clear();

			Debug.Log($"eventList.Count:{eventList.Count}, " +
				$"EventDic.Count:{EventDic.Count}, " +
				$"EventListenerDic.Count:{EventListenerDic.Count}");

		}


		
	}
}