using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPatsUpdate : MonoBehaviour
{
    public TextMesh text { get; private set; }
    public int shipPartsLeft;

  

    
    void Start()
    {
        text = gameObject.GetComponent<TextMesh>();
        shipPartsLeft = 4;
    }

    void Update()
    {
        if(shipPartsLeft > 0)
        text.text = "Ship Parts Left: " + shipPartsLeft;
    }


}
