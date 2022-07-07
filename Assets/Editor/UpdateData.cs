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
	
		DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(assetImporter.assetPath));
		DirectoryInfo[] diarr = di.GetDirectories();


		foreach (DirectoryInfo element in diarr)
		{
			string f_name = Path.GetFileName(element.Name);

			if (f_name == "txt")
			{
				Debug.Log("txt 폴더 있음");
			}
			else if(f_name == "script")
			{
				Debug.Log("script 폴더 있음");
			}
		}

        

        if (assetImporter.assetPath.Contains("Assets/00.Data/Excel"))
		{


			string a = UnityEngine.Application.dataPath.Replace("/", @"\");
			string b = assetImporter.assetPath.Replace("Assets", "");  //

            string exe_path = a + @"\Editor\TestExcelDataReader.exe";

           
           

			string f_path = a + b.Replace("/", @"\");            // 패스 경로

			//int idx = f_path.LastIndexOf("\\");                  // path?먯꽌 ?뚯씪 ?대쫫 ?꾩튂 李얘린.
			//string f_name = f_path.Substring(idx + 1);           // path?먯꽌 ?뚯씪 ?대쫫 異붿텧.

			string f_name = Path.GetFileName(f_path);            // 패스경로에서 이름 추출

			string f_extension = Path.GetExtension(f_path);      // 패스경로에서 확장자 추출

			
			if(f_extension.Contains(".xlsx"))
			{
				Debug.Log("엑셀파일 들어옴.");

                //ExcelDataParsing3.ExcelReader ER = new ExcelReader();
                //ER._Initialize(f_path);
                //ER.ReadSheet();
                //ER._Finalize();
                //ER._MakeExcelDataToCSFileAndTXTFile(f_path);

                System.Diagnostics.Process.Start(exe_path, f_path);

            }
			else
			{
				Debug.Log("엑셀파일 아님");
			}

            //Debug.Log(exe_path);
            //Debug.Log(f_path);
			//Debug.Log(f_name);
			//Debug.Log(f_extension);
		}
		else
			return;
	}




}
