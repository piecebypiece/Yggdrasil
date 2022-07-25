using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageManager : MonoBehaviour
{

    //추가 전체 맵 정보
    public struct MapInfo
    {
        public Map_TableExcel MapData;     //맵에 엑셀 데이터
        public Vector3 MapPos;             //맵에 위치
        public int row;
        public int column;
    }

    //private GameObject BossObj;

    public Dictionary<int, List<Map_TableExcel>> map_info;

    private List<Map_TableExcel> list;

    private int mapMaxCount = 30;


    //현재 플레이어가 있는 타일의 배열정보.
    //보스가 스킬을사용할때 Map에 저장되어있는 MapInfo[row,coulmn] 으로 접근해서 해당 타일의 Pos 를 안아낸후 그 위치 지점으로 부터 OverlapSphere로 범위 판정.
    public int m_PlayerRow;
    public int m_PlayerCoulmn;

    public int m_BossRow;
    public int m_BossColumn;

    public MapInfo[,] m_MapInfo;

    public Map map;

    private GameObject BossObj;


    public void SetPlayerRowAndCoulmn(int row, int coulmn)
    {
        m_PlayerRow = row;
        m_PlayerCoulmn = coulmn;
    }

    public StageManager()
    {

        if (null == map_info)
        {
            map_info = new Dictionary<int, List<Map_TableExcel>>();

            for (int i = 0; i < 3; i++)
            {
                int idx = i * 10;
                list = DataTableManager.Instance.GetDataTable<Map_TableExcelLoader>().DataList.OrderBy(x => x.No).Skip(idx).Take(mapMaxCount).ToList();
                map_info.Add(i + 1, list);
            }

            m_MapInfo = new MapInfo[5, 6];

           
            map = GameObject.Find("MapManager").GetComponent<Map>();

            BossObj = GameObject.Find("Boss");

            int x = 0;
            int z = 0;
            //각 맵 정보에 데이터 넣어주기.
            foreach (var item in DataTableManager.Instance.GetDataTable<Map_TableExcelLoader>().DataList)
            {
                MainManager.Instance.GetStageManager().m_MapInfo[z, x].MapData = item;
                MainManager.Instance.GetStageManager().m_MapInfo[z, x].row = z;       //가로
                MainManager.Instance.GetStageManager().m_MapInfo[z, x].column = x;    //세로

                //if (MainManager.Instance.GetStageManager().m_MapInfo[z, x].MapData.BossSummon != 0)
                //{
                //	BossObj.transform.position = mapGridPositions[z, x] + new Vector3(0, 2.3f, 0);
                //	MainManager.Instance.GetStageManager().m_BossRow = z;
                //	MainManager.Instance.GetStageManager().m_BossColumn = x;
                //}


                if (MainManager.Instance.xp == x && MainManager.Instance.zp == z)
                {
                    BossObj.transform.position = map.mapGridPositions[z, x] + new Vector3(0, 2.3f, 0);
                    MainManager.Instance.GetStageManager().m_BossRow = z;
                    MainManager.Instance.GetStageManager().m_BossColumn = x;
                }

                x++;
                if (x == MainManager.Instance.hexMapSizeX)
                {
                    z++;
                    x = 0;
                }
            }


            

            //디버그용
            //foreach(var item in map_info[1])
            //{
            //    Debug.Log(item.No);
            //}
        }

    }




    // Start is called before the first frame update
    void Start()
    {



        //디버그 확인용
        //Debug.Log(m_MapInfo[1, 1].MapData.No);
        //Debug.Log(m_MapInfo[1, 1].MapData.BossSummon);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
