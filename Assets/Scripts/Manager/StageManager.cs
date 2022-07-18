using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageManager : MonoBehaviour
{

	public Dictionary<int, List<Map_TableExcel>> map_info;

    private List<Map_TableExcel> list;

    private int mapMaxCount = 30;


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

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
