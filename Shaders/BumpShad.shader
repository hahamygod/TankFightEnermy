Shader "Custom/BumpShad" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpTex("Albedo (RGB)",2D)="white"{}
		_MaskTex("Albedo (RGB)",2D)="white"{}
		_BumpScale("Bump Scale",Float)=1.0
		_Specular("Specular",Color)=(1,1,1,1)
		_Gloss("Gloss",Range(8.0,256))=20
	}
	SubShader {
		Pass{
		Tags { "LightMode"="ForwardBase" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma vertex vert
		#pragma fragment frag
		#include "Lighting.cginc"

		sampler2D _MainTex;
		float4 _MainTex_ST;
		sampler2D _BumpTex;
		float4 _BumpTex_ST;
		float _BumpScale;
		sampler2D _MaskTex;
		float4 _MaskTex_ST;
		fixed4 _Specular;
		fixed4 _Color;
		float _Gloss;

		struct a2v{
			float4 vertex:POSITION;
			float3 normal:NORMAL;
			float4 tangent:TANGENT;
			float4 texcoord:TEXCOORD0;
		};

		struct v2f{
			float4 pos:SV_POSITION;
			float4 uv:TEXCOORD0;
			float3 lightDir:TEXCOORD1;
			float3 viewDir:TEXCOORD2;
		};

		v2f vert(a2v v){
			v2f o;
			o.pos=mul(UNITY_MATRIX_MVP,v.vertex);

			o.uv.xy=v.texcoord.xy*_MainTex_ST.xy+_MainTex_ST.zw;
			o.uv.zw=v.texcoord.xy*_BumpTex_ST.xy+_BumpTex_ST.zw;

			float3 binormal=cross(normalize(v.normal),normalize(v.tangent.xyz))*v.tangent.w;

			float3x3 rotation=float3x3(v.tangent.xyz,binormal,v.normal);

			o.lightDir=mul(rotation,ObjSpaceLightDir(v.vertex)).xyz;

			o.viewDir=mul(rotation,ObjSpaceViewDir(v.vertex)).xyz;

			return o;
		}

		fixed4 frag(v2f i):SV_Target{
			fixed3 tangentLightDir=normalize(i.lightDir);
			fixed3 tangentViewDir=normalize(i.viewDir);

			fixed4 packedNormal=tex2D(_BumpTex,i.uv.zw);
			fixed3 tangentNormal;

			tangentNormal=UnpackNormal(packedNormal);
			tangentNormal.xy*=_BumpScale;
			tangentNormal.z=sqrt(1.0-saturate(dot(tangentNormal.xy,tangentNormal.xy)));

			//cover
			fixed4 c=tex2D(_MainTex,i.uv);
			fixed4 c2=tex2D(_MaskTex,i.uv);
			fixed4 c3;
			c3.r=c2.r*c.r;
			c3.g=c2.g*c.g;
			c3.b=c2.b*c.b;
			c3.a=c2.a*c.a;
			clip(c3.a-1);

			fixed3 albedo=c3.rgb*_Color.rgb;

			//mask
			//fixed3 albedo=tex2D(_MainTex,i.uv).rgb*_Color.rgb;


			fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;

			fixed3 diffuse=_LightColor0.rgb*albedo*max(0,dot(tangentNormal,tangentLightDir));

			fixed3 halfDir=normalize(tangentLightDir+tangentViewDir);

			fixed3 specular=_LightColor0.rgb*_Specular.rgb*pow(max(0,dot(tangentNormal,halfDir)),_Gloss);

			return fixed4(ambient+diffuse+specular,1.0f);
		}
		
		ENDCG
		}
	}
	FallBack "Diffuse"
}
