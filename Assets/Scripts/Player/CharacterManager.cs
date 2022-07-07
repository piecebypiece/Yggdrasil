using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yggdrasil
{
    public class CharacterManager : Singleton_Ver1.Singleton<CharacterManager>
    {
        #region Char스탯 정보 가져오기
        DataTableManager M_DataTable => DataTableManager.Instance;
        private List<TempCharacter> M_CharacterList = new List<TempCharacter>();
        //private CharStat_TableExcel TableExcel;

        public GameObject obj_10001;
        public GameObject obj_10002;

        //public CharStat_TableExcel GetStatData(int _index)
        //{
        //    CharStat_TableExcelLoader m_CharStat = M_DataTable.GetDataTable<CharStat_TableExcelLoader>();
        //    CharStat_TableExcel statData = m_CharStat.DataList.Where(item => item.CharIndex == _index).SingleOrDefault();
        //    return statData;
        //}
        #endregion

        public void AddCharacter(int _index) // 캐릭터 추가하기
        {
            TempCharacter character = new TempCharacter();
            character.index = _index;
            switch (character.index)
            {
                case 10001:
                    //character.obj = obj_10001;
                    character.obj = Instantiate<GameObject>(obj_10001);
                    break;
                case 10002:
                    //character.obj = obj_10002;
                    character.obj = Instantiate<GameObject>(obj_10002);
                    break;
            }
            character.obj.transform.parent = this.transform;
            M_CharacterList.Add(character);
        }
    }
    public class TempCharacter
    {
        public int index { get; set; }
        public GameObject obj;
    }
}