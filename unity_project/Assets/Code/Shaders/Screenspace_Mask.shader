  Shader "Screenspace (Mask)" {
    Properties {
      //_OverTex ("Main", 2D) = "white" {}
      _MainTex ("Mask", 2D) = "white" {}
      _Detail ("Detail", 2D) = "gray" {}
      _TexW ("texture width", Float) = 100
      _TexH ("texture height", Float) = 100
    }
    SubShader {
      Tags { "Queue"="Transparent" "RenderType"="Transparent" }
      CGPROGRAM
      #pragma surface surf NoLighting alpha
      struct Input {
          float2 uv_MainTex;
          //float2 uv_OverTex;
          float4 screenPos;
      };
      sampler2D _MainTex;
      float _TexW;
      float _TexH;
      //sampler2D _OverTex;
      sampler2D _Detail;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2((_ScreenParams.x/_TexW),(_ScreenParams.y/_TexH));
          o.Albedo *= tex2D (_Detail, screenUV).rgb;
          //o.Albedo += tex2D (_OverTex, IN.uv_OverTex).a;
          o.Alpha += tex2D (_MainTex, IN.uv_MainTex).a*tex2D (_Detail, screenUV).a;
          
          //o.Albedo += (tex2D (_OverTex, IN.uv_OverTex).rgb;
          //o.Alpha += (tex2D (_OverTex, IN.uv_OverTex).a);
          //o.Albedo *= (tex2D (_OverTex, IN.uv_OverTex).rgb * 2 * (1-o.Alpha));
          
      }
      
      fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten){
          // Declare the variable that will store the final pixel color,
          fixed4 c;
          // Copy the diffuse color component from the SurfaceOutput to the final pixel.
          c.rgb = s.Albedo; 
          // Copy the alpha component from the SurfaceOutput to the final pixel.
          c.a = s.Alpha;
          return c;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }