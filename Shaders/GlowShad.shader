Shader "Custom/GlowShad"
		{
        Properties{
                _Color("Object's Color", Color) = (0, 1, 0, 1)
                _GlowColor("Glow's Color", Color) = (1, 0, 0, 0)
                _Strength("Glow Strength", Range(5.0, 1.0)) = 2.0
                _GlowRange("GlowRange",Range(0.1,1))=0.3
        }
        SubShader{
	        Pass{
	        Tags{ "LightMode" = "ForwardBase" }
	  
	        CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag


	        float4 _Color;
	  
	        float4 vert(float4 vertexPos : POSITION) : SV_POSITION{
	            return mul(UNITY_MATRIX_MVP, vertexPos);
	        }
	  
	        float4 frag(void) : COLOR{
	        	return _Color;
	        }
	  
	        ENDCG
	        }
  
            Pass{
            Tags{"LightMode" = "ForwardBase"        "Queue" = "Transparent"        "RenderType" = "Transparent"}
    //        Cull Front
            ZWrite Off
      
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
			  
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

             float4 _GlowColor;
             float  _Strength;;
             float _GlowRange;
	        struct vInput {
	                float4 vertex : POSITION;
	                float4 normal : NORMAL;
	        };
	  
	        struct v2f {
	                float4 position : SV_POSITION;
	                float4 col:COLOR;
	        };
	  
	        v2f vert(vInput i) {
	                v2f o;
	  
	                float4x4 modelMatrix = _Object2World;
	                float4x4 modelMatrixInverse = _World2Object;
	  
	                float3 normalDirection = normalize(mul(i.normal, modelMatrixInverse)).xyz;
	                float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(modelMatrix, i.vertex).xyz);
	  
	                float4 pos = i.vertex + (i.normal * _GlowRange);
	  
	                o.position = mul(UNITY_MATRIX_MVP, pos);
	                  
	                float3 normalDirectionT = normalize(normalDirection);
	                float3 viewDirectionT = normalize(viewDirection);
	                float strength = abs(dot(viewDirectionT, normalDirectionT));
	                float opacity = pow(strength, _Strength);
	  
	                float4 col = float4(_GlowColor.xyz, opacity);
	  
	                o.col = col;
	  
	                return o;
	        }
	  
	        float4 frag(v2f i) : COLOR{
	                  
	                return i.col;
	        }
  
            ENDCG
        	}
        }
}