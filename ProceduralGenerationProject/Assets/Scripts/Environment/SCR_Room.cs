using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SCR_Room : MonoBehaviour
{
    [SerializeField] public bool hasTop;
    [SerializeField] public bool hasBottom;
    [SerializeField] public bool hasLeft;
    [SerializeField] public bool hasRight;
    [SerializeField] public int possibleSpawns;

    private void Start()
    {
        Debug.Log(this.tag);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "SpawnPoint")
        {
            Debug.Log("Destroying SpawnPoint");
            Destroy(collision);
        }
        if (collision.tag == "Room")
        {
            Debug.Log("Destroying Room");
            Destroy(this);
        }
       
    }
}
