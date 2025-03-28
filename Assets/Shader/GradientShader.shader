Shader "Custom/GradientShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alpha = (1.0 - i.uv.x) * 1.5; // Nhân hệ số để đen hơn
                alpha = saturate(alpha); // Đảm bảo không vượt quá 1
                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}
