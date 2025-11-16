Shader "Psyren/AnimeMaskRim"{
	Properties {
		_RedColor("Red Color", Color) = (1,1,1,1)
		_GreenColor("Green Color", Color) = (1,1,1,1)
		_BlueColor("Blue Color", Color) = (1,1,1,1)
		_Texture("Texture", 2D) = "empty" {}
		_Mask("Mask", 2D) = "white" {}
		
		_Shininess ("Shininess(0.0:)", Float) = 1.0
		_ShadowThreshold ("Shadow Threshold(0.0:1.0)", Float) = 0.6
		_ShadowColor ("Shadow Color(RGBA)", Color) = (0,0,0,0.5)
		_ShadowSharpness ("Shadow Sharpness", Float) = 100
		
		_RimColor ("Rim Color", Color) = (0.9,0.9,0.9,0.0)
      	_RimPower ("Rim Power", Range(0.5,8.0)) = 2.0
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

		half4 _RedColor;
		half4 _GreenColor;
		half4 _BlueColor;
		half4 _YellowColor;
		sampler2D _Texture;
		sampler2D _Mask;
		sampler2D _LightningRamp;
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
			fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
            fixed4 ramp = tex2D(_LightningRamp, float2(NdotL * atten, 0));
            
			//Lighting paramater
			float4	lightColor = _LightColor0 * atten * ramp;
			float	lightStrength = dot(lightDir, s.Normal) * 0.5 + 0.5;
				
			//ToonMapping
			half shadowRate = abs( max( -1, ( min( lightStrength, _ShadowThreshold ) -_ShadowThreshold)*_ShadowSharpness ) )*_ShadowColor.a;
			float4 toon = float4(1,1,1,1) * (1-shadowRate) +  _ShadowColor *shadowRate;
			
			//Output
			//float4 color = saturate( _Color * (lightColor*2) ) * s.Color;
			float4 color = (lightColor) * s.Color * (atten*2) * _Shininess;
			
        	
			
			color *= toon ;
			color.a = s.Alpha;
			return color;
		}

		void vert (inout appdata_full v, out Input o) {
		    UNITY_INITIALIZE_OUTPUT(Input,o);
		}
		
		void surf (Input IN, inout ToonSurfaceOutput o) {
			o.Albedo = 0.0;
			o.Emission = 0.0;
			o.Gloss = 0.0;
			o.Specular = 0.0;

			half4 tex = tex2D(_Texture, IN.uv_Texture);
			half4 mask = tex2D(_Mask, IN.uv_Texture);
			
			o.Color = (mask.b*_BlueColor + mask.g*_GreenColor + mask.r*_RedColor)*(1-tex.a) + tex*tex.a;
			//o.Color = (lerp(mask.b, float4(0,0,1,0), 0)*_BlueColor + lerp(mask.g, float4(0,1,0,0), 0)*_GreenColor + lerp(mask.r, float4(1,0,0,0), 0)*_RedColor)*(1-tex.a) + tex*tex.a;
			o.Albedo = o.Color.rgb * 0.5f;
		
          	if(!(mask.r < 0.3 && mask.g < 0.3 && mask.b < 0.3 && mask.a < 0.5)) {
				half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	          	o.Emission = _RimColor.rgb * pow (rim, _RimPower);
          	}
          	o.Color.a = 1;
			o.Alpha	= 1;
		}

		ENDCG
		
		
	
	
	
		// Surface Shader Pass ( Back )
		Cull Front
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf Toon vertex:vert
		#pragma target 3.0

		half4 _RedColor;
		half4 _GreenColor;
		half4 _BlueColor;
		half4 _YellowColor;
		sampler2D _Texture;
		sampler2D _Mask;
		sampler2D _LightningRamp;
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
			o.Albedo = 0.0;
			o.Emission = 0.0;
			o.Gloss = 0.0;
			o.Specular = 0.0;

			half4 tex = tex2D(_Texture, IN.uv_Texture);
			half4 mask = tex2D(_Mask, IN.uv_Texture);
			
			o.Color = (mask.b*_BlueColor + mask.g*_GreenColor + mask.r*_RedColor)*(1-tex.a) + tex*tex.a;
			o.Albedo = o.Color.rgb * 0.5f;
			o.Alpha	= 1;
		}

		ENDCG
		
		
		
		
	}
	

	Fallback "Diffuse"
}
