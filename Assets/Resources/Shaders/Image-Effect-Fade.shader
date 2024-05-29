Shader "Hidden/Image-Effect-Fade"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}

        _Color("Color", Color) = (1,1,1,1)

        _Alpha("Alpha", Range(0, 1)) = 1
        _Blend("Blend", Range(0, 1)) = 0
    }

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _OverlayTex;

			fixed4 _Color;

            half _Alpha;
            half _Blend;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
				//fixed4 overlay = tex2D(_OverlayTex, i.uv);

				//if (overlay.b < _Blend)
					//return color = lerp(color, _Color, _Alpha);

			    if (i.uv.x < _Blend)
					return color = lerp(color, _Color, _Alpha);
                
				return color;
            }
            ENDCG
        }
    }
}
