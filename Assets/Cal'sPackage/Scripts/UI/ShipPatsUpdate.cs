using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPatsUpdate : MonoBehaviour
{
    private TextMesh text;
    public int shipPartsLeft;

    
    void Start()
    {
        text = gameObject.GetComponent<TextMesh>();
        shipPartsLeft = 4;
    }

    void Update()
    {
        text.text = "Ship Parts Left: " + shipPartsLeft;
    }


}
