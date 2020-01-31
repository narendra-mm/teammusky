// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Spacewalk/ToolReveal"
{
	Properties
	{
		_BaseTexture("Base Texture", 2D) = "white" {}
		_Tool1Mask("Tool1Mask", 2D) = "white" {}
		_ColortoBeFiltered("Color to Be Filtered", Color) = (0.4039216,0,1,1)
		_DifferenceThreshold("Difference Threshold", Range( 0 , 0.05)) = 0
		[IntRange]_Mask("Mask", Range( 0 , 2)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf StandardCustomLighting keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform sampler2D _BaseTexture;
		uniform float4 _BaseTexture_ST;
		uniform float _Mask;
		uniform sampler2D _Tool1Mask;
		uniform float4 _Tool1Mask_ST;
		uniform float4 _ColortoBeFiltered;
		uniform float _DifferenceThreshold;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			#ifdef UNITY_PASS_FORWARDBASE
			float ase_lightAtten = data.atten;
			if( _LightColor0.a == 0)
			ase_lightAtten = 0;
			#else
			float3 ase_lightAttenRGB = gi.light.color / ( ( _LightColor0.rgb ) + 0.000001 );
			float ase_lightAtten = max( max( ase_lightAttenRGB.r, ase_lightAttenRGB.g ), ase_lightAttenRGB.b );
			#endif
			#if defined(HANDLE_SHADOWS_BLENDING_IN_GI)
			half bakedAtten = UnitySampleBakedOcclusion(data.lightmapUV.xy, data.worldPos);
			float zDist = dot(_WorldSpaceCameraPos - data.worldPos, UNITY_MATRIX_V[2].xyz);
			float fadeDist = UnityComputeShadowFadeDistance(data.worldPos, zDist);
			ase_lightAtten = UnityMixRealtimeAndBakedShadows(data.atten, bakedAtten, UnityComputeShadowFade(fadeDist));
			#endif
			float2 uv_BaseTexture = i.uv_texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			float4 tex2DNode51 = tex2D( _BaseTexture, uv_BaseTexture );
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 temp_output_59_0 = ( ase_lightAtten * ase_lightColor );
			float2 uv_Tool1Mask = i.uv_texcoord * _Tool1Mask_ST.xy + _Tool1Mask_ST.zw;
			float4 tex2DNode52 = tex2D( _Tool1Mask, uv_Tool1Mask );
			float ifLocalVar121 = 0;
			if( _Mask > 1.0 )
				ifLocalVar121 = tex2DNode52.b;
			else if( _Mask == 1.0 )
				ifLocalVar121 = tex2DNode52.g;
			else if( _Mask < 1.0 )
				ifLocalVar121 = tex2DNode52.r;
			float3 normalizeResult116 = normalize( (( ase_lightColor * _WorldSpaceLightPos0.w )).rgb );
			float3 normalizeResult117 = normalize( (_ColortoBeFiltered).rgb );
			float dotResult96 = dot( normalizeResult116 , normalizeResult117 );
			c.rgb = ( ( tex2DNode51 * saturate( temp_output_59_0 ) ) + ( ifLocalVar121 * ( ( temp_output_59_0 * _WorldSpaceLightPos0.w ) *  ( dotResult96 - _DifferenceThreshold > 1.0 ? 0.0 : dotResult96 - _DifferenceThreshold <= 1.0 && dotResult96 + _DifferenceThreshold >= 1.0 ? 1.0 : 0.0 )  ) ) ).rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			float2 uv_BaseTexture = i.uv_texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			float4 tex2DNode51 = tex2D( _BaseTexture, uv_BaseTexture );
			o.Emission = tex2DNode51.rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
2112;22;1473;990;597.178;387.0431;1;True;True
Node;AmplifyShaderEditor.WorldSpaceLightPos;54;-1219.7,205.2679;Inherit;False;0;3;FLOAT4;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LightColorNode;84;-1132.925,359.0915;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-915.9946,345.3203;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;87;-1057.309,513.7495;Float;False;Property;_ColortoBeFiltered;Color to Be Filtered;2;0;Create;True;0;0;False;0;0.4039216,0,1,1;0,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;92;-734.5231,485.7451;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;91;-731.6473,379.2185;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;117;-461.5048,470.5531;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;116;-467.2045,386.9525;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LightAttenuation;53;-896.7832,16.67318;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.LightColorNode;58;-875.2192,128.332;Inherit;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;90;-721.6726,682.4335;Float;False;Property;_DifferenceThreshold;Difference Threshold;3;0;Create;True;0;0;False;0;0;0.0104;0;0.05;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;96;-303.6713,411.4413;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-681.7908,71.68948;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-678.827,588.9836;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCIf;100;-145.8477,538.6387;Inherit;False;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-421.8257,228.5361;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;52;-456.3876,-160.8523;Inherit;True;Property;_Tool1Mask;Tool1Mask;1;0;Create;True;0;0;False;0;-1;None;a6b31d822de6a254cb61ad502ead21ca;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;122;-497.178,60.95691;Inherit;False;Property;_Mask;Mask;4;1;[IntRange];Create;True;0;0;False;0;0;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;121;-69.17798,19.95691;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;79;-513.2228,-222.412;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;51;-760.7654,-475.1937;Inherit;True;Property;_BaseTexture;Base Texture;0;0;Create;True;0;0;False;0;-1;None;0688235f1fee46b4581bcc1cf189cf3a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;92.92843,269.5623;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;295.7195,105.5482;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;58.29993,-273.679;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;78;471.2068,-51.62606;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;823.8683,-312.9128;Float;False;True;-1;2;ASEMaterialInspector;0;0;CustomLighting;Spacewalk/ToolReveal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;85;0;84;0
WireConnection;85;1;54;2
WireConnection;92;0;87;0
WireConnection;91;0;85;0
WireConnection;117;0;92;0
WireConnection;116;0;91;0
WireConnection;96;0;116;0
WireConnection;96;1;117;0
WireConnection;59;0;53;0
WireConnection;59;1;58;0
WireConnection;100;0;96;0
WireConnection;100;1;98;0
WireConnection;100;3;98;0
WireConnection;100;5;90;0
WireConnection;55;0;59;0
WireConnection;55;1;54;2
WireConnection;121;0;122;0
WireConnection;121;2;52;3
WireConnection;121;3;52;2
WireConnection;121;4;52;1
WireConnection;79;0;59;0
WireConnection;99;0;55;0
WireConnection;99;1;100;0
WireConnection;57;0;121;0
WireConnection;57;1;99;0
WireConnection;80;0;51;0
WireConnection;80;1;79;0
WireConnection;78;0;80;0
WireConnection;78;1;57;0
WireConnection;0;2;51;0
WireConnection;0;13;78;0
ASEEND*/
//CHKSM=2A0B699C1F167F389A6CAC8C938EA981191B8F85