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
            StartCoroutine(PlayerDied(other));
            
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
    private IEnumerator PlayerDied(Collider enemy)
    {
        GetComponent<PlayerActions>().StopAllEnemies();
        playerCam.GetComponent<PlayerCamera>().allowCamToMove = false;
        GetComponent<PlayerMovement>().canPlayerMove = false;
        playerCam.LookAt = enemy.GetComponent<EnemyMovement>().lookAtPoint;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("DeathMenu");
    }
}
