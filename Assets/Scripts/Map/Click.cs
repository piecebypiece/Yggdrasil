using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    private void FixedUpdate()
    {
        BtnClick();
    }

    public void BtnClick()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;
                hitPos.y = 0.3f;

                Debug.Log($"{this.gameObject.name} 이 클릭됨");
            }
        }
    }
}
