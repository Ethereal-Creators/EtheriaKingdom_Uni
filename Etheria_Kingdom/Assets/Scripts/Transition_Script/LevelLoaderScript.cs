using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{

    //tutoriel ultiser pour la réalisation du code : https://www.youtube.com/watch?v=CE9VOZivb3I&t=930s

    public float timeUntileChangeScene = 1.5f;

    public Animator transition;

    public float timeTilChange = 1f;
    private float timeWhenChange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        //LoadLevelNoYield(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(timeUntileChangeScene);

        SceneManager.LoadScene(levelIndex);
    }

    private void LoadLevelNoYield(int levelIndex)
    {
        transition.SetTrigger("Start");
        timeWhenChange = Time.time + timeTilChange;

        timeTilChange -= Time.deltaTime;
        if (timeTilChange < 0)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}
