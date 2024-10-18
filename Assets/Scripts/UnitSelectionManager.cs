using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance;
    public LayerMask clickable;

    public List<GameObject> selectedUnits = new List<GameObject>();

    UnityEngine.Camera camera;

    private void Start()
    {
        camera = UnityEngine.Camera.main;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    ClearSelectedUnits();
                } 
                
                GameObject unit = hit.collider.gameObject;
                SelectUnit(unit);
            } else
            {
                ClearSelectedUnits();
            }
        }

    }

    void ClearSelectedUnits()
    {
        foreach (var unit in selectedUnits)
        {
            unit.GetComponent<UnitMovement>().enabled = false;
            unit.transform.GetChild(0).gameObject.SetActive(false);
        }
        selectedUnits.Clear();
    }

    void SelectUnit(GameObject unit)
    {
        if (selectedUnits.Contains(unit)) return;

        selectedUnits.Add(unit);
        unit.GetComponent<UnitMovement>().enabled = true;
        unit.transform.GetChild(0).gameObject.SetActive(true);
    }

}
