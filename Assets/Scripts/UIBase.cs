using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour
{

    public string UIName = "";
    public int UILayer = 0;
    protected CanvasGroup canvasGroup;
    protected virtual void Awake() { canvasGroup = GetComponent<CanvasGroup>(); }

    public virtual void DoOnEntering() { }

    public virtual void DoOnPausing() { }

    public virtual void DoOnResuming() { }

    public virtual void DoOnExiting() { }

}