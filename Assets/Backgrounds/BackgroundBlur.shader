Shader "UI/BlurBlueTint"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Size ("Blur Size", Float) = 1
        _TintColor ("Tint Color", Color) = (0.6, 0.8, 1.0, 1.0) // bleu clair
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Size;
            fixed4 _TintColor;

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv  : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = tex2D(_MainTex, uv) * 0.36;
                col += tex2D(_MainTex, uv + float2(_Size, 0)) * 0.12;
                col += tex2D(_MainTex, uv - float2(_Size, 0)) * 0.12;
                col += tex2D(_MainTex, uv + float2(0, _Size)) * 0.2;
                col += tex2D(_MainTex, uv - float2(0, _Size)) * 0.2;

                // Teinte bleue : mélange avec la couleur de teinte
                col.rgb = lerp(col.rgb, _TintColor.rgb, 0.4); // force de la teinte = 40%

                return col;
            }
            ENDCG
        }
    }
}
