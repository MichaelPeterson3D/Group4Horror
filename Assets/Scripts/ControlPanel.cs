using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Material greenMat;
    [SerializeField] private Animator doorOpen;
    [SerializeField] private Animator buttonPressed;
    [SerializeField] private GameObject EnemyBehindDoor;
    public bool canButtonBePressed;
    public bool isEnemyBehindDoor;
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
        StartCoroutine(CheckIfEnemyIsBehindDoor());
    }
    private IEnumerator CheckIfEnemyIsBehindDoor()
    {
        if (isEnemyBehindDoor == true)
        {
            yield return new WaitForSeconds(2.0f);
            EnemyBehindDoor.GetComponent<EnemyMovement>().ResumeEnemy();
        }
    }
}
