Shader "ShaderBasic/DiffuseVertex"
{
    Properties
    {
        _Diffuse("Diffuse", Color) = (1.0,1.0,1.0,1.0)
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

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR; 
            };


            v2f vert (a2v v)
            {
                v2f o;
                //transform the vertex from object space to projection space
                o.pos = UnityObjectToClipPos(v.vertex);
                
                //https://docs.unity3d.com/2020.3/Documentation/Manual/SL-UnityShaderVariables.html
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                //transform the normal from object sapce to world space
                //https://docs.unity3d.com/2020.3/Documentation/Manual/UpgradeGuide54.html
                fixed3 worldNormal = normalize(mul(unity_ObjectToWorld, v.normal));

                //get the light direction in world space
                //https://docs.unity3d.com/2020.3/Documentation/Manual/SL-UnityShaderVariables.html
                fixed3 worldLight = normalize(_WorldSpaceLightPos0);

                //https://docs.unity3d.com/2020.3/Documentation/Manual/SL-UnityShaderVariables.html
                //https://learn.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-saturate
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLight));

                o.color = ambient + diffuse;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color, 1.0);
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
