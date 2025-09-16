Shader "Custom/FillShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _FillTexture ("Fill Texture", 2D) = "white" {}
        _FillColor ("Fill Color", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(0,1)) = 0
        _FillSmoothness ("Fill Smoothness", Range(0,0.1)) = 0.05
        [Toggle(USE_TEXTURE)] _UseTexture ("Use Texture Fill", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature USE_TEXTURE
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _FillTexture;
            float4 _FillColor;
            float _FillAmount;
            float _FillSmoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainCol = tex2D(_MainTex, i.uv);
                
                step(i.uv.x, _FillAmount);
                float smoothFill = smoothstep(i.uv.x - _FillSmoothness, i.uv.x + _FillSmoothness, _FillAmount);
                
                fixed4 fillCol = tex2D(_FillTexture, i.uv);
    
                
                fixed4 col = lerp(mainCol, fillCol, smoothFill);
                
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}