using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReferenceConfirmation()
    {
        Debug.Log("The DummyPlayer is currently being referenced to by the Manager.");
    }

    public void PunishmentForPlayer()
    {
        //Will named poorly, this can be replaced later with whatever is in the real Player controller, or however else the player is to be punished.
        Debug.Log("THe DummyPlayer has correctly been punished.");
    }

}
