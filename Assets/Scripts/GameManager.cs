using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] rooms;
    public enum RoomType { red, green, blue };
    public RoomType room;

    public Dictionary<RoomType, float> roomData = new Dictionary<RoomType, float>();

    private void Start()
    {
        foreach (var room in rooms) 
        {
            RoomType type = (GameManager.RoomType)room.GetComponent<Room>().room;
            float health = room.GetComponent<Room>().GetHealth();

            roomData.Add(type, health);
        }
    }
}
