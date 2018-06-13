using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour
{
    #region Variables
    [SyncVar(hook = "SetHealth")]
    private float health;
    private GameObject[] clients;
    private List<Vector3> built;
    private float nextTime;//if currentTime > nextTime, add 1 gold.
    private Transform text;
    private GameObject canvas;
    private AudioSource _audioSource;

    [SerializeField]
    private int pID;
    [SerializeField]
    private float gold;
    [SerializeField]
    private float goldRatePerSec;

    public AudioClip[] clips;
    public List<Vector3> spawnLocations;
    public Grid grid;
    #endregion

    #region Properties

    #endregion

    #region Events
    public override void OnStartServer()
    {
        base.OnStartServer();
        SetHealth(2);
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        grid = FindObjectOfType<Grid>();
        built = new List<Vector3>();
        clients = GameObject.FindGameObjectsWithTag("Player");
        health = 50f;
        canvas = GameObject.Find("Canvas");
        text = canvas.transform.Find("CurrencyText");
        nextTime = 0;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (isLocalPlayer)
            {
                GameController.Instance.PlayDefeat();
            }
            else
            {
                GameController.Instance.PlayVictory();
            }
        }

        if (!isLocalPlayer)
            return;
        if (health <= 0)
        {
            GameNetworkManager.Instance.StopHost();
            return;
        }
        
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.rawPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if((hit.transform.name == "LeftField" && pID == 0) || (hit.transform.name == "RightField" && pID == 1))
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
                if((hit.transform.name == "LeftField" && pID == 0) || (hit.transform.name == "RightField" && pID == 1))
                    Build(ray, hit);
            }
        }

        AddGoldByTime();
        text.GetComponent<Text>().text = " X " + gold;
    }
    #endregion

    #region Methods
    private void Build(Ray ray, RaycastHit hit)
    {
        string name = GameController.Instance.buildings[1].name;
        int buildingCost = GetBuildingCost(name);
        Debug.Log(name);
        Vector3 pos = grid.GetNearestPointOnGrid(hit.point);
        //Debug.Log(pos);
        pos.y = 1f;
        if(built.Contains(pos))
            return;
        if (gold - buildingCost < 0)
            return;
        gold -= buildingCost;
        built.Add(pos);
        CmdSendBuildingInfo(
            name, 
            pos, 
            Quaternion.identity
        );
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

    private int GetBuildingCost(string name)
    {
        int cost;
        switch (name)
        {
            case "Common":
                cost = 3;
                break;
            case "Uncommon":
                cost = 7;
                break;
            case "Rare":
                cost = 10;
                break;
            case "Mythic":
                cost = 15;
                break;
            default:
                cost = 0;
                break;
        }
        return cost;
    }
    private void SpawnPlayer()
    {
        spawnLocations = new List<Vector3>
        (
            new Vector3[] { new Vector3(-8.5f, 1f, 0), new Vector3(8.5f, 1f, 0) }
        );
        if (clients.Length > 1)
        {
            transform.position = spawnLocations[1];
            pID = 1;
            transform.name = "Player2";
        }
        else
        {
            transform.position = spawnLocations[0];
            pID = 0;
            transform.name = "Player1";
        } 
    }

    public void TakeDamage(float damage)
    {
        SetHealth(health - damage);
    }

    private void SetHealth(float value)
    {
        if (value < health)
        {
            _audioSource.Play();
        }
        health = value;
    }

    public void AddGold(float g)
    {
        gold += g;
    }

    private void AddGoldByTime()
    {
        if(Time.time > nextTime)
        {
            nextTime = Time.time + goldRatePerSec;
            gold += 1f;
        }
    }
    #endregion

    #region Coroutines

    #endregion
}
