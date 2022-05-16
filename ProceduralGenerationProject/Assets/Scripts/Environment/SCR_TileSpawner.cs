using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TileSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> topRooms;//This will be our 0 room, it has to spawn below current room
    [SerializeField] private List<GameObject> bottomRooms;//This will be our 1 room, has to spawn above current room
    [SerializeField] private List<GameObject> leftRooms;//This will be our 2 room, has to spawn to the right of current room
    [SerializeField] private List<GameObject> rightRooms;//This will be our 3 room, has to spawn to the left of current room
    
    
    public RoomList roomContainer;
    [SerializeField] private GameObject startRoom;
    private int theRandRoom;
    private int theRandType;
    
    void Start()
    {   
        Instantiate(startRoom, this.transform);
        SpawnTiles(startRoom,startRoom.GetComponent<SCR_Room>().possibleSpawns);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnTiles(GameObject lastSpawned, int possibleSpawns)
    {
        GameObject spawnedRoom;
        for(int i = possibleSpawns; i > 0;)
        {
            theRandRoom = Random.Range(0, 3);
            theRandType = Random.Range(0, 7);
            spawnedRoom = roomContainer.allRooms[theRandRoom].someRooms[theRandType];
            if (theRandRoom == 0 && lastSpawned.GetComponent<SCR_Room>().hasBottom == true)//has to spawn beneath our current room
            {
                GameObject newRoom = Instantiate(spawnedRoom);
                newRoom.transform.position = lastSpawned.transform.position;
                newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x, newRoom.transform.localPosition.y - 10f, 0f);
                if(lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                { lastSpawned.GetComponent<SCR_Room>().possibleSpawns -= 1; }
                SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            else if (theRandRoom == 1 && lastSpawned.GetComponent<SCR_Room>().hasTop == true)//has to spawn beneath our current room
            {
                GameObject newRoom = Instantiate(spawnedRoom);
                newRoom.transform.position = lastSpawned.transform.position;
                newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x, newRoom.transform.localPosition.y + 10f, 0f);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                { lastSpawned.GetComponent<SCR_Room>().possibleSpawns -= 1; }
                SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            else if (theRandRoom == 2 && lastSpawned.GetComponent<SCR_Room>().hasRight == true)//has to spawn beneath our current room
            {
                GameObject newRoom = Instantiate(spawnedRoom);
                newRoom.transform.position = lastSpawned.transform.position;
                newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x + 16f, newRoom.transform.localPosition.y, 0f);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                { lastSpawned.GetComponent<SCR_Room>().possibleSpawns -= 1; }
                SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
            else if (theRandRoom == 3 && lastSpawned.GetComponent<SCR_Room>().hasLeft == true)//has to spawn beneath our current room
            {
                GameObject newRoom = Instantiate(spawnedRoom);
                newRoom.transform.position = lastSpawned.transform.position;
                newRoom.transform.localPosition = new Vector3(newRoom.transform.localPosition.x - 16f, newRoom.transform.localPosition.y, 0f);
                if (lastSpawned.GetComponent<SCR_Room>().possibleSpawns > 0)
                { lastSpawned.GetComponent<SCR_Room>().possibleSpawns -= 1; }
                SpawnTiles(newRoom, newRoom.GetComponent<SCR_Room>().possibleSpawns);
            }
        }
    }
}
