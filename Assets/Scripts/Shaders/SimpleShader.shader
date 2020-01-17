Shader "Unlit/SimpleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"
			#include "Lighting.cginc"
			//#include "AutoLight.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
				//float4 colors : COLOR;
				float4 normal : NORMAL;
				//float4 tangent : TANGENT;
				float2 uv0 : TEXCOORD0;
				//float2 uv1 : TEXCOORD1;
            };

            struct VertexOutput
            {
                float4 clipSpacePos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float3 normal : NORMAL;
            };

            //sampler2D _MainTex;
            //float4 _MainTex_ST;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
				o.uv0 = v.uv0;
				o.normal = v.normal;
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);

                return o;
            }

            float4 frag (VertexOutput o) : SV_Target
            {
				float2 uv = o.uv0;

				float3 lightDir = _WorldSpaceLightPos0.xyx;
				float3 lightColor = _LightColor0.rgb;


				float lightFalloff = saturate( dot(lightDir, o.normal));
				float3 diffuseLight = lightColor * lightFalloff;

				float3 ambientLight = float3(0.2, 0.2, 0.2);


                return float4(ambientLight + diffuseLight, 0);
            }
            ENDCG
        }
    }
}
