using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundNoise : MonoBehaviour
{
    public AudioSource ambientNoise;
    public AudioSource ambientNoise2;
    public AudioSource noteSound;
    public AudioSource leverSound;
    public AudioSource lampSound;
    public AudioSource flashlightSound;
    public AudioSource doorSound;
    public AudioSource pickUp;

    public static BackgroundNoise instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        if (SceneManager.GetActiveScene().name == "Level_1" || SceneManager.GetActiveScene().name == "Level_2")
        {
            ambientNoise.Play();
        }
        else if (SceneManager.GetActiveScene().name == "Level_3")
        {
            ambientNoise.Play();
            ambientNoise2.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteSound()
    {
        noteSound.Play();
    }
    public void LeverSound()
    {
        leverSound.Play();
    }
    public void LampSound()
    {
        lampSound.Play();
    }
    public void FlashlightSound()
    {
        flashlightSound.Play();
    }
    public void DoorSound()
    {
        doorSound.Play();
    }
    public void PickUpSound()
    {
        pickUp.Play();
    }
}
