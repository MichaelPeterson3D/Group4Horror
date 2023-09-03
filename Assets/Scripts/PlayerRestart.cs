using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRestart : MonoBehaviour
{
    public static string LevelBefore;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2" || SceneManager.GetActiveScene().name == "Level_3")
        {
            LevelBefore = SceneManager.GetActiveScene().name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(LevelBefore);
    }
}
