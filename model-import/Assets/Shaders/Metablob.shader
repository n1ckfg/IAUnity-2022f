// Original Source: http://www.iquilezles.org/apps/shadertoy/

Shader "ShaderToy/Metablob" {
	
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader {

		Pass {
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 screenCoord : TEXCOORD1;
			};

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.screenCoord.xy = ComputeScreenPos(o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
 				//the centre point for each blob
				float2 move1;
				move1.x = _CosTime.w*0.4;
				move1.y = sin(_Time.y*1.5)*0.4;
				float2 move2;
				move2.x = cos(_Time.z)*0.4;
				move2.y = sin(_Time.w)*0.4;
				    
				//screen coordinates
				//float2 p = -1.0 + 2.0 * i.screenCoord.xy * _ScreenParams.xy / _ScreenParams.xy;
				  
				//radius for each blob
				float r1 =(dot(i.uv-move1,i.uv-move1))*8.0;
				float r2 =(dot(i.uv+move2,i.uv+move2))*16.0;
				
				//sum the meatballs
				float metaball =(1.0/r1+1.0/r2);
				//alter the cut-off power
				float col = pow(metaball,8.0);
				float4 col2 = tex2D(_MainTex, i.uv) + float4(col, col, col, 1.0);
				
				//set the output color
				return col2;
			}
			ENDCG
		}
	}

}
