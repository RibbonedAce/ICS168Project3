using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MatchStorage : MonoBehaviour 
{
    #region Variables

    #endregion

    #region Properties
    /// <summary>
    /// <para>The match stored</para>
    /// </summary>
    public UnityEngine.Networking.Match.MatchInfoSnapshot Match { get; private set; }
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    private void Awake() 
	{
		
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
    /// Sets the match stored if none is present
    /// </summary>
    /// <param name="m">The match to set</param>
	public void SetMatch (UnityEngine.Networking.Match.MatchInfoSnapshot m)
    {
        if (Match == null)
        {
            Match = m;
            GetComponent<Button>().onClick.AddListener(SetMatchInfo);
        }
    }

    /// <summary>
    /// Set the manager's info for the currently tracked match
    /// </summary>
    private void SetMatchInfo()
    {
        GameNetworkManager.Instance.matchName = Match.name;
        GameNetworkManager.Instance.matchSize = (uint)Match.currentSize;
        GameNetworkManager.Instance.matchMaker.JoinMatch(Match.networkId, "", "", "", 0, 0, GameNetworkManager.Instance.OnMatchJoined);
    }
    #endregion

    #region Coroutines

    #endregion
}
