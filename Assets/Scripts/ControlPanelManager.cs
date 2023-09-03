using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    [SerializeField] private LayerMask controlPanel;

    private GameObject lookAtObject;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
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
}
