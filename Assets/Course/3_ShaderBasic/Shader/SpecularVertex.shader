Shader "ShaderBasic/SpecularVertex"
{
    Properties
    {
       _Diffuse("Diffuse", Color) = (1,1,1,1)
       _Specular("Specular", Color) = (1,1,1,1)
       _Gloss("Gloss", Range(8.0,256)) = 20
    }
    SubShader
    {
        Pass
        {
            Tags{"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
          
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            fixed4 _Diffuse;
            fixed4 _Specular;
            float _Gloss;

            struct appdata
            {
               float4 vertex : POSITION;
               float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR;
            };


            v2f vert (appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                fixed3 worldNormal = normalize(mul(unity_ObjectToWorld, v.normal));

                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

                //compute diffuse term
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir));

                //get the relfect direction in world space
                //https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-reflect
                fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));

                //get view direction in world space
                //https://docs.unity3d.com/2020.3/Documentation/Manual/SL-UnityShaderVariables.html
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);

                //compute specular term
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(reflectDir, viewDir)), _Gloss);

                o.color = ambient + diffuse + specular;
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
              return fixed4(i.color, 1.0);
            }
            ENDCG
        }
    }



    Fallback "Specular"
}
