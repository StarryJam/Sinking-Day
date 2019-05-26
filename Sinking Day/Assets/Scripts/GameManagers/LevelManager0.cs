using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager0 : MonoBehaviour {

    public static string nextSceneName;
    public static LevelManager0 levelManager = null;

    private void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
            nextSceneName = "MainMenu";
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadNextScene(string name)
    {
        nextSceneName = name;
        SceneManager.LoadScene("Loading");
    }

    
}
