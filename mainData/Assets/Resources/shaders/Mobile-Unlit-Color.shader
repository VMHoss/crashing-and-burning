Shader "Mobile/Stijns Unlit With Color" {
Properties {
 _ExtraRimColor ("Extra Rim Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader { 
 LOD 100
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "LIGHTMODE"="Vertex" "RenderType"="Opaque" }
  SetTexture [_MainTex] { ConstantColor [_ExtraRimColor] combine texture + constant }
 }
}
}