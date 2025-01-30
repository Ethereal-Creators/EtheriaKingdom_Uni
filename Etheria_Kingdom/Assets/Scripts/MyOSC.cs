using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class MyOSC : MonoBehaviour
{

    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject myTarget;
    public int multiplierXY;
    public int removeFromX;
    public int AddToX;
    public int removeFromY;
    public int AddToY;
    public string idNumber;
    private Vector3 newPosition;

    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    void IdTraiterMessageOSC(OSCMessage oscMessage)
    {
        // Récupérer une valeur numérique en tant que float
        // même si elle est de type float ou int :
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Int )
        {
            value = oscMessage.Values[0].IntValue;
            myTarget.gameObject.SetActive(true);
        } else if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            value = oscMessage.Values[0].FloatValue;
            myTarget.gameObject.SetActive(true);
        } else if (oscMessage.Values[0] == null)
        {
            myTarget.gameObject.SetActive(false);
        }
        else
        {
            // Si la valeur n'est ni un foat ou int, on quitte la méthode :
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        //float rotation = ScaleValue(value, 0, 360, 45, 315);
        //float negatedValue = value - (value * 2);
        
        // Appliquer la rotation au GameObject ciblé :
        //myTarget.transform.eulerAngles = new Vector3(0,0,negatedValue);
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
        float negatedValue = value - (value * 2);
        
        // Appliquer la rotation au GameObject ciblé :
        myTarget.transform.eulerAngles = new Vector3(0,0,negatedValue);
    }

    void YTraiterMessageOSC(OSCMessage oscMessage)
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
            newPosition[1] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - AddToY + removeFromY;
        
        // Appliquer la rotation au GameObject ciblé :
        //Vector3 newPositionY; = new Vector3(transform.position.x, augmentedValue, transform.position.z);
        newPosition[1] = augmentedValue;
        myTarget.transform.position = newPosition;
    }

    void XTraiterMessageOSC(OSCMessage oscMessage)
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
            newPosition[0] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - removeFromX + AddToX;

        // Appliquer la rotation au GameObject ciblé :
         //= new Vector3(augmentedValue, transform.position.y, transform.position.z);
        newPosition[0] = augmentedValue;
        myTarget.transform.position = newPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Mettre cette ligne dans la méthode start()
        oscReceiver.Bind("/id" + idNumber + "_0", IdTraiterMessageOSC);
        oscReceiver.Bind("/angle" + idNumber + "_0", TraiterMessageOSC);
        oscReceiver.Bind("/x" + idNumber + "_0", XTraiterMessageOSC);
        oscReceiver.Bind("/y" + idNumber + "_0", YTraiterMessageOSC);
        

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
