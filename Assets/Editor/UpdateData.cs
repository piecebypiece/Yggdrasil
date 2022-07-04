using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class UpdateData : AssetPostprocessor
{


	void OnPreprocessAsset()
	{
		// 데이터 파싱해서 업데이트 하는 부분.

		// dll 로 만든후 00.Data에 엑셀폴더를 만들고 그 폴더안에 엑셀데이터가 집어넣어지면(파일구분 필수 = EX )엑셀파일이 맞는지) dll를 실행해 cs폴더와 txt폴더안에
		// 만들어 지도록 만약 해당 폴더가 없다면 현재 디렉토리에 만들어지도록.

		// 이미 있던 스크립터블 오브젝트가 있을경우 업데이트 할 수 있도록...



		if (assetImporter.assetPath.Contains("Assets/00.Data"))
		{
			string name = assetImporter.assetBundleName;

			Debug.Log(name + "asd");
			Debug.Log("해당데이터는 Data폴더안에 생성됨");
		}
		else
		{
			Debug.Log("해당 데이터는 Data폴더밖에 생성됨");
		}


		//내용이 업데이트가 되면 dll(ExcelRoaderTest)을 사용하여 scriptable object로 적용될 수 있게끔..(이미 파일이 있으면 내용수정 없으면 생성되는식으로)

	}




}
