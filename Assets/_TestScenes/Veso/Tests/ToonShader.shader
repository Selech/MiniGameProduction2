 Shader "Custom/Outlined Diffuse" {
     Properties {
         _MainTex ("Base (RGB)", 2D) = "white" { }
         _Ramp ("Shading Ramp", 2D) = "gray" {}
     }
     
 CGINCLUDE
 #include "UnityCG.cginc"
 
 struct appdata {
     half4 vertex : POSITION;
     half3 normal : NORMAL;
 };
 
 struct v2f {
     half4 pos : POSITION;
 };
 
 v2f vert(appdata v) {
     // just make a copy of incoming vertex data but scaled according to normal direction
     v2f o;
     o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
 
     half3 norm   = mul ((half3x3)UNITY_MATRIX_IT_MV, v.normal);
     half2 offset = TransformViewToProjection(norm.xy);
 
     o.pos.xy += offset * o.pos.z * 0.002;
     return o;
 }
 
 half4 frag(v2f i) : COLOR {
     return half4(0.0, 0.0, 0.0, 1.0);
 }
 
 ENDCG
 
     SubShader {
         //Tags {"Queue" = "Geometry+100" }
 CGPROGRAM
         #pragma surface surf Ramp noambient noforwardadd approxview
 
           sampler2D_half _Ramp;
           
         half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
           half NdotL = dot (s.Normal, lightDir);
           half diff = NdotL * 0.5 + 0.5;
           half3 ramp = tex2D (_Ramp, half2(diff,diff)).rgb;
           half4 c;
           c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
           c.a = s.Alpha;
           return c;
           }
 
         sampler2D_half _MainTex;
 
         struct Input {
             half2 uv_MainTex;
         };
 
         void surf (Input IN, inout SurfaceOutput o) {
             fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
             o.Albedo = c.rgb;
             o.Alpha = c.a;
         }
 
 ENDCG
 
         // note that a vertex shader is specified here but its using the one above
         Pass {
             Name "OUTLINE"
             Tags { "LightMode" = "Always" }
             Cull Front
             ZWrite On
             //Offset 50,50
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag            
             ENDCG
         }
     }
                 
     Fallback "Diffuse"
 }