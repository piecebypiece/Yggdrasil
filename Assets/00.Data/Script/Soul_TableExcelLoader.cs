using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Soul_TableExcel
{
	public int No;
	public string Name_KR;
	public string Name_EN;
	public int Code;
	public int Soul_Class;
	public float Atk;
	public float Atk_Speed;
	public float Move_Speed;
	public float HP;
	public float Def;
	public float Crit_rate;
	public float Crit_Dmg;
	public float Duration;
	public float CoolTime;
	public int Skill1Code;
	public int Skill2Code;
}



/*====================================*/

[CreateAssetMenu(fileName="Soul_TableLoader", menuName= "Scriptable Object/Soul_TableLoader")]
public class Soul_TableExcelLoader :ScriptableObject
{
	[SerializeField] string filepath;
	public List<Soul_TableExcel> DataList;

	private Soul_TableExcel Read(string line)
	{
		line = line.TrimStart('\n');

		Soul_TableExcel data = new Soul_TableExcel();
		int idx =0;
		string[] strs= line.Split('`');

		data.No = int.Parse(strs[idx++]);
		data.Name_KR = strs[idx++];
		data.Name_EN = strs[idx++];
		data.Code = int.Parse(strs[idx++]);
		data.Soul_Class = int.Parse(strs[idx++]);
		data.Atk = float.Parse(strs[idx++]);
		data.Atk_Speed = float.Parse(strs[idx++]);
		data.Move_Speed = float.Parse(strs[idx++]);
		data.HP = float.Parse(strs[idx++]);
		data.Def = float.Parse(strs[idx++]);
		data.Crit_rate = float.Parse(strs[idx++]);
		data.Crit_Dmg = float.Parse(strs[idx++]);
		data.Duration = float.Parse(strs[idx++]);
		data.CoolTime = float.Parse(strs[idx++]);
		data.Skill1Code = int.Parse(strs[idx++]);
		data.Skill2Code = int.Parse(strs[idx++]);

		return data;
	}
	[ContextMenu("?åÏùº ?ΩÍ∏∞")]
	public void ReadAllFile()
	{
		DataList=new List<Soul_TableExcel>();

		string currentpath = System.IO.Directory.GetCurrentDirectory();
		string allText = System.IO.File.ReadAllText(System.IO.Path.Combine(currentpath,filepath));
		string[] strs = allText.Split(';');

		foreach (var item in strs)
		{
			if(item.Length<2)
				continue;
			Soul_TableExcel data = Read(item);
			DataList.Add(data);
		}
	}
}
