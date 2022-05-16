using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SavePoint : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            Debug.Log("Destroying SpawnPoint");
            Destroy(collision);
        }
        if (collision.CompareTag("Room"))
        {
            Debug.Log("Destroying Room");
            Destroy(this);
        }
    }
}
