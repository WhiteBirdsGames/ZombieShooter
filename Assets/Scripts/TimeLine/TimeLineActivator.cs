using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class TimeLineActivator : MonoBehaviour
{
    private PlayableDirector _playableDirector = default;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public void StartCinematic()
    {
        CinematicController.StartCinematicAction?.Invoke();
        _playableDirector.Play();
    }

}
