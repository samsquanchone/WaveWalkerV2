using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{
    Button returnButton;
    // Start is called before the first frame update
    void Start()
    {
        returnButton = GetComponentInChildren<Button>();

        returnButton.onClick.AddListener(delegate { ReturnToGameMenu(); });

    }

    void ReturnToGameMenu()
    {
        this.gameObject.SetActive(false);
    }
}
