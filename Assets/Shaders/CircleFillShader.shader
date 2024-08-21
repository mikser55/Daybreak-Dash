Shader "Custom/CircleFillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FillAmount ("Fill Amount", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
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
            float _FillAmount;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 centeredUV = i.uv - 0.5;

                float angle = atan2(centeredUV.y, centeredUV.x) / (2.0 * UNITY_PI) + 0.5;

                angle = fmod(angle + 0.75, 1.0);

                angle = 1.0 - angle;

                float dist = length(centeredUV);

                if (angle > _FillAmount || dist > 0.5)
                    discard;

                return col;
            }
            ENDCG
        }
    }
}
