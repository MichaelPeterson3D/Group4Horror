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
            wordsText.text = "Level 1 text";
        }
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            wordsText.text = "Level 2 text";
        }
        if (SceneManager.GetActiveScene().name == "Level_3")
        {
            wordsText.text = "Level 3 text";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
