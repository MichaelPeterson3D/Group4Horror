using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private LayerMask Key;
    [SerializeField] private LayerMask Lever;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private GameObject KeyImage;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private Animator doorFalling;
    private Rigidbody lookAtObject = null;
    private bool doesPlayerHaveKey;
    private bool canPlayerPickupKey;
    private bool startEnemyCoroutine = false;

    //------------------ [Kam added]-----------------------
    [SerializeField] private LayerMask Flashlight;
    [SerializeField] private GameObject FirstPersonCam;
    private bool doesPlayerHaveFlashlight;
    private bool canPlayerPickupFlashlight;
    private Light beam;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private GameObject flashlightObject;
    [SerializeField] private LayerMask LevelDoor;
    [SerializeField] private LayerMask EnemyDoor;
    private bool canPlayerCheckDoor;
    private bool canPlayerUseDoor;
    private bool DoorUp;

    [SerializeField] private AudioSource keySoundEffect;
    [SerializeField] private AudioSource doorBreakingDownSoundEffect;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        doesPlayerHaveKey = false;
        canPlayerPickupKey = false;
        //------------------ [Kam added]-----------------------
        doesPlayerHaveFlashlight = false;
        canPlayerPickupFlashlight = false;
        canPlayerCheckDoor = false;
        canPlayerUseDoor = false;
        beam = FirstPersonCam.GetComponent<Light>();
        DoorUp = true;
        //------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (canPlayerPickupKey == true)
        {
            PickKeyUp();
        }
        if (doesPlayerHaveKey == true && startEnemyCoroutine == true)
        {
            StartCoroutine(StartEnemySequence(2.5f, 2.0f, 2.0f));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        //------------------ [Kam added]-----------------------
        if (canPlayerPickupFlashlight == true)
        {
            PickUpFlashlight();
        }
        if (doesPlayerHaveFlashlight == true)
        {
            hintText.text = "Press F to use Flashlight";
            if (Input.GetKeyDown(KeyCode.F))
            {
                beam.enabled = !beam.enabled;
                hintText.text = "";
            }
        }
        if (DoorUp == true && canPlayerCheckDoor == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hintText.text = "This door is locked.";
            }
            else
            {
                hintText.text = "";
            }
        }
        if (canPlayerUseDoor == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (doesPlayerHaveKey == true)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    hintText.text = "This door is locked.";
                }
            }
        }
        //-----------------------------------------------------
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 8;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, Key))
        {
            mainText.text = "Left click to pick up Second Floor Key";
            canPlayerPickupKey = true;
            lookAtObject = hit.rigidbody;
        }
        //------------------ [Kam added]-----------------------
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Flashlight))
        {
            mainText.text = "Left click to pick up Flashlight";
            canPlayerPickupFlashlight = true;
            lookAtObject = hit.rigidbody;
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, EnemyDoor))
        {
            mainText.text = "2F";
            canPlayerCheckDoor = true;
            lookAtObject = hit.rigidbody;
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, LevelDoor))
        {
            mainText.text = "2F";
            canPlayerUseDoor = true;
            lookAtObject = hit.rigidbody;
        }
        //-----------------------------------------------------
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Lever))
        {
            mainText.text = "Left Click To Pull Lever";
        }
        else
        {
            mainText.text = "";
            hintText.text = "";
            canPlayerPickupKey = false;
            canPlayerPickupFlashlight = false;
            lookAtObject = null;
        }
    }
    
    //------------------ [Kam added]-----------------------
    private void PickUpFlashlight()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(flashlightObject);
            doesPlayerHaveFlashlight = true;
        }
    }
    //-----------------------------------------------------

    private void PickKeyUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //------------------ [Kam added]-----------------------
            keySoundEffect.Play();
            //-----------------------------------------------------

            Destroy(lookAtObject.gameObject);
            doesPlayerHaveKey = true;
            startEnemyCoroutine = true;
            KeyImage.SetActive(true);
        }
    }
    IEnumerator StartEnemySequence(float sec1, float sec2, float sec3)
    {
        DoorUp = false;
        startEnemyCoroutine = false;
        
        //------------------ [Kam added]-----------------------
        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(.1f);
        //-----------------------------------------------------

        GetComponent<VirtualCam>().LookAtTarget();

        //------------------ [Kam added]-----------------------
        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(1f);
        doorBreakingDownSoundEffect.Play();
        //-----------------------------------------------------

        yield return new WaitForSeconds(sec1);

        //------------------ [Kam added]-----------------------
        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(0.5f);
        //-----------------------------------------------------

        doorFalling.Play("DoorFallingDown");
        yield return new WaitForSeconds(sec2);
        GetComponent<VirtualCam>().ResumeAction();
        yield return new WaitForSeconds(sec3);
        ResumeAllEnemies();
    }
    public void StopAllEnemies()
    {
        for (int i = 0; i < enemyLists.Count; i++)
        {
            if (enemyLists[i].activeInHierarchy == true)
            {
                enemyLists[i].GetComponent<EnemyMovement>().StopEnemy();
            }
        }
    }
    public void ResumeAllEnemies()
    {
        for (int i = 0; i < enemyLists.Count; i++)
        {
            if (enemyLists[i].activeInHierarchy == true)
            {
                enemyLists[i].GetComponent<EnemyMovement>().ResumeEnemy();
            }
        }
    }
    private void PauseGame()
    {
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void Resumegame()
    {
        PauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
