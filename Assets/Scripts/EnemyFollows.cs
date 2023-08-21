using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollows : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    Vector3 playerDestination;
    public Camera playerCamera;
    public float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

        if (GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            enemy.speed = 0.0f;
            enemy.SetDestination(transform.position);
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            enemy.speed = enemySpeed;
            playerDestination = player.position;
            enemy.destination = playerDestination;
        }
    }
}
