using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TileSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> topRooms;//This will be our 0 room, it has to spawn below current room
    [SerializeField] private List<GameObject> bottomRooms;//This will be our 1 room, has to spawn above current room
    [SerializeField] private List<GameObject> leftRooms;//This will be our 2 room, has to spawn to the right of current room
    [SerializeField] private List<GameObject> rightRooms;//This will be our 3 room, has to spawn to the left of current room

    private List<Vector3> alreadySpawned;
    public RoomList roomContainer;
    [SerializeField] private GameObject startRoom;
    private int theRandRoom;
    private int theRandType;
    private int maxRooms;
    
    void Start()
    {
        alreadySpawned = new List<Vector3>();
        maxRooms = 40;
        Instantiate(startRoom, this.transform);
        alreadySpawned.Add(startRoom.transform.localPosition);
        StartSpawning();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void StartSpawning()
    {
        for (int i = 0; i < 4; i++)
        {
            int coo = Random.Range(0, 6);
            if(i == 0)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);
                spawnedRoom.transform.position = startRoom.transform.position;
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x, spawnedRoom.transform.localPosition.y - 10f, 0f);
                alreadySpawned.Add(spawnedRoom.transform.localPosition);
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            if (i == 1)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);
                spawnedRoom.transform.position = startRoom.transform.position;
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x, spawnedRoom.transform.localPosition.y + 10f, 0f);
                alreadySpawned.Add(spawnedRoom.transform.localPosition);
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            if (i == 2)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);
                spawnedRoom.transform.position = startRoom.transform.position;
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x + 16f, spawnedRoom.transform.localPosition.y, 0f);
                alreadySpawned.Add(spawnedRoom.transform.localPosition);
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            if (i == 3)
            {
                GameObject spawnedRoom = Instantiate(roomContainer.allRooms[i].someRooms[theRandType]);
                spawnedRoom.transform.position = startRoom.transform.position;
                spawnedRoom.transform.localPosition = new Vector3(spawnedRoom.transform.localPosition.x - 16f, spawnedRoom.transform.localPosition.y, 0f);
                alreadySpawned.Add(spawnedRoom.transform.localPosition);
                SpawnTiles(spawnedRoom, spawnedRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
        }
    }
    private void SpawnTiles(GameObject lastSpawned, int possibleSpawns)
    {
        GameObject spawnedRoom;
        //for(int i = possibleSpawns; i > 0;)
        //{
            if (maxRooms > 0)
            {
                theRandRoom = Random.Range(0, 3);
                theRandType = Random.Range(0, 6);
                spawnedRoom = roomContainer.allRooms[theRandRoom].someRooms[theRandType];
                if (theRandRoom == 0 && lastSpawned.GetComponent<SCR_Room>().hasBottom == true)//has to spawn beneath our current room
                {
                    GameObject newRoom = Instantiate(spawnedRoom);
                    newRoom.transform.position = lastSpawned.transform.position;
                    newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x, newRoom.transform.localPosition.y - 10f, 0f);
                    if(alreadySpawned.Contains(newRoom.transform.localPosition))
                    {
                        Destroy(newRoom);
                        SpawnTiles(lastSpawned, possibleSpawns);
                        return;
                    }
                alreadySpawned.Add(newRoom.transform.localPosition);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                    { lastSpawned.GetComponent<SCR_Room>().possibleSpawns = lastSpawned.GetComponent<SCR_Room>().possibleSpawns - 1; }
                    SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
                }
                else if (theRandRoom == 1 && lastSpawned.GetComponent<SCR_Room>().hasTop == true)//has to spawn beneath our current room
                {
                    GameObject newRoom = Instantiate(spawnedRoom);
                    newRoom.transform.position = lastSpawned.transform.position;
                    newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x, newRoom.transform.localPosition.y + 10f, 0f);
                if (alreadySpawned.Contains(newRoom.transform.localPosition))
                {
                    Destroy(newRoom);
                    SpawnTiles(lastSpawned, possibleSpawns);
                    return;
                }
                alreadySpawned.Add(newRoom.transform.localPosition);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                    { lastSpawned.GetComponent<SCR_Room>().possibleSpawns = lastSpawned.GetComponent<SCR_Room>().possibleSpawns - 1; }
                    SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
                }
                else if (theRandRoom == 2 && lastSpawned.GetComponent<SCR_Room>().hasRight == true)//has to spawn beneath our current room
                {
                    GameObject newRoom = Instantiate(spawnedRoom);
                    newRoom.transform.position = lastSpawned.transform.position;
                    newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x + 16f, newRoom.transform.localPosition.y, 0f);
                if (alreadySpawned.Contains(newRoom.transform.localPosition))
                {
                    Destroy(newRoom);
                    SpawnTiles(lastSpawned, possibleSpawns);
                    return;
                }
                alreadySpawned.Add(newRoom.transform.localPosition);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                    { lastSpawned.GetComponent<SCR_Room>().possibleSpawns = lastSpawned.GetComponent<SCR_Room>().possibleSpawns - 1; }
                    SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
                }
                else if (theRandRoom == 3 && lastSpawned.GetComponent<SCR_Room>().hasLeft == true)//has to spawn beneath our current room
                {
                    GameObject newRoom = Instantiate(spawnedRoom);
                    newRoom.transform.position = lastSpawned.transform.position;
                    newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x - 16f, newRoom.transform.localPosition.y, 0f);
                if (alreadySpawned.Contains(newRoom.transform.localPosition))
                {
                    Destroy(newRoom);
                    SpawnTiles(lastSpawned, possibleSpawns);
                    return;
                }
                alreadySpawned.Add(newRoom.transform.localPosition);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                    { lastSpawned.GetComponent<SCR_Room>().possibleSpawns = lastSpawned.GetComponent<SCR_Room>().possibleSpawns - 1; }
                    SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
                }
                maxRooms--;
            }
            else
            {
                possibleSpawns = possibleSpawns - 1;
            }
        //}
    }
}
