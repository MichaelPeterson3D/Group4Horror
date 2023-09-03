using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightLight;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private bool doesPlayerHaveFlashLight;
    [SerializeField] private LayerMask flashLightMask;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject battery;
    [SerializeField] private Image bottomMeter;
    [SerializeField] private Image middleMeter;
    [SerializeField] private Image topMeter;

    public bool canPlayerUseFlashLight = true;

    private GameObject lookAtObject = null;
    private int flashLightCharges;
    private bool canPlayerPickupFlashLight;
    private bool scanCam = false;

    // Start is called before the first frame update
    void Start()
    {
        flashLightCharges = 3;
        if (doesPlayerHaveFlashLight == true)
        {
            flashlight.SetActive(true);
        }
        else if (doesPlayerHaveFlashLight == false)
        {
            flashlight.SetActive(false);
            battery.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (scanCam == true)
        {
            CastLightRay();
        }
        PickUpFlashLight();
        if (doesPlayerHaveFlashLight == true && Input.GetMouseButtonDown(1) && canPlayerUseFlashLight == true && flashLightCharges > 0)
        {
            StartCoroutine(FlashLightAttack());
        }
        CheckBattery();
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 16;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, flashLightMask))
        {
            canPlayerPickupFlashLight = true;
            lookAtObject = hit.collider.gameObject;
        }
        else
        {
            canPlayerPickupFlashLight = false;
            lookAtObject = null;
        }
    }
    private void PickUpFlashLight()
    {
        if (canPlayerPickupFlashLight == true && Input.GetMouseButtonDown(0))
        {
            Destroy(lookAtObject);
            doesPlayerHaveFlashLight = true;
        }
        if(doesPlayerHaveFlashLight == true)
        {
            flashlight.SetActive(true);
            battery.SetActive(true);
        }
    }
    private IEnumerator FlashLightAttack()
    {
        scanCam = true;
        canPlayerUseFlashLight = false;
        flashlightLight.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        flashlightLight.SetActive(false);
        canPlayerUseFlashLight = true;
        flashLightCharges--;
        scanCam = false;
    }
    private void CastLightRay()
    {
       for (int i = 0; i < enemyLists.Count; i++)
        {
            if (enemyLists[i].GetComponent<EnemyFlash>().IsCamInEnemyView == true)
            {
                
                StartCoroutine(enemyLists[i].GetComponent<EnemyMovement>().StopEnemyforAFewSec(4.0f));
            }
        }
    }
    private void CheckBattery()
    {
        if (flashLightCharges == 3)
        {
            topMeter.gameObject.SetActive(true);
            middleMeter.gameObject.SetActive(true);
            bottomMeter.gameObject.SetActive(true);
            topMeter.color = Color.green;
            middleMeter.color = Color.green;
            bottomMeter.color = Color.green;
        }
        else if(flashLightCharges == 2)
        {
            topMeter.gameObject.SetActive(false);
            middleMeter.gameObject.SetActive(true);
            bottomMeter.gameObject.SetActive(true);
            middleMeter.color = Color.yellow;
            bottomMeter.color = Color.yellow;
        }
        else if (flashLightCharges == 1)
        {
            topMeter.gameObject.SetActive(false);
            middleMeter.gameObject.SetActive(false);
            bottomMeter.gameObject.SetActive(true);
            bottomMeter.color = Color.red;
        }
        else if (flashLightCharges == 0)
        {
            topMeter.gameObject.SetActive(false);
            middleMeter.gameObject.SetActive(false);
            bottomMeter.gameObject.SetActive(false);
        }
    }
}
