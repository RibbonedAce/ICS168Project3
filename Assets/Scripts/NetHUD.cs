using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class NetHUD : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// <para>The set of elements for the HUD</para>
    /// </summary>
    [SerializeField]
    private GameObject[] elements;

    /// <summary>
    /// <para>The set of text labels for the HUD</para>
    /// </summary>
    [SerializeField]
    private Text[] labels;

    /// <summary>
    /// <para>The button prefab to spawn for matches</para>
    /// </summary>
    [SerializeField]
    private GameObject buttonPrefab;

    /// <summary>
    /// <para>The network manager to reference</para>
    /// </summary>
    private NetworkManager manager;

    /// <summary>
    /// <para>Whether the server is being shown</para>
    /// </summary>
    private bool m_ShowServer;

    /// <summary>
    /// <para>The match buttons instantiated</para>
    /// </summary>
    private List<GameObject> matchButtons;

    /// <summary>
    /// <para>The match info snapshot to keep track of</para>
    /// </summary>
    private UnityEngine.Networking.Match.MatchInfoSnapshot currentMatch;
	#endregion
	
	#region Properties

	#endregion
	
	#region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake() 
	{
        manager = GetComponent<NetworkManager>();
        matchButtons = new List<GameObject>();
	}

    /// <summary>
    ///  Use this for initialization
    /// </summary>
    private void Start()
    {

	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() 
	{
        List<int> actives = new List<int>();

        bool flag = manager.client == null || manager.client.connection == null || manager.client.connection.connectionId == -1;
        if (!manager.IsClientConnected() && !NetworkServer.active && manager.matchMaker == null)
        {
            if (flag)
            {
                actives.Add(0);
            }
            else
            {
                actives.Add(1);
                labels[0].text = "Connecting to " + manager.networkAddress + ":" + manager.networkPort + "..";
            }
        }
        else
        {
            if (NetworkServer.active)
            {
                string text = "Server: port=" + manager.networkPort;
                if (manager.useWebSockets)
                    text += " (Using WebSockets)";
                actives.Add(2);
                labels[1].text = text;
            }
            if (manager.IsClientConnected())
            {
                actives.Add(3);
                labels[2].text = "Client: address=" + manager.networkAddress + " port=" + manager.networkPort;
            }
        }
        if (manager.IsClientConnected() && !ClientScene.ready)
        {
            actives.Add(4);
        }
        if (NetworkServer.active || manager.IsClientConnected())
        {
            actives.Add(5);
        }
        if (!(NetworkServer.active || manager.IsClientConnected() || !flag))
        {
            if (manager.matchMaker == null)
            {
                actives.Add(6);
            }
            else
            {
                if (manager.matchInfo == null)
                {
                    if (manager.matches == null)
                    {
                        actives.Add(7);
                        labels[3].text = "Room Name:";
                    }
                    else
                    {
                        actives.Add(8);
                    }
                }
                actives.Add(9);
                if (m_ShowServer)
                {
                    actives.Add(10);
                }
                actives.Add(11);
                labels[4].text = "MM Uri: " + manager.matchMaker.baseUri;
            }
        }

        for (int i = 0; i < elements.Length; ++i)
        {
            elements[i].SetActive(actives.Contains(i));
        }
    }


	#endregion
	
	#region Methods
    /// <summary>
    /// Toggle whether the server's shown
    /// </summary>
    public void ToggleServer()
    {
        m_ShowServer = !m_ShowServer;
    }

    /// <summary>
    /// Instantiate a button to join the currently tracked match
    /// </summary>
    public void MakeMatchButton()
    {
        GameObject g = Instantiate(buttonPrefab, elements[8].GetComponentInChildren<VerticalLayoutGroup>().transform);
        matchButtons.Add(g);
        g.GetComponentInChildren<Text>().text = "Join Match:" + currentMatch.name;
        g.GetComponent<Button>().onClick.AddListener(SetMatchInfo);
    }

    /// <summary>
    /// Set the manager's info for the currently tracked match
    /// </summary>
    private void SetMatchInfo()
    {
        manager.matchName = currentMatch.name;
        manager.matchSize = (uint)currentMatch.currentSize;
        manager.matchMaker.JoinMatch(currentMatch.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
    }

    /// <summary>
    /// Set the match host to a preset choice
    /// </summary>
    /// <param name="choice">The type of match to set the manager to</param>
    public void SetMatchType(int choice)
    {
        m_ShowServer = false;
        switch (choice)
        {
            case 0:
                manager.SetMatchHost("localhost", 1337, false);
                return;
            case 1:
                manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
                return;
            case 2:
                manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
                return;
        }
    }

    /// <summary>
    /// List and show all of the amtches
    /// </summary>
    public void ShowMatches()
    {
        manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);
        if (manager.matches != null)
        {
            foreach (UnityEngine.Networking.Match.MatchInfoSnapshot m in manager.matches)
            {
                currentMatch = m;
                MakeMatchButton();
            }
        }
    }

    /// <summary>
    /// Set the matches to null and delete buttons
    /// </summary>
    public void SetMatchesNull()
    {
        manager.matches = null;
        foreach (GameObject mb in matchButtons)
        {
            Destroy(mb);
        }
    }
    #endregion

    #region Coroutines

    #endregion
}
