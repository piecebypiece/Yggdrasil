using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{

	public bool SkillCheck()
	{
		//쿨타임 마나,타일의 정령여부 등을 체크해서 값을 반환

		//사용못할시 이쪽에서 오류 출력.

		return true;
	}

	#region Support_SkillManager
	public bool Support_Damage_Mgr(GameObject target) //해당 스킬 발동에 필요한 조건을 매개변수로 받음.
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;

	}

	public bool Support_Distance_Mgr(GameObject target)
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;
	}

	public bool Support_Speed_Mgr(GameObject target)
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;
	}
	#endregion

	#region Attack_SkillManager
	public bool Attack_Damage_Mgr(GameObject target) //해당 스킬 발동에 필요한 조건을 매개변수로 받음.
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;

	}

	public bool Attack_Distance_Mgr(GameObject target)
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;
	}

	public bool Attack_Speed_Mgr(GameObject target)
	{
		//대충 확인하는 내용 -이쪽에서 각종 조건 체크

		//결과에 따른 리턴
		return true;
	}
	#endregion

}
