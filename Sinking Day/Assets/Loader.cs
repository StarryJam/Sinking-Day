using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
    public LevelManager0 LM;
    private AsyncOperation async = null;
    public Text loadText;
    public GameObject loadIcon;

    private float timer = 0;
    // Use this for initialization
    void Start () {
        loadText.gameObject.SetActive(false);
        LM = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager0>();
        StartCoroutine(LoadScene());
	}

    private void Update()
    {
        timer += Time.deltaTime * 2;
        loadText.color =  new Color(255,255,255, (1 + Mathf.Cos(timer)) / 2);
    }

    public IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(LM.nextSceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress >= 0.9f)
            {
                loadIcon.SetActive(false);
                loadText.gameObject.SetActive(true);
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
