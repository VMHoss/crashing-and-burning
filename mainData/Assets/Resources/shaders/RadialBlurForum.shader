Shader "Hidden/radialBlur" {
Properties {
 _MainTex ("Input", 2D) = "white" {}
 _BlurStrength ("", Float) = 0.5
 _BlurWidth ("", Float) = 0.5
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
}
Program "fp" {
}
 }
}
}