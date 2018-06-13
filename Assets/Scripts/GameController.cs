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

    /// <summary>
    /// <para>The bullets spawned for each team</para>
    /// </summary>
    public GameObject[] bullets;

    /// <summary>
    /// <para>The colors for the troop teams</para>
    /// </summary>
    public Material[] troopColors;

    /// <summary>
    /// <para>The sound to play when defeated</para>
    /// </summary>
    public AudioClip defeatClip;

    /// <summary>
    /// <para>The sound to play when victorious</para>
    /// </summary>
    public AudioClip victoryClip;

    /// <summary>
    /// <para>The audio source component attached</para>
    /// </summary>
    private AudioSource _audioSource;
    #endregion

    private string buildingName = "Common";
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
        _audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
            buildingName = buildings[0].name;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            buildingName = buildings[1].name;
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    buildingName = buildings[2].name;
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //    buildingName = buildings[3].name;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Play the defeat sound
    /// </summary>
    public void PlayDefeat()
    {
        _audioSource.clip = defeatClip;
        _audioSource.Play();
    }

    public void PlayVictory()
    {
        _audioSource.clip = victoryClip;
        _audioSource.Play();
    }

    public string GetBuildingName()
    {
        return buildingName;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
