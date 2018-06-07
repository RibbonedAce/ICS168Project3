using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonExpansion : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The button to replace when it is compacted</para>
    /// </summary>
    [SerializeField]
    private Button compactButton;

    /// <summary>
    /// <para>The minimum anchor when the button is contracted</para>
    /// </summary>
    [SerializeField]
    private Vector2 contractMinAnchor;

    /// <summary>
    /// <para>The maximum anchor when the button is contracted</para>
    /// </summary>
    [SerializeField]
    private Vector2 contractMaxAnchor;

    /// <summary>
    /// <para>The minimum anchor when the button is expanded</para>
    /// </summary>
    [SerializeField]
    private Vector2 expandMinAnchor;

    /// <summary>
    /// <para>The maximum anchor when the button is expanded</para>
    /// </summary>
    [SerializeField]
    private Vector2 expandMaxAnchor;

    /// <summary>
    /// <para>The current routine for changing button size</para>
    /// </summary>
    private Coroutine changeRoutine;

    /// <summary>
    /// <para>The rect transform component attached</para>
    /// </summary>
    private RectTransform _rectTransform;
    #endregion

    #region Properties

    #endregion

    #region Events
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    private void Awake() 
	{
        _rectTransform = GetComponent<RectTransform>();
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

    /// <summary>
    /// Called when script enabled or game object set active
    /// </summary>
    private void OnEnable()
    {
        if (_rectTransform != null)
        {
            Contract();
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Start contraction of the button
    /// </summary>
    public void StartContraction()
    {
        if (changeRoutine != null)
        {
            StopCoroutine(changeRoutine);
        }
        changeRoutine = StartCoroutine(Contract(1f));
    }

    /// <summary>
    /// Start expansion of the button
    /// </summary>
    public void StartExpansion()
    {
        if (changeRoutine != null)
        {
            StopCoroutine(changeRoutine);
        }
        changeRoutine = StartCoroutine(Expand(1f));
    }

    /// <summary>
    /// Contract the button immediately
    /// </summary>
    public void Contract()
    {
        _rectTransform.SetAnchors(contractMinAnchor, contractMaxAnchor);
        compactButton.gameObject.SetActive(true);
    }
    #endregion

    #region Coroutines
    /// <summary>
    /// Contract the button in
    /// </summary>
    /// <param name="time">How long the contraction takes</param>
    /// <returns>Returns the time to expand at every frame</returns>
    private IEnumerator Contract(float time)
    {
        Vector2 oldMin = _rectTransform.anchorMin;
        Vector2 oldMax = _rectTransform.anchorMax;
        for (float t = 0f; t < time; t += Time.deltaTime)
        {
            _rectTransform.SetAnchors(Vector2.Lerp(oldMin, contractMinAnchor, t / time), Vector2.Lerp(oldMax, contractMaxAnchor, t / time));
            yield return null;
        }
        _rectTransform.SetAnchors(contractMinAnchor, contractMaxAnchor);
        compactButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Expand the button out
    /// </summary>
    /// <param name="time">How long the expansion takes</param>
    /// <returns>Returns the time to contract at every frame and delay to contract again</returns>
    private IEnumerator Expand(float time)
    {
        compactButton.gameObject.SetActive(false);
        Vector2 oldMin = _rectTransform.anchorMin;
        Vector2 oldMax = _rectTransform.anchorMax;
        for (float t = 0f; t < time; t += Time.deltaTime)
        {
            _rectTransform.SetAnchors(Vector2.Lerp(oldMin, expandMinAnchor, t / time), Vector2.Lerp(oldMax, expandMaxAnchor, t / time));
            yield return null;
        }
        _rectTransform.SetAnchors(expandMinAnchor, expandMaxAnchor);
        yield return new WaitForSeconds(time * 3);
        StartContraction();
    }
    #endregion
}
