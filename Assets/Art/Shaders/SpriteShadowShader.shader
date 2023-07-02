Shader "Custom/SpriteShadowShader" {
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _ShadowTex ("Shadow Texture", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 1)
    }

    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            sampler2D _ShadowTex;
            float4 _ShadowColor;

            v2f vert(appdata_t v, uint id : SV_InstanceID) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_SETUP_INSTANCE_ID(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 c = tex2D(_MainTex, i.uv) * _Color;

                // Calculate the shadow contribution
                float shadow = tex2D(_ShadowTex, i.uv).r;
                c.rgb += _ShadowColor.rgb * shadow;

                return c;
            }
            ENDCG
        }
    }
}
