using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager0 : MonoBehaviour {

    public string nextSceneName;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene(string name)
    {
        nextSceneName = name;
        SceneManager.LoadScene("Loading");
    }

    
}
