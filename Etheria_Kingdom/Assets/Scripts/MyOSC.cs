using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class MyOSC : MonoBehaviour
{

    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;

    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    void MessageReceived(OSCMessage oscMessage)
    {
        
        // We will store the value in a float even if we get an int
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            value = oscMessage.Values[0].IntValue;
        }
        else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        }
        else
        {
            // If message is neither Int or Float do nothing
            return;
        }

        /*
        float rotation = ScaleValue(value, 0, 4095, 45, 315);
        potGameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        */
        //targetRigidbody2D.AddTorque(value * torqueMultiplier);
    }

    // Start is called before the first frame update
    void Start()
    {
        //targetRigidbody2D = targetGameObject.GetComponent<Rigidbody2D>();
        /*for (int i = 0; i <= 35; i++) 
        {
            string text = "id" + i + "_0";
            int val = oscReceiver.Bind(text, MessageReceived);
            Debug.Log(val);
        }*/
        
        oscReceiver.Bind("id10_0", MessageReceived);

        //oscReceiver.Bind("/but", MessageReceived);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
