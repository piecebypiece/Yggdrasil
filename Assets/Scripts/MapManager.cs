using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 해야할 목록
 * 1. 메모리풀 만들고 메모리풀에서 타일 관리하도록 하기.
 * (
 * + 추가적으로 타일에 따라 타입을 지정할 것이라고 기획님이 말씀 하셨으니까 타입별로 메모리풀을 만들어서
 *    각 타일에 대한 정보를 저장 할 것.
 * )
 * 2.타일 클릭에 대한 명령 짜기( ex-정령소환이 활성화 된 경우 타일 클릭 가능, 클릭된 타일 정보를 요청한 곳에서 사용하도록) 
 * 3.Tile 스크립트 생성하여 타일마다 가지고 있어야 할 정보를 셋팅해두기.(위치,타일 타입 등 자세한건 기획서 나오면)
 */
public class MapManager : MonoBehaviour
{
    [SerializeField]
    Vector3 m_StartPos;
    [SerializeField]
    int m_mapWidth;
    [SerializeField]
    int m_mapHeight;
    [SerializeField]
    bool flag;
    #region TileInfo
    public GameObject HexTilePrefab;
    float m_CToSVertex_Length; //중점과 도형 바깥쪽 꼭지점까지의 길이
    float m_CToSLine_Length;  //중점과 바깥 선까지의 길이
    float m_SideLine_Length; //바깥 선 길이
    #endregion
    #region GroundInfo
    public GameObject GroundPrefab;
    //타일 1개 추가당 바닥 크기 늘어날 수치
    float GroundNomalWidth = 48.3f;
    float GroundNomalHeight = 44f;
    GameObject Ground;
    #endregion
    void Start()
    {
        InitTileInfo();
        CreateHexTileMap();
    }

    void InitTileInfo()
    {
        Ground = null;
        float CosValue = Mathf.Cos(36 * Mathf.Deg2Rad);
        float SinValue = Mathf.Sin(36 * Mathf.Deg2Rad);

        m_CToSVertex_Length = 21.4f;
        m_CToSLine_Length = m_CToSVertex_Length * CosValue;
        m_SideLine_Length = m_CToSVertex_Length * SinValue * 2;

        if (!flag)
        {
            m_mapWidth = 6;
            m_mapHeight = 5;
        }
    }
    void CreateHexTileMap()
    {
        Vector3 m_StartPoint = m_StartPos;
        Vector3 m_CurrentPos = m_StartPos;
        Ground = Instantiate<GameObject>(GroundPrefab);
        Ground.transform.localScale = new Vector3(GroundNomalWidth * m_mapWidth, 0, GroundNomalHeight * m_mapHeight);
        Vector3 result = new Vector3(m_StartPos.x + m_CToSVertex_Length * (m_mapWidth - 0.5f), 0, m_StartPos.z + m_CToSLine_Length * (m_mapHeight - 1));
        Ground.transform.position = result;
        for (int y = 0; y < m_mapHeight; ++y)
        {
            if (y % 2 != 0)
            {
                m_CurrentPos.x = m_StartPos.x + m_CToSVertex_Length;
            }
            else m_CurrentPos.x = m_StartPos.x;
            for (int x = 0; x < m_mapWidth; ++x)
            {
                GameObject obj = Instantiate<GameObject>(HexTilePrefab);
                obj.transform.position = m_CurrentPos;
                m_CurrentPos.x += m_CToSVertex_Length * 2;
            }
            m_CurrentPos.z += m_CToSLine_Length * 2;
        }
    }
}
