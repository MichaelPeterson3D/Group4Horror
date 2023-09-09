using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject HelpMenu;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2" || SceneManager.GetActiveScene().name == "Level_3")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>().StopMusic();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameObject.FindGameObjectWithTag("Music").GetComponent<MusicController>().PlayMusic();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OnClickHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnPauseHelpMenu()
    {
        HelpMenu.SetActive(true);
    }
    public void OnPauseReturn()
    {
        HelpMenu.SetActive(false);
    }
}
