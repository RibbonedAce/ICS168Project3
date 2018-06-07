using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Bullet : NetworkBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The team the bullet came from</para>
    /// </summary>
    public int team;

    /// <summary>
    /// <para>The speed in u/s to travel at</para>
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// <para>The rigidbody component attached</para>
    /// </summary>
    private Rigidbody _rigidbody;
    #endregion

    #region Properties

    #endregion

    #region Events
    /// <summary>
    /// When the object is initialized on the server
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(Utils.DoAfterTime(NetworkServer.Destroy, gameObject, 2f));
    }

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
    /// FixedUpdate is called once per physics step
    /// </summary>
    private void FixedUpdate() 
	{
        if (!isServer)
            return;

        _rigidbody.MovePositionBy(transform.up * Time.deltaTime * speed);
	}

    /// <summary>
    /// Called when a collider enters this object's triggers
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        if (other.CompareTag("minion") && other.GetComponent<MoveAndAttack>().team != team)
        {
            other.GetComponent<minionInfo>().currentHealth -= 10;
            NetworkServer.Destroy(gameObject);
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
