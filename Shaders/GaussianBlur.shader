Shader "Custom/GaussianBlur" {
	Properties {
		_MainTex("Base (RGB)",2D)="white"{}
		_BlurSize("Blur Size",Float)=1.0
	}
	SubShader {
		CGINCLUDE
			sampler2D _MainTex;
			half4 _MainTex_TexelSize;
			float _BlurSize;

			struct a2v{
				float4 vertex:POSITION;
				half2 texcoord:TEXCOORD0;
			};
			struct v2f{
				float4 pos:SV_POSITION;
				half2 uv[5]:TEXCOORD0;
			};



			fixed4 fragBlur(v2f i):SV_Target{
				float weight[3]={0.4026,0.2442,0.0545};

				fixed3 sum=tex2D(_MainTex,i.uv[0]).rgb*weight[0];

				for(int it=1;it<3;it++){
					sum+=tex2D(_MainTex,i.uv[it*2-1]).rgb*weight[it];
					sum+=tex2D(_MainTex,i.uv[it*2]).rgb*weight[it];
				}

				return fixed4(sum,1.0f);
			}
		ENDCG

		ZTest Always Cull Off ZWrite Off
		pass{
			Name "GAUSSIAN_BLUR_VERTICAL"
			CGPROGRAM
			#pragma vertex vertBlurVertical
			#pragma fragment fragBlur

			v2f vertBlurVertical(a2v v){
				v2f o;
				o.pos=mul(UNITY_MATRIX_MVP,v.vertex);

				half2 uv=v.texcoord;

				o.uv[0]=uv;
				o.uv[1]=uv+float2(0.0,_MainTex_TexelSize.y*1.0)*_BlurSize;
				o.uv[2]=uv-float2(0.0,_MainTex_TexelSize.y*1.0)*_BlurSize;
				o.uv[3]=uv+float2(0.0,_MainTex_TexelSize.y*2.0)*_BlurSize;
				o.uv[4]=uv-float2(0.0,_MainTex_TexelSize.y*2.0)*_BlurSize;

				return o;
			}
			ENDCG
			}
		Pass{
			Name "GAUSSIAN_BLUR_HORIZONTAL"
			CGPROGRAM
			#pragma vertex vertBlurHorizontal
			#pragma fragment fragBlur

			v2f vertBlurHorizontal(a2v v){
				v2f o;

				o.pos=mul(UNITY_MATRIX_MVP,v.vertex);

				half2 uv=v.texcoord;

				o.uv[0]=uv;
				o.uv[1]=uv+float2(_MainTex_TexelSize.x*1.0,0.0)*_BlurSize;
				o.uv[2]=uv-float2(_MainTex_TexelSize.x*1.0,0.0)*_BlurSize;
				o.uv[3]=uv+float2(_MainTex_TexelSize.x*2.0,0.0)*_BlurSize;
				o.uv[4]=uv-float2(_MainTex_TexelSize.x*2.0,0.0)*_BlurSize;

				return o;
			}
			ENDCG

		}
	}
	FallBack "Diffuse"
}
