using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    public static MainManager instance;


    private UIManager m_UIManager;
    private StageManager m_StageManager;
    private AnimationManager m_Animanager;


    public int xp, zp;
    public int hexMapSizeX = 6;
    public int hexMapSizeZ = 5;

    //private ResourceManager m_Resource;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            m_UIManager = new UIManager();
            m_StageManager = new StageManager();
            m_Animanager = new AnimationManager();
            //m_Resource = new ResourceManager();

            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }

    }


    public static MainManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }

    }

    //public static ResourceManager Resource { get { return Instance.m_Resource; } }



    public AnimationManager GetAnimanager()
    {
        return m_Animanager;
    }


    public UIManager GetUIManager()
    {
        return m_UIManager;
    }

    public StageManager GetStageManager()
    {
        return m_StageManager;
    }


    public void GameStart()//CharacterData character, string mapid)
    {
        // 플레이어 정보 ( 캐릭터 종류, 레벨, 장착)
        // 디비 맵아이디로 맵 데이터 가져오기

        // 프로그래스 바 UI 있는 씬
        // 로딩 씬 로드

        // 비동기로드 
        // 캐릭터
        // 스킬, 이펙트, 사운드, 3D 

        // 맵 타일, 배경
        // 배경음 BGM

        // 전투에 쓸 UI

        // 최소 램 권장량


        // 리소스 로드 메모리
        // 인스턴싱 GameObject
        // instance

        // 리소스 매니져
        // 캐릭터 일반화

        // 보스, 플레이어
        // 
        // (3D 모델링, 애니메이팅) 채은씨
        // 이동처리

        // 
        // 스킬 사용    

        // 스텟고정 (초기 스텟)
        // 변경될 스텟
        // 대미지 받아들이기  Damage(상대 스텟, 스킬 (ATK, 2))


        // 스킬 처리
        // 투사체, 버프( 소환), 특정 타일 공격판정
        // 사용, 애니메이션(스킬), 시간값
        // 시전 시전되는 시간동안 애니메이션 재생 특정 시점에 버프걸기, 대미지 판정, 투사체 만들기
        // SkillUpdate(float timeDelta)

        // 3d fbx 이 데이터를 받아서 씬에 보여주고 등록된 애니메이션을 재생시킬 수 있는 클래스

        // 리소스 매니저
        
        // Addressables.AsyncLoad

        // 캐싱   (캐싱)
        // 
        // 로드           (메모리) 캐싱되어있으면 캐싱된걸 반환
        // 비동기 로드    // 완료시 할 처리 Delegate 
        // 동기 로드
        // 언로드
        // 제네릭 처리
    }

    //public class CharacterController
    //{
    //    public CharacterVisualController vCon;
    //    public Stats stat;  // 체력 최대체력
    //    public Stats runTimeStats; // 현재체력, 현재마나
    //    public SkillController skillCon; // < 스킬리스트
        

    //    public void UseSkill()
    //    {
         
    //    }

    //    public void Move(Vector2 v)
    //    {

    //    }
    //}

    // 3d 모델링을 보여주고 애니메이션 제어
    public class CharacterVisualController
    {
        public GameObject fbx;
        

        public void Init(GameObject fbx)
        {

        }

        public void PlayAnimation(string animationName) // 속도, //playtime)
        {

        }
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
