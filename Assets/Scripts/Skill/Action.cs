using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSkillSet;  //SkillFactory.cs -> PlayerSkillSet(namespace)




public class Action : MonoBehaviour
{
	public PlayerManager pm = new PlayerManager();
	public GameObject target;
	public GameObject player;

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

	#region Support Skill 
	public void Support_Damage()
	{
		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)
		if(skillMgr.Support_Damage_Mgr(target))
		{
			//여기서    target.transform.position = pm.transform.position; 오류남.....(왜지?)
			target.transform.position = player.transform.position;
		}

		

		//서버에서 데이터 받고 스킬사용이 가능하다면
	}
	
	public void Support_Distance()
	{

	}

	public void Support_Speed()
	{

	}
	#endregion

	#region Attack Skill
	public void Attack_Damage()
	{
		//Trigger(조건체크) -> 서버에 데이터를 보내서 확인받음(우선 매니저로만)

		// 대충 서버에서 확인받는 내용

		//서버에서 데이터 받고 스킬사용이 가능하다면
	}

	public void Attack_Distance()
	{

	}

	public void Attack_Speed()
	{

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
