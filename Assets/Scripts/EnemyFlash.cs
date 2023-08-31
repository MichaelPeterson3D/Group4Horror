using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlash : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    public bool IsCamInEnemyView = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyIsInCam();
    }
    private void CheckIfEnemyIsInCam()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            IsCamInEnemyView = true;
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            IsCamInEnemyView = false;
        }
    }
}
