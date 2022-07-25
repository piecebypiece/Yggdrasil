using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderChk : MonoBehaviour
{
    public bool coliderchk = false;
    MeshRenderer mesh;
    Material mat;

    public int m_row;
    public int m_coulmn;

    private void OnTriggerStay(Collider other)
    {
        //플레이어로 변경.
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("오브젝트들어옴");

            MainManager.Instance.GetStageManager().SetPlayerRowAndCoulmn(m_row, m_coulmn);
            coliderchk = true;
            mat.color = new Color(1, 0, 0);
        }
    }

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
    }

    void Update()
    {
        
    }
}
