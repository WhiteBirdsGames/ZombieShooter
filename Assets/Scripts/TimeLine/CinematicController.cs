using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;

public class CinematicController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _startCinematicDisable = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _startCinematicEnable = new List<GameObject>();
  
    public static Action StartCinematicAction { get; private set; }

    private void Awake()
    {
        StartCinematicAction += StartCinematic;
    }
    public void StartCinematic()
    {
        Debug.Log("1");
        foreach (var item in _startCinematicDisable)
        {
            item.SetActive(false);
        }

        foreach (var item in _startCinematicEnable)
        {
            item.SetActive(true);
        }
    }

    public void EndCinematic()
    {
        foreach (var item in _startCinematicDisable)
        {
            item.SetActive(true);
        }

        foreach (var item in _startCinematicEnable)
        {
            item.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        StartCinematicAction -= StartCinematic;
    }
}
