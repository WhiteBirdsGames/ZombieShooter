using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpManager : MonoBehaviour
{
    public GameObject FPS_OB, WakeUp_OB, Tip_0_OB, WakeUpCanvas;
    public Animation AnimationWakeUp;

    public bool IsDisabledStartAnimation;

    private void Awake()
    {
        WakeUpCanvas.SetActive(true);
        if (IsDisabledStartAnimation)
        {
            GoEndAnimation();
        }
    }

    void Update()
    {
        if (!AnimationWakeUp.isPlaying)
        {
            GoEndAnimation();
        }
    }

    public void GoEndAnimation ()
    {
        WakeUpCanvas.SetActive(false);
        FPS_OB.SetActive(true);
        WakeUp_OB.SetActive(false);
        Tip_0_OB.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
