Shader "Custom/SplitColorShader"
{
    Properties
    {
        _SplitPosition ("Split Position", Range(0, 1)) = 0.5
        _Color1 ("Color 1", Color) = (0, 0, 0, 1)
        _Color2 ("Color 2", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            float _SplitPosition;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color;
                if (i.uv.y < _SplitPosition)
                    color = _Color1;
                else
                    color = _Color2;

                return color;
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}