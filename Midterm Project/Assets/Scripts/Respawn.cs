using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Respawn RespawnInstance;
    // Start is called before the first frame update
    void Start()
    {
        RespawnInstance = this;
    }

    public void RespawnAtLevel(int level,GameObject player)
    {
        Debug.Log(player.name);
        Transform respawnPoint = gameObject.transform.Find("Level" + level + " Respawn");
        while(player.transform.position != respawnPoint.position)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, respawnPoint.position, 1f);

        }
    }
}
