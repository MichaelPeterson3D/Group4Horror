using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private List<GameObject> keylocations = new List<GameObject>();
    [SerializeField] private LayerMask keyMask;
    [SerializeField] private AudioSource keySoundEffect;
    [SerializeField] private AudioSource doorBreakingDownSoundEffect;
    [SerializeField] private GameObject keyImage;
    [SerializeField] private Animator doorFalling;
    [SerializeField] private GameObject enemy;

    public bool doesPlayerHaveKey;
    private GameObject lookAtObject = null;
    private bool canPlayerPickUpKey;
    private PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
        if (canPlayerPickUpKey == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPickedUpKey();
            }
        }
    }
    private void SetUp()
    {
        for (int i = 0; i < keylocations.Count; i++)
        {
            keylocations[i].SetActive(false);
        }
        playerActions = GetComponent<PlayerActions>();
        canPlayerPickUpKey = false;
        doesPlayerHaveKey = false;
        SpawnKey();
    }
    private void SpawnKey()
    {
        int randomNum;
        randomNum = Random.Range(0, keylocations.Count);
        keylocations[randomNum].SetActive(true);
    }
    private void CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int rayMaxDistance = 20;

        if (Physics.Raycast(ray, out hit, rayMaxDistance, keyMask))
        {
            lookAtObject = hit.collider.gameObject;
            canPlayerPickUpKey = true;
        }
        else
        {
            canPlayerPickUpKey = false;
            lookAtObject = null;
        }
    }
    private void PlayerPickedUpKey()
    {
        keySoundEffect.Play();
        Destroy(lookAtObject);
        keyImage.SetActive(true);
        StartCoroutine(StartEnemySequence());
        canPlayerPickUpKey = false;
        doesPlayerHaveKey = true;
    }
    private IEnumerator StartEnemySequence()
    {
        //DoorUp = false;

        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(.1f);

        GetComponent<VirtualCam>().LookAtTarget();

        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(1.0f);
        doorBreakingDownSoundEffect.Play();

        yield return new WaitForSeconds(2.5f);

        doorBreakingDownSoundEffect.Play();
        yield return new WaitForSeconds(0.5f);

        doorFalling.SetBool("StartDoor", true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<VirtualCam>().ResumeAction();
        yield return new WaitForSeconds(2.0f);
        playerActions.ResumeAllEnemies();
        enemy.GetComponent<EnemyMovement>().canEnemyBeMoved = true;
    }
}
