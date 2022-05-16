using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rooms
{
    public List<GameObject> someRooms;//This will hold all of our rooms
}
[System.Serializable]
public class RoomList
{
    public List<Rooms> allRooms;//This will hold all of our rooms
}
[System.Serializable]
public class RoomLocations
{
    public List<Vector3> roomLocations;
}
