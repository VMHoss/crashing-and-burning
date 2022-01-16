Shader "MaskedTexture" {
Properties {
 _Color ("_Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _Mask ("Culling Mask", 2D) = "white" {}
 _Cutoff ("Alpha cutoff", Range(0,1)) = 0.1
}
SubShader { 
 Tags { "QUEUE"="Overlay+500" }
 Pass {
  Tags { "QUEUE"="Overlay+500" }
  ZTest Always
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_Mask] { combine texture }
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * constant, previous alpha * constant alpha }
 }
}
}