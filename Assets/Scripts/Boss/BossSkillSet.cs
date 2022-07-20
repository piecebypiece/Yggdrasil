using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yggdrasil.BossSkillInfo;

public class BossSkillSet : MonoBehaviour
{

    public BossSkillData[] m_bossSkillData;

    public BossSkillData b_SkillData1 = new BossSkillData();
    public BossSkillData b_SkillData2 = new BossSkillData();
    public BossSkillData b_SkillData3 = new BossSkillData();


    public bool m_endSkill = false;



    // Start is called before the first frame update
    void Start()
    {
        b_SkillData1.SkillType = BossSkillType.WIDE;
        b_SkillData1.SkillAdded = 1;
        b_SkillData1.lifeTime = 5f;

        b_SkillData2.SkillType = BossSkillType.LINE;
        b_SkillData2.SkillAdded = 2;
        b_SkillData2.lifeTime = 3f;

        b_SkillData3.SkillType = BossSkillType.TARGET;
        b_SkillData3.SkillAdded = 0;
        b_SkillData3.lifeTime = 5f;

        m_bossSkillData = new BossSkillData[3];

        m_bossSkillData[0] = b_SkillData1;
        m_bossSkillData[1] = b_SkillData2;
        m_bossSkillData[2] = b_SkillData3;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
