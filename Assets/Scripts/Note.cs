using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Note : MonoBehaviour
{
    public GameObject wordsObject;
    public TextMeshProUGUI wordsText;
    public static Note instance;

    // Start is called before the first frame update
    void Start()
    {
        wordsText = wordsObject.GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            wordsText.text = "I keep on losing my keys. In my old age, I can’t remember a thing. This message is for my own recollection. The spare keys should be around here somewhere. I know I always kept it beside a candle, but which one was it again?";
        }
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            wordsText.text = "My creatures are coming along well. They should replace my servants in a matter of months. I will just need to teach them how to clean and cook so they can take care of the mansion.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
