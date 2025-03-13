using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class positionPlayerScript : MonoBehaviour
{
    public GameObject playerSelected;

    public UnityEvent OnPlaced;

    public UnityEvent OnLeave;

    [Header("------- Audio Effects -------")]
    [SerializeField]
    private AudioSource source;

    public List<AudioClip> clipsEnter = new List<AudioClip>();

    public List<AudioClip> clipsExit = new List<AudioClip>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == playerSelected.gameObject)
        {
            if (source != null && clipsEnter.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsEnter.Count);
                source.PlayOneShot(clipsEnter[randomClipIndex]);
            }
            OnPlaced.Invoke();
            Debug.Log("Placer le " + collision.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == playerSelected.gameObject)
        {
            if (source != null && clipsExit.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsExit.Count);
                source.PlayOneShot(clipsExit[randomClipIndex]);
            }
            OnLeave.Invoke();
            Debug.Log("Enlever le " + collision.gameObject.name);
        }
    }
}
