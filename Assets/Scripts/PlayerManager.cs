using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerSkillSet;

public class PlayerManager : MonoBehaviour
{
	//플레이어 오브젝트
	public static GameObject p_Object;

	//스탯
	public Yggdrasil.CharacterStats p_Status;
	

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

			var type_num = (SkillType)skillType_num;

			switch (type_num)
			{
				case SkillType.Attack:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Attack";
					break;
				case SkillType.Defense:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Defense";
					break;
				case SkillType.Support:
					skill = SkillFactory.SkillTypeSet(type_num);
					SkillType_txt.text = "Support";
					break;
			}

		}


		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//스킬을 사용할수 있는지 1차 점검(마나,쿨타임 등 체크)

			//1차점검에서 통과되면 스킬발동
			skill.SkillAction(AbilityType.Damage);
			Debug.Log("1");
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			//스킬을 사용할수 있는지 1차 점검(마나,쿨타임 등 체크)

			skill.SkillAction(AbilityType.Distance);
			Debug.Log("2");
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			//스킬을 사용할수 있는지 1차 점검(마나,쿨타임 등 체크)

			skill.SkillAction(AbilityType.Speed);
			Debug.Log("3");
		}


	}



	private void Move()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(h, 0, v) * p_Status.MoveSpeed * Time.deltaTime);
	}
	

	// Start is called before the first frame update
	void Start()
    {
		p_Object = this.gameObject;
		p_Status = new Yggdrasil.CharacterStats();

		

		//캐릭터 초기셋팅
		p_Status.MoveSpeed = 7f; //기본스피드

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
