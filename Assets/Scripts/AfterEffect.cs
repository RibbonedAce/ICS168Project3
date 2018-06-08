using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterEffect : MonoBehaviour 
{
	#region Variables

	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake() 
	{
        StartCoroutine(Perform());
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
	
	#endregion
	
	#region Coroutines
    /// <summary>
    /// Use the effect then die
    /// </summary>
    /// <returns>The amount of time to keep alive to perform the actions</returns>
	private IEnumerator Perform()
    {
        AudioSource _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }
	#endregion
}
