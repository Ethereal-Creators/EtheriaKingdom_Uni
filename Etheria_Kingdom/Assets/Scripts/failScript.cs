using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.Rendering;

public class failScript : MonoBehaviour
{

    public float timeTilFail;
    private float timeWhenFail;

    public GameObject crystalIsActive;
    public GameObject failPanel;

    public TextMeshProUGUI countDown;

    // POUR AJOUTER FAIL ===> public Animator failCountdown;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    private bool hasSoundPlayed = false;

    [SerializeField] UnityEvent gameFail;
    // Start is called before the first frame update
    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();
        timeWhenFail = Time.time + timeTilFail;
    }

    // Update is called once per frame
    void Update()
    {
        if (crystalIsActive == null /*&& failPanel.activeSelf == true*/)
        {

            gameFail.Invoke();

            if (source != null && clipsStart.Count > 0 && hasSoundPlayed == false)
            {
                Debug.Log("Play fail sound!");
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.PlayOneShot(clipsStart[randomClipIndex]);
                hasSoundPlayed = true;
            }
            Debug.Log("Don't play fail sound!");


            countDown.text = "Fail";
            timeTilFail -= Time.deltaTime;
            if (timeTilFail < 0)
            {
                SceneManager.LoadScene("Menu");
                Debug.Log("Fail return to start.");
            }
        }
    }
}
