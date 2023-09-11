using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    [SerializeField] private LayerMask controlPanel;
    [SerializeField] private List<GameObject> controlPanelSet1 = new List<GameObject>();
    [SerializeField] private List<GameObject> controlPanelSet2 = new List<GameObject>();
    [SerializeField] private List<GameObject> doorSet1 = new List<GameObject>();
    [SerializeField] private List<GameObject> doorSet2 = new List<GameObject>();
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;

    public int correctControlPanelSet1;
    public int correctControlPanelSet2;
    private GameObject lookAtObject;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        ControlPanelSetUp();

    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }
    private void SetUp()
    {
        lookAtObject = null;
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 16;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, controlPanel))
        {
            lookAtObject = hit.collider.gameObject;
            if (lookAtObject.GetComponent<ControlPanel>().canButtonBePressed == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    lookAtObject.GetComponent<ControlPanel>().ButtonWasPressed();
                }
            }
        }
        else
        {
            lookAtObject = null;
        }
    }
    private int RandomizeControlPanel()
    {
        int num = Random.Range(0, 2);
        return num;
    }
    private void ControlPanelSetUp()
    {
        correctControlPanelSet1 = RandomizeControlPanel();
        correctControlPanelSet2 = RandomizeControlPanel();
        if (correctControlPanelSet1 == 0)
        {
            controlPanelSet1[0].GetComponent<ControlPanel>().isEnemyBehindDoor = false;
            controlPanelSet1[1].GetComponent<ControlPanel>().isEnemyBehindDoor = true;
            controlPanelSet1[1].GetComponent<ControlPanel>().EnemyBehindDoor = enemy1;
            controlPanelSet1[0].GetComponent<ControlPanel>().doorOpen = doorSet1[1].GetComponent<Animator>();
            controlPanelSet1[1].GetComponent<ControlPanel>().doorOpen = doorSet1[0].GetComponent<Animator>();
        }
        else if (correctControlPanelSet1 == 1)
        {
            controlPanelSet1[0].GetComponent<ControlPanel>().isEnemyBehindDoor = true;
            controlPanelSet1[1].GetComponent<ControlPanel>().isEnemyBehindDoor = false;
            controlPanelSet1[0].GetComponent<ControlPanel>().EnemyBehindDoor = enemy1;
            controlPanelSet1[0].GetComponent<ControlPanel>().doorOpen = doorSet1[0].GetComponent<Animator>();
            controlPanelSet1[1].GetComponent<ControlPanel>().doorOpen = doorSet1[1].GetComponent<Animator>();
        }
        if (correctControlPanelSet2 == 0)
        {
            controlPanelSet2[0].GetComponent<ControlPanel>().isEnemyBehindDoor = false;
            controlPanelSet2[1].GetComponent<ControlPanel>().isEnemyBehindDoor = true;
            controlPanelSet2[1].GetComponent<ControlPanel>().EnemyBehindDoor = enemy2;
            controlPanelSet2[0].GetComponent<ControlPanel>().doorOpen = doorSet2[0].GetComponent<Animator>();
            controlPanelSet2[1].GetComponent<ControlPanel>().doorOpen = doorSet2[1].GetComponent<Animator>();
        }
        else if (correctControlPanelSet2 == 1)
        {
            controlPanelSet2[0].GetComponent<ControlPanel>().isEnemyBehindDoor = true;
            controlPanelSet2[1].GetComponent<ControlPanel>().isEnemyBehindDoor = false;
            controlPanelSet2[0].GetComponent<ControlPanel>().EnemyBehindDoor = enemy2;
            controlPanelSet2[0].GetComponent<ControlPanel>().doorOpen = doorSet2[1].GetComponent<Animator>();
            controlPanelSet2[1].GetComponent<ControlPanel>().doorOpen = doorSet2[0].GetComponent<Animator>();
        }
    }
}
