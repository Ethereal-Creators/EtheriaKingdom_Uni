using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Rendering;

public class winScript : MonoBehaviour
{

    //private float timetest = 0.0f;
    public float timeTilSucces;
    private float timeWhenWin;

    public float timeTilChangeScene;
    private float timeWhenChangeScene;

    public GameObject crystalIsActive;
    public TextMeshProUGUI countDown;

    private float timeDown = 0.0f;
    private int currentTime = 0;

    private bool winSlowDown = false;

    [SerializeField] UnityEvent gameWin;

    // Start is called before the first frame update
    void Start()
    {
        timeWhenWin = Time.time + timeTilSucces;
        timeWhenChangeScene = Time.time + timeTilChangeScene;
    }

    // Update is called once per frame
    void Update()
    {
        timeDown += Time.deltaTime;
        if (timeDown >= 1f)
        {
            timeDown = 0.0f;
            currentTime++;
            countDown.text = currentTime.ToString();
        }

        //time += Time.deltaTime;
        //source du code : https://discussions.unity.com/t/how-to-check-if-object-is-active/116705
        if (timeWhenWin <= Time.time)
        {
            if (crystalIsActive.activeSelf == true)
            {
                winSlowDown = true;
                gameWin.Invoke();
                countDown.text = "Win";
            }



            timeWhenWin = Time.time + timeTilSucces;



        }

        if (winSlowDown == true)
        {
            timeTilChangeScene -= Time.deltaTime;
            if (timeTilChangeScene < 0)
            {
                // Permet de changer de scenes
                //SceneManager.LoadScene("Menu");
                Debug.Log("Win return to start.");
            }
        }
    }
}
