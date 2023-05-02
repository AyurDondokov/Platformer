using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] loadingScript loading;
    public void StartLevel() 
    {
        SceneManager.LoadScene("Loading scene");
    }
}
