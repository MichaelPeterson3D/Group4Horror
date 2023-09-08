using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject videoThatShows;
    [SerializeField] private GameObject highLightArea;
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        videoThatShows.SetActive(true);
        highLightArea.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        videoThatShows.SetActive(false);
        highLightArea.SetActive(false);
    }
}
