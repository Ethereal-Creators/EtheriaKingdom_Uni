using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotationScript : MonoBehaviour
{
    public float distanceTextY;

    public float distanceTextXAdd = 180;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.parent.rotation.x + distanceTextXAdd, this.gameObject.transform.parent.rotation.y, 0);
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.parent.position.x, (this.gameObject.transform.parent.position.y + distanceTextY));
    }
}
