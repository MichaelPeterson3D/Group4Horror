using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Lever : MonoBehaviour
{
    [SerializeField] private List<GameObject> leverLocations = new List<GameObject>();
    [SerializeField] private List<bool> isLeverActive = new List<bool>();
    [SerializeField] private List<GameObject> basementGates = new List<GameObject>();
    [SerializeField] private GameObject basementLampLight1;
    [SerializeField] private GameObject basementLampLight2;
    [SerializeField] private GameObject lightBulb1;
    [SerializeField] private GameObject lightBuld2;
    [SerializeField] private Material lightGreenMat;
    [SerializeField] private LayerMask lever;
    [SerializeField] private CinemachineVirtualCamera firstPersonCam;
    [SerializeField] private CinemachineVirtualCamera basementDoorCam;
    [SerializeField] private PlayerCamera playerCamera;

    [SerializeField] private AudioSource leverSound;

    private GameObject lookAtObject = null;
    private int numberOfLeverPulled;
    private int amountOfLeversActive;
    private bool canPlayerPullLever;
    private PlayerMovement playerMovement;
    private PlayerActions playerActions;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (canPlayerPullLever == true && Input.GetMouseButtonDown(0))
        {
            PlayerPulledLever();
        }
    }
    private void SetUp()
    {
        for (int i = 0; i < leverLocations.Count; i++)
        {
            isLeverActive.Add(false);
            leverLocations[i].SetActive(false);
        }
        numberOfLeverPulled = 0;
        amountOfLeversActive = 0;
        SpawnLevers(2);
        canPlayerPullLever = false;
        playerMovement = GetComponent<PlayerMovement>();
        playerActions = GetComponent<PlayerActions>();
    }
    private void SpawnLevers(int leverCount)
    {
        int randomNum;
        while (amountOfLeversActive < leverCount)
        {
            randomNum = Random.Range(0, leverLocations.Count);
            if (isLeverActive[randomNum] == false)
            {
                isLeverActive[randomNum] = true;
                leverLocations[randomNum].SetActive(true);
                amountOfLeversActive++;
            }
        }
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 16;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, lever))
        {
            lookAtObject = hit.collider.gameObject;
            if (lookAtObject.GetComponent<LeverPulled>().hasLeverBeenPulled == false)
            {
                canPlayerPullLever = true;
            }
        }
        else
        {
            canPlayerPullLever = false;
            lookAtObject = null;
        }
    }
    private void PlayerPulledLever()
    {
        leverSound.Play();
        lookAtObject.GetComponent<LeverPulled>().StartAnimation();
        lookAtObject.GetComponent<LeverPulled>().hasLeverBeenPulled = true;
        numberOfLeverPulled++;
        StartCoroutine(StartCamShift());
    }
    private IEnumerator StartCamShift()
    {
        playerActions.StopAllEnemies();
        playerCamera.allowCamToMove = false;
        //lookAtObject.SetActive(false);
        playerMovement.canPlayerMove = false;

        yield return new WaitForSeconds(2.0f);
        basementDoorCam.Priority = 11;

        yield return new WaitForSeconds(2.5f);
        ChangeLampLight();

        if (numberOfLeverPulled == 2)
        {
            yield return new WaitForSeconds(1.0f);
            OpenBasement();
        }
        yield return new WaitForSeconds(2.0f);
        basementDoorCam.Priority = 9;

        yield return new WaitForSeconds(2.0f);
        playerCamera.allowCamToMove = true;
        playerMovement.canPlayerMove = true;
        playerActions.ResumeAllEnemies();
    }
    private void ChangeLampLight()
    {
        if (numberOfLeverPulled == 1)
        {
            basementLampLight1.GetComponent<Light>().color = Color.green;
            lightBulb1.GetComponent<Renderer>().material = lightGreenMat;
        }
        else if (numberOfLeverPulled == 2)
        {
            basementLampLight2.GetComponent<Light>().color = Color.green;
            lightBuld2.GetComponent<Renderer>().material = lightGreenMat;
        }
    }
    private void OpenBasement()
    {
        basementGates[1].SetActive(true);
        basementGates[0].SetActive(false);
    }
}
