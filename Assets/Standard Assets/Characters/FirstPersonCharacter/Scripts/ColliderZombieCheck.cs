using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderZombieCheck : MonoBehaviour
{
    public ZombieControl zombieControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (zombieControl.State == ZombieControl.States.Idle)
                zombieControl.State = ZombieControl.States.Run;
        }
    }
}
