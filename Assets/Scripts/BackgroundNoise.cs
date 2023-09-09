using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundNoise : MonoBehaviour
{
    [SerializeField] private AudioSource ambientNoise;
    [SerializeField] private AudioSource ambientNoise2;
    [SerializeField] private AudioSource ambientNoise3;
    [SerializeField] private AudioSource ambientNoise4;
    [SerializeField] private AudioSource ambientNoise5;

    // Start is called before the first frame update
    void Start()
    {
        ambientNoise.Play();
        ambientNoise2.Play();
        //ambientNoise3.Play();
        //ambientNoise4.Play();
        //ambientNoise5.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
