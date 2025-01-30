using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class MyOSC : MonoBehaviour
{

    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public GameObject myTarget;
    public GameObject myTargetTwo;
    public GameObject myTargetThree;
    public int multiplierXY;
    public int removeFromX;
    public int AddToX;
    public int removeFromY;
    public int AddToY;
    public int idNumberOne;
    public int idNumberTwo;
    public int idNumberThree;
    private Vector3 newPosition;
    private Vector3 newPositionTwo;
    private Vector3 newPositionThree;

    public static float ScaleValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    // Object #1

    void IdTraiterMessageOSC(OSCMessage oscMessage)
    {
        // Récupérer une valeur numérique en tant que float
        // même si elle est de type float ou int :
        if (oscMessage == null) {
            myTarget.gameObject.SetActive(false);
        } else {
            myTarget.gameObject.SetActive(true);
        }
        
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            if (oscMessage.Values[0].FloatValue == idNumberOne) {
                myTarget.gameObject.SetActive(true);
            } else {
                myTarget.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].FloatValue;
            myTarget.gameObject.SetActive(true);
        } else if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            if (oscMessage.Values[0].IntValue == idNumberOne) {
                myTarget.gameObject.SetActive(true);
            } else {
                myTarget.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].IntValue;
            
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
        float negatedValue = augmentedValue - (augmentedValue * 2);

        // Appliquer la rotation au GameObject ciblé :
         //= new Vector3(augmentedValue, transform.position.y, transform.position.z);
        newPosition[0] = negatedValue;
        myTarget.transform.position = newPosition;
    }

    // Object #2

    void TwoIdTraiterMessageOSC(OSCMessage oscMessage)
    {
        // Récupérer une valeur numérique en tant que float
        // même si elle est de type float ou int :
        if (oscMessage == null) {
            myTargetTwo.gameObject.SetActive(false);
        } else {
            myTargetTwo.gameObject.SetActive(true);
        }
        
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            if (oscMessage.Values[0].FloatValue == idNumberTwo) {
                myTargetTwo.gameObject.SetActive(true);
            } else {
                myTargetTwo.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].FloatValue;
            myTargetTwo.gameObject.SetActive(true);
        } else if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            if (oscMessage.Values[0].IntValue == idNumberTwo) {
                myTargetTwo.gameObject.SetActive(true);
            } else {
                myTargetTwo.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].IntValue;
            
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

    void TwoTraiterMessageOSC(OSCMessage oscMessage)
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
        myTargetTwo.transform.eulerAngles = new Vector3(0,0,negatedValue);
    }

    void TwoYTraiterMessageOSC(OSCMessage oscMessage)
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
            newPositionTwo[1] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - AddToY + removeFromY;
        
        // Appliquer la rotation au GameObject ciblé :
        //Vector3 newPositionY; = new Vector3(transform.position.x, augmentedValue, transform.position.z);
        newPositionTwo[1] = augmentedValue;
        myTargetTwo.transform.position = newPositionTwo;
    }

    void TwoXTraiterMessageOSC(OSCMessage oscMessage)
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
            newPositionTwo[0] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - removeFromX + AddToX;
        float negatedValue = augmentedValue - (augmentedValue * 2);

        // Appliquer la rotation au GameObject ciblé :
         //= new Vector3(augmentedValue, transform.position.y, transform.position.z);
        newPositionTwo[0] = negatedValue;
        myTargetTwo.transform.position = newPositionTwo;
    }

    // Object #3

    void ThreeIdTraiterMessageOSC(OSCMessage oscMessage)
    {
        // Récupérer une valeur numérique en tant que float
        // même si elle est de type float ou int :
        if (oscMessage == null) {
            myTargetThree.gameObject.SetActive(false);
        } else {
            myTargetThree.gameObject.SetActive(true);
        }
        
        float value;
        if (oscMessage.Values[0].Type == OSCValueType.Float)
        {
            if (oscMessage.Values[0].FloatValue == idNumberThree) {
                myTargetThree.gameObject.SetActive(true);
            } else {
                myTargetThree.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].FloatValue;
            myTargetThree.gameObject.SetActive(true);
        } else if (oscMessage.Values[0].Type == OSCValueType.Int)
        {
            if (oscMessage.Values[0].IntValue == idNumberThree) {
                myTargetThree.gameObject.SetActive(true);
            } else {
                myTargetThree.gameObject.SetActive(false);
            }
            value = oscMessage.Values[0].IntValue;
            
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

    void ThreeTraiterMessageOSC(OSCMessage oscMessage)
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
        myTargetThree.transform.eulerAngles = new Vector3(0,0,negatedValue);
    }

    void ThreeYTraiterMessageOSC(OSCMessage oscMessage)
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
            newPositionThree[1] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - AddToY + removeFromY;
        
        // Appliquer la rotation au GameObject ciblé :
        //Vector3 newPositionY; = new Vector3(transform.position.x, augmentedValue, transform.position.z);
        newPositionThree[1] = augmentedValue;
        myTargetThree.transform.position = newPositionThree;
    }

    void ThreeXTraiterMessageOSC(OSCMessage oscMessage)
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
            newPositionThree[0] = 35;
            return;
        }
        
        // Changer l'échelle de la valeur pour l'appliquer à la rotation :
        float rotation = ScaleValue(value, 0, 360, 45, 315);
        float augmentedValue = value * multiplierXY - removeFromX + AddToX;
        float negatedValue = augmentedValue - (augmentedValue * 2);

        // Appliquer la rotation au GameObject ciblé :
         //= new Vector3(augmentedValue, transform.position.y, transform.position.z);
        newPositionThree[0] = negatedValue;
        myTargetThree.transform.position = newPositionThree;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Mettre cette ligne dans la méthode start()
        oscReceiver.Bind("/id" + idNumberOne + "_0", IdTraiterMessageOSC);
        oscReceiver.Bind("/angle" + idNumberOne + "_0", TraiterMessageOSC);
        oscReceiver.Bind("/x" + idNumberOne + "_0", XTraiterMessageOSC);
        oscReceiver.Bind("/y" + idNumberOne + "_0", YTraiterMessageOSC);

        oscReceiver.Bind("/id" + idNumberTwo + "_0", TwoIdTraiterMessageOSC);
        oscReceiver.Bind("/angle" + idNumberTwo + "_0", TwoTraiterMessageOSC);
        oscReceiver.Bind("/x" + idNumberTwo + "_0", TwoXTraiterMessageOSC);
        oscReceiver.Bind("/y" + idNumberTwo + "_0", TwoYTraiterMessageOSC);

        oscReceiver.Bind("/id" + idNumberThree + "_0", ThreeIdTraiterMessageOSC);
        oscReceiver.Bind("/angle" + idNumberThree + "_0", ThreeTraiterMessageOSC);
        oscReceiver.Bind("/x" + idNumberThree + "_0", ThreeXTraiterMessageOSC);
        oscReceiver.Bind("/y" + idNumberThree + "_0", ThreeYTraiterMessageOSC);
        

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
