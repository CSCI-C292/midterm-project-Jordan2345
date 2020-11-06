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

    public IEnumerator RespawnAtLevel(int level,GameObject player)
    {
        Debug.Log(player.name);
        Transform respawnPoint = gameObject.transform.Find("Level" + level + " Respawn");
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(.6f);
        float error = .2f;
        float currX = Mathf.Abs(player.transform.position.x-respawnPoint.position.x);
        float currY = Mathf.Abs(player.transform.position.y-respawnPoint.position.y);
        bool isWithinError = currX <= error && currY <= error;
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        while (!isWithinError)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, respawnPoint.position, 1f);
            currX = Mathf.Abs(player.transform.position.x - respawnPoint.position.x);
            currY = Mathf.Abs(player.transform.position.y - respawnPoint.position.y);
            isWithinError = currX <= error && currY <= error;
        }
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;

    }
}
