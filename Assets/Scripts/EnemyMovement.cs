using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new List<Transform>();
    [SerializeField] private GameObject player;
    [SerializeField] private float distance;

    private bool isPlayerSafe;
    private bool stopEnemy;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stopEnemy = true;
        isPlayerSafe = false;
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            stopEnemy = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < .5 && stopEnemy == false)
        {
            GoToPoint();
        }
        if (stopEnemy == false && isPlayerSafe == false)
        {
            FoundPlayer(distance);
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
    private void FoundPlayer(float distance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
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
    public void ResetPath()
    {
        agent.ResetPath();
    }
    public void SetPlayerToSafe(bool setTrueOrFalse)
    {
        isPlayerSafe = setTrueOrFalse;
    }
}
