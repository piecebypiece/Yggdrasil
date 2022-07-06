using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderChk : MonoBehaviour
{
    public bool coliderchk = false;
    MeshRenderer mesh;
    Material mat;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            Debug.Log("오브젝트들어옴");
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
