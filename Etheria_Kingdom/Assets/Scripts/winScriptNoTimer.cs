using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class winScriptNoTimer : MonoBehaviour
{
    public float timeTilChangeScene; // Time until scene change
    private float timeWhenChangeScene;

    private bool isWon = false;

    private bool winSlowDown = false;

    public GameObject crystalIsActive;

    private bool hasSoundPlayed = false;

    private bool isActionAfterAnimationDone = false;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    [SerializeField] UnityEvent gameWin;

    [SerializeField] UnityEvent actionAfterAnimation;

    

    // Start is called before the first frame update
    void Start()
    {
        timeWhenChangeScene = Time.time + timeTilChangeScene;
        source = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWon == true)
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
                StartAnimationEndLevel();
                //countDown.text = "Win"; // Removed, as we're using a progress bar now
            }
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

    public void StartAnimationEndLevel()
    {
        winSlowDown = true;
    }

    public void IsWon()
    {
        isWon = true;
    }
}
