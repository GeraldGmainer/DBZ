// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Psyren/AnimeOutlineRim"{
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		
		_Shininess ("Shininess(0.0:)", Float) = 1.0
		_ShadowThreshold ("Shadow Threshold(0.0:1.0)", Float) = 0.6
		_ShadowColor ("Shadow Color(RGBA)", Color) = (0,0,0,0.5)
		_ShadowSharpness ("Shadow Sharpness", Float) = 100
		
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline Width", Range (.001, 0.03)) = 0.0024
		_OutlineDistancePower ("Outline Distance Power", Range(.001, 1)) = 0.722
		
		_RimColor ("Rim Color", Color) = (0.9,0.9,0.9,0.0)
      	_RimPower ("Rim Power", Float) = 2.0
	}

	SubShader {
		// Settings
		Tags {"Queue" = "Transparent" "IgnoreProjector"="False" "RenderType" = "Transparent"}
		
		
		// Surface Shader Pass ( Front )
		Cull Back
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Toon vertex:vert
		#pragma target 3.0

		half4 _Color;
		float	_ShadowThreshold;
		float4	_ShadowColor;
		float	_ShadowSharpness;
		float	_Shininess;
		float4 _RimColor;
      	float _RimPower;
		
		struct Input {
			float2 uv_Texture;
			float3 viewDir;
			float3 worldNormal;
		};

		struct ToonSurfaceOutput {
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
			half4 Color;
		};

		inline half4 LightingToon (ToonSurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
			//Lighting paramater
			float4	lightColor = _LightColor0 * atten  ;
			float	lightStrength = dot(lightDir, s.Normal) * 0.5 + 0.5;
				
			//ToonMapping
			half shadowRate = abs( max( -1, ( min( lightStrength, _ShadowThreshold ) -_ShadowThreshold)*_ShadowSharpness ) )*_ShadowColor.a;
			float4 toon = float4(1,1,1,1) * (1-shadowRate) +  _ShadowColor * shadowRate;
			
			//Output
			//float4 color = saturate( _Color * (lightColor*2) ) * s.Color;
			float4 color = (lightColor) * s.Color * (atten*2) * _Shininess;
			
			color *= toon;
			color.a = s.Alpha;
			return color;
		}

		void vert (inout appdata_full v, out Input o) {
		    UNITY_INITIALIZE_OUTPUT(Input,o);
		}
		
		void surf (Input IN, inout ToonSurfaceOutput o) {
			o.Gloss = 0.0;
			o.Specular = 0.0;
			o.Color = _Color;
			o.Albedo = o.Color.rgb * 0.5f;
			o.Alpha	= 1;
			
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	        o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}

		ENDCG
		
		
	
	
	
		// Surface Shader Pass ( Back )
		Cull Front
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Toon vertex:vert
		#pragma target 3.0

		half4 _Color;
		float	_ShadowThreshold;
		float4	_ShadowColor;
		float	_ShadowSharpness;
		float	_Shininess;
		float4 _RimColor;
      	float _RimPower;


		struct ToonSurfaceOutput{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Gloss;
			half Specular;
			half Alpha;
			half4 Color;
		};
		
		struct Input {
			float2 uv_Texture;
		};

		inline half4 LightingToon (ToonSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			//Lighting paramater
			float4	lightColor = _LightColor0 * atten;
			float	lightStrength = dot(lightDir, s.Normal) * 0.5 + 0.5;
				
			//ToonMapping
			half shadowRate = abs( max( -1, ( min( lightStrength, _ShadowThreshold ) -_ShadowThreshold)*_ShadowSharpness ) )*_ShadowColor.a;
			float4 toon = float4(1,1,1,1) * (1-shadowRate) +  _ShadowColor *shadowRate;
			
			//Output
			//float4 color = saturate( _Color * (lightColor*2) ) * s.Color;
			float4 color = (lightColor) * s.Color * (atten*2) * _Shininess;
			
			color *= toon;
			color.a = s.Alpha;
			return color;
		}

		void vert (inout appdata_full v, out Input o) {
		    UNITY_INITIALIZE_OUTPUT(Input,o);
		}


		void surf (Input IN, inout ToonSurfaceOutput o) {
			o.Emission = 0.0;
			o.Gloss = 0.0;
			o.Specular = 0.0;
			
			o.Color = _Color;
			o.Albedo = o.Color.rgb * 0.5f;
			o.Alpha	= 1;
		}

		ENDCG
		
		
		
		
		Pass {
            // Pass drawing outline
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha
           
            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag
           
            float _Outline;
            float4 _OutlineColor;
            float4 _Color;
            float _OutlineDistancePower;
            sampler2D _Mask;
 
            struct v2f {
                float4 pos : POSITION;
                float4 color : COLOR;
            };
           
            v2f vert(appdata_base v) {
            	float dist = pow(distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex)), _OutlineDistancePower);
            	
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(norm.xy);
                o.pos.xy += offset  * _Outline * dist;
                o.color = _OutlineColor;
                
                return o;
            }
           
            half4 frag(v2f i) :COLOR {
                return i.color;
            }
                   
            ENDCG
        }
       
        
	}
	

	Fallback "Diffuse"
}
