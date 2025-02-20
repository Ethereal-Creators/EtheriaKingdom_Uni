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

    [SerializeField] UnityEvent gameFail;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crystalIsActive == null && failPanel.activeSelf == true)
        {
            gameFail.Invoke();
            timeWhenFail = Time.time + timeTilFail;
            countDown.text = "Fail";
            timeTilFail -= Time.deltaTime;
            // POUR AJOUTER FAIL ===> failCountdown.SetBool("isFail", true);
            if (timeTilFail < 0)
            {
                
                SceneManager.LoadScene("Menu");
                Debug.Log("Fail return to start.");
                // POUR AJOUTER FAIL ===> failCountdown.SetBool("isFail", false);
            }
            /*if (timeWhenFail <= Time.time)
            {

                



                timeWhenFail = Time.time + timeTilFail;



            }*/
        }
    }
}
