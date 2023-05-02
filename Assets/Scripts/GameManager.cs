using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] int levelNumber;

    [SerializeField] UnityEvent onWinEvent;
    [SerializeField] UnityEvent onLoseEvent;

    public void Win()
    {
        onWinEvent.Invoke();
    }

    public void Lose() 
    {
        onLoseEvent.Invoke();
    }

    public void OnRandomLevel() 
    {
        SceneManager.LoadScene("Level " + (Random.Range(1,4)).ToString());
    }
    public void OnMenu() 
    {
        SceneManager.LoadScene(0);
    }
    public void Pause() 
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
}
