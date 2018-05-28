using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerController : NetworkBehaviour {

#region Attributes
    private float Health;

    public List<GameObject> Buildings;
#endregion

    void Awake()
    {
        transform.position = new Vector3(-8.5f,1f,0);
        Buildings = new List<GameObject>();
        Health = 50f;
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;
        Build();
	}

    private void Build()
    {
    }
}
