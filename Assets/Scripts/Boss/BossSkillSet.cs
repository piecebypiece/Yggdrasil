using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yggdrasil;

public class BossSkillSet : MonoBehaviour
{

	public Yggdrasil.BossSkillType m_bossSkillType;

	public Yggdrasil.BossSkillData[] m_bossSkillData;

    // Start is called before the first frame update
    void Start()
    {
		m_bossSkillData = new Yggdrasil.BossSkillData[3];  //데이터 생성.


		m_bossSkillData[0].SkillType = BossSkillType.WIDE;
		m_bossSkillData[0].SkillAdded = 1;

		m_bossSkillData[1].SkillType = BossSkillType.WIDE;
		m_bossSkillData[1].SkillAdded = 2;

		m_bossSkillData[2].SkillType = BossSkillType.TARGET;
		m_bossSkillData[2].SkillAdded = 0;



	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
