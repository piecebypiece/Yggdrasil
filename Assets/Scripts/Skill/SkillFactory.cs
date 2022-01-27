using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Yggdrasil.PlayerSkillSet  //플레이어 스킬 셋팅 관련
{
	


	//추상 팩토리 패턴
	public abstract class Skill
	{
		//지정해주는 타입에 따라 액션에서 나가는 스킬이 달라지도록
		public abstract void SkillAction(AbilityType type);
	}

	//발동할 스킬의 능력
	public enum AbilityType
	{
		Distance,     //거리관련
		Speed,        //이동관련
		Damage        //피해관련
	}

	public enum SkillType
	{
		Attack,     //공격형 타입
		Defense,    //방어형 타입
		Support     //지원형 타입
	}

	public class SkillFactory
	{
		public static PlayerSkillSet.Skill SkillTypeSet(SkillType type)
		{
			PlayerSkillSet.Skill SkillSet = null;

			switch (type)
			{
				case SkillType.Attack:
					SkillSet = new Attack();
					break;
				case SkillType.Defense:
					SkillSet = new Defense();
					break;
				case SkillType.Support:
					SkillSet = new Support();
					break;
					//예외처리
			}
			return SkillSet;

		}

	}

	////////////////////////////////////////공격/////////////////////////////////////
	public class Attack : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					Action.Instance.AttackDamage();
					Debug.Log("공격-피해형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.AttackDistance();
					Debug.Log("공격-거리형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.AttackSpeed();
					Debug.Log("공격-지원형 스킬");
					break;

			}
		}
	}

	////////////////////////////////////////생존/////////////////////////////////////
	public class Defense : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					Action.Instance.DefenseDamage();
					Debug.Log("방어-피해형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.DefenseDistance();
					Debug.Log("방어-거리형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.DefenseSpeed();
					Debug.Log("방어-지원형 스킬");
					break;
			}
		}
	}

	////////////////////////////////////////지원/////////////////////////////////////
	public class Support : PlayerSkillSet.Skill
	{
		

		public override void SkillAction(PlayerSkillSet.AbilityType type)
		{
			switch (type)
			{
				case PlayerSkillSet.AbilityType.Damage:
					//여기는 스킬 실행내용만(실제로 스킬 실행은 액션의 함수)
					Action.Instance.SupportDamage();
					//디버그
					Debug.Log("지원-피해형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Distance:
					Action.Instance.SupportDistance();
					Debug.Log("지원-거리형 스킬");
					break;

				case PlayerSkillSet.AbilityType.Speed:
					Action.Instance.SupportSpeed();
					Debug.Log("지원-지원형 스킬");
					break;
			}
		}
	}



}

