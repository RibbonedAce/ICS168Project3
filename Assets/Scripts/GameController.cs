using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The buildings that can spawn</para>
    /// </summary>
    public GameObject[] buildings;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The instance to reference</para>
    /// </summary>
    public static GameController Instance { get; private set; }
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
        else
        {
            Destroy(this);
        }
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
		
	}
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}
