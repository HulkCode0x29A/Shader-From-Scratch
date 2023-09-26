Shader "ShaderBasic/SpecularPixel"
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
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };


            v2f vert (appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);
                //transform the normal form object space to world space
                o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
                //transform the vertex from object sapce to world space
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

                //diffuse term
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir));
                //reflect direction
                fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));
                //get view direction in world space
                fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
                //compute specular term
                fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(reflectDir, viewDir)), _Gloss);

                fixed3 color = ambient + diffuse + specular;
                
                return fixed4(color, 1.0);

            }
            ENDCG
        }
    }
}
