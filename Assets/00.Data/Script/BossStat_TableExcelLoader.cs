using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStat_TableExcel
{
	public int No;
	public string Name_KR;
	public string Name_EN;
	public int BossIndex;
	public float Atk;
	public float HP;
	public float Def;
	public float SRange;
	public float CRange;
	public float FRange;
	public float MaxStamina;
	public float Speed;
	public float MoveStUsed;
	public int Skill1;
	public int Skill2;
	public int Skill3;
	public int Skill4;
}



/*====================================*/

[CreateAssetMenu(fileName="BossStat_TableLoader", menuName= "Scriptable Object/BossStat_TableLoader")]
public class BossStat_TableExcelLoader :ScriptableObject
{
	[SerializeField] string filepath =@"Assets\00.Data\Txt\BossStat_Table.txt";
	public List<BossStat_TableExcel> DataList;

	private BossStat_TableExcel Read(string line)
	{
		line = line.TrimStart('\n');

		BossStat_TableExcel data = new BossStat_TableExcel();
		int idx =0;
		string[] strs= line.Split('`');

		data.No = int.Parse(strs[idx++]);
		data.Name_KR = strs[idx++];
		data.Name_EN = strs[idx++];
		data.BossIndex = int.Parse(strs[idx++]);
		data.Atk = float.Parse(strs[idx++]);
		data.HP = float.Parse(strs[idx++]);
		data.Def = float.Parse(strs[idx++]);
		data.SRange = float.Parse(strs[idx++]);
		data.CRange = float.Parse(strs[idx++]);
		data.FRange = float.Parse(strs[idx++]);
		data.MaxStamina = float.Parse(strs[idx++]);
		data.Speed = float.Parse(strs[idx++]);
		data.MoveStUsed = float.Parse(strs[idx++]);
		data.Skill1 = int.Parse(strs[idx++]);
		data.Skill2 = int.Parse(strs[idx++]);
		data.Skill3 = int.Parse(strs[idx++]);
		data.Skill4 = int.Parse(strs[idx++]);

		return data;
	}
	[ContextMenu("파일 읽기")]
	public void ReadAllFile()
	{
		DataList=new List<BossStat_TableExcel>();

		string currentpath = System.IO.Directory.GetCurrentDirectory();
		string allText = System.IO.File.ReadAllText(System.IO.Path.Combine(currentpath,filepath));
		string[] strs = allText.Split(';');

		foreach (var item in strs)
		{
			if(item.Length<2)
				continue;
			BossStat_TableExcel data = Read(item);
			DataList.Add(data);
		}
	}
}
