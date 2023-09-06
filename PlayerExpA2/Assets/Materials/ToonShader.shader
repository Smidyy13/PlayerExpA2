Shader "Guidev/NewUnlitShader"
{
    Properties
    {
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

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the cosine of the angle between the normal vector and the light direction
                // The world space light direction is stored in _worldSpaceLightPose0
                float cosineAngle = dot(normalize(o.worldNormal), normalize(_worldSpaceLightPose0.xyz));
                cosineAngle = max(cosineAngle, 0.0);
                return fixed4(cosineAngle,cosineAngle,cosineAngle,1.0);
            }
            ENDCG
        }
    }
}
