using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NoteText : MonoBehaviour
{
    public GameObject noteContents;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI noteContentsText = noteContents.GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            noteContentsText.text = "Level 1 note";
        }
        else if (SceneManager.GetActiveScene().name == "Level_2")
        {
            noteContentsText.text = "Level 2 note";
        }
        else if (SceneManager.GetActiveScene().name == "Level_3")
        {
            noteContentsText.text = "Level 3 note";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
