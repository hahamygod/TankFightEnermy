using UnityEngine;
using System.Collections;

public class WZAttachPrefab : WZEffectBehaviour {
	// Attribute ------------------------------------------------------------------------
	public		enum			AttachType				{Active, Destroy};
	public		AttachType		m_AttachType			= AttachType.Active;

	public		GameObject		m_AttachPrefab;
	public 		bool m_bEnabled;

	GameObject targetObj;

	// Use this for initialization
	protected virtual void Start () {
		if (m_AttachPrefab != null) {
			
			targetObj = CreateGameObject(this.gameObject,m_AttachPrefab);
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

	protected override void OnDestroy()
	{
		//  	Debug.Log("OnDestroy");
		if (m_bEnabled && m_AttachType == AttachType.Destroy && m_AttachPrefab != null)
			CreateAttachPrefab();
		base.OnDestroy();
	}

	// Control Function -----------------------------------------------------------------
	void CreateAttachPrefab()
	{
		CreateAttachGameObject();

	}

	void CreateAttachGameObject()
	{
		GameObject createObj = (GameObject)CreateGameObject(this.gameObject, (this.gameObject == gameObject ? null : transform), m_AttachPrefab);

	}

}
