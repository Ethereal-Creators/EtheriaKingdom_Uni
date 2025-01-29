using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class MyOSC : MonoBehaviour
{

    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject myTarget;

    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    void TraiterMessageOSC(OSCMessage oscMessage)
    {
        // Récupérer une valeur numérique en tant que float
        // même si elle est de type float ou int :
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int )
        {
            value = oscMessage.Values[0].IntValue;
        } else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
        } else
        {
            // Si la valeur n'est ni un foat ou int, on quitte la méthode :
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        
        // Appliquer la rotation au GameObject ciblé :
        target.transform.eulerAngles = new Vector3(0,0,value);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Mettre cette ligne dans la méthode start()
        oscReceiver.Bind("/angle10_0", TraiterMessageOSC);
        

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
