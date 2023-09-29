Shader "Animation/ImageSequenceAnimation"
{
    Properties
    {
        _Color("Color Tint", Color) = (1,1,1,1)
        _MainTex("Image Sequence", 2D) = "white"{}
        _HorizontalAmount("Horizontal Amount", Float) = 4
        _VerticalAmount("Vertical Amount", Float) = 4
        _Speed("Speed", Range(1,100)) = 30

    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Opaque" }

        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HorizontalAmount;
            float _VerticalAmount;
            float _Speed;

            struct a2v
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (a2v v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //https://docs.unity3d.com/2020.3/Documentation/Manual/SL-UnityShaderVariables.html
                //https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-floor
                // (t/20, t, t*2, t*3)
                float time = floor(_Time.y  * _Speed);

                //_Time.y    row                 column
               //   8       floor(8/4)=2        8-2*4=0
               //   9       floor(9/4)=2        9-2*4=1
               //   10      floor(10/4)=2       10-2*4=2
               //   11      floor(11/4)=2       11-2*4=3
               //   12      floor(12/4)=3       12-3*4=0
                float row = floor(time / _HorizontalAmount);
                float column = time - row * _VerticalAmount;

              
                //若取i.uv=(1,1) _HorizontalAmount=_VerticalAmount=4
                //i.uv.x/ _HorizontalAmount=1/4=0.25   i.uv.y/ _VerticalAmount=1/4=0.25
                half2 uv = float2(i.uv.x / _HorizontalAmount, i.uv.y / _VerticalAmount);

                //column     row   i.uv.x            i.uv.y
               //   0        2    0.25+0/4=0.25     0.25-2/4=-0.25
               //   1        2    0.25+1/4=0.5      0.25-2/4=-0.25
               //   2        2    0.25+2/4=0.75     0.25-2/4=-0.25
               //   3        2    0.25+3/4=1        0.25-2/4=-0.25
                uv.x += column / _HorizontalAmount;
                uv.y -= row / _VerticalAmount;

                fixed4 c = tex2D(_MainTex, uv);
                c.rgb *= _Color;

                return c;
            }
            ENDCG
        }
    }
}
