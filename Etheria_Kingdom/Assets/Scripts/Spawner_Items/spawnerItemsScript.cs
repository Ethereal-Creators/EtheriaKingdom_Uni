using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerItemsScript : MonoBehaviour
{
    public GameObject item;
    private float timeDown = 0.0f;

    private float timeCheck = 0.0f;

    public float minTimeUntilNextSpawn;

    public float maxTimeUntilNextSpawn;

    [Header("------- Max Spawn Item -------")]

    public int maxAmountOfItems = 3;

    public int currentItemsCount;

    [Header("------- Position Spawn Item -------")]

    public float minimumRandomX;
    public float maximumRandomX;
    public float minimumRandomY;
    public float maximumRandomY;

    [Header("------- Audio Effects Spawn Item -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    [Header("------- Spawned Item (do not add object) -------")]

    public List<GameObject> ListItems = new List<GameObject>();

    private bool setMaxItem = false;

    //healthPackClones.add(Instantiate(healthPack,healthPackSpawns[0].transform.position,healthPackSpawns[0].transform.rotation) as GameObject);

    // Start is called before the first frame update
    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCheck += Time.deltaTime;
        if (timeCheck >= 0.5f)
        {
            timeCheck = 0.0f;

            for (int i = 0; i < ListItems.Count; i++)
            {
                if (ListItems[i] == null && setMaxItem == false /*&& maxAmountOfItems < currentItemsCount*/)
                {
                    ListItems.RemoveAll(item => item.gameObject == null);
                    currentItemsCount = currentItemsCount - 1;
                    setMaxItem = true;
                }
            }
        }

        float randomValue = Random.Range(minTimeUntilNextSpawn, maxTimeUntilNextSpawn);
        timeDown += Time.deltaTime;
        if (timeDown >= randomValue)
        {
            timeDown = 0.0f;

            if (maxAmountOfItems > currentItemsCount)
            {
                if (setMaxItem == true)
                {
                    setMaxItem = false;
                }
                currentItemsCount++;
                //ListItems.add(Instantiate(item, (new Vector2(Random.Range(minimumRandomX, maximumRandomX), Random.Range(minimumRandomY, maximumRandomY))), Quaternion.identity));

                GameObject newItem = Instantiate(item, (new Vector2(Random.Range(minimumRandomX, maximumRandomX), Random.Range(minimumRandomY, maximumRandomY))), Quaternion.identity);
                ListItems.Add(newItem);
            }


            if (source != null && clipsStart.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.PlayOneShot(clipsStart[randomClipIndex]);
            }


            //countDown.text = currentTime.ToString();  // Removed
        }
    }
}
