using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private LayerMask Key;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private GameObject KeyImage;
    private Rigidbody lookAtObject = null;
    private bool doesPlayerHaveKey;
    private bool canPlayerPickupKey;
    // Start is called before the first frame update
    void Start()
    {
        doesPlayerHaveKey = false;
        canPlayerPickupKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (canPlayerPickupKey == true)
        {
            PickKeyUp();
        }
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 8;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, Key))
        {
            mainText.text = "Left click to pick up key";
            canPlayerPickupKey = true;
            lookAtObject = hit.rigidbody;
        }
        else
        {
            mainText.text = "";
            canPlayerPickupKey = false;
            lookAtObject = null;
        }
    }
    private void PickKeyUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(lookAtObject.gameObject);
            doesPlayerHaveKey = true;
            KeyImage.SetActive(true);
        }
    }
}
