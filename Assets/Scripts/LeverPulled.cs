using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LeverPulled : MonoBehaviour
{
    [SerializeField] private Animator leverPulled;
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
