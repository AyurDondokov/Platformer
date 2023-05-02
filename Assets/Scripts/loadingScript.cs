using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScript : MonoBehaviour
{
    string scene;
    [Header("Остальные объекты")]
    public Image LoadingImg;
    public Text progressText;

    void Start()
    {
        scene = "Level 1";
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            LoadingImg.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}