using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerSkillSet;

public class PlayerManager : MonoBehaviour
{
	
	//스탯
	public Yggdrasil.CharacterStats p_status;


	//스킬
	private Skill skill;


	public Text SkillType_txt;
	private int skillType_num;
	// 이벤트 관련
	//private TempCodes.TempMessageSystem eSystem;
	//private MoveEvent me;
	//private Handler player;



	private void InputCheck()
	{

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			
			skillType_num++;
			skillType_num %= 3;


			switch (skillType_num)
			{
				case 0:
					skill = SkillFactory.SkillTypeSet(SkillType.Attack);
					SkillType_txt.text = "Attack";
					break;
				case 1:
					skill = SkillFactory.SkillTypeSet(SkillType.Defense);
					SkillType_txt.text = "Defense";
					break;
				case 2:
					skill = SkillFactory.SkillTypeSet(SkillType.Support);
					SkillType_txt.text = "Support";
					break;
			}

		}


		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			skill.SkillAction(AbilityType.Damage);
			Debug.Log("1");
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			skill.SkillAction(AbilityType.Distance);
			Debug.Log("2");
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			skill.SkillAction(AbilityType.Speed);
			Debug.Log("3");
		}


	}



	private void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, 0, v) * p_status.MoveSpeed * Time.deltaTime);
	}
	

	// Start is called before the first frame update
	void Start()
    {
		
		p_status = new Yggdrasil.CharacterStats();

		

		//캐릭터 초기셋팅
		p_status.MoveSpeed = 7f; //기본스피드

		skillType_num = 0;
		skill = SkillFactory.SkillTypeSet(SkillType.Attack);
	}

    // Update is called once per frame
    void Update()
    {
		//이동
		Move();

		//키보드 입력 체크 함수.
		InputCheck();

	}
}
