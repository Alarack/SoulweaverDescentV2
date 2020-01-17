Shader "Unlit/PostProcessBlurTest"
{
    //show values to edit in the inspector (properties)
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0,0.5)) = 0
        [KeywordEnum(Low, Medium, High)]
        _Samples ("Sample Amount", Float) = 0
        [Toggle(GAUSS)] _Gauss ("Gaussian Blur", float) = 0
        [PowerSlider(3)]_StandardDeviation("Standard Deviation (Gauss Only)", Range(0.00, 0.3)) = 0.02
        
    }
    SubShader
    {
        //markers that specify we don't need culling
        // or reading/writing to the depth buffer
        Cull Off
        ZWrite Off
        ZTest Always

        //Vertical Blur
        Pass
        {
            CGPROGRAM
            //Include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
            #pragma shader_feature GAUSS
            
            //texture and transforms of the Texture
            sampler2D _MainTex;
            float _BlurSize;
            float _StandardDeviation;

            #define PI 3.14159265359
            #define E 2.71828182846

            #if _SAMPLES_LOW
                #define SAMPLES 10
            #elif _SAMPLES_MEDIUM
                #define SAMPLES 30
            #else
                #define SAMPLES 100
            #endif

            //the object data thats put into the vertex shader 
            struct vertexData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
			};
            
            //the data that's used to generate fragments and can be read by the fragment shader
            struct vert2frag
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
			};

            //the vertex Shader
            vert2frag vert (vertexData v)
            {
                vert2frag o;
                //convert the vertex positions from the object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
			};

            //the fragment Shader
            fixed4 frag(vert2frag i) : SV_Target
            {
            #if GAUSS
                //failsafe so we can use turn off the blur by setting the deviation to 0
                if (_StandardDeviation == 0)
                return tex2D(_MainTex, i.uv);
            #endif
                //init color variable
                float4 col = 0;
            #if GAUSS
                float sum = 0;
            #else
                float sum = SAMPLES;
            #endif
                    //iterate over blur samples 
                    for (float index = 0; index < SAMPLES; index++)
                    {
                        //get the offset of the Sample
                        float offset = (index/(SAMPLES - 1) - 0.5) * _BlurSize;
                        //get uv coordinate of Sample
                        float2 uv = i.uv + float2(0, offset);
                    #if !GAUSS
                        //simply add the color if we don't have the guassian blur (box)
                        col += tex2D(_MainTex, uv);
                    #else
                        //calculate the result of the gaussian functions
                        float stDevSquared = _StandardDeviation * _StandardDeviation;
                        float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(E, -((offset * offset) / (2 *stDevSquared)));
                        //add result to sum
                        //why the fuck do I write += what the hell does that mean
                        sum += gauss;
                        //multiply color with influence from gaussian function and add it to the sum color
                        col += tex2D(_MainTex, uv) * gauss;
                    #endif
					}
                    //divide the sum of values by the amount of samples
                    col = col / sum;
                    return col;
		    }

            ENDCG
        }

        //Horizontal blur
        Pass
        {
            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
            #pragma shader_feature GAUSS
            
            //texture and transforms of the Texture
            sampler2D _MainTex;
            float _BlurSize;
            float _StandardDeviation;

            #define PI 3.14159265359
            #define E 2.71828182846

            #if _SAMPLES_LOW
                #define SAMPLES 10
            #elif _SAMPLES_MEDIUM
                #define SAMPLES 30
            #else
                #define SAMPLES 100
            #endif

                //the object data thats put into the vertex shader 
                struct vertexData
                    {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
			        };
            
                //the data that's used to generate fragments and can be read by the fragment shader
                struct vert2frag
                    {
                        float4 position : SV_POSITION;
                        float2 uv : TEXCOORD0;
			        };

                //the vertex Shader
                vert2frag vert (vertexData v)
                    {
                        vert2frag o;
                        //convert the vertex positions from the object space to clip space so they can be rendered
                        o.position = UnityObjectToClipPos(v.vertex);
                        o.uv = v.uv;
                        return o;
			        }

                //the fragment Shader
                fixed4 frag(vert2frag i) : SV_TARGET
                {
                #if GAUSS
                       //failsafe so we can turn off the blur by setting the deviation to 0
                       if(_StandardDeviation == 0)
                       return tex2D(_MainTex, i.uv);
                #endif
                        //calculate aspect ratio
                        float invAspect = _ScreenParams.y / _ScreenParams.x;
                        //init color variable
                        float4 col = 0;
                #if GAUSS
                        float sum = 0;
                #else
                        float sum = SAMPLES;
                #endif

                        //iterate over blur samples
                        for(float index = 0; index < SAMPLES; index++)
                        {
                            //get the offset of the Sample
                            float offset = (index/(SAMPLES - 1) - 0.5) * _BlurSize;
                            //get uv coordinate of Sample
                            float2 uv = i.uv + float2(offset, 0);
                        #if !GAUSS
                            //simply add the color if we don't have a guassian blur (box)
                            col += tex2D(_MainTex, uv);
                        #else
                            //calculate the result of the gaussian function
                            float stDevSquared = _StandardDeviation * _StandardDeviation;
                            float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(E, -((offset *offset) / (2 * stDevSquared)));
                            //add result to sum
                            sum += gauss;
                            //multiply color with influence from the gaussian function and add it to sum color
                            col += tex2D(_MainTex, uv) * gauss;
                        #endif
						}

                        //divide the sum of values by the amount of samples
                        col = col / sum;
                        return col;
				}

                ENDCG
		}
    }
}
