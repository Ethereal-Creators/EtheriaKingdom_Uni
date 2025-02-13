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

    [SerializeField] UnityEvent gameFail;
    // Start is called before the first frame update
    void Start()
    {
        timeWhenFail = Time.time + timeTilFail;
    }

    // Update is called once per frame
    void Update()
    {
        if (crystalIsActive.activeSelf == false && failPanel.activeSelf == true)
        {
            countDown.text = "Fail";
            timeTilFail -= Time.deltaTime;
            if (timeTilFail < 0)
            {
                gameFail.Invoke();
                Debug.Log("Fail return to start.");
            }
            /*if (timeWhenFail <= Time.time)
            {

                



                timeWhenFail = Time.time + timeTilFail;



            }*/
        }
    }
}
