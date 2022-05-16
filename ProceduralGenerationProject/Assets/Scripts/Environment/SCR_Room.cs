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

    private void OnCollisionEnter2d(Collider2D collision)
    {
        Debug.Log("Destroying SpawnPoint");
        if(collision.CompareTag("SpawnPoint"))
        {
            Destroy(collision);
        }
        if(collision.CompareTag("Room"))
        {
            Destroy(this);
        }
       
    }
}
