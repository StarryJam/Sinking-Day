using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneBtn : MonoBehaviour {

    public string nextSceneName;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        LevelManager0.LoadNextScene(nextSceneName);
    }

}
