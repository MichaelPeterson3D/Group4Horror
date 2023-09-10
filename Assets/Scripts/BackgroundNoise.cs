using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundNoise : MonoBehaviour
{
    [SerializeField] private AudioSource ambientNoise;
    [SerializeField] private AudioSource ambientNoise2;
    [SerializeField] private AudioSource noteSound;
    [SerializeField] private AudioSource leverSound;
    [SerializeField] private AudioSource lampSound;
    [SerializeField] private AudioSource flashlightSound;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource pickUp;

    // Start is called before the first frame update
    void Start()
    {
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
