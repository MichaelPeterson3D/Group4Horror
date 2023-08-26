using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLever : MonoBehaviour
{
    [SerializeField] private List<GameObject> leverLocations = new List<GameObject>();
    [SerializeField] private List<bool> isLeverActive = new List<bool>();
    private int amountOfLeversActive;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < leverLocations.Count; i++)
        {
            isLeverActive.Add(false);
            leverLocations[i].SetActive(false);
        }
        amountOfLeversActive = 0;
        SpawnLevers(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnLevers(int leverCount)
    {
        int randomNum;
        while (amountOfLeversActive < leverCount)
        {
            randomNum = Random.Range(0, leverLocations.Count);
            if (isLeverActive[randomNum] == false)
            {
                isLeverActive[randomNum] = true;
                leverLocations[randomNum].SetActive(true);
                amountOfLeversActive++;
            }
        }
    }
}
