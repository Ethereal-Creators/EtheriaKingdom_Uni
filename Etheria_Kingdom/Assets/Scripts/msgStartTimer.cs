using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class msgStartTimer : MonoBehaviour
{
    public float timeTilHide;
    private float timeWhenHide;

    [SerializeField] UnityEvent actionAfterTimer;

    public float timeTilShow;
    private float timeWhenShow;

    [SerializeField] UnityEvent actionBeforeTimer;


    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    private bool hasSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        timeWhenHide = Time.time + timeTilHide;
        timeWhenShow = Time.time + timeTilShow;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeTilShow -= Time.deltaTime;
        if (timeTilShow < 0)
        {
            if (clipsStart.Count > 0 && source != null && hasSoundPlayed == false)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.PlayOneShot(clipsStart[randomClipIndex]);
                hasSoundPlayed = true;
            }
            actionBeforeTimer.Invoke();
        }

        timeTilHide -= Time.deltaTime;
        if (timeTilHide < 0)
        {
            actionAfterTimer.Invoke();
        }
    }
}
