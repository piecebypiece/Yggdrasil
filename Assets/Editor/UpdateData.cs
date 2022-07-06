using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;


public class UpdateData : AssetPostprocessor
{


	void OnPreprocessAsset()
	{
		// 데이터 파싱해서 업데이트 하는 부분.

		// dll 로 만든후 00.Data에 엑셀폴더를 만들고 그 폴더안에 엑셀데이터가 집어넣어지면(파일구분 필수 = EX )엑셀파일이 맞는지) dll를 실행해 cs폴더와 txt폴더안에
		// 만들어 지도록 만약 해당 폴더가 없다면 현재 디렉토리에 만들어지도록.

		// 이미 있던 스크립터블 오브젝트가 있을경우 업데이트 할 수 있도록...


		DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(assetImporter.assetPath));
		DirectoryInfo[] diarr = di.GetDirectories();


		foreach (DirectoryInfo element in diarr)
		{
			string f_name = Path.GetFileName(element.Name);

			if (f_name == "txt")
			{
				Debug.Log("txt폴더 있음");
			}
			else if(f_name == "script")
			{
				Debug.Log("script폴더 있음");
			}
		}



		if (assetImporter.assetPath.Contains("Assets/00.Data/Excel"))
		{

			string a = Application.dataPath.Replace("/", @"\");
			string b = assetImporter.assetPath.Replace("Assets", "");  //

			string f_path = a + b.Replace("/", @"\");            // path경로 설정

			//int idx = f_path.LastIndexOf("\\");                  // path에서 파일 이름 위치 찾기.
			//string f_name = f_path.Substring(idx + 1);           // path에서 파일 이름 추출.

			string f_name = Path.GetFileName(f_path);            // Path클래스를 사용해서 이름 추출

			string f_extension = Path.GetExtension(f_path);      // Path클래스를 사용해서 확장자 추출

			//dll 집어넣고 path넣어서 사용.
			

			//file의 확장자가 Excel파일인지 확인.
			// if(f_extension.Contains(".xlsx"))   { // Tooo  }

			Debug.Log(f_path);
			Debug.Log(f_name);
			Debug.Log(f_extension);
		}
		else
			return;
	
		//내용이 업데이트가 되면 dll(ExcelRoaderTest)을 사용하여 scriptable object로 적용될 수 있게끔..(이미 파일이 있으면 내용수정 없으면 생성되는식으로)

	}




}
