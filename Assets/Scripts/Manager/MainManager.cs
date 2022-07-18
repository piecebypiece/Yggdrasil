using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
	

	public static MainManager instance;


	//[Header("筌??怨쀬뵠??)]
	//public BossStat_TableExcelLoader m_data1;
	//public BossStat_TableExcelLoader m_data2;
	//public BossStat_TableExcelLoader m_data3;



	private UIManager m_UIManager;
	private StageManager m_StageManager;


	private void Awake()
	{

		

		if (null == instance)
		{
			instance = this;

			//筌띲끇??? ???샒?봔??
			m_UIManager = new UIManager();
			m_StageManager = new StageManager();
			//m_StageManager.SetMapInfo(m_data1, m_data1, m_data1);


			DontDestroyOnLoad(this.gameObject);

		}
		else
		{
			Destroy(this.gameObject);
		}

	}


	public static MainManager Instance
	{
		get
		{
			if (null == instance)
			{
				return null;
			}
			return instance;
		}

	}

	public UIManager GetUIManager()
	{
		return m_UIManager;
	}

	public StageManager GetStageManager()
	{
		return m_StageManager;
	}


	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
