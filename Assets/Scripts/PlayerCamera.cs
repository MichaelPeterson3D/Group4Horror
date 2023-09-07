using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private Transform orientation;
    public bool allowCamToMove;
    private float xRotation;
    private float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        allowCamToMove = true;
        if (SceneManager.GetActiveScene().name == "Level_2")
        {
            yRotation = -90.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (allowCamToMove == true)
        {
            float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
            float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * -ySensitivity;

            yRotation += xMouse;
            xRotation += yMouse;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
