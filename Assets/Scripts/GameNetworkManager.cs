using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager 
{
    #region Variables

	#endregion
	
	#region Properties
    /// <summary>
    /// <para>The instance to reference</para>
    /// </summary>
	public static GameNetworkManager Instance { get; private set; }
	#endregion
	
	#region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake() 
	{
		if (Instance == null)
        {
            Instance = this;
        }
	}

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start() 
	{
		
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update() 
	{
		
	}
	#endregion
	
	#region Methods
    /// <summary>
    /// Void return function of StartHost()
    /// </summary>
	public void VoidStartHost()
    {
        StartHost();
    }

    /// <summary>
    /// Void return function of StartClient()
    /// </summary>
	public void VoidStartClient()
    {
        StartClient();
    }

    /// <summary>
    /// Void return function of StartServer()
    /// </summary>
	public void VoidStartServer()
    {
        StartServer();
    }

    /// <summary>
    /// Set the manager's match name
    /// </summary>
    /// <param name="value">The name of the match</param>
    public void ChangeMatchName(string value)
    {
        matchName = value;
    }

    /// <summary>
    /// Ready the client and add the player
    /// </summary>
    public void Ready()
    {
        ClientScene.Ready(client.connection);
        if (ClientScene.localPlayers.Count == 0)
            ClientScene.AddPlayer((short)0);
    }

    /// <summary>
    /// Create a simple matchmaking match
    /// </summary>
    public void CreateMatch()
    {
        matchMaker.CreateMatch(matchName, matchSize, true, "", "", "", 0, 0, OnMatchCreate);
    }
    #endregion

    #region Coroutines

    #endregion
}
