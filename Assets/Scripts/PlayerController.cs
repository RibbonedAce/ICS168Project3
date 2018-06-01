using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    #region Variables
    private float Health;
    private GameObject[] clients;

    public List<Vector3> spawnLocations;
    #endregion

    #region Properties

    #endregion

    #region Events
    void Awake()
    { 
        clients = GameObject.FindGameObjectsWithTag("Player");
        Health = 50f;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.rawPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Build(ray, hit);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Build(ray, hit);
            }
        }
	}
    #endregion

    #region Methods
    private void Build(Ray ray, RaycastHit hit)
    {
        /*GameObject g = Instantiate
          (
               GameController.Instance.buildings[Random.Range(0, GameController.Instance.buildings.Length)],
               GameController.Instance.buildings[0],
               hit.point.MoveToAxisPosition(Axis.Y, 1, ray.direction), 
               Quaternion.identity
          );*/
        //NetworkServer.Spawn(g);
        CmdSendBuildingInfo(GameController.Instance.buildings[0].name, hit.point.MoveToAxisPosition(Axis.Y, 1, ray.direction), Quaternion.identity);
    }

    [Command]
    void CmdSendBuildingInfo(string name,Vector3 pos,Quaternion rot)
    {
        GameObject r;
        switch (name)
        {
            case "Common":
                r = Instantiate(GameController.Instance.buildings[0], pos, rot);
                NetworkServer.Spawn(r);
                break;
            case "Uncommon":
                r = Instantiate(GameController.Instance.buildings[1], pos, rot);
                NetworkServer.Spawn(r);
                break;
            case "Rare":
                r = Instantiate(GameController.Instance.buildings[2], pos, rot);
                NetworkServer.Spawn(r);
                break;
            case "Mythic":
                r = Instantiate(GameController.Instance.buildings[3], pos, rot);
                NetworkServer.Spawn(r);
                break;
            default:
                break;
        }
    }

    private void SpawnPlayer()
    {
        spawnLocations = new List<Vector3>
        (
            new Vector3[] { new Vector3(-8.5f, 3f, 0), new Vector3(8.5f, 3f, 0) }
        );
        if (clients.Length > 1)
            transform.position = spawnLocations[1];
        else transform.position = spawnLocations[0];
    }
    #endregion

    #region Coroutines

    #endregion
}
