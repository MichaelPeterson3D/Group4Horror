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
    [SerializeField] private PlayerMovement playerMovement;
    public Texture2D cursorHand;
    public bool noteHintChecked;
    [SerializeField] private LayerMask Note1;
    [SerializeField] private LayerMask Note2;
    [SerializeField] private LayerMask Note3;
    public GameObject wordsObject;
    public TextMeshProUGUI wordsText;

    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        wordsText = wordsObject.GetComponent<TextMeshProUGUI>();
        Time.timeScale = 1;
        //------------------ [Kam added]-----------------------
        //playerMovement = GetComponent<PlayerMovement>();
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
            Physics.Raycast(ray, out hit, rayMaxDistance, ExitLevel3) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, Note1) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, Note2) ||
            Physics.Raycast(ray, out hit, rayMaxDistance, Note3))
        {
            Cursor.visible = true;
            Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            if (isPlayerPaused == false)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
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
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Note1))
        {
            if (Input.GetMouseButtonUp(0))
            {
                wordsText.text = "The creatures are loose! The generator lost power; I must have been using it too much. I was able to restart the systems, but many areas are still not turning on. Good thing I added in preventive measures so the creatures wouldn�t escape. The bad thing is that I am also locked in here. I will have to find those old levers; the lights should guide my way.";

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
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Note2))
        {
            if (Input.GetMouseButtonUp(0))
            {
                wordsText.text = "The trees� even the trees are watching me. I can hear the creatures growling. Through my studies, I found the creatures at this stage are affected by light or was it sound? I wish I had a flashlight to test it, but I don�t want to go back through the lab. Those creatures were lying in wait for me to make another slip up. ";

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
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
        else if (Physics.Raycast(ray, out hit, rayMaxDistance, Note3))
        {
            if (Input.GetMouseButtonUp(0))
            {
                wordsText.text = "Hands and trees, hands and trees. Everywhere I look, I see hands. They were supposed to be my trusted servants. Hands that clean, hands that kill. I saw them� dragging servants away that were trying to escape. I watched from the forest. The trees see all and know all. They brought them to the lab� what unspeakable things could have happened in there. I fear I am next�  I can hear them. I must try to make it to the courtyard, but my knees weren�t what they used to be.";

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
                    Cursor.lockState = CursorLockMode.Locked;
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
                mainText.text = "Left click to escape.";
                if (Input.GetMouseButtonDown(0))
                {
                    BackgroundNoise.instance.DoorSound();

                    SceneManager.LoadScene("Credits");
                }
            }
            else
            {
                mainText.text = "The Door is Closed";
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
        GetComponent<PlayerMovement>().canPlayerMove = false;
        isPlayerPaused = true;
        PauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void Resumegame()
    {
        GetComponent<PlayerMovement>().canPlayerMove = true;
        PauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isPlayerPaused = false;
    }
}
