using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    UnityEngine.Camera camera;
    NavMeshAgent agent;
    public LayerMask layerMask;

    private void Start()
    {
        camera = UnityEngine.Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RaycastHit hit;
        
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) 
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
