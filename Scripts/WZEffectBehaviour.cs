using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WZEffectBehaviour : MonoBehaviour {
	public class _RuntimeIntance
	{
		public GameObject	m_ParentGameObject;
		public GameObject	m_ChildGameObject;
		public _RuntimeIntance(GameObject	parentGameObject, GameObject childGameObject)
		{
			m_ParentGameObject	= parentGameObject;
			m_ChildGameObject	= childGameObject;
		}
	}

	// Attribute ------------------------------------------------------------------------
	private	static	bool				m_bShuttingDown		= false;
	private	static	GameObject			m_RootInstance;
	public			float				m_fUserTag;
	protected		MeshFilter			m_MeshFilter;
	protected		List<Material>		m_RuntimeMaterials;
	protected		bool				m_bReplayState		= false;


	public WZEffectBehaviour()
	{
		m_MeshFilter	= null;
	}


	// Property -------------------------------------------------------------------------
	public static float GetEngineTime()
	{
		if (Time.time == 0)
			return 0.000001f;
		return Time.time;
	}

	public static float GetEngineDeltaTime()
	{
		return Time.deltaTime;
	}

	protected GameObject CreateEditorGameObject(GameObject srcGameObj)
	{
		// 烙矫
		if (srcGameObj.name.Contains("flare 24"))
		{
			return srcGameObj;
		}
		return srcGameObj;
	}

	public GameObject CreateGameObject(string name)
	{
		return CreateEditorGameObject(new GameObject(name));
	}

	public GameObject CreateGameObject(GameObject original)
	{
		return CreateEditorGameObject((GameObject)Object.Instantiate(original));
	}

	public GameObject CreateGameObject(GameObject prefabObj, Vector3 position, Quaternion rotation)
	{
		return CreateEditorGameObject((GameObject)Object.Instantiate(prefabObj, position, rotation));
	}

	public GameObject CreateGameObject(GameObject parentObj, Transform parentTrans, GameObject prefabObj)
	{
		GameObject newChild = CreateGameObject(prefabObj);
		if (parentObj != null)
			ChangeParent(parentObj.transform, newChild.transform, true, parentTrans);
		return newChild;
	}

	public GameObject CreateGameObject(GameObject parentObj, GameObject prefabObj)
	{
		GameObject newChild = CreateGameObject(prefabObj);
		if (parentObj != null)
			ChangeParent(parentObj.transform, newChild.transform, true, null);
		return newChild;
	}

	protected void ChangeParent(Transform newParent, Transform child, bool bKeepingLocalTransform, Transform addTransform)
	{
		// Keeping transform
		WZTransformTool trans = new WZTransformTool(child.transform);

		if (bKeepingLocalTransform)
		{
			if (addTransform != null)
				trans.AddTransform(addTransform);
		}

		child.parent = newParent;

		if (bKeepingLocalTransform)
			trans.CopyToLocalTransform(child.transform);
	}

	// Loop Function --------------------------------------------------------------------
	protected virtual void OnDestroy()
	{
		// RuntimeMaterials
		if (m_RuntimeMaterials != null)
		{
			foreach (Material mat in m_RuntimeMaterials)
				Destroy(mat);
			m_RuntimeMaterials = null;
		}
	}

	// Control Function -----------------------------------------------------------------
	// Event Function -------------------------------------------------------------------
	public void OnApplicationQuit()
	{
		m_bShuttingDown = true;
	}
}
