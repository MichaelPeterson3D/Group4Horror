using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundNoise : MonoBehaviour
{
    [SerializeField] private AudioSource ambientNoise;
    [SerializeField] private AudioSource ambientNoise2;

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
}
