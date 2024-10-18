using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using SpaceEngineers.Enums;

public class Room : MonoBehaviour
{
    Image healthbar;
    public Slider slider;

    public UnitType room;
    public float maxHealth = 100f;
    private float health;
    private float time;
    public float defaultHealthReductionSpeedPerSec = 1f;
    public float currentHealthDelta = 0;
    public float healthReductionAnimSpeed = 2f;

    [SerializeField] private List<GameObject> unitsInRoomList = new List<GameObject>();

    private void Start()
    {
        health = maxHealth;
        healthbar = GetComponent<Healthbar>().healthbar;
        currentHealthDelta = -defaultHealthReductionSpeedPerSec;
    }

    private void Update()
    {
        HealthTick();
        UpdateHealthbar();
    }

    void HealthTick()
    {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            time = 0.0f;

            health = Mathf.Clamp(health + currentHealthDelta, 0f, maxHealth);
            Debug.Log("Health: " + health);
        }

    }

    void UpdateHealthbar()
    {
        float target = Mathf.Clamp(health / maxHealth, 0f, 1f);

        slider.value = target;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Engineer"))
        {
            var unit = other.gameObject.GetComponent<Unit>();
            unitsInRoomList.Add(other.gameObject);

            currentHealthDelta += unit.healModifiers[room];

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Engineer"))
        {
            var unit = other.gameObject.GetComponent<Unit>();
            unitsInRoomList.Remove(other.gameObject);

            currentHealthDelta -= unit.healModifiers[room];
        }
    }

    public float GetHealth() { return health; }
}
