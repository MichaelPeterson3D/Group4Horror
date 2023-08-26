using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyLists = new List<GameObject>();
    [SerializeField] private Image vignette;
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
            Debug.Log("Safe");
        }
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
            Debug.Log("NotSafe");
        }
    }
}
