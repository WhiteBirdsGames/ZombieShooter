using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class StartCinematicToTrigger : MonoBehaviour
{
    [SerializeField]
    private TimeLineActivator _timeLineActivator = default;
    bool canCinematic = false;
    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.GetComponent<FirstPersonController>();

        if (fps != null)
        {
            if (canCinematic == false)
            {
                canCinematic = true;
                _timeLineActivator.StartCinematic();
            }
        }
    }
}
