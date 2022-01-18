using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    private int index; // 각 보스가 가지고 있는 인덱스번호

    private BossStat_TableExcel TableExcel;

    #region Boss스탯 정보 가져오기
    // BossIndex에 해당되는 보스 TableExcel을 가져온다.
    public BossStat_TableExcel GetStatData(int _index)
    {
        BossStat_TableExcel stat = new BossStat_TableExcel();
        var list = DataTableManager.Instance.GetDataTable<BossStat_TableExcelLoader>().DataList;

        foreach (var it in list)
        {
            if (_index == it.BossIndex)
            {
                stat = it;
                return stat;
            }
        }
        return stat;
    }
    #endregion

    void Start()
    {
        // 해당보스 TableExcel 가져오기
        TableExcel = GetStatData(index);

        Debug.Log(TableExcel.Name_KR);
    }
}
