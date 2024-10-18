using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    Image healthbar;
    public Slider slider;

    public enum RoomType { red, green, blue };
    public RoomType room;
    public float maxHealth = 100f;
    private float health;
    private float time;
    public float defaultHealthReductionSpeed;
    public float currentHealAmount = 0;
    public float healthReductionAnimSpeed = 2f;
    private bool shouldHeal = false;

    [SerializeField] private List<GameObject> unitsInRoomList = new List<GameObject>();

    private void Start()
    {
        health = maxHealth;
        healthbar = GetComponent<Healthbar>().healthbar;
    }

    private void Update()
    {
        shouldHeal =  unitsInRoomList.Count == 0 ? false : true;

        if (!shouldHeal)
        {
            ReduceHealthEverySecond(defaultHealthReductionSpeed);
        } else
        {
            HealEverySecond();
        }
        
        Debug.Log(currentHealAmount);

        UpdateHealthbar();
    }
    
    void ReduceHealthEverySecond(float amount)
    {
        time += Time.deltaTime;

        

        if (time > 1.0f)
        {
            time = 0.0f;

            health -= amount;

        }
    }


    void HealEverySecond()
    {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            time = 0.0f;

            health += currentHealAmount;
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

            if ((int)room == (int)unit.GetComponent<Unit>().type)
            {
                currentHealAmount += unit.fixAmount;
            } else
            {
                currentHealAmount += unit.fixAmount / 2.0f;
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Engineer"))
        {
            var unit = other.gameObject.GetComponent<Unit>();
            unitsInRoomList.Remove(other.gameObject);

            if ((int)room == (int)unit.GetComponent<Unit>().type)
            {
                currentHealAmount -= unit.fixAmount;
            }
            else
            {
                currentHealAmount -= unit.fixAmount / 2.0f;
            }
        }
    }

    public float GetHealth() { return health; }
}
