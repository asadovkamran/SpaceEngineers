using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{
    
    public Vector3 inputVec;
    public float cameraSpeed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = inputVec * cameraSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + velocity);       
    }

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector3>();
    }
}
