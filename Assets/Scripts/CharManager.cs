using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yggdrasil
{
    public class CharManager
    {
        #region Char스탯 정보 가져오기
        DataTableManager M_DataTable => DataTableManager.Instance;
        private CharStat_TableExcel TableExcel;

        public CharStat_TableExcel GetStatData(int _index)
        {
            CharStat_TableExcel stat = new CharStat_TableExcel();
            var list = DataTableManager.Instance.GetDataTable<CharStat_TableExcelLoader>().DataList;

            CharStat_TableExcelLoader m_BossStat = M_DataTable.GetDataTable<CharStat_TableExcelLoader>();
            CharStat_TableExcel statData = m_BossStat.DataList.Where(item => item.CharIndex == _index).SingleOrDefault();
            return statData;
        }
        #endregion

        public CharManager(int _index)
        {
            TableExcel = GetStatData(_index);
        }
    }
}