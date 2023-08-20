using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private GameObject player;

    private bool stopEnemy;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stopEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < .5 && stopEnemy == false)
        {
            GoToPoint();
        }
        if (stopEnemy == false)
        {
            FoundPlayer();
        }
    }
    private int ChooseAPos()
    {
        int index = Random.Range(0, points.Count);
        return index;
    }
    private void GoToPoint()
    {
        agent.destination = points[ChooseAPos()].position;
    }
    private void FoundPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 60)
        {
            agent.destination = player.transform.position;
        }
    }
    public void StopEnemy()
    {
        stopEnemy = true;
        agent.ResetPath();
    }
    public void ResumeEnemy()
    {
        stopEnemy = false;
    }
}
