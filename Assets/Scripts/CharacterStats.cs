using UnityEngine;

namespace Yggdrasil
{
    public class CharacterStats : MonoBehaviour
    {
        public int PhysicDefence { get; set; }
        public int MagicDefence { get; set; }
        public int Attack { get; set; }
        public int Cost { get; set; }
        public float MoveSpeed { get; set; }
        public float AttackSpeed { get; set; }
        public float SummonSpeed { get; set; }
        public float SummonPersistence { get; set; }
        public float InstallRange { get; set; }
        public float AttackRange { get; set; }
        public float Sight { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }

        // ++

        #region Char스탯 정보 가져오기
        public CharStat_TableExcel GetStatData(int _index)
        {
            CharStat_TableExcel stat = new CharStat_TableExcel();
            var list = DataTableManager.Instance.GetDataTable<CharStat_TableExcelLoader>().DataList;

            foreach (var it in list)
            {
                if (_index == it.CharIndex)
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
            CharStat_TableExcel TableExcel = GetStatData(10001);

            // 일단 임시 계산식을 넣어준다.
            PhysicDefence = TableExcel.PhyDef;
            MagicDefence = TableExcel.MagDef;
            Attack = TableExcel.Atk;
            Cost = TableExcel.EQuipCost;
            MoveSpeed = TableExcel.MoveSpeed;
            AttackSpeed = TableExcel.AtkSpeed;
            SummonSpeed = TableExcel.SumSpeed;
            SummonPersistence = TableExcel.SumPer;
            InstallRange = TableExcel.SumRange;
            AttackRange = TableExcel.AtkRange;
            Sight = TableExcel.Sight;
            HP = TableExcel.HP;
            MaxHP = TableExcel.HP;
        }
    }
}