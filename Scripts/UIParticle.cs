using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class UIParticle : UIWidget {

	public Material mMaterial;
	public Texture mMainTexture;
	public Shader mShader;
	public Renderer mRender;

	//重写
	public override Material material {
		get {
			return mMaterial;
		}
		set {
			mMaterial = value;
		}
	}

	public override Shader shader{
		get{ 
			return mShader;
		}
		set{ 
			mShader = value;
		}
	}

	public override Texture mainTexture{
		get{ 
			return mMainTexture;
		}
		set{ 
			mMainTexture = value;
		}
	}

	protected override void Awake(){

		if (GetComponent<Renderer> ()) {
			mRender = GetComponent<Renderer> ();

			mMaterial = mRender.sharedMaterial;
			mMainTexture = mMaterial.mainTexture;

			mShader = mMaterial.shader;
		}

		//onRenderQueueChanged = OnRQChanged;

		base.Awake ();
	}

	void OnRQChanged(int rq){
		//回调指定渲染层级
		GetComponent<Renderer>().sharedMaterial.renderQueue=rq;
	}

	protected override void OnInit(){
		base.OnInit ();
	}

	protected override void OnStart(){
		base.OnStart ();
	}

	protected override void OnEnable(){
		base.OnEnable();
	}

	protected override void OnDisable(){
		base.OnDisable ();
	}
	protected override void OnUpdate(){
		base.OnUpdate();
	}

	public override void OnFill(BetterList<Vector3> verts,BetterList<Vector2> uvs,BetterList<Color> cols){

		//创建一个空四边形张菊一个dc

		verts.Add (new Vector3 (1, 0, 1));
		verts.Add (new Vector3 (1, 0, -1));
		verts.Add (new Vector3 (-1, 0, 1));
		verts.Add (new Vector3 (-1, 0, -1));

		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (0, 0));

		cols.Add(new Color(255,255,255,255));
		cols.Add(new Color(255,255,255,255));
		cols.Add(new Color(255,255,255,255));
		cols.Add(new Color(255,255,255,255));

		base.OnFill (verts, uvs, cols);
		if (onPostFill != null) {
			onPostFill (this, verts.size,verts, uvs, cols);
		}
	}
}
