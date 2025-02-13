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
    // Start is called before the first frame update
    void Start()
    {
        timeWhenHide = Time.time + timeTilHide;
        timeWhenShow = Time.time + timeTilShow;
    }

    // Update is called once per frame
    void Update()
    {
        timeTilShow -= Time.deltaTime;
        if (timeTilShow < 0)
        {
            actionBeforeTimer.Invoke();
        }

        timeTilHide -= Time.deltaTime;
        if (timeTilHide < 0)
        {
            actionAfterTimer.Invoke();
        }
    }
}
