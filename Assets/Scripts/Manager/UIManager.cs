using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{

	

	public void ShowUI(GameObject UI)
	{
		UI.SetActive(true);
	}

	public void HideUI(GameObject UI)
	{
		UI.SetActive(false);
	}

}
