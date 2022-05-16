using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /*  
    AUTHOR - Johnathan Bates
    CLASS NAME: SCR_TileSpawner
    This is the Tile Spawner Class. It's central to the procedural generation of the map. 
    NEEDS FIX:
    1. Rooms can spawn on top of each other. Need a method to figure out if a room is already in a position where I'm trying to spawn a room and deal with it.
    2. Rooms spawn incorrectly next to each other. There is no check in place to see if a room spawns next to another, if that room can connect.
    */
public class SCR_TileSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> topRooms;//This will be our 0 room, it has to spawn below current room
    [SerializeField] private List<GameObject> bottomRooms;//This will be our 1 room, has to spawn above current room
    [SerializeField] private List<GameObject> leftRooms;//This will be our 2 room, has to spawn to the right of current room
    [SerializeField] private List<GameObject> rightRooms;//This will be our 3 room, has to spawn to the left of current room

    private List<Vector3> alreadySpawned;
    public RoomList roomContainer;
    /*spawnedRoomContainer will hold all the rooms that have spawned. As rooms are spawning, this container will be referenced to see if a room exist above, below, to the left or right
      If a room does exist, checks to need be done to see if the current room has an opening as well as the located room.*/
    private RoomList spawnedRoomContainer;
    [SerializeField] private GameObject startRoom;
    private int theRandRoom;
    private int theRandType;
    private int maxRooms;

    void Start()
    {
        spawnedRoomContainer = new RoomList();
        alreadySpawned = new List<Vector3>();
        maxRooms = 40;
        Instantiate(startRoom, this.transform);
        alreadySpawned.Add(startRoom.transform.position);
        StartSpawning();
    }
    // Update is called once per frame
    void Update()
    {

    }
    /*
    Author - Johnathan Bates
    FUNCTION NAME:StartSpawning()
    FUNCTION TYPE:Mutator
    FUNCTION VARIABLES: GameObject spawnedRoom, public RoomList roomContainer, private List<Vector3> alreadySpawned
    FUNCTION CALLS: private void SpawnTiles(GameObject lastSpawned, int possibleSpawns)
    FUNCTION DESCRIPTION: StartSpawning() will be used at the start of the scene to create the center room, as well as the neighboring rooms above, below, to the left and right of the central room. 
    From there, each room that offshoots from the central room will make a call to the SpawnTiles() function, which will each begin to spawn rooms randomly. 
    */
    private void StartSpawning()
    {
        for (int i = 0; i < 4; i++)//Cycle through till all four rooms are instantiated, top, bottom, left and right
        {
            if (i == 0)//This will spawn a room with a top opening, thus it must spawn below our central room
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);//Creates the room.
                spawnedRoom.transform.position = startRoom.transform.position;//Initializes the position to be the room that spawned it
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x, spawnedRoom.transform.localPosition.y - 10f, 0f);//Moves the room appropriately
                alreadySpawned.Add(spawnedRoom.transform.position);//This is the container that holds the locations of rooms
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);//Begin spawning offshoot rooms
            }
            if (i == 1)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);//Creates the room.
                spawnedRoom.transform.position = startRoom.transform.position;//Initializes the position to be the room that spawned it
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x, spawnedRoom.transform.localPosition.y + 10f, 0f);//Moves the room appropriately
                alreadySpawned.Add(spawnedRoom.transform.position);//This is the container that holds the locations of rooms
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);//Begin spawning offshoot rooms
            }
            if (i == 2)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);//Creates the room.
                spawnedRoom.transform.position = startRoom.transform.position;//Initializes the position to be the room that spawned it
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x + 16f, spawnedRoom.transform.localPosition.y, 0f);//Moves the room appropriately
                alreadySpawned.Add(spawnedRoom.transform.position);//This is the container that holds the locations of rooms
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);//Begin spawning offshoot rooms
            }
            if (i == 3)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);//Creates the room.
                spawnedRoom.transform.position = startRoom.transform.position;//Initializes the position to be the room that spawned it
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x - 16f, spawnedRoom.transform.localPosition.y, 0f);//Moves the room appropriately
                alreadySpawned.Add(spawnedRoom.transform.position);//This is the container that holds the locations of rooms
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);//Begin spawning offshoot rooms
            }
        }
    }
    /*
    Author - Johnathan Bates
    FUNCTION NAME:SpawnTiles(GameObject lastSpawned, int possibleSpawns)
    FUNCTION TYPE:Mutator
    FUNCTION VARIABLES: GameObject spawnedRoom, private int theRandRoom, private int theRandType, public RoomList roomContainer, private List<Vector3> alreadySpawned, GameObject newRoom
    FUNCTION CALLS: private void MakeRoom(GameObject lastSpawned, GameObject spawnedRoom, int possibleSpawns, Vector3 offset)
    FUNCTION DESCRIPTION: SpawnTiles() will be used to continue spawning rooms after the initial StartSpawning() call. This is where the procedural generation is beginning to happen.
    This occurs due to the nesting of SpawnTiles() that is within the MakeRoom() Function.
    */
    private void SpawnTiles(GameObject lastSpawned, int possibleSpawns)
    {
        GameObject spawnedRoom;
        for (int i = maxRooms; i > 0;)
        {
            if (maxRooms > 0)
            {
                theRandRoom = Random.Range(0, 3);
                theRandType = Random.Range(0, 6);
                spawnedRoom = roomContainer.allRooms[theRandRoom].someRooms[theRandType];
                if (theRandRoom == 0 && lastSpawned.GetComponent<SCR_Room>().hasBottom == true)//has to spawn beneath our current room
                { MakeRoom(lastSpawned, spawnedRoom, possibleSpawns, new Vector3(lastSpawned.transform.localPosition.x, lastSpawned.transform.localPosition.y - 10f, 0f)); }
                else if (theRandRoom == 1 && lastSpawned.GetComponent<SCR_Room>().hasTop == true)//has to spawn beneath our current room
                { MakeRoom(lastSpawned, spawnedRoom, possibleSpawns, new Vector3(lastSpawned.transform.localPosition.x, lastSpawned.transform.localPosition.y + 10f, 0f)); }
                else if (theRandRoom == 2 && lastSpawned.GetComponent<SCR_Room>().hasRight == true)//has to spawn beneath our current room
                { MakeRoom(lastSpawned, spawnedRoom, possibleSpawns, new Vector3(lastSpawned.transform.localPosition.x + 16f, lastSpawned.transform.localPosition.y, 0f)); }
                else if (theRandRoom == 3 && lastSpawned.GetComponent<SCR_Room>().hasLeft == true)//has to spawn beneath our current room
                { MakeRoom(lastSpawned, spawnedRoom, possibleSpawns, new Vector3(lastSpawned.transform.localPosition.x - 16f, lastSpawned.transform.localPosition.y, 0f)); }
                maxRooms--;
                Debug.Log(maxRooms);
            }
            else if(maxRooms <= 0)
            {
                return;
            }
            /*else
            {
                possibleSpawns = possibleSpawns - 1;
            }*/
        }
    }
    private void MakeRoom(GameObject lastSpawned, GameObject spawnedRoom, int possibleSpawns, Vector3 offset)
    {
        GameObject newRoom = Instantiate(spawnedRoom);
        newRoom.transform.position = lastSpawned.transform.position;
        newRoom.transform.localPosition = offset;
        if (alreadySpawned.Contains(newRoom.transform.position))
        {
            Destroy(newRoom);
            SpawnTiles(lastSpawned, possibleSpawns);
            return;
        }
        if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
        { lastSpawned.GetComponent<SCR_Room>().possibleSpawns = lastSpawned.GetComponent<SCR_Room>().possibleSpawns - 1; }
        SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
    }
}
