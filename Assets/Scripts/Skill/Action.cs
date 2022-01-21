using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillSet;  //SkillFactory.cs -> PlayerSkillSet(namespace)




public class Action : MonoBehaviour
{
	
	public GameObject Spirit_Prefab;  //임시


	private SkillManager skillMgr;
	private static Action instance = null;
	

	public static Action Instance
	{
		get
		{
			if(null== instance)
			{
				return null;
			}

			return instance;
		}
	}


	GameObject FindEnemy(GameObject findStartObject,float distance) //근처에 있는 적 찾기 함수
	{
		float Dist = 0f;
		float near = 0f;
		GameObject nearEnemy = null;

		//범위 내의 적을 찾는다.
		Collider[] colls = Physics.OverlapSphere(findStartObject.transform.position, 10f, 1 << 9);  //9번째 레이어 = Enemy

		if (colls.Length == 0)
		{
			Debug.Log("범위에 적이 없습니다.");
			DestroyObject(findStartObject);
			return null;
		}
		else
		{
			//적이 있다면 그 적들 중에
			for (int i = 0; i < colls.Length; i++)
			{

				//정령과의 거리를 계산후
				Dist = Vector3.Distance(findStartObject.transform.position, colls[i].transform.position);

				if (i == 0)
				{
					near = Dist;
					nearEnemy = colls[i].gameObject;
				}

				//그 거리가 작다면 거리를 저장하고 해당 오브젝트를 저장
				if (Dist < near)
				{
					near = Dist;
					nearEnemy = colls[i].gameObject;
				}
			}

			return nearEnemy;
		}

	}



	/**************지원관련 스킬과 코루틴****************/

	#region Support Skill 

	public void Support_Damage()  //지원-피해
	{
		GameObject SupDmg_Spirit = Instantiate(Spirit_Prefab);

		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Support_Damage_Mgr(SupDmg_Spirit))
		{
			//정령을 플레이어 위치에 이동.
			SupDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;

			//코루틴으로 실행.
			StartCoroutine(Support_Damage_Skill(SupDmg_Spirit));
		}
		else
			Debug.Log("스킬을 사용할 수 없습니다.");
	}


	public void Support_Distance() //지원-거리
	{ 
		GameObject nearEnemy = null;
		GameObject SupDis_Spirit = Instantiate(Spirit_Prefab);
		//정령을 플레이어 위치에 이동
		SupDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Support_Distance_Mgr(SupDis_Spirit))
		{
			//근처에 적을 찾는다
			nearEnemy = FindEnemy(SupDis_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Support_Distance_Skill(SupDis_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(SupDis_Spirit);
			Debug.Log("스킬을 사용할 수 없습니다.");
		}
			
	}

	public void Support_Speed()  //지원-이동
	{
		GameObject nearEnemy = null;
		GameObject SupSpeed_Spirit = Instantiate(Spirit_Prefab);
		//정령을 플레이어 위치에 이동
		SupSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Support_Speed_Mgr(SupSpeed_Spirit))
		{
			//근처에 적을 찾는다
			nearEnemy = FindEnemy(SupSpeed_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Support_Speed_Skill(SupSpeed_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(SupSpeed_Spirit);
			Debug.Log("스킬을 사용할 수 없습니다.");
		}

	}
	#endregion

	#region Support_Skill_Coroutine 
	IEnumerator Support_Damage_Skill(GameObject target)        //지원-피해 스킬
	{
		//2초동안 정령 지속시킬 스킬내용 
		float sprit_range = 5f;  //정령(스킬)을 적용시킬 범위
		float spirit_time = 0f;   //정령 지속 시간 변수

		while (true)
		{
			spirit_time += Time.deltaTime;

			//범위내의 적을 찾아서 
			Collider[] colls = Physics.OverlapSphere(target.transform.position, sprit_range, 1 << 9);  //9번째 레이어 = Enemy

			foreach (var rangeCollider in colls)
			{
				//범위내에서 적용시킬 디버프 내용

				//디버깅
				Debug.Log(rangeCollider.ToString());


			}

			//정령 지속시간이 경과시 
			if (spirit_time > 2f)
			{
				//오브젝트 파괴후 코루틴 종료
				DestroyObject(target);
				yield break;
			}


			yield return null;
		}
	}

	IEnumerator Support_Distance_Skill(GameObject Spirit,GameObject target) //지원-거리 스킬
	{
		float pull_range = 5f;  //끌어당길범위
		float pull_time = 0f; //끌어당길 시간 체크 변수.

		//제일 가까운 적한테 이동 후
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if(Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}

		//지정범위내의 모든 에너미(몹들만)를 자신의 위치로 당긴 후
		Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, pull_range, 1 << 9);  //9번째 레이어 = Enemy


		while (true)
		{
			pull_time += Time.deltaTime;

			foreach (var rangeCollider in colls)
			{
				//보스가 아니라면
				if(rangeCollider.gameObject.name != "Boss")
				{
					rangeCollider.gameObject.transform.position = Vector3.MoveTowards(rangeCollider.gameObject.transform.position, Spirit.transform.position, 0.05f);
				}
			}

			if (pull_time >= 0.75f)
				break;


			yield return null;
		}

		//해당 에너미의 이동속도를 0(스턴 효과)로 만든다.

		//foreach (var rangeCollider in colls)
		//{
			//rangeCollider.gameObject  -> 게임 오브젝트를 얻어와서 스탯부분을 참조해 이동속도를 지정시간(0.75)동안 0으로 만든다.
		//}

		DestroyObject(Spirit);
		yield return null;

	}

	IEnumerator Support_Speed_Skill(GameObject Spirit, GameObject target) //지원-이동 스킬
	{
		float sprit_range = 15f;  //정령(스킬)을 적용시킬 범위
		float spirit_time = 0f;   //정령 지속 시간 변수

		//제일 가까운 적한테 이동 후
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if (Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}


		while (true)
		{
			spirit_time += Time.deltaTime;

			if (spirit_time >= 2f)
			{
				DestroyObject(Spirit);
				yield break;
			}
				

			//범위내의 적을 찾는다.
			Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, sprit_range, 1 << 9);  //9번째 레이어 = Enemy

			foreach (var rangeCollider in colls)
			{
				//여기서 이동속도 감소 하는 내용 추가.

				//디버깅
				Debug.Log($"{rangeCollider.name}의 이동속도는 -50% 감소합니다.");
			}


			yield return null;
		}

	}

	#endregion

	/**********************************************/



	#region Attack Skill
	public void Attack_Damage()
	{
		GameObject nearEnemy = null;
		GameObject AtkDis_Spirit = Instantiate(Spirit_Prefab);
		//정령을 플레이어 위치에 이동
		AtkDis_Spirit.transform.position = PlayerManager.p_Object.transform.position;

		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Attack_Damage_Mgr(AtkDis_Spirit))
		{
			//근처에 적을 찾는다
			nearEnemy = FindEnemy(AtkDis_Spirit,15f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Damage_Skill(AtkDis_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkDis_Spirit);
			Debug.Log("스킬을 사용할 수 없습니다.");
		}


		// 대충 서버에서 확인받는 내용

		//서버에서 데이터 받고 스킬사용이 가능하다면
	}

	public void Attack_Distance()
	{
		GameObject nearEnemy = null;
		GameObject AtkDmg_Spirit = Instantiate(Spirit_Prefab);
		//정령을 플레이어 위치에 이동
		AtkDmg_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Attack_Distance_Mgr(AtkDmg_Spirit))
		{
			//근처에 적을 찾는다
			nearEnemy = FindEnemy(AtkDmg_Spirit,10f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Distance_Skill(AtkDmg_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkDmg_Spirit);
			Debug.Log("스킬을 사용할 수 없습니다.");
		}

	}

	public void Attack_Speed()
	{
		GameObject nearEnemy = null;
		GameObject AtkSpeed_Spirit = Instantiate(Spirit_Prefab);
		//정령을 플레이어 위치에 이동
		AtkSpeed_Spirit.transform.position = PlayerManager.p_Object.transform.position;


		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if (skillMgr.Attack_Speed_Mgr(AtkSpeed_Spirit))
		{
			//근처에 적을 찾는다
			nearEnemy = FindEnemy(AtkSpeed_Spirit, 10f);

			if (nearEnemy == null)
				return;
			else
				StartCoroutine(Attack_Speed_Skill(AtkSpeed_Spirit, nearEnemy));

		}
		else
		{
			DestroyObject(AtkSpeed_Spirit);
			Debug.Log("스킬을 사용할 수 없습니다.");
		}

	}


	#endregion



	#region Attack_Skill_Coroutine

	IEnumerator Attack_Damage_Skill(GameObject Spirit, GameObject target)
	{
		float attack_range = 5f;  //공격범위
		//float attack_time = 0f; //공격 시간.

		int randvalue = 0;  //랜덤값
		

		//제일 가까운 적한테 이동 후
		while (true)
		{
			Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position, 0.1f);

			if (Spirit.transform.position == target.transform.position)
				break;

			yield return null;
		}

		//지정범위내의 모든 에너미(몹들만)를 찾은 후
		Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9번째 레이어 = Enemy

		

		for(int i=0; i<10; i++)
		{
			randvalue = Random.Range(0, colls.Length);

			//같은 대상에게 피해를 입힐시 20% 감소된 피해를 줍니다.
			Debug.Log($"{colls[randvalue].name}에게 대미지를 줍니다");

			//yield return null;
		}

		DestroyObject(Spirit);

		yield return null;
	}


	IEnumerator Attack_Distance_Skill(GameObject Spirit, GameObject target)
	{
		float attack_range = 5f;  //공격범위
		float attack_time = 0f;  //공격 시간
		float check_time = 0f;  //공격 종료 시간

		//플레이어가 마주보는 방향의 임의의 위치를(6f) 구한다.
		var heading = target.transform.position - Spirit.transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;
		direction.y = 0;
		direction *= 6f;




		//제일 가까운 적한테 이동 후
		while (true)
		{
			//공격시간체크
			attack_time += Time.deltaTime;

			//소멸시간체크
			check_time += Time.deltaTime;


			if (check_time >= 2.2f)
			{
				//소멸 시간
				yield return new WaitForSeconds(0.3f);
				DestroyObject(Spirit);
				yield break;
			}
				
			if (attack_time >= 0.5f)
			{
				attack_time = 0f;

				Spirit.transform.position = Vector3.MoveTowards(Spirit.transform.position, target.transform.position+direction, 1.5f);

				//지정범위내의 모든 에너미(몹들만)를 자신의 위치로 당긴 후
				Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9번째 레이어 = Enemy

				foreach(var rangeCollider in colls)
				{
					//대미지 코드
					Debug.Log($"{rangeCollider.name}에게 피해를 줍니다.");
				}

			}

			yield return null;
		}

	}


	IEnumerator Attack_Speed_Skill(GameObject Spirit, GameObject target)
	{

		float attack_range = 3f;  //공격범위
		float attack_time = 0f;  //공격 시간
		float check_time = 0f;  //공격 종료 시간

		//가장 가까운 적에게 순간이동후
		Spirit.transform.position = target.transform.position;



		// 0.33초에 한번씩 3초동안 유지되는 장판을 시전한다.

		while(true)
		{

			attack_time += Time.deltaTime;
			check_time += Time.deltaTime;


			if (check_time >= 3.2f)
			{
				//소멸 시간
				yield return new WaitForSeconds(0.3f);
				DestroyObject(Spirit);
				yield break;
			}



			if (attack_time >= 0.33f)
			{
				attack_time = 0f;


				//지정범위내의 모든 에너미(몹들만)를 자신의 위치로 당긴 후
				Collider[] colls = Physics.OverlapSphere(Spirit.transform.position, attack_range, 1 << 9);  //9번째 레이어 = Enemy



				foreach (var rangeCollider in colls)
				{
					//대미지 코드
					Debug.Log($"{rangeCollider.name}에게 장판 피해를 줍니다.");
				}

			}


			yield return null;
		}

		
	}


	#endregion

	#region Defense Skill
	public void Defense_Damage()
	{
		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)

		// 대충 서버에서 확인받는 내용

		//서버에서 데이터 받고 스킬사용이 가능하다면
	}

	public void Defense_Distance()
	{

	}

	public void Defense_Speed()
	{

	}
	#endregion


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

		skillMgr = new SkillManager();
	}


	// Start is called before the first frame update
	void Start()
	{
	
	}


	// Update is called once per frame
	void Update()
	{

	}
}
