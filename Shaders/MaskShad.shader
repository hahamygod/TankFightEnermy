Shader "Custom/MaskShad" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex("Albedo (RGB)",2D)="white"{}
	}
	SubShader {
		Pass{		
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma vertex vert
			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;

			struct a2v {
				float4 vertex:POSITION;
				half2 texcoord:TEXCOORD0;
			};

			struct v2f{
				float4 pos:SV_POSITION;
				half4 uv:TEXCOORD0;
			};

			v2f vert(a2v v){
				v2f o;

				o.pos=mul(UNITY_MATRIX_MVP,v.vertex);
				o.uv.xy=v.texcoord*_MainTex_ST.xy+_MainTex_ST.zw;
				o.uv.zw=v.texcoord*_MaskTex_ST.xy+_MaskTex_ST.zw;

				return o;
			}

			fixed4 frag(v2f i):SV_Target{

				fixed4 c1=tex2D(_MainTex,i.uv.xy);
				fixed4 c2=tex2D(_MaskTex,i.uv.zw);
				fixed4 c;

				c=c1*c2;
				clip(c.a-1);
				return c;
			}
		
			ENDCG
		}
	}
	FallBack "Diffuse"
}
