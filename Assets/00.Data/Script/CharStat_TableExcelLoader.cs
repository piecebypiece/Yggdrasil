using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharStat_TableExcel
{
	public int No;
	public string Name_KR;
	public string Name_EN;
	public int CharIndex;
	public int PhyDef;
	public int MagDef;
	public int Atk;
	public int EQuipCost;
	public int MoveSpeed;
	public int AtkSpeed;
	public int HP;
	public int SumSpeed;
	public int SumPer;
	public int SumRange;
	public int AtkRange;
	public int Sight;
}



/*====================================*/

[CreateAssetMenu(fileName="CharStat_TableLoader", menuName= "Scriptable Object/CharStat_TableLoader")]
public class CharStat_TableExcelLoader :ScriptableObject
{
	[SerializeField] string filepath;
	public List<CharStat_TableExcel> DataList;

	private CharStat_TableExcel Read(string line)
	{
		line = line.TrimStart('\n');

		CharStat_TableExcel data = new CharStat_TableExcel();
		int idx =0;
		string[] strs= line.Split('`');

		data.No = int.Parse(strs[idx++]);
		data.Name_KR = strs[idx++];
		data.Name_EN = strs[idx++];
		data.CharIndex = int.Parse(strs[idx++]);
		data.PhyDef = int.Parse(strs[idx++]);
		data.MagDef = int.Parse(strs[idx++]);
		data.Atk = int.Parse(strs[idx++]);
		data.EQuipCost = int.Parse(strs[idx++]);
		data.MoveSpeed = int.Parse(strs[idx++]);
		data.AtkSpeed = int.Parse(strs[idx++]);
		data.HP = int.Parse(strs[idx++]);
		data.SumSpeed = int.Parse(strs[idx++]);
		data.SumPer = int.Parse(strs[idx++]);
		data.SumRange = int.Parse(strs[idx++]);
		data.AtkRange = int.Parse(strs[idx++]);
		data.Sight = int.Parse(strs[idx++]);

		return data;
	}
	[ContextMenu("파일 읽기")]
	public void ReadAllFile()
	{
		DataList=new List<CharStat_TableExcel>();

		string currentpath = System.IO.Directory.GetCurrentDirectory();
		string allText = System.IO.File.ReadAllText(System.IO.Path.Combine(currentpath,filepath));
		string[] strs = allText.Split(';');

		foreach (var item in strs)
		{
			if(item.Length<2)
				continue;
			CharStat_TableExcel data = Read(item);
			DataList.Add(data);
		}
	}
}
