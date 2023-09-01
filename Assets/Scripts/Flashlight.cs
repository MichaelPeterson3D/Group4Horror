using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private GameObject flashlight;
    [SerializeField] private GameObject flashlightLight;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private bool doesPlayerHaveFlashLight;
    [SerializeField] private LayerMask flashLightMask;
    [SerializeField] private LayerMask enemyMask;

    private GameObject lookAtObject = null;
    private bool canPlayerPickupFlashLight;
    private bool scanCam = false;
    public bool canPlayerUseFlashLight = true;

    // Start is called before the first frame update
    void Start()
    {
        if (doesPlayerHaveFlashLight == true)
        {
            flashlight.SetActive(true);
        }
        else if (doesPlayerHaveFlashLight == false)
        {
            flashlight.SetActive(false);
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
        if (doesPlayerHaveFlashLight == true && Input.GetMouseButtonDown(1) && canPlayerUseFlashLight == true)
        {
            StartCoroutine(FlashLightAttack());
        }
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
        scanCam = false;
    }
    private void CastLightRay()
    {
       for (int i = 0; i < enemyLists.Count; i++)
        {
            if (enemyLists[i].GetComponent<EnemyFlash>().IsCamInEnemyView == true)
            {
                
                StartCoroutine(enemyLists[i].GetComponent<EnemyMovement>().StopEnemyforAFewSec(2.0f));
            }
        }
    }
}
