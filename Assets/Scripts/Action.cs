using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playerBuf
{


	public class Buf
	{
		
		public bool trigger;

		private Yggdrasil.CharacterStats copy_stat;

		public void BufTriggerManager()
		{
			
			// 버프가 사용가능한지 불가능하지 판별(서버)
			trigger = true;

			
			// 서버에서 판별후 버프 내용 처리
			if(trigger)
			{
				copy_stat.Attack += 2;
				Debug.Log("버프를 사용합니다.");
				Debug.Log("Attack +2 UP!");
			}
			else
			{
				Debug.Log("버프를 사용할수 없습니다.");
			}
			

		}

		public Buf(ref Yggdrasil.CharacterStats a)
		{
			copy_stat = a;
		}
	}

}





public class Action : MonoBehaviour
{

	// 1.시스템 생성
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
		eSystem = TempCodes.TempMessageSystem.Instance;

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


	}
}
