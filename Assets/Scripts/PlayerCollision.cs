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
    [SerializeField] private GameObject pivot;
    [SerializeField] private Image vignette;
    [SerializeField] private Image redVignette;
    [SerializeField] private Image Fadeout;
    public CinemachineVirtualCamera playerCam;
    private int checksAmount;
    private GameObject currentSafeZone = null;

    //------------------ [Kam added]------------------------
    public TMP_Text hint;
    public AudioSource heartbeat;
    public AudioSource deathSound;
    public AudioSource deathSound2;
    public bool enemyNearby;
    public bool lampHintChecked;
    public static PlayerCollision instance;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        lampHintChecked = false;
        enemyNearby = false;
        redVignette.CrossFadeAlpha(0, .01f, false);
        Fadeout.CrossFadeAlpha(0, 1.5f, false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfEnemyIsNear();
        
        if (enemyNearby)
        {
            heartbeat.Play();
        }
        else if (!enemyNearby)
        {
            heartbeat.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ExitLevel2")
        {
            SceneManager.LoadScene("Level_3");
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
            currentSafeZone = other.gameObject;
            if (currentSafeZone.GetComponent<LampOnOff>().shouldLampTurnOnOff == true)
            {
                currentSafeZone.GetComponent<LampOnOff>().LampIsTurnedOn();
            }
            GetComponent<Flashlight>().flashLightCharges = 3;
        }
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(PlayerDied(other));
            
        }
        //------------------ [Kam added]------------------------
        if (other.gameObject.tag == "LampHint" && lampHintChecked == false)
        {
            hint.text = "Charge your flashlight by standing under lamps";
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
            if (currentSafeZone.GetComponent<LampOnOff>().shouldLampTurnOnOff == true)
            {
                currentSafeZone.GetComponent<LampOnOff>().LampIsTurnedOff();
            }
            currentSafeZone = null;
        }

        //------------------ [Kam added]------------------------

        if (other.gameObject.tag == "LampHint")
        {
            lampHintChecked = true;
            hint.text = "";
        }

        //------------------------------------------------------
    }
    private IEnumerator PlayerDied(Collider enemy)
    {
        GetComponent<Flashlight>().canPlayerUseFlashLight = false;
        GetComponent<PlayerActions>().StopAllEnemies();
        playerCam.GetComponent<PlayerCamera>().allowCamToMove = false;
        GetComponent<PlayerMovement>().canPlayerMove = false;
        playerCam.LookAt = enemy.GetComponent<EnemyMovement>().lookAtPoint;
        enemy.GetComponent<EnemyMovement>().monsterSoundPatrolling.Play();
        yield return new WaitForSeconds(1.0f);
        playerCam.LookAt = null;
        StartCoroutine(Rotate());
        deathSound.Play();
        deathSound2.Play();
        yield return new WaitForSeconds(2.0f);
        Fadeout.CrossFadeAlpha(1, 1.5f, false);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("DeathMenu");
    }
    private void CheckIfEnemyIsNear()
    {
        checksAmount = 0;
        for (int i = 0; i < enemyLists.Count; i++)
        {
            if (enemyLists[i].GetComponent<EnemyMovement>().isEnemyNear == true)
            {
                redVignette.CrossFadeAlpha(1, 1.0f, false);
                enemyNearby = true;
            }
            else if (enemyLists[i].GetComponent<EnemyMovement>().isEnemyNear == false)
            {
                checksAmount++;
                if (checksAmount == enemyLists.Count)
                {
                    redVignette.CrossFadeAlpha(0, 1.0f, false);
                    enemyNearby = false;
                }
            }
        }
    }
    IEnumerator Rotate()
    {
        float timeElapsed = 0;
        Quaternion startRotation = pivot.transform.rotation; //Gets the current rotation of the player
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 90); //Rotates the player to -90.x rotation
        while (timeElapsed < .5f)
        {
            GetComponent<Rigidbody>().useGravity = false; // Turns off player gravity
            pivot.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / .5f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
