  Shader "Screenspace" {
    Properties {
      //_OverTex ("Main", 2D) = "white" {}
      _MainTex ("Mask", 2D) = "white" {}
      _Detail ("Detail", 2D) = "gray" {}
    }
    SubShader {
      Tags { "Queue"="Transparent" "RenderType"="Transparent" }
      CGPROGRAM
      #pragma surface surf Lambert alpha
      struct Input {
          float2 uv_MainTex;
          //float2 uv_OverTex;
          float4 screenPos;
      };
      sampler2D _MainTex;
      //sampler2D _OverTex;
      sampler2D _Detail;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
          screenUV *= float2(8,6);
          o.Albedo *= tex2D (_Detail, screenUV).rgb;
          //o.Albedo += tex2D (_OverTex, IN.uv_OverTex).a;
          o.Alpha += tex2D (_MainTex, IN.uv_MainTex).a;
          
          //o.Albedo += (tex2D (_OverTex, IN.uv_OverTex).rgb;
          //o.Alpha += (tex2D (_OverTex, IN.uv_OverTex).a);
          //o.Albedo *= (tex2D (_OverTex, IN.uv_OverTex).rgb * 2 * (1-o.Alpha));
          
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }