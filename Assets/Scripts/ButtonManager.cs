using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject HelpMenu;
    public GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindGameObjectWithTag("Music");

        if (SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2" || SceneManager.GetActiveScene().name == "Level_3")
        {
            if (music == null)
            {
                return;
            }
            else
            {
                music.GetComponent<MusicController>().StopMusic();
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            music.GetComponent<MusicController>().PlayMusic();
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickPlay()
    {
        music.GetComponent<MusicController>().StopMusic();
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
