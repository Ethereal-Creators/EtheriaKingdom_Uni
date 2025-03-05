using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownUntilSpawn : MonoBehaviour
{

    public float timeTilShowFirts;
    public float timeTilShowNextMin;
    public float timeTilShowNextMax;

    private bool isFirstDone = false;

    private float timeDown = 0.0f;

    [Header("------- Power Up Box -------")]

    public List<GameObject> ListPowerUpBox = new List<GameObject>();

    private int currentMaxSpawned = 0;

    //public GameObject[] ListPowerUpBox;

    // Update is called once per frame
    void Update()
    {
        timeDown += Time.deltaTime;

        if (isFirstDone == false)
        {
            ActivateEventBox(timeTilShowFirts);
        }
        if (isFirstDone == true)
        {
            float randomValue = Random.Range(timeTilShowNextMin, timeTilShowNextMax);
            ActivateEventBox(randomValue);
        }

    }

    private void ActivateEventBox(float timeTil)
    {
        if (timeDown >= timeTil)
        {
            if (isFirstDone == false)
            {
                isFirstDone = true;
            }

            timeDown = 0.0f;
            Debug.Log("hello number : " + currentMaxSpawned);
            if ((currentMaxSpawned + 1) <= ListPowerUpBox.Count)
            {
                if (ListPowerUpBox[currentMaxSpawned] != null)
                {
                    ListPowerUpBox[currentMaxSpawned].SetActive(true);
                    currentMaxSpawned++;
                }
            }
        }
    }
}
