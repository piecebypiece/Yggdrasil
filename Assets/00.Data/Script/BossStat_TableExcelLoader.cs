using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossStat_TableExcel
{
	public int No;
	public string Name_KR;
	public string Name_EN;
	public int BossIndex;
	public int Atk;
	public int HP;
	public int Def;
	public int MaxTM;
	public int Speed;
	public int MoveRange;
	public int MoveTMUsed;
	public int Skill1;
	public int Skill2;
	public int Skill3;
}



/*====================================*/

[CreateAssetMenu(fileName="BossStat_TableLoader", menuName= "Scriptable Object/BossStat_TableLoader")]
public class BossStat_TableExcelLoader :ScriptableObject
{
	[SerializeField] string filepath;
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
		data.Atk = int.Parse(strs[idx++]);
		data.HP = int.Parse(strs[idx++]);
		data.Def = int.Parse(strs[idx++]);
		data.MaxTM = int.Parse(strs[idx++]);
		data.Speed = int.Parse(strs[idx++]);
		data.MoveRange = int.Parse(strs[idx++]);
		data.MoveTMUsed = int.Parse(strs[idx++]);
		data.Skill1 = int.Parse(strs[idx++]);
		data.Skill2 = int.Parse(strs[idx++]);
		data.Skill3 = int.Parse(strs[idx++]);

		return data;
	}
	[ContextMenu("?åÏùº ?ΩÍ∏∞")]
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
