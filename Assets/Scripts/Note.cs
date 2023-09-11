using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Note : MonoBehaviour
{
    public GameObject wordsObject;
    private TextMeshProUGUI wordsText;

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
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            if (gameObject.CompareTag("Note1"))
            {
                wordsText.text = "The creatures are loose! The generator lost power; I must have been using it too much. I was able to restart the systems, but many areas are still not turning on. Good thing I added in preventive measures so the creatures wouldn’t escape. The bad thing is that I am also locked in here. I will have to find those old levers; the lights should guide my way.";
            }
            if (gameObject.CompareTag("Note2"))
            {
                wordsText.text = "The trees… even the trees are watching me. I can hear the creatures growling. Through my studies, I found the creatures at this stage are affected by light or was it sound? I wish I had a flashlight to test it, but I don’t want to go back through the lab. Those creatures were lying in wait for me to make another slip up. ";
            }
            if (gameObject.CompareTag("Note3"))
            {
                wordsText.text = "Hands and trees, hands and trees. Everywhere I look, I see hands. They were supposed to be my trusted servants. Hands that clean, hands that kill. I saw them… dragging servants away that were trying to escape. I watched from the forest. The trees see all and know all. They brought them to the lab… what unspeakable things could have happened in there. I fear I am next…  I can hear them. I must try to make it to the courtyard, but my knees weren’t what they used to be.";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
