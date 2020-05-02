Shader "Custom/Terrain"
{
    Properties
    {
		testTexture ("Texture", 2D) = "white"{}
		testScale ("Scale", float) = 1
		_Curvature ("Curvature", Float) = 0.001
		test ("testt", float) = 0.001
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

		const static int maxLayerCount=8;
		const static float epsilon = 1E-4;

		int layerCount;
		float3 baseColors[maxLayerCount];
		float baseStartHeights[maxLayerCount];
		float baseBlends[maxLayerCount];
		float baseColorStrength[maxLayerCount];
		float baseTextureScales[maxLayerCount];

        float minHeight;
		float maxHeight;

		sampler2D testTexture;
		float testScale;

		UNITY_DECLARE_TEX2DARRAY(baseTextures);
		uniform float _Curvature;
		float test;
		

        struct Input
        {
            float3 worldPos;
			float3 worldNormal;
        };
		 
		float inverseLerp(float a, float b, float value){
				return saturate( (value-a)/(b-a) );
		}

		float3 triplanar(float3 worldPos, float scale, float3 blendAxes, int textureIndex){
			float3 scaledWorldPos = worldPos/scale;

			float3 xProjection = UNITY_SAMPLE_TEX2DARRAY(baseTextures, float3(scaledWorldPos.y, scaledWorldPos.z, textureIndex))*blendAxes.x;
			float3 yProjection = UNITY_SAMPLE_TEX2DARRAY(baseTextures, float3(scaledWorldPos.x, scaledWorldPos.z, textureIndex))*blendAxes.y;
			float3 zProjection = UNITY_SAMPLE_TEX2DARRAY(baseTextures, float3(scaledWorldPos.x, scaledWorldPos.y, textureIndex))*blendAxes.z;
			return xProjection+yProjection+zProjection;

		}

		

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float heightPercent = inverseLerp(minHeight,maxHeight,IN.worldPos.y);

			float3 blendAxes = abs(IN.worldNormal);
			blendAxes /= blendAxes.x+blendAxes.y+blendAxes.z;

            for (int i= 0; i < layerCount; i++) {
				float drawStrenght = inverseLerp(-baseBlends[i]/2-epsilon,baseBlends[i]/2-epsilon+1,heightPercent-baseStartHeights[i]+test*1.0f);

				float3 baseColor = baseColors[i]*baseColorStrength[i];
				float3 textureColor = triplanar(IN.worldPos, baseTextureScales[i], blendAxes, i) * (1-baseColorStrength[i]);

				o.Albedo=o.Albedo*(1-drawStrenght)+(baseColor+textureColor)*drawStrenght;
			}

        }

		void vert(inout appdata_full v){
			// Transform the vertex coordinates from model space into world space
            float4 vv = mul( unity_ObjectToWorld, v.vertex );
 
            // Now adjust the coordinates to be relative to the camera position
            vv.xyz -= _WorldSpaceCameraPos.xyz;
			
            // Reduce the y coordinate (i.e. lower the "height") of each vertex based
            // on the square of the distance from the camera in the z axis, multiplied
            // by the chosen curvature factor
            vv = float4( 0.0f, (vv.x * vv.x + vv.z * vv.z) * - _Curvature, 0.0f, 0.0f );
			v.vertex += mul(unity_WorldToObject, vv);
			test = vv.y;
		}
        ENDCG
    }
    FallBack "Diffuse"
}
