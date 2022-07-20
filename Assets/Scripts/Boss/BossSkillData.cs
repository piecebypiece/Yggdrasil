using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Yggdrasil.BossSkillInfo
{

	public enum BossSkillType { WIDE=1,TARGET,LINE,DIFFUSION, SUMMONS  }

	public class BossSkillData
	{

        public BossSkillType SkillType;
        public float lifeTime;
        public int SkillAdded;

        public string Name_KR { get; set; }  //한국이름 
		public string Name_EN { get; set; }     //영문이름
		public int BossIndex { get; set; }    //스킬 인덱스 값 아직까진 어떻게 사용할지 애매함.
		public int TargetType { get; set; }   // 적군이냐 아군이냐 (아군일경우 거의 확정적으로 버프스킬), 모두( 범위내에 있는 모든 오브젝트) 
		public float Power { get; set; }  //파워
		public float CoolTime { get; set; }  //
		public float SkillDistance { get; set; }
		public float SkillRange { get; set; }
		//public BossSkillType SkillType { get; set; }       // 스킬의 형태 (광역이냐 ,대상이냐 등등)   => 임의로 float에서 BossSkillType형으로 바꿈.

		//public float direction { get; set; }   //방향 현재는 사용하지않음 (6방향존재)

		public bool move { get; set; }  //이동

		//public float lifeTime { get; set; }  //생존

		public float DoT { get; set; }   // 스킬 주기
		//public int SkillAdded { get; set; }      // 추가스킬
		public int BuffADDED { get; set; }        //추가버프 -> 버프 테이블을 따로 만듬.

		public int SkillAnimation { get; set; }     //스킬 애니메이션

		public int AreaPrefab { get; set; }    //판정 프리팹.

		public int LunchPrefab { get; set; }  //발사 프리팹.

		public int FirePrefab { get; set; }  //투사체 프리팹

		public int DamPrefab { get; set; }  //피격 프리팹.

	}
}

