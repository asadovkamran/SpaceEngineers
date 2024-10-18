using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public enum RoomType { red, green, blue };
    public RoomType room;

    public Dictionary<RoomType, float> roomData = new Dictionary<RoomType, float>();
    public int secondsLeft = 3;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI winText;

    private float time = 0;
    private void Start()
    {
        UpdateCountdown(secondsLeft);
        foreach (var room in rooms)
        {
            RoomType type = (GameManager.RoomType)room.GetComponent<Room>().room;
            float health = room.GetComponent<Room>().GetHealth();

            roomData.Add(type, health);
        }
    }

    private void Update()
    {
        SecondTick();
    }

    void SecondTick()
    {
        time += Time.deltaTime;

        if (time > 1.0f)
        {
            time = 0.0f;

            secondsLeft--;
            UpdateCountdown(secondsLeft);
        }

        CheckWinCondition();
    }

    void UpdateCountdown(int secondsRemaining)
    {
        countDownText.text = "Time left: " + secondsRemaining;
    }

    void CheckWinCondition()
    {
        if (secondsLeft <= 0)
        {
            Time.timeScale = 0;
            winText.gameObject.SetActive(true);
        }
    }
}
