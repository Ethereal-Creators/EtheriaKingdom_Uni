using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;  // Include this to access the UI Image component

public class winScript : MonoBehaviour
{
    public float timeTilSucces; // Time for success
    private float timeWhenWin;

    public float timeTilChangeScene; // Time until scene change
    private float timeWhenChangeScene;

    public GameObject crystalIsActive;
    public Image progressBar; // UI Image that will serve as the progress bar

    private float timeDown = 0.0f;
    private int currentTime = 0;

    private bool winSlowDown = false;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    private bool hasSoundPlayed = false;

    [SerializeField] UnityEvent gameWin;

    [SerializeField] UnityEvent actionAfterAnimation;

    private bool isActionAfterAnimationDone = false;

    // Start is called before the first frame update
    void Start()
    {
        timeWhenWin = Time.time + timeTilSucces;
        timeWhenChangeScene = Time.time + timeTilChangeScene;
        source = this.gameObject.GetComponent<AudioSource>();

        // Set the initial progress bar fill to 0
        if (progressBar != null)
        {
            progressBar.fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Time until win (progress bar)
        float progress = Mathf.InverseLerp(0, timeTilSucces, Time.time - timeWhenWin + timeTilSucces);
        if (progressBar != null)
        {
            progressBar.fillAmount = progress; // Update the progress bar fill
        }

        // Count down timer is replaced by progress bar, no need for text display
        timeDown += Time.deltaTime;
        if (timeDown >= 1f)
        {
            timeDown = 0.0f;
            currentTime++;
            //countDown.text = currentTime.ToString();  // Removed
        }

        if (timeWhenWin <= Time.time)
        {
            if (crystalIsActive != null)
            {
                if (source != null && clipsStart.Count > 0 && hasSoundPlayed == false)
                {
                    int randomClipIndex = Random.Range(0, clipsStart.Count);
                    source.PlayOneShot(clipsStart[randomClipIndex]);
                    hasSoundPlayed = true;
                }
                winSlowDown = true;
                gameWin.Invoke();
                //countDown.text = "Win"; // Removed, as we're using a progress bar now
            }
            timeWhenWin = Time.time + timeTilSucces;
        }

        if (winSlowDown == true)
        {
            timeTilChangeScene -= Time.deltaTime;
            if (timeTilChangeScene < 0 && isActionAfterAnimationDone == false)
            {
                isActionAfterAnimationDone = true;
                actionAfterAnimation.Invoke();
                // Change scene after win
                // SceneManager.LoadScene("Menu");
                Debug.Log("Win return to start.");
            }
        }
    }
}
