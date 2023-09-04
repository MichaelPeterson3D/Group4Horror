using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesLooking : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        RotateTreeToLookAtEnemy();
    }
    private void RotateTreeToLookAtEnemy()
    {
        Vector3 awayDirection = transform.position - player.position;
        Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
        transform.rotation = awayRotation;
    }
}
