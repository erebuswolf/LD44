using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathComponent : MonoBehaviour
{
    bool playerIsdying;

    public void PlayerDies() {
        if(playerIsdying) {
            return;
        }
        playerIsdying = true;
        SendMessage("KillPlayer");
        GetComponentInChildren<JumpComponent>().KillPlayer();
        StartCoroutine(deathCoroutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator deathCoroutine() {
        yield return new WaitForSeconds(1);
        GetComponent<RespawningComponent>().StartRespawnPlayer();
        playerIsdying = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
