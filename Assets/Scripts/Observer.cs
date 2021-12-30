using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//옵저버 추상클래스  == 이벤트 추상 클래스
//: 옵저버들이 구현해야 할 인터페이스 메서드  == 이벤트에 구현할 공통적인 매서드
public abstract class Event
{

	public abstract void Notify();

}

//옵저버 구현클래스1(이벤트 종류1)
public class EventHandler1 : Event
{
	//대상타입의 클래스에서 이 메소드를 실행시킴
	public override void Notify()
	{
		Debug.Log("이벤트1 호출");
	}

}

//옵저버 구현클래스2(이벤트 종류2)
public class EventHandler2 : Event
{
	public override void Notify()
	{
		Debug.Log("이벤트2 호출");
	}
}


//대상 인터페이스 == 구현할 대상클래스에서 반드시 넣어야 하는 약속
//: 옵저버 관리, 활용에 관한 타입 정의 == 이벤트 관리 및 활용 
public interface ISubject
{

	public void Subscribe(Event e);  //구독
	public void Unsubscribe(Event e);  //구독 해지
	public void Notify();  //구독자에게 전원 알림


}


//대상클래스 == 직접적으로 이벤트를 호출할 클래스
//: 대상 인터페이스를 구현할 클래스
public class Observer : MonoBehaviour,ISubject
{

	//이벤트들을 리스트로 관리
	List<Event> EventList = new List<Event>();


 
	//약속(인터페이스) 목록  == 반드시 구현해야 되는 것
	public void Subscribe(Event e)  
	{
		EventList.Add(e);
	}
	public void Unsubscribe(Event e)
	{

		if (EventList.IndexOf(e) > 0) EventList.Remove(e);
	}
	public void Notify()
	{
		//for 이용
		//for(int i=0;i<EventList.Count; i++)
		//{
		//	EventList[i].Notify();
		//}

		//foreach 이용
		foreach(Event e in EventList)
		{
			e.Notify();
		}



	}

	// Start is called before the first frame update
	void Start()
    {
		Event eve1 = new EventHandler1();
		Event eve2 = new EventHandler2();


		Subscribe(eve1);
		Subscribe(eve2);

		Notify();

		Unsubscribe(eve2);

		Notify();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
