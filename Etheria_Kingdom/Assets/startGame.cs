using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    
    public void OnTriggerEnter2D(Collider2D obj) {
        Debug.Log("WE COLLIDED MF");
        SceneManager.LoadScene("TimerManche");
    }

}
