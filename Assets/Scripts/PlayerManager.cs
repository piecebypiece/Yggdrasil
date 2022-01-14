using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	
	public float Speed = 7f;
	private float h, v;

	public Yggdrasil.CharacterStats p_status; 

	private TempCodes.TempMessageSystem eSystem;
	private MoveEvent me;
	private Handler player;
    // Start is called before the first frame update
    void Start()
    {
		

		// 1.시스템 생성
		eSystem = TempCodes.TempMessageSystem.Instance;

		// 2.접속할 이벤트 생성
		me = new MoveEvent();

		// 3.핸들러 등록(생성)
		player = new Handler();

		// 4.이벤트 추가
		eSystem.AddEvent(me);

		// 5.타입 매치
		eSystem.AddEventdic(me);

		// 6.구독 및 해지
		eSystem.Subscribe<MoveEvent>(player);

		eSystem.ShowSubscribeMember<MoveEvent>();



		p_status = new Yggdrasil.CharacterStats();
		p_status.Attack = 5;
		

	}

    // Update is called once per frame
    void Update()
    {


		if(Input.GetKeyDown(KeyCode.Space))
		{
			playerBuf.Buf b = new playerBuf.Buf(ref p_status);

			//버프 사용전 공격력
			Debug.Log("Attack:" + p_status.Attack);


			//매니저에서 버프의 사용을 확인받음
			b.BufTriggerManager();

			//버프 사용후 공격력
			Debug.Log("Attack:" + p_status.Attack);
		}

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, 0, v) * Speed * Time.deltaTime);



	}
}
