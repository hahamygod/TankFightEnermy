Shader "Custom/ChangeShape" {  
   Properties {  
        _Color ("Main Color", Color) = (.5,.5,.5,1)  
       _Outline ("Outline width", Range (0.0, 10)) = 2  
        _MainTex ("Base (RGB)", 2D) = "white" { }  
    }  
    SubShader {  
        Tags { "Queue" = "Geometry" }  
        Pass {  
            Tags { "LightMode" = "Always" }  
            Cull Off 
            //ZWrite Off  
            //ZTest Always//始终通过深度测试，即可以渲染  
           	//ColorMask RGB // alpha not used  
            Blend SrcAlpha OneMinusSrcAlpha // Normal  
  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  

            sampler2D _MainTex;
	        uniform float _Outline;  

	        struct v2f {  
	            float4 pos : SV_POSITION;  
				half2 uv:TEXCOORD0;
	        };  


	        struct a2v{  
	            float4 vertex : POSITION;
	            float3 normal : NORMAL;
	           	half2 texcoord:TEXCOORD0;
	        };  


	        v2f vert(in a2v v) {  
	            v2f o;  

	            fixed4 n=fixed4(v.normal.xyz,0.0f);
	            v.vertex+=n*_Outline;
	           	o.pos = mul(UNITY_MATRIX_MVP, v.vertex);  

	            o.uv=v.texcoord;
	           
	            return o;  
	        }  
	        fixed4 frag(v2f i) :SV_Target {  
	            fixed4 c=tex2D(_MainTex,i.uv);
	            return c;
	        }  
            ENDCG  
       }  
        
   }  
    Fallback "Diffuse"  
}  
