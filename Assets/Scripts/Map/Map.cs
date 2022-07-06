using UnityEngine;
public class Map : MonoBehaviour
{
    //declare grid types
    public static int GRIDTYPE_HEXA_MAP = 2;

    public static int hexMapSizeX = 6;
    public static int hexMapSizeZ = 5;

    public Plane m_Plane;

    //시작 위치
    public Transform mapStartPosition;

    //육각형 오브젝트
    public GameObject hexaIndicator;

    //일반적 색상
    public Color indicatorDefaultColor;
    
    //충돌 이후의 색상
    public Color indicatorActiveColor;

    void Start()
    {
        CreateGridPosition();
        CreateIndicators();

        m_Plane = new Plane(Vector3.up, Vector3.zero);
    }

    //맵 그리드 포지션 위치를 벡터의 배열에 저장
    [HideInInspector]
    public Vector3[,] mapGridPositions;

    //맵의 그리드 포지션 설정
    private void CreateGridPosition()
    {
        //맵 생성하는 그리드 배열 선언
        mapGridPositions = new Vector3[hexMapSizeX, hexMapSizeZ];

        //맵 위치 생성
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                //육각형 6x5형식의 맵이기에 위와 아래가 맞물리도록 설정을 위한 rowOffset 변수 선언
                int rowOffset = z % 2;

                //x, z축 계산
                float offsetX = x * -5.7f + rowOffset * 3f;
                float offsetZ = z * -4.7f;

                //각 칸의 포지션을 계산 후에 저장
                Vector3 position = GetMapHitPoint(mapStartPosition.position + new Vector3(offsetX, 0, offsetZ));

                //계산된 포지션을 그리드 배열에 맞게 저장
                mapGridPositions[x, z] = position;
            }
          
        }

    }

    //육각형의 배열을 선언
    [HideInInspector]
    public GameObject[] ownIndicatorArray;
    [HideInInspector]
    public GameObject[,] mapIndicatorArray;

    //생성된 육각형들의 부모가 될 빈 게임 오브젝트
    private GameObject indicatorContainer;

    //포지션에 맞게 육각형 생성
    private void CreateIndicators()
    {
        //create a container for indicators
        indicatorContainer = new GameObject();
        indicatorContainer.name = "IndicatorContainer";

        //create a container for triggers
        GameObject triggerContainer = new GameObject();
        triggerContainer.name = "TriggerContainer";

        mapIndicatorArray = new GameObject[hexMapSizeX, hexMapSizeZ];
     
        //iterate map grid position
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                //create indicator gameobject
                GameObject indicatorGO = Instantiate(hexaIndicator);

                //set indicator gameobject position
                indicatorGO.transform.position = mapGridPositions[x,z];

                //set indicator parent
                indicatorGO.transform.parent = indicatorContainer.transform;

                //store indicator gameobject in array
                mapIndicatorArray[x, z] = indicatorGO;

            }
        }

    }

    public Vector3 GetMapHitPoint(Vector3 p) //마우스 레이캐스트 이벤트
    {
        Vector3 newPos = p;

        RaycastHit hit;

        if (Physics.Raycast(newPos + new Vector3(0, 10, 0), Vector3.down, out hit, 15))
        {
            newPos = hit.point;
        }

        return newPos;
    }

    public void resetIndicators()
    {
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                mapIndicatorArray[x, z].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
            }
        }

        
        for (int x = 0; x < 9; x++)
        {
           ownIndicatorArray[x].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
        }
        
    }
}
