using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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

		//순서 

		// 1.시스템 생성
		eSystem = new TempCodes.TempMessageSystem();

		// 2.이벤트 생성
		ae = new AttackEvent();
		de = new DamageEvent();
		me = new MoveEvent();


		// 3.핸들러 생성
		atkHandler = new AttackHandler();
		dmgHandler = new DamageHandler();
		moveHandler = new MoveHandler();


		// 4.이벤트 리스트에 추가
		eSystem.AddEvent(ae);
		eSystem.AddEvent(de);
		eSystem.AddEvent(me);

		// 5.타입과 이벤트 매칭
		eSystem.AddEventdic(ae);
		eSystem.AddEventdic(de);
		eSystem.AddEventdic(me);


		// 6.원하는 이벤트를 들을 핸들러들 추가(구독) 및 해지
		eSystem.Subscribe<AttackEvent>(atkHandler);
		eSystem.Subscribe<DamageEvent>(dmgHandler);
		eSystem.Subscribe<MoveEvent>(moveHandler);




	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//게임 종료시 데이타 초기화.
			eSystem.ResetData();
		}


		// 디버깅용
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
