using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private LayerMask Key;
    [SerializeField] private TMP_Text mainText;
    [SerializeField] private GameObject KeyImage;
    [SerializeField] private GameObject PauseMenu;

    private Rigidbody lookAtObject = null;
    private bool doesPlayerHaveKey;
    private bool canPlayerPickupKey;
    // Start is called before the first frame update
    void Start()
    {
        doesPlayerHaveKey = false;
        canPlayerPickupKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (canPlayerPickupKey == true)
        {
            PickKeyUp();
        }
        if (doesPlayerHaveKey == true)
        {
            ActivateEnemy();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 8;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, Key))
        {
            mainText.text = "Left click to pick up key";
            canPlayerPickupKey = true;
            lookAtObject = hit.rigidbody;
        }
        else
        {
            mainText.text = "";
            canPlayerPickupKey = false;
            lookAtObject = null;
        }
    }
    private void PickKeyUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(lookAtObject.gameObject);
            doesPlayerHaveKey = true;
            KeyImage.SetActive(true);
        }
    }
    private void ActivateEnemy()
    {
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
