using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class UpdateData : AssetPostprocessor
{

	void OnPreprocessAsset()
	{
		//데이터 파싱해서 업데이트 하는 부분.

		//dll로 빼버린후  기획자분들이 데이터를 유니티 안에 집어넣었을때 업데이트가 가능하게끔(업데이트할 파일을 구분해야함 예를들어 엑셀파일만 업데이트 한다던가.)


		if(assetImporter.assetPath.Contains("Assets/00.Data"))
		{
			Debug.Log("해당 데이터는 Data폴더안에 생성됨");
		}
		else
		{
			Debug.Log("해당 데이터는 Data폴더밖에 생성됨");
		}


		//내용이 업데이트가 되면 dll(ExcelRoaderTest)을 사용하여 scriptable object로 적용될 수 있게끔..(이미 파일이 있으면 내용수정 없으면 생성되는식으로)

	}



}
