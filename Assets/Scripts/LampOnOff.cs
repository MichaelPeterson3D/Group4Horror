using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampOnOff : MonoBehaviour
{
    [SerializeField] private GameObject spotLight;
    [SerializeField] private GameObject lampLight;
    [SerializeField] private Material lampLightOn;
    [SerializeField] private Material lampLightOff;

    public bool shouldLampTurnOnOff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LampIsTurnedOn()
    {
        spotLight.SetActive(true);
        lampLight.GetComponent<Renderer>().material = lampLightOn;
    }
    public void LampIsTurnedOff()
    {
        spotLight.SetActive(false);
        lampLight.GetComponent<Renderer>().material = lampLightOff;
    }
}
