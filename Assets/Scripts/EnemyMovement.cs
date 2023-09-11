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

    public bool isEnemyNear;
    public Transform lookAtPoint;
    public bool canEnemyBeMoved;
    private bool isEnemyCloseToPlayer;
    private bool isPlayerSafe;
    public bool stopEnemy;
    public bool isEnemyInLevel3;
    private NavMeshAgent agent;

    //------------------ [Kam added]------------------------
    public AudioSource monsterSoundSpotted;
    public AudioSource monsterSoundStunned;
    public AudioSource monsterSoundPatrolling;
    private bool madeSoundAlready = false;
    private bool isInvoked;
    private Color enemyColor;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        //monsterSound.Play();
        agent = GetComponent<NavMeshAgent>();
        isPlayerSafe = false;
        isEnemyCloseToPlayer = false;
        isEnemyNear = false;
        enemyColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterSounds();

        CheckIfEnemyIsNearFlash();
        if (agent.remainingDistance < .5 && stopEnemy == false && isEnemyInLevel3 == false)
        {
            GoToPoint();
        }
        if (stopEnemy == false && isPlayerSafe == false)
        {
            FoundPlayer(distance);
        }
        if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
        if (Vector3.Distance(transform.position, player.transform.position) > distance)
        {
            isEnemyNear = false;
        }

        
    }
    private int ChooseAPos()
    {
        int index = Random.Range(0, points.Count);
        return index;
    }
    private void GoToPoint()
    {
        //agent.destination = points[ChooseAPos()].position;
        agent.SetDestination(points[ChooseAPos()].position);
    }
    private void FoundPlayer(float distance)
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            //agent.destination = player.transform.position;
            agent.SetDestination(player.transform.position);
            isEnemyNear = true;
        }

    }
    private void CheckIfEnemyIsNearFlash()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 80)
        {
            isEnemyCloseToPlayer = true;
        }
        else
        {
            isEnemyCloseToPlayer = false;
        }
    }
    public void StopEnemy()
    {
        stopEnemy = true;
        agent.ResetPath();
        agent.SetDestination(transform.position);
    }
    public IEnumerator StopEnemyforAFewSec(float timeStoped)
    {
        if (canEnemyBeMoved == true)
        {
            if (isEnemyCloseToPlayer == true)
            {
                Color originalColor = enemyColor;
                enemyColor = Color.clear;
                stopEnemy = true;
                monsterSoundStunned.Play();
                agent.SetDestination(transform.position);
                yield return new WaitForSeconds(timeStoped);
                enemyColor = originalColor;
                agent.ResetPath();
                if (player.GetComponent<PlayerActions>().isCutScenePlaying == false)
                {
                    stopEnemy = false;
                }
            }
        }
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

    public void MonsterSounds()
    {
        if (!isEnemyCloseToPlayer)
        {
            madeSoundAlready = false;

            if (!isInvoked)
            {
                InvokeRepeating("PatrollingNoise", 2f, 10f);
                isInvoked = true;
            }
            return;
        }
        if (isEnemyCloseToPlayer && !madeSoundAlready)
        {
            CancelInvoke("PatrollingNoise");
            isInvoked = false;
            monsterSoundSpotted.Play();
            madeSoundAlready = true;
        }
    }

    public void PatrollingNoise()
    {
        monsterSoundPatrolling.Play();
    }
}
