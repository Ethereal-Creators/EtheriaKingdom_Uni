using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderScript : MonoBehaviour
{

    //tutoriel ultiser pour la réalisation du code : https://www.youtube.com/watch?v=CE9VOZivb3I&t=930s

    public float timeUntileChangeScene = 1.5f;

    public GameObject LoadingScreen;

    public Slider sliderLoadingScreen;

    public Animator transition;

    public float timeTilChange;
    private float timeWhenChange;

    private bool isStartAnimation = false;

    public int NextSceneIndex;

    public string NextSceneName;

    void Start()
    {
        timeWhenChange = Time.time + timeTilChange;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && isStartAnimation == false)
        {
            isStartAnimation = true;
        }*/

        if (isStartAnimation == true)
        {
            transition.SetTrigger("Start");

            timeTilChange -= Time.deltaTime;
            if (timeTilChange < 0)
            {
                LoadNextLevel();
            }
        }
    }

    public void StartAnimAction()
    {
        isStartAnimation = true;
    }

    public void LoadNextLevel()
    {
        Debug.Log("animation done!");
        if (LoadingScreen != null)
        {
            LoadingScreen.SetActive(true);
        }
        //LoadLevelNoYield(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        if (NextSceneName != "")
        {
            StartCoroutine(LoadAsynchronouslyName(NextSceneName));
        }
        else if (NextSceneIndex != null)
        {
            StartCoroutine(LoadAsynchronously(NextSceneIndex));
        }
        else
        {
            StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
        }

    }


    /*
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(timeUntileChangeScene);

        SceneManager.LoadScene(levelIndex);
    }*/

    IEnumerator LoadAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        if (operation != null)
        {
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 9f);

                if (sliderLoadingScreen != null)
                {
                    sliderLoadingScreen.value = progress;
                }


                Debug.Log("loading progress : " + progress);

                yield return null;
            }
        }
    }

    IEnumerator LoadAsynchronouslyName(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        if (operation != null)
        {
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 9f);

                if (sliderLoadingScreen != null)
                {
                    sliderLoadingScreen.value = progress;
                }

                Debug.Log("loading progress : " + progress);

                yield return null;
            }
        }
    }

    /*
    private void LoadLevelNoYield(int levelIndex)
    {
        transition.SetTrigger("Start");
        timeWhenChange = Time.time + timeTilChange;

        timeTilChange -= Time.deltaTime;
        if (timeTilChange < 0)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }*/
}
