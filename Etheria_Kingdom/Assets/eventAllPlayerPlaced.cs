using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class eventAllPlayerPlaced : MonoBehaviour
{
    [Header("------- Ne pas cocher (seulment pour visualiser) -------")]
    public bool isPlayerOnePlaced = false;
    public bool isPlayerTwoPlaced = false;
    public bool isPlayerThreePlaced = false;

    [SerializeField] UnityEvent actionAfterAnimation;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOnePlaced == true && isPlayerTwoPlaced == true && isPlayerThreePlaced == true)
        {
            actionAfterAnimation.Invoke();
        }
    }

    public void SetPlayerOnePlaced()
    {
        isPlayerOnePlaced = true;
    }
    public void SetPlayerTwoPlaced()
    {
        isPlayerTwoPlaced = true;
    }
    public void SetPlayerThreePlaced()
    {
        isPlayerThreePlaced = true;
    }


    public void SetPlayerOneRemoved()
    {
        isPlayerOnePlaced = false;
    }
    public void SetPlayerTwoRemoved()
    {
        isPlayerTwoPlaced = false;
    }
    public void SetPlayerThreeRemoved()
    {
        isPlayerThreePlaced = false;
    }
}
