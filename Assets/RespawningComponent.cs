using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningComponent : MonoBehaviour
{
    HashSet <string> savedPickups = new HashSet<string>();

    [SerializeField]
    RespawnPoint lastSave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterPickup(string pickupID) {
        savedPickups.Add(pickupID);
        Debug.LogWarningFormat("{0}", pickupID);
    }

    public void SetLastSave(RespawnPoint save) {
        lastSave = save;
    }
    
    public void StartRespawnPlayer() {
        Debug.LogWarning("respawning");
        this.gameObject.transform.position = lastSave.GetRespawnPosition().transform.position;
        SendMessage("RespawnPlayer");
        GetComponentInChildren<JumpComponent>().RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
