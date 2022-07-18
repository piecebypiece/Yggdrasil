using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

	public Dictionary<int, Map_TableExcelLoader> m_info;




	public StageManager()
	{

		//if(null == m_info)
		//{
		//	m_info = new Dictionary<int, BossStat_TableExcelLoader>();
		//}

	}

    //스테이지 매니저 수정하기.

	//public void SetMapInfo(BossStat_TableExcelLoader m1, BossStat_TableExcelLoader m2, BossStat_TableExcelLoader m3)
	//{
	//	m_info.Add(1, m1);
	//	m_info.Add(2, m2);
	//	m_info.Add(3, m3);
	//}


	
    // Start is called before the first frame update
    void Start()
    {

		//이런 식으로 접근할 수 있음.
		//for (int i = 0; i < MainManager.Instance.GetStageManager().m_info.Count; i++)
		//{
		//	foreach (var element in MainManager.Instance.GetStageManager().m_info[i + 1].DataList)
		//	{
		//		Debug.Log(element.Name_KR);
		//	}
		//}

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
