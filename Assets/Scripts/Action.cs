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



public class Action : MonoBehaviour
{
	//시스템
	TempCodes.TempMessageSystem eSystem;


	//이벤트 목록
	AttackEvent ae;
	DamageEvent de;
	MoveEvent me;



	//핸들러 목록
	AttackHandler atkHandler;
	DamageHandler dmgHandler;
	MoveHandler moveHandler;






	// Start is called before the first frame update
	void Start()
	{
		//시스템 생성
		eSystem = new TempCodes.TempMessageSystem();

		//이벤트 생성
		ae = new AttackEvent();
		de = new DamageEvent();
		me = new MoveEvent();




		//핸들러 생성
		atkHandler = new AttackHandler();
		dmgHandler = new DamageHandler();
		moveHandler = new MoveHandler();


		//이벤트 리스트에 이벤트 등록
		eSystem.AddEvent(ae);
		eSystem.AddEvent(de);
		eSystem.AddEvent(me);


		//이벤트를 들을 핸들러 등록.
		eSystem.Subscribe<AttackEvent>(atkHandler);
		eSystem.Subscribe<DamageEvent>(dmgHandler);
		eSystem.Subscribe<MoveEvent>(moveHandler);


		//SubscribeMember 디버깅용
		//eSystem.Subscribe<AttackEvent>(moveHandler, dmgHandler);
		//eSystem.Subscribe<DamageEvent>(atkHandler, moveHandler);
		//eSystem.Subscribe<MoveEvent>(atkHandler, dmgHandler);

		//eSystem.SubscribeMember(ae,me,de);

		//eSystem.Unsubscribe<AttackEvent>(moveHandler, dmgHandler);
		//eSystem.Unsubscribe<DamageEvent>(atkHandler, moveHandler);
		//eSystem.Unsubscribe<MoveEvent>(atkHandler, dmgHandler);


		//오류 체크
		eSystem.Subscribe<AttackEvent>(moveHandler, dmgHandler);

		eSystem.Notify<AttackEvent>(moveHandler, dmgHandler);



	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			eSystem.Notify();
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			eSystem.Subscribe<AttackEvent>(moveHandler);
			eSystem.Notify(ae);
			eSystem.Unsubscribe<AttackEvent>(moveHandler);
		}


	}
}
