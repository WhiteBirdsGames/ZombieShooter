using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour, IInteration
{
    private Animation _animation = default;
    private bool _takeDoor = default;

    [SerializeField]
    private UnityEvent _openDoorEvent = default;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }
    public void Interation()
    {
        if (!_takeDoor)
        {
            _openDoorEvent?.Invoke();
        }
    }


}
