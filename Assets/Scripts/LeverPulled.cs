using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Cinemachine;

public class LeverPulled : MonoBehaviour
{
    [SerializeField] private Animator leverPulled;
    public CinemachineVirtualCamera leverCamera;
    public bool hasLeverBeenPulled;

    // Start is called before the first frame update
    void Start()
    {
        hasLeverBeenPulled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartAnimation()
    {
        leverPulled.Play("LeverPulled");
    }
}
