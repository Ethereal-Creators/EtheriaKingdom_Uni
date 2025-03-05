using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotationScript : MonoBehaviour
{
    public float distanceTextY;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.parent.rotation.x, this.gameObject.transform.parent.rotation.y, 0);
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.parent.position.x, (this.gameObject.transform.parent.position.y + distanceTextY));
    }
}
