using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonExpansion : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>Whether the button is currently changing size</para>
    /// </summary>
    public bool changing;

    /// <summary>
    /// <para>Whether the compact button is active</para>
    /// </summary>
    public bool compactActive;

    /// <summary>
    /// <para>The button to replace when it is compacted</para>
    /// </summary>
    [SerializeField]
    private Button compactButton;

    /// <summary>
    /// <para>The animator component attached</para>
    /// </summary>
    private Animator _animator;
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
	private void Awake() 
	{
        _animator = GetComponent<Animator>();
        _animator.SetBool("changing", changing);
        _animator.SetBool("expanded", false);
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
        _animator.SetBool("changing", changing);
        compactButton.gameObject.SetActive(compactActive);
    }
	#endregion
	
	#region Methods
    /// <summary>
    /// Contract the button back
    /// </summary>
	public void StartContraction()
    {
        _animator.SetBool("expanded", false);
        changing = true;
    }

    /// <summary>
    /// Expand the button out
    /// </summary>
    public void StartExpansion()
    {
        _animator.SetBool("expanded", true);
        changing = true;
        Invoke("StartContraction", 3f);
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
