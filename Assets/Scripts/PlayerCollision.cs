using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private Image vignette;
    public CinemachineVirtualCamera playerCam;

    //------------------ [Kam added]------------------------
    [SerializeField] private TMP_Text hint;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("DeathMenu");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            for (int i = 0; i < enemyLists.Count; i++)
            {
                enemyLists[i].GetComponent<EnemyMovement>().ResetPath();
                enemyLists[i].GetComponent<EnemyMovement>().SetPlayerToSafe(true);
                vignette.CrossFadeAlpha(0, 1.0f, false);
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("DeathMenu");
            //GetComponent<PlayerActions>().StopAllEnemies();
            //playerCam.GetComponent<PlayerCamera>().allowCamToMove = false;
            //Vector3 dir = other.transform.position - transform.position;
            //Debug.Log(dir);
            //playerCam.GetCinemachineComponent<CinemachineHardLookAt>().LookAtTarget
        }
        //------------------ [Kam added]------------------------
        if (other.gameObject.tag == "LampHint")
        {
            hint.text = "Hint: Enemies aren't fond of light.";
        }
        //------------------------------------------------------
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            for (int i = 0; i < enemyLists.Count; i++)
            {
                enemyLists[i].GetComponent<EnemyMovement>().ResetPath();
                enemyLists[i].GetComponent<EnemyMovement>().SetPlayerToSafe(false);
                vignette.CrossFadeAlpha(1, 1.0f, false);
            }
        }

        //------------------ [Kam added]------------------------

        if (other.gameObject.tag == "LampHint")
        {
            hint.text = "";
        }

        //------------------------------------------------------
    }
    private void PlayerDeath()
    {

    }
}
