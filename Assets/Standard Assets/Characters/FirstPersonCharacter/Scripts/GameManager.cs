using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Window_Win, Window_Lose, Window_Pause;

    public UnityEvent OnWin, OnLose, OnPause;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void RestartScene ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if(!Window_Pause.activeSelf)
        {
            OnPause.Invoke();
            Window_Pause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Window_Pause.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void ShowWin ()
    {
        Window_Pause.SetActive(false);
        OnWin.Invoke();
        Window_Win.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowLose()
    {
        Time.timeScale = 1f;
        Window_Pause.SetActive(false);
        OnLose.Invoke();
        Invoke("InvokeShowWin", 2f);
    }

    public void InvokeShowWin()
    {
        Window_Win.SetActive(true);
        Time.timeScale = 0;
    }

}
