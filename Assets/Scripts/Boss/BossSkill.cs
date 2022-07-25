using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Yggdrasil.BossSkillInfo;

public partial class EMath
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        float Func(float x) => 4 * (-height * x * x + height * x);

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t));
    }
}



public class BossSkill : MonoBehaviour
{

    public enum BossSkillType { WIDE = 1, TARGET, LINE, DIFFUSION, SUMMONS }

    public GameObject target;
    public GameObject[] EnemyPrefebs = new GameObject[2];
    public GameObject Laser_Effect;
    public GameObject Lightning_Effect;
    public GameObject Thunderbolt_Effect;

    private Animator boss_anim;
    private string currentState;

    //Animation States
    const string BOSS_IDLE = "Idle01";
    const string BOSS_SKILL01 = "Skill01";
    const string BOSS_SKILL02 = "Skill02";
    const string BOSS_SKILL03 = "Skill03";
    const string BOSS_SKILL04 = "Skill04";


    public float angleRange = 60f;
    public float distance = 5f;
    public bool isCollision = false;

    private float checkTime = 0f;
    private bool checkSkill = false;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    Vector3 direction;

    float dotValue = 0f;

    //private BossSkillSet m_bossSkillSet;

    private int m_SkillCheck = 0;  //보스의 스킬연계가 얼만큼 진행되는지.
    private bool skillAction = true;
    private int m_BossSkillIndex;


    private int m_PlayerRow;
    private int m_PlayerColumn;

    private int m_BossRow;
    private int m_BossColumn;

    private Map map;

    //private BossStat_TableExcel m_CurrentBossStat;
    private BossSkill_TableExcel m_CurrentBossSkill;

    public void BossSkillAction(int skillIndex)
    {
        //m_BossSkillIndex = skillIndex;

        Debug.Log("스킬진입");

        foreach (var item in DataTableManager.Instance.GetDataTable<BossSkill_TableExcelLoader>().DataList)
        {
            if (item.BossIndex == skillIndex)
            {
                m_CurrentBossSkill = item; //현재 스킬 정보를 찾아낸다.
                break;
            }
        }

        //플레이어의 위치와 보스의 배열 위치를 알아낸다.
        m_PlayerRow = MainManager.Instance.GetStageManager().m_PlayerRow;
        m_PlayerColumn = MainManager.Instance.GetStageManager().m_PlayerCoulmn;
        m_BossRow = MainManager.Instance.GetStageManager().m_BossRow;
        m_BossColumn = MainManager.Instance.GetStageManager().m_BossColumn;



        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {

        Debug.Log("스킬 액션 실행");



        switch ((BossSkillType)m_CurrentBossSkill.SkillType)
        {
            case BossSkillType.WIDE:
                StartCoroutine(SkillWideAction());
                break;
            case BossSkillType.TARGET:
                StartCoroutine(SkillTargetAction());
                break;
            case BossSkillType.LINE:
                StartCoroutine(SkillLineAction());
                break;
            case BossSkillType.DIFFUSION:
                StartCoroutine(SkillDiffusionAction());
                break;
        }

        yield break;

        //if (m_CurrentBossSkill.SkillAdded == 0)  //추가스킬이 없다면 스킬이 종료된다 + 스킬 지속시간만큼이 지나면.
        //	yield break;
        //else
        //	BossSkillAction(m_CurrentBossSkill.SkillAdded);





        //while (true)
        //{
        //	if (skillAction)
        //	{
        //		//MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL01, currentState);
        //		checkSkill = true;

        //		// m_CurrentBossSkill.TargetType 스킬타입 -> 1 아군(쫄몹포함) , 2 적군, 3 모든대상
        //		// Tag나 Layer등으로 구분하는 것이 좋을 것같다.


        //		//m_CurrentBossSkill.SkillDistance -> 해당 스킬의 발동거리(거리안에 해당플레이어가 있을경우 해당스킬발동조건 충족)
        //		//m_CurrentBossSkill.SkillRange -> 스킬의 범위  


        //		// 타켓스킬일 경우 다음에 연계스킬의 시작좌표는 대상을 기준으로 스킬처리
        //		// 그외의 스킬일 경우 다음에 연계스킬은 보스기준의 좌표로로 스킬처리
        //		switch((BossSkillType)m_CurrentBossSkill.SkillType)
        //		{
        //			case BossSkillType.WIDE:
        //				StartCoroutine(SkillWideAction());
        //				break;
        //			case BossSkillType.TARGET:
        //				StartCoroutine(SkillTargetAction());
        //				break;
        //			case BossSkillType.LINE:
        //				StartCoroutine(SkillLineAction());
        //				break;
        //			case BossSkillType.DIFFUSION:
        //				StartCoroutine(SkillDiffusionAction());
        //				break;
        //		}

        //		skillAction = false;

        //		if (m_CurrentBossSkill.SkillAdded == 0)  //추가스킬이 없다면 스킬이 종료된다 + 스킬 지속시간만큼이 지나면.
        //			yield break;
        //		else
        //			BossSkillAction(m_CurrentBossSkill.SkillAdded);
        //	}

        //	yield return null;
        //}
    }



    IEnumerator SkillWideAction()
    {

        Debug.Log("와이드 스킬 실행");
        //m_CurrentBossSkill.
        float range = 0;

        range = m_CurrentBossSkill.SkillRange - 1.0f;

        Debug.Log(m_CurrentBossSkill.SkillRange);  //2
        float xRange = m_CurrentBossSkill.SkillRange + range;

        // 규칙 1
        // 가운데 줄은 해당 거리+ -1 칸만큼   ex) 범위가 2인스킬의 경우 2+(2-1) = 3칸이 보스 중심의 가장 먼 거리가 되고 대칭으로 -1씩줄어들며 3칸...2칸이됨
        // ex) 범위가 3인스킬의 경우 3+(3-1) = 5칸이 보스위치의 가장 먼거리가 되고 대칭으로 -1씩 줄어들며 5칸...4칸...3칸으로 칸수가 된다.
        // 따라서 가운데 범위는 보스가 위치한 좌표를 기준으로 [z,x-(범위-1)]~ [z,x+(범위-1)]한 범위가 된다 ex) [2,2]가 보스위치고 범위가 3이라면 [2,0] ~ [2,4]의 위치로.

        // 규칙 2 
        // 보스라인이 끝나면 나머지 줄은 대칭해서  ex)  ([z-1,x] / [z+1,x]) | ([z-2,x] / [z+2,x])의     페어형식으로 증가한다.

        // 규칙 3
        // 보스의 위치가 배열의 0,2,4번째요소(1,3,5번째 줄) 일 경우 제일 마지막배열에서 row값+-1,  column값은 변경이없다. 
        // 배열의 1,3번째요소(2,4번째 줄) 일 경우 제일 마지막배열에서 row값+-1, column값 +1 값을 해줘야한다.

        if (range > 0)  //range는 -1을한값 범위가 2부터 여기 들어온다. 범위가 1일경우는 해당 타일에 계산하면 된다.
        {

            Debug.Log($"현재의 xLength는{xRange}");
            Debug.Log("인디케이터 빨간색으로 칠해줌");
            Debug.Log($"현재 보스의 좌표는 {m_BossRow}/{m_BossColumn}");


            int saveRow = m_BossRow;
            int saveColumn = 0;

            int checkRow_P;   //보스 기준 아래쪽에 있는 Column값
            int checkRow_M;   //보스 기준 위쪽에 있는 Column값
            int checkColumn;  //현재 색을 바꿀 타일의 Column값

            for (float i = 0; i < m_CurrentBossSkill.SkillRange; i += 1.0f)
            {
                checkRow_P = m_BossRow + (int)i;
                checkRow_M = m_BossRow - (int)i;

                if (checkRow_P % 2 == 0) //024(135라인)
                    saveColumn++;



                for (float j = 0; j < xRange; j += 1.0f)
                {

                    if (i != 0) //보스가 있는 라인의 +-1라인씩 그림.
                    {

                        checkColumn = saveColumn + (int)j;

                        if (checkColumn < 0 || checkColumn > 5)
                            continue;


                        if (checkRow_P < 5)
                        {

                            map.mapIndicatorArray[checkRow_P, checkColumn].GetComponent<MeshRenderer>().material.color = Color.red;

                            //if(checkRow_P-1 % 2 ==0)
                            //	map.mapIndicatorArray[checkRow_P, checkColumn].GetComponent<MeshRenderer>().material.color = Color.red;
                            //else
                            //	map.mapIndicatorArray[checkRow_P, checkColumn+1].GetComponent<MeshRenderer>().material.color = Color.red;
                        }

                        if (checkRow_M >= 0)
                        {
                            map.mapIndicatorArray[checkRow_M, checkColumn].GetComponent<MeshRenderer>().material.color = Color.red;
                            //if (checkRow_M-1 % 2 == 0)
                            //	map.mapIndicatorArray[checkRow_M, checkColumn].GetComponent<MeshRenderer>().material.color = Color.red;
                            //else
                            //	map.mapIndicatorArray[checkRow_M, checkColumn+1].GetComponent<MeshRenderer>().material.color = Color.red;
                        }




                    }
                    else  //보스가 있는 라인을 쭉그림.
                    {

                        checkColumn = m_BossColumn - (int)range + (int)j;

                        if (j == 0)
                            saveColumn = checkColumn;   //보스라인에서 첫번째 타일 색변환위치 저장.

                        if (checkColumn < 0 || checkColumn > 5)
                            continue;

                        map.mapIndicatorArray[m_BossRow, checkColumn].GetComponent<MeshRenderer>().material.color = Color.red;
                    }

                    //범위를 알려주고 
                    //map.mapIndicatorArray[m_BossRow, Mathf.Abs(m_BossColumn - (int)(range + i))].GetComponent<MeshRenderer>().material.color = Color.red;


                    //경고시간(모든스킬 0.5f초로 고정)이  경과하면 빨간색을 다시 원래색깔로 돌린후 그 범위에 이펙트 출현하고 데미지 로직 처리.
                }

                Debug.Log($"{i + 1}번째 SaveColumn={saveColumn}");

                xRange -= 1.0f;
            }



            //for(float i=0; i< xRange; i+=1.0f)
            //{


            //	//스킬의 범위만큼 타일을 붉은색으로 칠해주기.
            //	//map.mapIndicatorArray[m_BossRow, Mathf.Abs(m_BossColumn-(int)(range+i))].GetComponent<MeshRenderer>().material.color = Color.red;
            //	//MainManager.Instance.GetStageManager().map.mapIndicatorArray[m_BossRow, Mathf.Abs(m_BossColumn - (int)(range + i))].GetComponent<MeshRenderer>().material.color = Color.red;
            //}

        }

        yield break;
    }

    IEnumerator SkillTargetAction()
    {
        yield return null;

    }

    IEnumerator SkillLineAction()
    {

        yield return null;
    }

    IEnumerator SkillDiffusionAction()
    {

        yield return null;
    }




    public void Pull()
    {

        Debug.Log("2");
        //boss_anim.SetInteger("IdleToSkill", 4);
        //ChangeAnimationState(BOSS_SKILL04);
        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL04, currentState);

        //yield return new WaitForSeconds(0.01f);
        //float curAnimationTime = anim.GetCurrentAnimatorStateInfo(0).length;
        //yield return new WaitForSeconds(curAnimationTime);


        checkSkill = true;

        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
        direction = target.transform.position - transform.position;

        if (direction.magnitude < distance)
        {
            Debug.Log("범위안에 있다.");
            if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
            {
                Debug.Log("코루틴 시작.");
                StartCoroutine(PullAction(target, transform.position, 0.5f));
            }
        }


    }

    public void Lightning()
    {
        Debug.Log("3");

        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL01, currentState);
        checkSkill = true;
        ////지정범위내의 모든 플레이어를 찾은 후
        Collider[] colls = Physics.OverlapSphere(transform.position, 15f, 1 << 8);  //8번째 레이어 = Player

        //플레이어가 있을경우
        if (colls.Length != 0)
        {
            GameObject lightning = Instantiate(Lightning_Effect);
            int rand = Random.Range(0, colls.Length);
            Vector3 targetPos = colls[rand].gameObject.transform.position;

            DOTween.To(setter: value =>
            {
                if (value == 1)
                {
                    DestroyObject(lightning);
                }
                Debug.Log(value);
                lightning.transform.position = EMath.Parabola(transform.position, targetPos, 10, value);
            }, startValue: 0, endValue: 1, duration: 1).SetEase(Ease.Linear);

            if (lightning.transform.position == targetPos)
                DestroyObject(lightning);

        }
        else
            Debug.Log("주변의 플레이어가 없습니다.");
    }

    public void Thumderbolt()
    {
        Debug.Log("4");
        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL03, currentState);
        checkSkill = true;
        StartCoroutine(ThumderboltAction(5.0f, transform.position.x, transform.position.z));

    }

    public void LaserFire()
    {

        Debug.Log("5");
        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL02, currentState);
        checkSkill = true;
        StartCoroutine(LaserFireAction(target.transform.position, 3f));

    }

    public void SpeedDown()
    {
        Debug.Log("6");
        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL01, currentState);
        checkSkill = true;

        //지정범위내의 모든 플레이어를 찾은 후
        Collider[] colls = Physics.OverlapSphere(transform.position, 5f, 1 << 8);  //8번째 레이어 = Player

        if (colls.Length != 0)
        {
            StartCoroutine(SpeedDownAction(2f, colls));
        }
        else
            Debug.Log("주변의 플레이어가 없습니다.");

    }

    public void MonsterSummon()
    {

        Debug.Log("7");
        MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_SKILL03, currentState);
        checkSkill = true;

        StartCoroutine(MonsterSummonAction(10, transform.position.x, transform.position.z));
    }






    IEnumerator PullAction(GameObject target, Vector3 currentPos, float endtime)
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime;

            if (time >= endtime)
                yield break;


            target.transform.position = Vector3.MoveTowards(target.transform.position, currentPos, 0.05f);

            yield return null;
        }
    }

    IEnumerator ThumderboltAction(float endTime, float bossX, float bossZ)
    {
        float time = 0f;
        float skillTime = 0f;

        GameObject[] bolt = new GameObject[20];
        Vector3[] pos = new Vector3[20];
        int boltnum = 0;


        for (int i = 0; i < bolt.Length; i++)
        {
            bolt[i] = Instantiate(Thunderbolt_Effect);

            float randX = Random.Range(bossX - 10f, bossX + 10f);
            float randZ = Random.Range(bossZ - 10f, bossZ + 10f);

            bolt[i].transform.position = new Vector3(randX, 15f, randZ);
            pos[i] = bolt[i].transform.position;
            pos[i].y = 0f;
            bolt[i].SetActive(false);
        }

        while (true)
        {
            time += Time.deltaTime;
            skillTime += Time.deltaTime;

            if (time >= endTime)
            {

                for (int i = 0; i < bolt.Length; i++)
                {
                    DestroyObject(bolt[i]);
                }

                skillAction = true;
                yield break;
            }

            if (skillTime >= 0.25f && time <= endTime)
            {
                bolt[boltnum].SetActive(true);  //떨어질 번개를 활성화.
                skillTime = 0f;
                boltnum++;
            }

            for (int i = 0; i < bolt.Length; i++)
            {

                if (bolt[i].activeSelf == true)
                {
                    bolt[i].transform.position = Vector3.MoveTowards(bolt[i].transform.position, pos[i], 0.2f);
                }


                if (bolt[i].transform.position.y <= 0f)
                {
                    bolt[i].SetActive(false);
                }

            }

            yield return null;
        }




    }

    IEnumerator LaserFireAction(Vector3 playerPos, float endTime)
    {

        GameObject laser = Instantiate(Laser_Effect);
        laser.transform.position = transform.position;
        laser.transform.LookAt(playerPos);

        float time = 0f;

        while (true)
        {
            time += Time.deltaTime;

            if (time > endTime)
            {
                DestroyObject(laser);
                skillAction = true;
                yield break;
            }

            laser.gameObject.transform.position = Vector3.MoveTowards(laser.gameObject.transform.position, playerPos, 0.3f);

            yield return null;
        }

    }

    IEnumerator SpeedDownAction(float endtime, Collider[] colls)
    {
        float time = 0f;

        while (true)
        {

            time += Time.deltaTime;

            if (time >= endtime)
            {
                //지속시간이 끝나면 속도를 원래대로 해줌.
                for (int i = 0; i < colls.Length; i++)
                {
                    colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 8f;
                }
                yield break;
            }


            for (int i = 0; i < colls.Length; i++)
            {
                //float speed= colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed;
                //speed /= 2;
                //colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = speed;

                colls[i].GetComponent<PlayerManager>().p_Status.MoveSpeed = 2f;
            }

            yield return null;
        }

    }


    IEnumerator MonsterSummonAction(int summonNum, float bossX, float bossZ)
    {

        GameObject[] monsters = new GameObject[summonNum];

        for (int i = 0; i < summonNum; i++)
        {
            float randX = Random.Range(bossX - 5f, bossX + 5f);
            float randZ = Random.Range(bossZ - 5f, bossZ + 5f);

            if (i > 4)
                monsters[i] = Instantiate(EnemyPrefebs[0]);
            else
                monsters[i] = Instantiate(EnemyPrefebs[1]);


            monsters[i].transform.position = new Vector3(randX, 0, randZ);
        }

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_CurrentBossStat = DataTableManager.Instance.GetDataTable<BossStat_TableExcelLoader>().DataList[0];
        boss_anim = transform.GetChild(0).GetComponent<Animator>();

        //map = GameObject.Find("MapManager").GetComponent<Map>();

        map = MainManager.Instance.GetStageManager().map;

        //m_bossSkillSet = transform.GetComponent<BossSkillSet>();
    }


    // Update is called once per frame
    void Update()
    {
        //디버그 확인용.
        dotValue = Mathf.Cos(Mathf.Deg2Rad * (angleRange / 2));
        direction = target.transform.position - transform.position;

        if (direction.magnitude < distance)
        {
            if (Vector3.Dot(direction.normalized, transform.forward) > dotValue)
                isCollision = true;
            else
                isCollision = false;
        }
        else
            isCollision = false;

        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //    SettingSkillAction();

        if (Input.GetKeyDown(KeyCode.Keypad2))
            Pull();

        if (Input.GetKeyDown(KeyCode.Keypad3))
            Lightning();

        if (Input.GetKeyDown(KeyCode.Keypad4))
            Thumderbolt();

        if (Input.GetKeyDown(KeyCode.Keypad5))
            LaserFire();

        if (Input.GetKeyDown(KeyCode.Keypad6))
            SpeedDown();

        if (Input.GetKeyDown(KeyCode.Keypad7))
            MonsterSummon();



        if (checkSkill)
        {
            checkTime += Time.deltaTime;

            if (checkTime > boss_anim.GetCurrentAnimatorStateInfo(0).length)
            {
                checkSkill = false;
                checkTime = 0f;

                //boss_anim.SetInteger("IdleToSkill", 0);

                MainManager.Instance.GetAnimanager().ChangeAnimationState(boss_anim, BOSS_IDLE, currentState);
            }
        }

    }

#if UNITY_EDITOR
    //씬뷰에서 확인용
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = isCollision ? _red : _blue;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, distance);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, distance);
    }
#endif

}
