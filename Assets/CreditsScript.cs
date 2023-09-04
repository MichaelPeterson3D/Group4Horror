using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject video;
    public GameObject credits1;
    public GameObject credits2;
    public GameObject thanks;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Credits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Credits()
    {
        yield return new WaitForSeconds(5);
        winScreen.SetActive(false);
        video.SetActive(true);
        credits1.SetActive(true);
        yield return new WaitForSeconds(5);
        credits1.SetActive(false);
        credits2.SetActive(true);
        yield return new WaitForSeconds(5);
        credits2.SetActive(false);
        thanks.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
