using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleSoundScript : MonoBehaviour
{

    [SerializeField]
    private AudioSource source;

    public List<AudioClip> clips = new List<AudioClip>();

    [SerializeField]
    private float _timeBetweenSound = 3f; // Time between each shot (in seconds)

    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= _timeBetweenSound)
        {
            time = 0.0f;
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
        
    }
}
