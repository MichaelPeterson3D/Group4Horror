using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material redMat;
    [SerializeField] private Animator doorOpen;
    [SerializeField] private Animator buttonPressed;

    public bool canButtonBePressed;
    // Start is called before the first frame update
    void Start()
    {
        canButtonBePressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonWasPressed()
    {
        buttonPressed.SetBool("ButtonPressed", true);
        button.GetComponent<Renderer>().material = greenMat;
        doorOpen.SetBool("OpenDoor", true);
        canButtonBePressed = false;
    }
}
