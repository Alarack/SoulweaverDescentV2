Shader "Unlit/TestTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // Your surface shader can be decorated with one or more tags.  These tags define things that let the hardware decide when to call your shader.
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            // In programming, pragma refers to: The code that consists of useful information on how a compiler or interpreter or assembler should process the program.
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            // Unity contains several files that can be used by your shader programs to bring in predefined variables and helper functions. This is done by the standard #include directive, e.g.:
            #include "UnityCG.cginc"

            struct input
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            vertexOutput vert (input vertexData)
            {
                vertexOutput output;
                output.vertex = UnityObjectToClipPos(vertexData.vertex);
                output.uv = TRANSFORM_TEX(vertexData.uv, _MainTex);
                UNITY_TRANSFER_FOG(output,output.vertex);
                return output;
            }

            fixed4 frag (vertexOutput i) : SV_Target
            {
                // sample the texture
                fixed4 color = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, color);
                return color;
            }
            ENDCG
        }
    }
}
