using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    [SerializeField] UnityEvent actionAfterAnimation;

    public void OnTriggerEnter2D(Collider2D obj) {
        Debug.Log("WE COLLIDED MF");
        actionAfterAnimation.Invoke();
        //SceneManager.LoadScene("test01_OSC");
    }

}
