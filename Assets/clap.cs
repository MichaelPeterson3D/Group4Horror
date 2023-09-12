using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clap : MonoBehaviour
{
    public AudioSource clapSound;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Credits")
        {
            clapSound.Play();
        }
        else
        {
            if (clapSound == null)
            {
                return;
            }
            else
            {
                clapSound.Stop();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
