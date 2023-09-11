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
    [SerializeField] private LayerMask level2Door;
    [SerializeField] private LayerMask ExitLevel3;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private GameObject PauseMenu;
    public bool isCutScenePlaying = false;

    private Rigidbody lookAtObject = null;
    private GameObject lookAtLever = null;
    private bool isPlayerPaused = false;

    //------------------ [Kam added]-----------------------
    [SerializeField] private LayerMask Flashlight;
    [SerializeField] private GameObject FirstPersonCam;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private LayerMask LevelDoor;
    [SerializeField] private LayerMask EnemyDoor;
    [SerializeField] private GameObject noteUI;
    [SerializeField] private LayerMask NoteLayer;
    private PlayerMovement playerMovement;
    public Texture2D cursorHand;
    public bool noteHintChecked;
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //------------------ [Kam added]-----------------------
        playerMovement = GetComponent<PlayerMovement>();
        noteHintChecked = false;
        //------------------------------------------------------
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 20;

        if(Physics.Raycast(ray, out hit, rayMaxDistance, Key) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, Flashlight) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, LevelDoor) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, NoteLayer) ||
            Physics.Raycast(ray, out hit, 16, Lever) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, ExitLevel3))
        {
            Cursor.visible = true;
            Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            if (isPlayerPaused == false)
            {
                Cursor.visible = false;
            }
            
        }

        if (Physics.Raycast(ray, out hit, rayMaxDistance, Key))
        {
            mainText.text = "Left click to pick up Green Key";
        }
        //------------------ [Kam added]-----------------------
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Flashlight))
        {
            mainText.text = "Left click to pick up Flashlight";
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, LevelDoor))
        {
            if (GetComponent<Key>().doesPlayerHaveKey == true)
            {
                mainText.text = "Lab";
                if(Input.GetMouseButton(0))
                {
                    BackgroundNoise.instance.DoorSound();
                    SceneManager.LoadScene("Level_2");
                }
            }
            else
            {
                mainText.text = "Need Key to unlock";
            }
            lookAtObject = hit.rigidbody;
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, NoteLayer))
        {
            if (SceneManager.GetActiveScene().name == "Level_1")
            {
                if (!noteHintChecked)
                {
                    mainText.text = "Left click to read";
                }
                else if (noteHintChecked)
                {
                    mainText.text = "";
                }
            }
            if (SceneManager.GetActiveScene().name == "Level_2" || SceneManager.GetActiveScene().name == "Level_3")
            {
                mainText.text = "";
            }  

            if (Input.GetMouseButtonUp(0))
            {
                BackgroundNoise.instance.NoteSound();
                noteHintChecked = true;

                if (noteUI.activeInHierarchy == false)
                {
                    noteUI.SetActive(true);
                    playerMovement.canPlayerMove = false;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                }
                else if (noteUI.activeInHierarchy == true)
                {
                    noteUI.SetActive(false);
                    playerMovement.canPlayerMove = true;
                    Time.timeScale = 1;
                }

            }

            
        }
        //-----------------------------------------------------
        else if (Physics.Raycast(ray, out hit, 16, Lever))
        {
            lookAtLever = hit.collider.gameObject;
            if (lookAtLever.GetComponent<LeverPulled>().hasLeverBeenPulled == false)
            {
                mainText.text = "Left Click To Pull Lever";
            }
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, ExitLevel3))
        {
            if (GetComponent<Lever>().numberOfLeverPulled == 2)
            {
                mainText.text = "Left click to Basement";
                if (Input.GetMouseButtonDown(0))
                {
                    BackgroundNoise.instance.DoorSound();

                    SceneManager.LoadScene("Credits");
                }
            }
            else
            {
                mainText.text = "Basement Door is Closed";
            }
        }
        else
        {
            mainText.text = "";
            hintText.text = "";
            lookAtObject = null;
            lookAtLever = null;
        }
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
        isPlayerPaused = true;
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
        isPlayerPaused = false;
    }
}
