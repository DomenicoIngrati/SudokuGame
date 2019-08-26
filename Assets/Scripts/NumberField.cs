using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NumberField : MonoBehaviour
{
   

    int x1, y1;

    int value;

    string identifier;

    public Text number;

    public void SetValue(int _x1, int _y1, int _value,string _identifier)
    {
        x1 = _x1;
        y1 = _y1;
        value = _value;
        identifier = _identifier;
    

        number.text = (value != 0) ? value.ToString() : "";

        GetComponentInParent<Button>().interactable = false;
        
    }

    public int getX()
    {
        return x1;
    }

    public int getY()
    {
        return y1;
    }


    public void SetHint(int _value)
    {
        value = _value;
        number.text = value.ToString();
        number.color = Color.red;
        GetComponentInParent<Button>().interactable = false;
    }

}
