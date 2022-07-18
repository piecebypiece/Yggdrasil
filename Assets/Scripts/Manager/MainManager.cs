using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{

	public static MainManager instance;


	private UIManager m_UIManager;
	private StageManager m_StageManager;


	private void Awake()
	{

		

		if (null == instance)
		{
			instance = this;
			m_UIManager = new UIManager();
			m_StageManager = new StageManager();
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
