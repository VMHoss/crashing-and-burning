Shader "CarShaderModel 3.0" {
Properties {
 _MainColor ("_MainColor", Color) = (1,1,1,1)
 _Diffuse ("_Diffuse", 2D) = "black" {}
 _Gloss ("_Gloss", Range(0,1)) = 0.045
 _SpecularColor ("_SpecularColor", Color) = (0.985075,0.892335,0.683671,1)
 _Cube ("_Cube", CUBE) = "black" {}
 _Mask ("_Mask", 2D) = "white" {}
 _DamageMask ("_DamageMask", 2D) = "black" {}
 _DamageColor ("_DamageColor", Color) = (0,0,0,1)
 _RimColor ("_RimColor", Color) = (0,0,0,0)
 _Damage ("_Damage", Range(0,1)) = 0
}
SubShader { 
 Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="False" "RenderType"="Opaque" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Geometry" "IGNOREPROJECTOR"="False" "RenderType"="Opaque" }
Program "vp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_26;
  highp vec4 tmpvar_27;
  tmpvar_27.w = 1.0;
  tmpvar_27.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_29;
  mediump vec4 normal_30;
  normal_30 = tmpvar_28;
  highp float vC_31;
  mediump vec3 x3_32;
  mediump vec3 x2_33;
  mediump vec3 x1_34;
  highp float tmpvar_35;
  tmpvar_35 = dot (unity_SHAr, normal_30);
  x1_34.x = tmpvar_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAg, normal_30);
  x1_34.y = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAb, normal_30);
  x1_34.z = tmpvar_37;
  mediump vec4 tmpvar_38;
  tmpvar_38 = (normal_30.xyzz * normal_30.yzzx);
  highp float tmpvar_39;
  tmpvar_39 = dot (unity_SHBr, tmpvar_38);
  x2_33.x = tmpvar_39;
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBg, tmpvar_38);
  x2_33.y = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBb, tmpvar_38);
  x2_33.z = tmpvar_41;
  mediump float tmpvar_42;
  tmpvar_42 = ((normal_30.x * normal_30.x) - (normal_30.y * normal_30.y));
  vC_31 = tmpvar_42;
  highp vec3 tmpvar_43;
  tmpvar_43 = (unity_SHC.xyz * vC_31);
  x3_32 = tmpvar_43;
  tmpvar_29 = ((x1_34 + x2_33) + x3_32);
  shlight_3 = tmpvar_29;
  tmpvar_9 = shlight_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_27).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  highp vec3 tmpvar_30;
  tmpvar_30 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_31;
  lightDir_31 = xlv_TEXCOORD5;
  mediump vec3 viewDir_32;
  viewDir_32 = tmpvar_30;
  mediump vec4 res_33;
  highp float nh_34;
  mediump float tmpvar_35;
  tmpvar_35 = max (0.0, dot (tmpvar_29, normalize((lightDir_31 + viewDir_32))));
  nh_34 = tmpvar_35;
  mediump float arg1_36;
  arg1_36 = (tmpvar_12 * 128.0);
  res_33.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_31, tmpvar_29)));
  lowp float tmpvar_37;
  tmpvar_37 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_38;
  tmpvar_38 = (pow (nh_34, arg1_36) * tmpvar_37);
  res_33.w = tmpvar_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (res_33 * 2.0);
  res_33 = tmpvar_39;
  mediump vec4 c_40;
  c_40.xyz = ((tmpvar_10 * tmpvar_39.xyz) + (tmpvar_39.xyz * (tmpvar_39.w * tmpvar_11)));
  c_40.w = 1.0;
  c_1 = c_40;
  mediump vec3 tmpvar_41;
  tmpvar_41 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_41;
  mediump vec3 tmpvar_42;
  tmpvar_42 = c_1.xyz;
  c_1.xyz = tmpvar_42;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 500
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 477
v2f_surf vert_surf( in appdata_full v ) {
    #line 479
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 483
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 487
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 491
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 495
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 500
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 411
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 415
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 419
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 500
lowp vec4 frag_surf( in v2f_surf IN ) {
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    #line 504
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    #line 508
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    #line 512
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    o.Alpha = 0.0;
    surf( surfIN, o);
    #line 516
    lowp float atten = 1.0;
    lowp vec4 c = vec4( 0.0);
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    #line 520
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w)));
  highp vec3 tmpvar_10;
  highp vec3 tmpvar_11;
  tmpvar_10 = tmpvar_1.xyz;
  tmpvar_11 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_12;
  tmpvar_12[0].x = tmpvar_10.x;
  tmpvar_12[0].y = tmpvar_11.x;
  tmpvar_12[0].z = tmpvar_2.x;
  tmpvar_12[1].x = tmpvar_10.y;
  tmpvar_12[1].y = tmpvar_11.y;
  tmpvar_12[1].z = tmpvar_2.y;
  tmpvar_12[2].x = tmpvar_10.z;
  tmpvar_12[2].y = tmpvar_11.z;
  tmpvar_12[2].z = tmpvar_2.z;
  vec4 v_13;
  v_13.x = _Object2World[0].x;
  v_13.y = _Object2World[1].x;
  v_13.z = _Object2World[2].x;
  v_13.w = _Object2World[3].x;
  highp vec4 tmpvar_14;
  tmpvar_14.xyz = (tmpvar_12 * v_13.xyz);
  tmpvar_14.w = tmpvar_9.x;
  highp vec4 tmpvar_15;
  tmpvar_15 = (tmpvar_14 * unity_Scale.w);
  tmpvar_4 = tmpvar_15;
  vec4 v_16;
  v_16.x = _Object2World[0].y;
  v_16.y = _Object2World[1].y;
  v_16.z = _Object2World[2].y;
  v_16.w = _Object2World[3].y;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_12 * v_16.xyz);
  tmpvar_17.w = tmpvar_9.y;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].z;
  v_19.y = _Object2World[1].z;
  v_19.z = _Object2World[2].z;
  v_19.w = _Object2World[3].z;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_12 * v_19.xyz);
  tmpvar_20.w = tmpvar_9.z;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_12 * (((_World2Object * tmpvar_22).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  highp vec4 TexCUBE0_11;
  highp vec4 DamageMask_12;
  highp vec4 Tex2D2_13;
  highp vec4 Tex2D0_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_14 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_13 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_12 = tmpvar_17;
  highp vec3 tmpvar_18;
  tmpvar_18.x = tmpvar_3.z;
  tmpvar_18.y = tmpvar_4.z;
  tmpvar_18.z = tmpvar_5.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_18, tmpvar_2) * tmpvar_18)));
  lowp vec4 tmpvar_20;
  tmpvar_20 = textureCube (_Cube, tmpvar_19.xyz);
  TexCUBE0_11 = tmpvar_20;
  highp vec4 tmpvar_21;
  tmpvar_21 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_22;
  tmpvar_22 = mix (((Tex2D0_14 + (Tex2D2_13 * (TexCUBE0_11 * tmpvar_21))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_12.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_23;
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.z, _Damage)));
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = (tmpvar_10 + ((_RimColor * pow (tmpvar_21, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_25;
  lowp vec3 tmpvar_26;
  tmpvar_26 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz);
  mediump vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 * tmpvar_26);
  c_1.xyz = tmpvar_27;
  c_1.w = 1.0;
  mediump vec3 tmpvar_28;
  tmpvar_28 = c_1.xyz;
  c_1.xyz = tmpvar_28;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 474
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D unity_Lightmap;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 477
v2f_surf vert_surf( in appdata_full v ) {
    #line 479
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 483
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 487
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.lmap.xy = ((v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    #line 491
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 496
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec2(xl_retval.lmap);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 474
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D unity_Lightmap;
#line 176
lowp vec3 DecodeLightmap( in lowp vec4 color ) {
    #line 178
    return (2.0 * color.xyz);
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 499
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 501
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 505
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 509
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 513
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp float atten = 1.0;
    lowp vec4 c = vec4( 0.0);
    #line 517
    lowp vec4 lmtex = texture( unity_Lightmap, IN.lmap.xy);
    lowp vec3 lm = DecodeLightmap( lmtex);
    c.xyz += (o.Albedo * lm);
    c.w = o.Alpha;
    #line 521
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lmap = vec2(xlv_TEXCOORD5);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_26;
  highp vec4 tmpvar_27;
  tmpvar_27.w = 1.0;
  tmpvar_27.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_29;
  mediump vec4 normal_30;
  normal_30 = tmpvar_28;
  highp float vC_31;
  mediump vec3 x3_32;
  mediump vec3 x2_33;
  mediump vec3 x1_34;
  highp float tmpvar_35;
  tmpvar_35 = dot (unity_SHAr, normal_30);
  x1_34.x = tmpvar_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAg, normal_30);
  x1_34.y = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAb, normal_30);
  x1_34.z = tmpvar_37;
  mediump vec4 tmpvar_38;
  tmpvar_38 = (normal_30.xyzz * normal_30.yzzx);
  highp float tmpvar_39;
  tmpvar_39 = dot (unity_SHBr, tmpvar_38);
  x2_33.x = tmpvar_39;
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBg, tmpvar_38);
  x2_33.y = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBb, tmpvar_38);
  x2_33.z = tmpvar_41;
  mediump float tmpvar_42;
  tmpvar_42 = ((normal_30.x * normal_30.x) - (normal_30.y * normal_30.y));
  vC_31 = tmpvar_42;
  highp vec3 tmpvar_43;
  tmpvar_43 = (unity_SHC.xyz * vC_31);
  x3_32 = tmpvar_43;
  tmpvar_29 = ((x1_34 + x2_33) + x3_32);
  shlight_3 = tmpvar_29;
  tmpvar_9 = shlight_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_27).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
  xlv_TEXCOORD7 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  lowp float tmpvar_30;
  mediump float lightShadowDataX_31;
  highp float dist_32;
  lowp float tmpvar_33;
  tmpvar_33 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  dist_32 = tmpvar_33;
  highp float tmpvar_34;
  tmpvar_34 = _LightShadowData.x;
  lightShadowDataX_31 = tmpvar_34;
  highp float tmpvar_35;
  tmpvar_35 = max (float((dist_32 > (xlv_TEXCOORD7.z / xlv_TEXCOORD7.w))), lightShadowDataX_31);
  tmpvar_30 = tmpvar_35;
  highp vec3 tmpvar_36;
  tmpvar_36 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_37;
  lightDir_37 = xlv_TEXCOORD5;
  mediump vec3 viewDir_38;
  viewDir_38 = tmpvar_36;
  mediump float atten_39;
  atten_39 = tmpvar_30;
  mediump vec4 res_40;
  highp float nh_41;
  mediump float tmpvar_42;
  tmpvar_42 = max (0.0, dot (tmpvar_29, normalize((lightDir_37 + viewDir_38))));
  nh_41 = tmpvar_42;
  mediump float arg1_43;
  arg1_43 = (tmpvar_12 * 128.0);
  res_40.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_37, tmpvar_29)));
  lowp float tmpvar_44;
  tmpvar_44 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_45;
  tmpvar_45 = (pow (nh_41, arg1_43) * tmpvar_44);
  res_40.w = tmpvar_45;
  mediump vec4 tmpvar_46;
  tmpvar_46 = (res_40 * (atten_39 * 2.0));
  res_40 = tmpvar_46;
  mediump vec4 c_47;
  c_47.xyz = ((tmpvar_10 * tmpvar_46.xyz) + (tmpvar_46.xyz * (tmpvar_46.w * tmpvar_11)));
  c_47.w = 1.0;
  c_1 = c_47;
  mediump vec3 tmpvar_48;
  tmpvar_48 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_48;
  mediump vec3 tmpvar_49;
  tmpvar_49 = c_1.xyz;
  c_1.xyz = tmpvar_49;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 500
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    #line 508
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
    xlv_TEXCOORD7 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 411
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 415
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 419
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 423
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 427
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    highp float dist = textureProj( _ShadowMapTexture, shadowCoord).x;
    mediump float lightShadowDataX = _LightShadowData.x;
    #line 388
    return max( float((dist > (shadowCoord.z / shadowCoord.w))), lightShadowDataX);
}
#line 510
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 512
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 516
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 520
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 524
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    #line 528
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD6;
varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w)));
  highp vec3 tmpvar_10;
  highp vec3 tmpvar_11;
  tmpvar_10 = tmpvar_1.xyz;
  tmpvar_11 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_12;
  tmpvar_12[0].x = tmpvar_10.x;
  tmpvar_12[0].y = tmpvar_11.x;
  tmpvar_12[0].z = tmpvar_2.x;
  tmpvar_12[1].x = tmpvar_10.y;
  tmpvar_12[1].y = tmpvar_11.y;
  tmpvar_12[1].z = tmpvar_2.y;
  tmpvar_12[2].x = tmpvar_10.z;
  tmpvar_12[2].y = tmpvar_11.z;
  tmpvar_12[2].z = tmpvar_2.z;
  vec4 v_13;
  v_13.x = _Object2World[0].x;
  v_13.y = _Object2World[1].x;
  v_13.z = _Object2World[2].x;
  v_13.w = _Object2World[3].x;
  highp vec4 tmpvar_14;
  tmpvar_14.xyz = (tmpvar_12 * v_13.xyz);
  tmpvar_14.w = tmpvar_9.x;
  highp vec4 tmpvar_15;
  tmpvar_15 = (tmpvar_14 * unity_Scale.w);
  tmpvar_4 = tmpvar_15;
  vec4 v_16;
  v_16.x = _Object2World[0].y;
  v_16.y = _Object2World[1].y;
  v_16.z = _Object2World[2].y;
  v_16.w = _Object2World[3].y;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_12 * v_16.xyz);
  tmpvar_17.w = tmpvar_9.y;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].z;
  v_19.y = _Object2World[1].z;
  v_19.z = _Object2World[2].z;
  v_19.w = _Object2World[3].z;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_12 * v_19.xyz);
  tmpvar_20.w = tmpvar_9.z;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_12 * (((_World2Object * tmpvar_22).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD6;
varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _ShadowMapTexture;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  highp vec4 TexCUBE0_11;
  highp vec4 DamageMask_12;
  highp vec4 Tex2D2_13;
  highp vec4 Tex2D0_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_14 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_13 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_12 = tmpvar_17;
  highp vec3 tmpvar_18;
  tmpvar_18.x = tmpvar_3.z;
  tmpvar_18.y = tmpvar_4.z;
  tmpvar_18.z = tmpvar_5.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_18, tmpvar_2) * tmpvar_18)));
  lowp vec4 tmpvar_20;
  tmpvar_20 = textureCube (_Cube, tmpvar_19.xyz);
  TexCUBE0_11 = tmpvar_20;
  highp vec4 tmpvar_21;
  tmpvar_21 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_22;
  tmpvar_22 = mix (((Tex2D0_14 + (Tex2D2_13 * (TexCUBE0_11 * tmpvar_21))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_12.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_23;
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.z, _Damage)));
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = (tmpvar_10 + ((_RimColor * pow (tmpvar_21, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_25;
  lowp float tmpvar_26;
  mediump float lightShadowDataX_27;
  highp float dist_28;
  lowp float tmpvar_29;
  tmpvar_29 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6).x;
  dist_28 = tmpvar_29;
  highp float tmpvar_30;
  tmpvar_30 = _LightShadowData.x;
  lightShadowDataX_27 = tmpvar_30;
  highp float tmpvar_31;
  tmpvar_31 = max (float((dist_28 > (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w))), lightShadowDataX_27);
  tmpvar_26 = tmpvar_31;
  lowp vec3 tmpvar_32;
  tmpvar_32 = min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz), vec3((tmpvar_26 * 2.0)));
  mediump vec3 tmpvar_33;
  tmpvar_33 = (tmpvar_10 * tmpvar_32);
  c_1.xyz = tmpvar_33;
  c_1.w = 1.0;
  mediump vec3 tmpvar_34;
  tmpvar_34 = c_1.xyz;
  c_1.xyz = tmpvar_34;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 483
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 508
uniform sampler2D unity_Lightmap;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.lmap.xy = ((v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    #line 500
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec2(xl_retval.lmap);
    xlv_TEXCOORD6 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 483
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 508
uniform sampler2D unity_Lightmap;
#line 176
lowp vec3 DecodeLightmap( in lowp vec4 color ) {
    #line 178
    return (2.0 * color.xyz);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    highp float dist = textureProj( _ShadowMapTexture, shadowCoord).x;
    mediump float lightShadowDataX = _LightShadowData.x;
    #line 388
    return max( float((dist > (shadowCoord.z / shadowCoord.w))), lightShadowDataX);
}
#line 509
lowp vec4 frag_surf( in v2f_surf IN ) {
    Input surfIN;
    #line 512
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    surfIN.TtoW0 = IN.TtoW0.xyz;
    #line 516
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    EditorSurfaceOutput o;
    #line 520
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    o.Alpha = 0.0;
    #line 524
    surf( surfIN, o);
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    lowp vec4 lmtex = texture( unity_Lightmap, IN.lmap.xy);
    #line 528
    lowp vec3 lm = DecodeLightmap( lmtex);
    c.xyz += (o.Albedo * min( lm, vec3( (atten * 2.0))));
    c.w = o.Alpha;
    c.xyz += o.Emission;
    #line 532
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lmap = vec2(xlv_TEXCOORD5);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_27;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_29;
  tmpvar_29.w = 1.0;
  tmpvar_29.xyz = tmpvar_26;
  mediump vec3 tmpvar_30;
  mediump vec4 normal_31;
  normal_31 = tmpvar_29;
  highp float vC_32;
  mediump vec3 x3_33;
  mediump vec3 x2_34;
  mediump vec3 x1_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAr, normal_31);
  x1_35.x = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAg, normal_31);
  x1_35.y = tmpvar_37;
  highp float tmpvar_38;
  tmpvar_38 = dot (unity_SHAb, normal_31);
  x1_35.z = tmpvar_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (normal_31.xyzz * normal_31.yzzx);
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBr, tmpvar_39);
  x2_34.x = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBg, tmpvar_39);
  x2_34.y = tmpvar_41;
  highp float tmpvar_42;
  tmpvar_42 = dot (unity_SHBb, tmpvar_39);
  x2_34.z = tmpvar_42;
  mediump float tmpvar_43;
  tmpvar_43 = ((normal_31.x * normal_31.x) - (normal_31.y * normal_31.y));
  vC_32 = tmpvar_43;
  highp vec3 tmpvar_44;
  tmpvar_44 = (unity_SHC.xyz * vC_32);
  x3_33 = tmpvar_44;
  tmpvar_30 = ((x1_35 + x2_34) + x3_33);
  shlight_3 = tmpvar_30;
  tmpvar_9 = shlight_3;
  highp vec3 tmpvar_45;
  tmpvar_45 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_46;
  tmpvar_46 = (unity_4LightPosX0 - tmpvar_45.x);
  highp vec4 tmpvar_47;
  tmpvar_47 = (unity_4LightPosY0 - tmpvar_45.y);
  highp vec4 tmpvar_48;
  tmpvar_48 = (unity_4LightPosZ0 - tmpvar_45.z);
  highp vec4 tmpvar_49;
  tmpvar_49 = (((tmpvar_46 * tmpvar_46) + (tmpvar_47 * tmpvar_47)) + (tmpvar_48 * tmpvar_48));
  highp vec4 tmpvar_50;
  tmpvar_50 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_46 * tmpvar_26.x) + (tmpvar_47 * tmpvar_26.y)) + (tmpvar_48 * tmpvar_26.z)) * inversesqrt(tmpvar_49))) * (1.0/((1.0 + (tmpvar_49 * unity_4LightAtten0)))));
  highp vec3 tmpvar_51;
  tmpvar_51 = (tmpvar_9 + ((((unity_LightColor[0].xyz * tmpvar_50.x) + (unity_LightColor[1].xyz * tmpvar_50.y)) + (unity_LightColor[2].xyz * tmpvar_50.z)) + (unity_LightColor[3].xyz * tmpvar_50.w)));
  tmpvar_9 = tmpvar_51;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_28).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
}



#endif
#ifdef FRAGMENT

varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  highp vec3 tmpvar_30;
  tmpvar_30 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_31;
  lightDir_31 = xlv_TEXCOORD5;
  mediump vec3 viewDir_32;
  viewDir_32 = tmpvar_30;
  mediump vec4 res_33;
  highp float nh_34;
  mediump float tmpvar_35;
  tmpvar_35 = max (0.0, dot (tmpvar_29, normalize((lightDir_31 + viewDir_32))));
  nh_34 = tmpvar_35;
  mediump float arg1_36;
  arg1_36 = (tmpvar_12 * 128.0);
  res_33.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_31, tmpvar_29)));
  lowp float tmpvar_37;
  tmpvar_37 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_38;
  tmpvar_38 = (pow (nh_34, arg1_36) * tmpvar_37);
  res_33.w = tmpvar_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (res_33 * 2.0);
  res_33 = tmpvar_39;
  mediump vec4 c_40;
  c_40.xyz = ((tmpvar_10 * tmpvar_39.xyz) + (tmpvar_39.xyz * (tmpvar_39.w * tmpvar_11)));
  c_40.w = 1.0;
  c_1 = c_40;
  mediump vec3 tmpvar_41;
  tmpvar_41 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_41;
  mediump vec3 tmpvar_42;
  tmpvar_42 = c_1.xyz;
  c_1.xyz = tmpvar_42;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 95
highp vec3 Shade4PointLights( in highp vec4 lightPosX, in highp vec4 lightPosY, in highp vec4 lightPosZ, in highp vec3 lightColor0, in highp vec3 lightColor1, in highp vec3 lightColor2, in highp vec3 lightColor3, in highp vec4 lightAttenSq, in highp vec3 pos, in highp vec3 normal ) {
    highp vec4 toLightX = (lightPosX - pos.x);
    highp vec4 toLightY = (lightPosY - pos.y);
    #line 99
    highp vec4 toLightZ = (lightPosZ - pos.z);
    highp vec4 lengthSq = vec4( 0.0);
    lengthSq += (toLightX * toLightX);
    lengthSq += (toLightY * toLightY);
    #line 103
    lengthSq += (toLightZ * toLightZ);
    highp vec4 ndotl = vec4( 0.0);
    ndotl += (toLightX * normal.x);
    ndotl += (toLightY * normal.y);
    #line 107
    ndotl += (toLightZ * normal.z);
    highp vec4 corr = inversesqrt(lengthSq);
    ndotl = max( vec4( 0.0, 0.0, 0.0, 0.0), (ndotl * corr));
    highp vec4 atten = (1.0 / (1.0 + (lengthSq * lightAttenSq)));
    #line 111
    highp vec4 diff = (ndotl * atten);
    highp vec3 col = vec3( 0.0);
    col += (lightColor0 * diff.x);
    col += (lightColor1 * diff.y);
    #line 115
    col += (lightColor2 * diff.z);
    col += (lightColor3 * diff.w);
    return col;
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 477
v2f_surf vert_surf( in appdata_full v ) {
    #line 479
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 483
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 487
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 491
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 495
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    highp vec3 worldPos = (_Object2World * v.vertex).xyz;
    o.vlight += Shade4PointLights( unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0, unity_LightColor[0].xyz, unity_LightColor[1].xyz, unity_LightColor[2].xyz, unity_LightColor[3].xyz, unity_4LightAtten0, worldPos, worldN);
    #line 500
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 411
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 415
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 419
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 502
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 504
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 508
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 512
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 516
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp float atten = 1.0;
    lowp vec4 c = vec4( 0.0);
    #line 520
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_27;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_29;
  tmpvar_29.w = 1.0;
  tmpvar_29.xyz = tmpvar_26;
  mediump vec3 tmpvar_30;
  mediump vec4 normal_31;
  normal_31 = tmpvar_29;
  highp float vC_32;
  mediump vec3 x3_33;
  mediump vec3 x2_34;
  mediump vec3 x1_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAr, normal_31);
  x1_35.x = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAg, normal_31);
  x1_35.y = tmpvar_37;
  highp float tmpvar_38;
  tmpvar_38 = dot (unity_SHAb, normal_31);
  x1_35.z = tmpvar_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (normal_31.xyzz * normal_31.yzzx);
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBr, tmpvar_39);
  x2_34.x = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBg, tmpvar_39);
  x2_34.y = tmpvar_41;
  highp float tmpvar_42;
  tmpvar_42 = dot (unity_SHBb, tmpvar_39);
  x2_34.z = tmpvar_42;
  mediump float tmpvar_43;
  tmpvar_43 = ((normal_31.x * normal_31.x) - (normal_31.y * normal_31.y));
  vC_32 = tmpvar_43;
  highp vec3 tmpvar_44;
  tmpvar_44 = (unity_SHC.xyz * vC_32);
  x3_33 = tmpvar_44;
  tmpvar_30 = ((x1_35 + x2_34) + x3_33);
  shlight_3 = tmpvar_30;
  tmpvar_9 = shlight_3;
  highp vec3 tmpvar_45;
  tmpvar_45 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_46;
  tmpvar_46 = (unity_4LightPosX0 - tmpvar_45.x);
  highp vec4 tmpvar_47;
  tmpvar_47 = (unity_4LightPosY0 - tmpvar_45.y);
  highp vec4 tmpvar_48;
  tmpvar_48 = (unity_4LightPosZ0 - tmpvar_45.z);
  highp vec4 tmpvar_49;
  tmpvar_49 = (((tmpvar_46 * tmpvar_46) + (tmpvar_47 * tmpvar_47)) + (tmpvar_48 * tmpvar_48));
  highp vec4 tmpvar_50;
  tmpvar_50 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_46 * tmpvar_26.x) + (tmpvar_47 * tmpvar_26.y)) + (tmpvar_48 * tmpvar_26.z)) * inversesqrt(tmpvar_49))) * (1.0/((1.0 + (tmpvar_49 * unity_4LightAtten0)))));
  highp vec3 tmpvar_51;
  tmpvar_51 = (tmpvar_9 + ((((unity_LightColor[0].xyz * tmpvar_50.x) + (unity_LightColor[1].xyz * tmpvar_50.y)) + (unity_LightColor[2].xyz * tmpvar_50.z)) + (unity_LightColor[3].xyz * tmpvar_50.w)));
  tmpvar_9 = tmpvar_51;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_28).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
  xlv_TEXCOORD7 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  lowp float tmpvar_30;
  mediump float lightShadowDataX_31;
  highp float dist_32;
  lowp float tmpvar_33;
  tmpvar_33 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD7).x;
  dist_32 = tmpvar_33;
  highp float tmpvar_34;
  tmpvar_34 = _LightShadowData.x;
  lightShadowDataX_31 = tmpvar_34;
  highp float tmpvar_35;
  tmpvar_35 = max (float((dist_32 > (xlv_TEXCOORD7.z / xlv_TEXCOORD7.w))), lightShadowDataX_31);
  tmpvar_30 = tmpvar_35;
  highp vec3 tmpvar_36;
  tmpvar_36 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_37;
  lightDir_37 = xlv_TEXCOORD5;
  mediump vec3 viewDir_38;
  viewDir_38 = tmpvar_36;
  mediump float atten_39;
  atten_39 = tmpvar_30;
  mediump vec4 res_40;
  highp float nh_41;
  mediump float tmpvar_42;
  tmpvar_42 = max (0.0, dot (tmpvar_29, normalize((lightDir_37 + viewDir_38))));
  nh_41 = tmpvar_42;
  mediump float arg1_43;
  arg1_43 = (tmpvar_12 * 128.0);
  res_40.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_37, tmpvar_29)));
  lowp float tmpvar_44;
  tmpvar_44 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_45;
  tmpvar_45 = (pow (nh_41, arg1_43) * tmpvar_44);
  res_40.w = tmpvar_45;
  mediump vec4 tmpvar_46;
  tmpvar_46 = (res_40 * (atten_39 * 2.0));
  res_40 = tmpvar_46;
  mediump vec4 c_47;
  c_47.xyz = ((tmpvar_10 * tmpvar_46.xyz) + (tmpvar_46.xyz * (tmpvar_46.w * tmpvar_11)));
  c_47.w = 1.0;
  c_1 = c_47;
  mediump vec3 tmpvar_48;
  tmpvar_48 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_48;
  mediump vec3 tmpvar_49;
  tmpvar_49 = c_1.xyz;
  c_1.xyz = tmpvar_49;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 512
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 95
highp vec3 Shade4PointLights( in highp vec4 lightPosX, in highp vec4 lightPosY, in highp vec4 lightPosZ, in highp vec3 lightColor0, in highp vec3 lightColor1, in highp vec3 lightColor2, in highp vec3 lightColor3, in highp vec4 lightAttenSq, in highp vec3 pos, in highp vec3 normal ) {
    highp vec4 toLightX = (lightPosX - pos.x);
    highp vec4 toLightY = (lightPosY - pos.y);
    #line 99
    highp vec4 toLightZ = (lightPosZ - pos.z);
    highp vec4 lengthSq = vec4( 0.0);
    lengthSq += (toLightX * toLightX);
    lengthSq += (toLightY * toLightY);
    #line 103
    lengthSq += (toLightZ * toLightZ);
    highp vec4 ndotl = vec4( 0.0);
    ndotl += (toLightX * normal.x);
    ndotl += (toLightY * normal.y);
    #line 107
    ndotl += (toLightZ * normal.z);
    highp vec4 corr = inversesqrt(lengthSq);
    ndotl = max( vec4( 0.0, 0.0, 0.0, 0.0), (ndotl * corr));
    highp vec4 atten = (1.0 / (1.0 + (lengthSq * lightAttenSq)));
    #line 111
    highp vec4 diff = (ndotl * atten);
    highp vec3 col = vec3( 0.0);
    col += (lightColor0 * diff.x);
    col += (lightColor1 * diff.y);
    #line 115
    col += (lightColor2 * diff.z);
    col += (lightColor3 * diff.w);
    return col;
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 500
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    highp vec3 worldPos = (_Object2World * v.vertex).xyz;
    o.vlight += Shade4PointLights( unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0, unity_LightColor[0].xyz, unity_LightColor[1].xyz, unity_LightColor[2].xyz, unity_LightColor[3].xyz, unity_4LightAtten0, worldPos, worldN);
    #line 508
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
    xlv_TEXCOORD7 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform sampler2D _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 512
#line 411
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 415
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 419
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 423
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 427
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    highp float dist = textureProj( _ShadowMapTexture, shadowCoord).x;
    mediump float lightShadowDataX = _LightShadowData.x;
    #line 388
    return max( float((dist > (shadowCoord.z / shadowCoord.w))), lightShadowDataX);
}
#line 512
lowp vec4 frag_surf( in v2f_surf IN ) {
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    #line 516
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    #line 520
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    #line 524
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    o.Alpha = 0.0;
    surf( surfIN, o);
    #line 528
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    #line 532
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_26;
  highp vec4 tmpvar_27;
  tmpvar_27.w = 1.0;
  tmpvar_27.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_29;
  mediump vec4 normal_30;
  normal_30 = tmpvar_28;
  highp float vC_31;
  mediump vec3 x3_32;
  mediump vec3 x2_33;
  mediump vec3 x1_34;
  highp float tmpvar_35;
  tmpvar_35 = dot (unity_SHAr, normal_30);
  x1_34.x = tmpvar_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAg, normal_30);
  x1_34.y = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAb, normal_30);
  x1_34.z = tmpvar_37;
  mediump vec4 tmpvar_38;
  tmpvar_38 = (normal_30.xyzz * normal_30.yzzx);
  highp float tmpvar_39;
  tmpvar_39 = dot (unity_SHBr, tmpvar_38);
  x2_33.x = tmpvar_39;
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBg, tmpvar_38);
  x2_33.y = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBb, tmpvar_38);
  x2_33.z = tmpvar_41;
  mediump float tmpvar_42;
  tmpvar_42 = ((normal_30.x * normal_30.x) - (normal_30.y * normal_30.y));
  vC_31 = tmpvar_42;
  highp vec3 tmpvar_43;
  tmpvar_43 = (unity_SHC.xyz * vC_31);
  x3_32 = tmpvar_43;
  tmpvar_29 = ((x1_34 + x2_33) + x3_32);
  shlight_3 = tmpvar_29;
  tmpvar_9 = shlight_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_27).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
  xlv_TEXCOORD7 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  lowp float shadow_30;
  lowp float tmpvar_31;
  tmpvar_31 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD7.xyz);
  highp float tmpvar_32;
  tmpvar_32 = (_LightShadowData.x + (tmpvar_31 * (1.0 - _LightShadowData.x)));
  shadow_30 = tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_33 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_34;
  lightDir_34 = xlv_TEXCOORD5;
  mediump vec3 viewDir_35;
  viewDir_35 = tmpvar_33;
  mediump float atten_36;
  atten_36 = shadow_30;
  mediump vec4 res_37;
  highp float nh_38;
  mediump float tmpvar_39;
  tmpvar_39 = max (0.0, dot (tmpvar_29, normalize((lightDir_34 + viewDir_35))));
  nh_38 = tmpvar_39;
  mediump float arg1_40;
  arg1_40 = (tmpvar_12 * 128.0);
  res_37.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_34, tmpvar_29)));
  lowp float tmpvar_41;
  tmpvar_41 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_42;
  tmpvar_42 = (pow (nh_38, arg1_40) * tmpvar_41);
  res_37.w = tmpvar_42;
  mediump vec4 tmpvar_43;
  tmpvar_43 = (res_37 * (atten_36 * 2.0));
  res_37 = tmpvar_43;
  mediump vec4 c_44;
  c_44.xyz = ((tmpvar_10 * tmpvar_43.xyz) + (tmpvar_43.xyz * (tmpvar_43.w * tmpvar_11)));
  c_44.w = 1.0;
  c_1 = c_44;
  mediump vec3 tmpvar_45;
  tmpvar_45 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_45;
  mediump vec3 tmpvar_46;
  tmpvar_46 = c_1.xyz;
  c_1.xyz = tmpvar_46;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 500
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    #line 508
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
    xlv_TEXCOORD7 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];
float xll_shadow2D(mediump sampler2DShadow s, vec3 coord) { return texture (s, coord); }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 411
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 415
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 419
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 423
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 427
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    lowp float shadow = xll_shadow2D( _ShadowMapTexture, shadowCoord.xyz.xyz);
    shadow = (_LightShadowData.x + (shadow * (1.0 - _LightShadowData.x)));
    #line 388
    return shadow;
}
#line 510
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 512
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 516
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 520
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 524
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    #line 528
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD6;
varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w)));
  highp vec3 tmpvar_10;
  highp vec3 tmpvar_11;
  tmpvar_10 = tmpvar_1.xyz;
  tmpvar_11 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_12;
  tmpvar_12[0].x = tmpvar_10.x;
  tmpvar_12[0].y = tmpvar_11.x;
  tmpvar_12[0].z = tmpvar_2.x;
  tmpvar_12[1].x = tmpvar_10.y;
  tmpvar_12[1].y = tmpvar_11.y;
  tmpvar_12[1].z = tmpvar_2.y;
  tmpvar_12[2].x = tmpvar_10.z;
  tmpvar_12[2].y = tmpvar_11.z;
  tmpvar_12[2].z = tmpvar_2.z;
  vec4 v_13;
  v_13.x = _Object2World[0].x;
  v_13.y = _Object2World[1].x;
  v_13.z = _Object2World[2].x;
  v_13.w = _Object2World[3].x;
  highp vec4 tmpvar_14;
  tmpvar_14.xyz = (tmpvar_12 * v_13.xyz);
  tmpvar_14.w = tmpvar_9.x;
  highp vec4 tmpvar_15;
  tmpvar_15 = (tmpvar_14 * unity_Scale.w);
  tmpvar_4 = tmpvar_15;
  vec4 v_16;
  v_16.x = _Object2World[0].y;
  v_16.y = _Object2World[1].y;
  v_16.z = _Object2World[2].y;
  v_16.w = _Object2World[3].y;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_12 * v_16.xyz);
  tmpvar_17.w = tmpvar_9.y;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].z;
  v_19.y = _Object2World[1].z;
  v_19.z = _Object2World[2].z;
  v_19.w = _Object2World[3].z;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_12 * v_19.xyz);
  tmpvar_20.w = tmpvar_9.z;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_12 * (((_World2Object * tmpvar_22).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD6;
varying highp vec2 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  highp vec4 TexCUBE0_11;
  highp vec4 DamageMask_12;
  highp vec4 Tex2D2_13;
  highp vec4 Tex2D0_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_14 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_13 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_12 = tmpvar_17;
  highp vec3 tmpvar_18;
  tmpvar_18.x = tmpvar_3.z;
  tmpvar_18.y = tmpvar_4.z;
  tmpvar_18.z = tmpvar_5.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_18, tmpvar_2) * tmpvar_18)));
  lowp vec4 tmpvar_20;
  tmpvar_20 = textureCube (_Cube, tmpvar_19.xyz);
  TexCUBE0_11 = tmpvar_20;
  highp vec4 tmpvar_21;
  tmpvar_21 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_22;
  tmpvar_22 = mix (((Tex2D0_14 + (Tex2D2_13 * (TexCUBE0_11 * tmpvar_21))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_12.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_23;
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_12.z, _Damage)));
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = (tmpvar_10 + ((_RimColor * pow (tmpvar_21, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_25;
  lowp float shadow_26;
  lowp float tmpvar_27;
  tmpvar_27 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD6.xyz);
  highp float tmpvar_28;
  tmpvar_28 = (_LightShadowData.x + (tmpvar_27 * (1.0 - _LightShadowData.x)));
  shadow_26 = tmpvar_28;
  lowp vec3 tmpvar_29;
  tmpvar_29 = min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz), vec3((shadow_26 * 2.0)));
  mediump vec3 tmpvar_30;
  tmpvar_30 = (tmpvar_10 * tmpvar_29);
  c_1.xyz = tmpvar_30;
  c_1.w = 1.0;
  mediump vec3 tmpvar_31;
  tmpvar_31 = c_1.xyz;
  c_1.xyz = tmpvar_31;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 483
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 508
uniform sampler2D unity_Lightmap;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.lmap.xy = ((v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    #line 500
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec2(xl_retval.lmap);
    xlv_TEXCOORD6 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];
float xll_shadow2D(mediump sampler2DShadow s, vec3 coord) { return texture (s, coord); }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 483
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 508
uniform sampler2D unity_Lightmap;
#line 176
lowp vec3 DecodeLightmap( in lowp vec4 color ) {
    #line 178
    return (2.0 * color.xyz);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    lowp float shadow = xll_shadow2D( _ShadowMapTexture, shadowCoord.xyz.xyz);
    shadow = (_LightShadowData.x + (shadow * (1.0 - _LightShadowData.x)));
    #line 388
    return shadow;
}
#line 509
lowp vec4 frag_surf( in v2f_surf IN ) {
    Input surfIN;
    #line 512
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    surfIN.TtoW0 = IN.TtoW0.xyz;
    #line 516
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    EditorSurfaceOutput o;
    #line 520
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    o.Alpha = 0.0;
    #line 524
    surf( surfIN, o);
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    lowp vec4 lmtex = texture( unity_Lightmap, IN.lmap.xy);
    #line 528
    lowp vec3 lm = DecodeLightmap( lmtex);
    c.xyz += (o.Albedo * min( lm, vec3( (atten * 2.0))));
    c.w = o.Alpha;
    c.xyz += o.Emission;
    #line 532
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lmap = vec2(xlv_TEXCOORD5);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosX0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 shlight_3;
  highp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  lowp vec4 tmpvar_7;
  lowp vec3 tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_4.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_4.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * (_glesVertex.xyz - ((_World2Object * tmpvar_10).xyz * unity_Scale.w)));
  highp vec3 tmpvar_13;
  highp vec3 tmpvar_14;
  tmpvar_13 = tmpvar_1.xyz;
  tmpvar_14 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_15;
  tmpvar_15[0].x = tmpvar_13.x;
  tmpvar_15[0].y = tmpvar_14.x;
  tmpvar_15[0].z = tmpvar_2.x;
  tmpvar_15[1].x = tmpvar_13.y;
  tmpvar_15[1].y = tmpvar_14.y;
  tmpvar_15[1].z = tmpvar_2.y;
  tmpvar_15[2].x = tmpvar_13.z;
  tmpvar_15[2].y = tmpvar_14.z;
  tmpvar_15[2].z = tmpvar_2.z;
  vec4 v_16;
  v_16.x = _Object2World[0].x;
  v_16.y = _Object2World[1].x;
  v_16.z = _Object2World[2].x;
  v_16.w = _Object2World[3].x;
  highp vec4 tmpvar_17;
  tmpvar_17.xyz = (tmpvar_15 * v_16.xyz);
  tmpvar_17.w = tmpvar_12.x;
  highp vec4 tmpvar_18;
  tmpvar_18 = (tmpvar_17 * unity_Scale.w);
  tmpvar_5 = tmpvar_18;
  vec4 v_19;
  v_19.x = _Object2World[0].y;
  v_19.y = _Object2World[1].y;
  v_19.z = _Object2World[2].y;
  v_19.w = _Object2World[3].y;
  highp vec4 tmpvar_20;
  tmpvar_20.xyz = (tmpvar_15 * v_19.xyz);
  tmpvar_20.w = tmpvar_12.y;
  highp vec4 tmpvar_21;
  tmpvar_21 = (tmpvar_20 * unity_Scale.w);
  tmpvar_6 = tmpvar_21;
  vec4 v_22;
  v_22.x = _Object2World[0].z;
  v_22.y = _Object2World[1].z;
  v_22.z = _Object2World[2].z;
  v_22.w = _Object2World[3].z;
  highp vec4 tmpvar_23;
  tmpvar_23.xyz = (tmpvar_15 * v_22.xyz);
  tmpvar_23.w = tmpvar_12.z;
  highp vec4 tmpvar_24;
  tmpvar_24 = (tmpvar_23 * unity_Scale.w);
  tmpvar_7 = tmpvar_24;
  mat3 tmpvar_25;
  tmpvar_25[0] = _Object2World[0].xyz;
  tmpvar_25[1] = _Object2World[1].xyz;
  tmpvar_25[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_26;
  tmpvar_26 = (tmpvar_25 * (tmpvar_2 * unity_Scale.w));
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_15 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_8 = tmpvar_27;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = _WorldSpaceCameraPos;
  highp vec4 tmpvar_29;
  tmpvar_29.w = 1.0;
  tmpvar_29.xyz = tmpvar_26;
  mediump vec3 tmpvar_30;
  mediump vec4 normal_31;
  normal_31 = tmpvar_29;
  highp float vC_32;
  mediump vec3 x3_33;
  mediump vec3 x2_34;
  mediump vec3 x1_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAr, normal_31);
  x1_35.x = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAg, normal_31);
  x1_35.y = tmpvar_37;
  highp float tmpvar_38;
  tmpvar_38 = dot (unity_SHAb, normal_31);
  x1_35.z = tmpvar_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (normal_31.xyzz * normal_31.yzzx);
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBr, tmpvar_39);
  x2_34.x = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBg, tmpvar_39);
  x2_34.y = tmpvar_41;
  highp float tmpvar_42;
  tmpvar_42 = dot (unity_SHBb, tmpvar_39);
  x2_34.z = tmpvar_42;
  mediump float tmpvar_43;
  tmpvar_43 = ((normal_31.x * normal_31.x) - (normal_31.y * normal_31.y));
  vC_32 = tmpvar_43;
  highp vec3 tmpvar_44;
  tmpvar_44 = (unity_SHC.xyz * vC_32);
  x3_33 = tmpvar_44;
  tmpvar_30 = ((x1_35 + x2_34) + x3_33);
  shlight_3 = tmpvar_30;
  tmpvar_9 = shlight_3;
  highp vec3 tmpvar_45;
  tmpvar_45 = (_Object2World * _glesVertex).xyz;
  highp vec4 tmpvar_46;
  tmpvar_46 = (unity_4LightPosX0 - tmpvar_45.x);
  highp vec4 tmpvar_47;
  tmpvar_47 = (unity_4LightPosY0 - tmpvar_45.y);
  highp vec4 tmpvar_48;
  tmpvar_48 = (unity_4LightPosZ0 - tmpvar_45.z);
  highp vec4 tmpvar_49;
  tmpvar_49 = (((tmpvar_46 * tmpvar_46) + (tmpvar_47 * tmpvar_47)) + (tmpvar_48 * tmpvar_48));
  highp vec4 tmpvar_50;
  tmpvar_50 = (max (vec4(0.0, 0.0, 0.0, 0.0), ((((tmpvar_46 * tmpvar_26.x) + (tmpvar_47 * tmpvar_26.y)) + (tmpvar_48 * tmpvar_26.z)) * inversesqrt(tmpvar_49))) * (1.0/((1.0 + (tmpvar_49 * unity_4LightAtten0)))));
  highp vec3 tmpvar_51;
  tmpvar_51 = (tmpvar_9 + ((((unity_LightColor[0].xyz * tmpvar_50.x) + (unity_LightColor[1].xyz * tmpvar_50.y)) + (unity_LightColor[2].xyz * tmpvar_50.z)) + (unity_LightColor[3].xyz * tmpvar_50.w)));
  tmpvar_9 = tmpvar_51;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_4;
  xlv_TEXCOORD1 = (tmpvar_15 * (((_World2Object * tmpvar_28).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_5;
  xlv_TEXCOORD3 = tmpvar_6;
  xlv_TEXCOORD4 = tmpvar_7;
  xlv_TEXCOORD5 = tmpvar_8;
  xlv_TEXCOORD6 = tmpvar_9;
  xlv_TEXCOORD7 = (unity_World2Shadow[0] * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
varying highp vec4 xlv_TEXCOORD7;
varying lowp vec3 xlv_TEXCOORD6;
varying lowp vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform lowp vec4 _LightColor0;
uniform highp vec4 _LightShadowData;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  tmpvar_6.x = xlv_TEXCOORD2.w;
  tmpvar_6.y = xlv_TEXCOORD3.w;
  tmpvar_6.z = xlv_TEXCOORD4.w;
  tmpvar_2 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = xlv_TEXCOORD2.xyz;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD3.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD4.xyz;
  tmpvar_5 = tmpvar_9;
  mediump vec3 tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump float tmpvar_12;
  highp vec4 TexCUBE0_13;
  highp vec4 DamageMask_14;
  highp vec4 Tex2D2_15;
  highp vec4 Tex2D0_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_16 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_15 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_14 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20.x = tmpvar_3.z;
  tmpvar_20.y = tmpvar_4.z;
  tmpvar_20.z = tmpvar_5.z;
  highp vec4 tmpvar_21;
  tmpvar_21.w = 1.0;
  tmpvar_21.xyz = (tmpvar_2 - (2.0 * (dot (tmpvar_20, tmpvar_2) * tmpvar_20)));
  lowp vec4 tmpvar_22;
  tmpvar_22 = textureCube (_Cube, tmpvar_21.xyz);
  TexCUBE0_13 = tmpvar_22;
  highp vec4 tmpvar_23;
  tmpvar_23 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_24;
  tmpvar_24 = mix (((Tex2D0_16 + (Tex2D2_15 * (TexCUBE0_13 * tmpvar_23))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_14.x, (_Damage * 10.0)))).xyz;
  tmpvar_10 = tmpvar_24;
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.y, (_Damage * 5.0))));
  tmpvar_10 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_10, _DamageColor.xyz, vec3(mix (0.0, DamageMask_14.z, _Damage)));
  tmpvar_10 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (tmpvar_10 + ((_RimColor * pow (tmpvar_23, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_10 = tmpvar_27;
  tmpvar_12 = _Gloss;
  highp vec3 tmpvar_28;
  tmpvar_28 = _SpecularColor.xyz;
  tmpvar_11 = tmpvar_28;
  mediump vec3 tmpvar_29;
  tmpvar_29 = normalize(vec3(0.0, 0.0, 1.0));
  lowp float shadow_30;
  lowp float tmpvar_31;
  tmpvar_31 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD7.xyz);
  highp float tmpvar_32;
  tmpvar_32 = (_LightShadowData.x + (tmpvar_31 * (1.0 - _LightShadowData.x)));
  shadow_30 = tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_33 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_34;
  lightDir_34 = xlv_TEXCOORD5;
  mediump vec3 viewDir_35;
  viewDir_35 = tmpvar_33;
  mediump float atten_36;
  atten_36 = shadow_30;
  mediump vec4 res_37;
  highp float nh_38;
  mediump float tmpvar_39;
  tmpvar_39 = max (0.0, dot (tmpvar_29, normalize((lightDir_34 + viewDir_35))));
  nh_38 = tmpvar_39;
  mediump float arg1_40;
  arg1_40 = (tmpvar_12 * 128.0);
  res_37.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_34, tmpvar_29)));
  lowp float tmpvar_41;
  tmpvar_41 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_42;
  tmpvar_42 = (pow (nh_38, arg1_40) * tmpvar_41);
  res_37.w = tmpvar_42;
  mediump vec4 tmpvar_43;
  tmpvar_43 = (res_37 * (atten_36 * 2.0));
  res_37 = tmpvar_43;
  mediump vec4 c_44;
  c_44.xyz = ((tmpvar_10 * tmpvar_43.xyz) + (tmpvar_43.xyz * (tmpvar_43.w * tmpvar_11)));
  c_44.w = 1.0;
  c_1 = c_44;
  mediump vec3 tmpvar_45;
  tmpvar_45 = (c_1.xyz + (tmpvar_10 * xlv_TEXCOORD6));
  c_1.xyz = tmpvar_45;
  mediump vec3 tmpvar_46;
  tmpvar_46 = c_1.xyz;
  c_1.xyz = tmpvar_46;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 512
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 95
highp vec3 Shade4PointLights( in highp vec4 lightPosX, in highp vec4 lightPosY, in highp vec4 lightPosZ, in highp vec3 lightColor0, in highp vec3 lightColor1, in highp vec3 lightColor2, in highp vec3 lightColor3, in highp vec4 lightAttenSq, in highp vec3 pos, in highp vec3 normal ) {
    highp vec4 toLightX = (lightPosX - pos.x);
    highp vec4 toLightY = (lightPosY - pos.y);
    #line 99
    highp vec4 toLightZ = (lightPosZ - pos.z);
    highp vec4 lengthSq = vec4( 0.0);
    lengthSq += (toLightX * toLightX);
    lengthSq += (toLightY * toLightY);
    #line 103
    lengthSq += (toLightZ * toLightZ);
    highp vec4 ndotl = vec4( 0.0);
    ndotl += (toLightX * normal.x);
    ndotl += (toLightY * normal.y);
    #line 107
    ndotl += (toLightZ * normal.z);
    highp vec4 corr = inversesqrt(lengthSq);
    ndotl = max( vec4( 0.0, 0.0, 0.0, 0.0), (ndotl * corr));
    highp vec4 atten = (1.0 / (1.0 + (lengthSq * lightAttenSq)));
    #line 111
    highp vec4 diff = (ndotl * atten);
    highp vec3 col = vec3( 0.0);
    col += (lightColor0 * diff.x);
    col += (lightColor1 * diff.y);
    #line 115
    col += (lightColor2 * diff.z);
    col += (lightColor3 * diff.w);
    return col;
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 486
v2f_surf vert_surf( in appdata_full v ) {
    #line 488
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 492
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 496
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    #line 500
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 504
    highp vec3 shlight = ShadeSH9( vec4( worldN, 1.0));
    o.vlight = shlight;
    highp vec3 worldPos = (_Object2World * v.vertex).xyz;
    o.vlight += Shade4PointLights( unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0, unity_LightColor[0].xyz, unity_LightColor[1].xyz, unity_LightColor[2].xyz, unity_LightColor[3].xyz, unity_4LightAtten0, worldPos, worldN);
    #line 508
    o._ShadowCoord = (unity_World2Shadow[0] * (_Object2World * v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out lowp vec3 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
    xlv_TEXCOORD7 = vec4(xl_retval._ShadowCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];
float xll_shadow2D(mediump sampler2DShadow s, vec3 coord) { return texture (s, coord); }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 400
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 431
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 471
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    lowp vec3 lightDir;
    lowp vec3 vlight;
    highp vec4 _ShadowCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _ShadowOffsets[4];
uniform lowp sampler2DShadow _ShadowMapTexture;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 392
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 396
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 411
#line 419
#line 442
#line 484
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 512
#line 411
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 415
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 419
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 423
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 427
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 442
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 446
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 450
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 454
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 458
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 462
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 466
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 384
lowp float unitySampleShadow( in highp vec4 shadowCoord ) {
    lowp float shadow = xll_shadow2D( _ShadowMapTexture, shadowCoord.xyz.xyz);
    shadow = (_LightShadowData.x + (shadow * (1.0 - _LightShadowData.x)));
    #line 388
    return shadow;
}
#line 512
lowp vec4 frag_surf( in v2f_surf IN ) {
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    #line 516
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    #line 520
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    #line 524
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    o.Alpha = 0.0;
    surf( surfIN, o);
    #line 528
    lowp float atten = unitySampleShadow( IN._ShadowCoord);
    lowp vec4 c = vec4( 0.0);
    c = LightingBlinnPhongEditor( o, IN.lightDir, normalize(IN.viewDir), atten);
    c.xyz += (o.Albedo * IN.vlight);
    #line 532
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in lowp vec3 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xlt_IN._ShadowCoord = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
}
 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "QUEUE"="Geometry" "IGNOREPROJECTOR"="False" "RenderType"="Opaque" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend One One
Program "vp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES


#ifdef VERTEX

varying highp vec3 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_glesVertex.xyz - ((_World2Object * tmpvar_8).xyz * unity_Scale.w)));
  highp vec3 tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_11 = tmpvar_1.xyz;
  tmpvar_12 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_13;
  tmpvar_13[0].x = tmpvar_11.x;
  tmpvar_13[0].y = tmpvar_12.x;
  tmpvar_13[0].z = tmpvar_2.x;
  tmpvar_13[1].x = tmpvar_11.y;
  tmpvar_13[1].y = tmpvar_12.y;
  tmpvar_13[1].z = tmpvar_2.y;
  tmpvar_13[2].x = tmpvar_11.z;
  tmpvar_13[2].y = tmpvar_12.z;
  tmpvar_13[2].z = tmpvar_2.z;
  vec4 v_14;
  v_14.x = _Object2World[0].x;
  v_14.y = _Object2World[1].x;
  v_14.z = _Object2World[2].x;
  v_14.w = _Object2World[3].x;
  highp vec4 tmpvar_15;
  tmpvar_15.xyz = (tmpvar_13 * v_14.xyz);
  tmpvar_15.w = tmpvar_10.x;
  highp vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_15 * unity_Scale.w);
  tmpvar_4 = tmpvar_16;
  vec4 v_17;
  v_17.x = _Object2World[0].y;
  v_17.y = _Object2World[1].y;
  v_17.z = _Object2World[2].y;
  v_17.w = _Object2World[3].y;
  highp vec4 tmpvar_18;
  tmpvar_18.xyz = (tmpvar_13 * v_17.xyz);
  tmpvar_18.w = tmpvar_10.y;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_18 * unity_Scale.w);
  tmpvar_5 = tmpvar_19;
  vec4 v_20;
  v_20.x = _Object2World[0].z;
  v_20.y = _Object2World[1].z;
  v_20.z = _Object2World[2].z;
  v_20.w = _Object2World[3].z;
  highp vec4 tmpvar_21;
  tmpvar_21.xyz = (tmpvar_13 * v_20.xyz);
  tmpvar_21.w = tmpvar_10.z;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_21 * unity_Scale.w);
  tmpvar_6 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (tmpvar_13 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_7 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.w = 1.0;
  tmpvar_24.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_13 * (((_World2Object * tmpvar_24).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = tmpvar_7;
  xlv_TEXCOORD6 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7.x = xlv_TEXCOORD2.w;
  tmpvar_7.y = xlv_TEXCOORD3.w;
  tmpvar_7.z = xlv_TEXCOORD4.w;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD2.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump float tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_4.z;
  tmpvar_21.y = tmpvar_5.z;
  tmpvar_21.z = tmpvar_6.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_3 - (2.0 * (dot (tmpvar_21, tmpvar_3) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_11 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_11 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_11 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_11 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_11 = tmpvar_28;
  tmpvar_13 = _Gloss;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_12 = tmpvar_29;
  mediump vec3 tmpvar_30;
  tmpvar_30 = normalize(vec3(0.0, 0.0, 1.0));
  mediump vec3 tmpvar_31;
  tmpvar_31 = normalize(xlv_TEXCOORD5);
  lightDir_2 = tmpvar_31;
  highp vec3 tmpvar_32;
  tmpvar_32 = normalize(xlv_TEXCOORD1);
  highp float tmpvar_33;
  tmpvar_33 = dot (xlv_TEXCOORD6, xlv_TEXCOORD6);
  lowp vec4 tmpvar_34;
  tmpvar_34 = texture2D (_LightTexture0, vec2(tmpvar_33));
  mediump vec3 lightDir_35;
  lightDir_35 = lightDir_2;
  mediump vec3 viewDir_36;
  viewDir_36 = tmpvar_32;
  mediump float atten_37;
  atten_37 = tmpvar_34.w;
  mediump vec4 res_38;
  highp float nh_39;
  mediump float tmpvar_40;
  tmpvar_40 = max (0.0, dot (tmpvar_30, normalize((lightDir_35 + viewDir_36))));
  nh_39 = tmpvar_40;
  mediump float arg1_41;
  arg1_41 = (tmpvar_13 * 128.0);
  res_38.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_35, tmpvar_30)));
  lowp float tmpvar_42;
  tmpvar_42 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_43;
  tmpvar_43 = (pow (nh_39, arg1_41) * tmpvar_42);
  res_38.w = tmpvar_43;
  mediump vec4 tmpvar_44;
  tmpvar_44 = (res_38 * (atten_37 * 2.0));
  res_38 = tmpvar_44;
  mediump vec4 c_45;
  c_45.xyz = ((tmpvar_11 * tmpvar_44.xyz) + (tmpvar_44.xyz * (tmpvar_44.w * tmpvar_12)));
  c_45.w = 1.0;
  c_1.xyz = c_45.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "POINT" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 394
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 425
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 465
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec3 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 388
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 392
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 405
#line 413
#line 436
#line 477
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return ((objSpaceLightPos.xyz * unity_Scale.w) - v.xyz);
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 479
v2f_surf vert_surf( in appdata_full v ) {
    #line 481
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 485
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 489
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    #line 493
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    o._LightCoord = (_LightMatrix0 * (_Object2World * v.vertex)).xyz;
    #line 498
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out mediump vec3 xlv_TEXCOORD5;
out highp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval._LightCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 394
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 425
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 465
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec3 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 388
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 392
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 405
#line 413
#line 436
#line 477
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 405
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 409
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 413
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 417
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 421
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 436
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 440
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 444
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 448
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 452
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 456
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 460
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 500
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 502
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 506
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 510
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 514
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 lightDir = normalize(IN.lightDir);
    lowp vec4 c = LightingBlinnPhongEditor( o, lightDir, normalize(IN.viewDir), (texture( _LightTexture0, vec2( dot( IN._LightCoord, IN._LightCoord))).w * 1.0));
    #line 518
    c.w = 0.0;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in mediump vec3 xlv_TEXCOORD5;
in highp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN._LightCoord = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES


#ifdef VERTEX

varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_glesVertex.xyz - ((_World2Object * tmpvar_8).xyz * unity_Scale.w)));
  highp vec3 tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_11 = tmpvar_1.xyz;
  tmpvar_12 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_13;
  tmpvar_13[0].x = tmpvar_11.x;
  tmpvar_13[0].y = tmpvar_12.x;
  tmpvar_13[0].z = tmpvar_2.x;
  tmpvar_13[1].x = tmpvar_11.y;
  tmpvar_13[1].y = tmpvar_12.y;
  tmpvar_13[1].z = tmpvar_2.y;
  tmpvar_13[2].x = tmpvar_11.z;
  tmpvar_13[2].y = tmpvar_12.z;
  tmpvar_13[2].z = tmpvar_2.z;
  vec4 v_14;
  v_14.x = _Object2World[0].x;
  v_14.y = _Object2World[1].x;
  v_14.z = _Object2World[2].x;
  v_14.w = _Object2World[3].x;
  highp vec4 tmpvar_15;
  tmpvar_15.xyz = (tmpvar_13 * v_14.xyz);
  tmpvar_15.w = tmpvar_10.x;
  highp vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_15 * unity_Scale.w);
  tmpvar_4 = tmpvar_16;
  vec4 v_17;
  v_17.x = _Object2World[0].y;
  v_17.y = _Object2World[1].y;
  v_17.z = _Object2World[2].y;
  v_17.w = _Object2World[3].y;
  highp vec4 tmpvar_18;
  tmpvar_18.xyz = (tmpvar_13 * v_17.xyz);
  tmpvar_18.w = tmpvar_10.y;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_18 * unity_Scale.w);
  tmpvar_5 = tmpvar_19;
  vec4 v_20;
  v_20.x = _Object2World[0].z;
  v_20.y = _Object2World[1].z;
  v_20.z = _Object2World[2].z;
  v_20.w = _Object2World[3].z;
  highp vec4 tmpvar_21;
  tmpvar_21.xyz = (tmpvar_13 * v_20.xyz);
  tmpvar_21.w = tmpvar_10.z;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_21 * unity_Scale.w);
  tmpvar_6 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (tmpvar_13 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_7 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.w = 1.0;
  tmpvar_24.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_13 * (((_World2Object * tmpvar_24).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = tmpvar_7;
}



#endif
#ifdef FRAGMENT

varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7.x = xlv_TEXCOORD2.w;
  tmpvar_7.y = xlv_TEXCOORD3.w;
  tmpvar_7.z = xlv_TEXCOORD4.w;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD2.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump float tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_4.z;
  tmpvar_21.y = tmpvar_5.z;
  tmpvar_21.z = tmpvar_6.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_3 - (2.0 * (dot (tmpvar_21, tmpvar_3) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_11 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_11 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_11 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_11 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_11 = tmpvar_28;
  tmpvar_13 = _Gloss;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_12 = tmpvar_29;
  mediump vec3 tmpvar_30;
  tmpvar_30 = normalize(vec3(0.0, 0.0, 1.0));
  lightDir_2 = xlv_TEXCOORD5;
  highp vec3 tmpvar_31;
  tmpvar_31 = normalize(xlv_TEXCOORD1);
  mediump vec3 lightDir_32;
  lightDir_32 = lightDir_2;
  mediump vec3 viewDir_33;
  viewDir_33 = tmpvar_31;
  mediump vec4 res_34;
  highp float nh_35;
  mediump float tmpvar_36;
  tmpvar_36 = max (0.0, dot (tmpvar_30, normalize((lightDir_32 + viewDir_33))));
  nh_35 = tmpvar_36;
  mediump float arg1_37;
  arg1_37 = (tmpvar_13 * 128.0);
  res_34.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_32, tmpvar_30)));
  lowp float tmpvar_38;
  tmpvar_38 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_39;
  tmpvar_39 = (pow (nh_35, arg1_37) * tmpvar_38);
  res_34.w = tmpvar_39;
  mediump vec4 tmpvar_40;
  tmpvar_40 = (res_34 * 2.0);
  res_34 = tmpvar_40;
  mediump vec4 c_41;
  c_41.xyz = ((tmpvar_11 * tmpvar_40.xyz) + (tmpvar_40.xyz * (tmpvar_40.w * tmpvar_12)));
  c_41.w = 1.0;
  c_1.xyz = c_41.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 474
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 476
v2f_surf vert_surf( in appdata_full v ) {
    #line 478
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 482
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 486
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    #line 490
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    #line 494
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out mediump vec3 xlv_TEXCOORD5;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 474
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 411
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 415
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 419
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 496
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 498
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 502
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 506
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 510
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 lightDir = IN.lightDir;
    lowp vec4 c = LightingBlinnPhongEditor( o, lightDir, normalize(IN.viewDir), 1.0);
    #line 514
    c.w = 0.0;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in mediump vec3 xlv_TEXCOORD5;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_glesVertex.xyz - ((_World2Object * tmpvar_8).xyz * unity_Scale.w)));
  highp vec3 tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_11 = tmpvar_1.xyz;
  tmpvar_12 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_13;
  tmpvar_13[0].x = tmpvar_11.x;
  tmpvar_13[0].y = tmpvar_12.x;
  tmpvar_13[0].z = tmpvar_2.x;
  tmpvar_13[1].x = tmpvar_11.y;
  tmpvar_13[1].y = tmpvar_12.y;
  tmpvar_13[1].z = tmpvar_2.y;
  tmpvar_13[2].x = tmpvar_11.z;
  tmpvar_13[2].y = tmpvar_12.z;
  tmpvar_13[2].z = tmpvar_2.z;
  vec4 v_14;
  v_14.x = _Object2World[0].x;
  v_14.y = _Object2World[1].x;
  v_14.z = _Object2World[2].x;
  v_14.w = _Object2World[3].x;
  highp vec4 tmpvar_15;
  tmpvar_15.xyz = (tmpvar_13 * v_14.xyz);
  tmpvar_15.w = tmpvar_10.x;
  highp vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_15 * unity_Scale.w);
  tmpvar_4 = tmpvar_16;
  vec4 v_17;
  v_17.x = _Object2World[0].y;
  v_17.y = _Object2World[1].y;
  v_17.z = _Object2World[2].y;
  v_17.w = _Object2World[3].y;
  highp vec4 tmpvar_18;
  tmpvar_18.xyz = (tmpvar_13 * v_17.xyz);
  tmpvar_18.w = tmpvar_10.y;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_18 * unity_Scale.w);
  tmpvar_5 = tmpvar_19;
  vec4 v_20;
  v_20.x = _Object2World[0].z;
  v_20.y = _Object2World[1].z;
  v_20.z = _Object2World[2].z;
  v_20.w = _Object2World[3].z;
  highp vec4 tmpvar_21;
  tmpvar_21.xyz = (tmpvar_13 * v_20.xyz);
  tmpvar_21.w = tmpvar_10.z;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_21 * unity_Scale.w);
  tmpvar_6 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (tmpvar_13 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_7 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.w = 1.0;
  tmpvar_24.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_13 * (((_World2Object * tmpvar_24).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = tmpvar_7;
  xlv_TEXCOORD6 = (_LightMatrix0 * (_Object2World * _glesVertex));
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _LightTextureB0;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7.x = xlv_TEXCOORD2.w;
  tmpvar_7.y = xlv_TEXCOORD3.w;
  tmpvar_7.z = xlv_TEXCOORD4.w;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD2.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump float tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_4.z;
  tmpvar_21.y = tmpvar_5.z;
  tmpvar_21.z = tmpvar_6.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_3 - (2.0 * (dot (tmpvar_21, tmpvar_3) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_11 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_11 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_11 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_11 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_11 = tmpvar_28;
  tmpvar_13 = _Gloss;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_12 = tmpvar_29;
  mediump vec3 tmpvar_30;
  tmpvar_30 = normalize(vec3(0.0, 0.0, 1.0));
  mediump vec3 tmpvar_31;
  tmpvar_31 = normalize(xlv_TEXCOORD5);
  lightDir_2 = tmpvar_31;
  highp vec3 tmpvar_32;
  tmpvar_32 = normalize(xlv_TEXCOORD1);
  lowp vec4 tmpvar_33;
  highp vec2 P_34;
  P_34 = ((xlv_TEXCOORD6.xy / xlv_TEXCOORD6.w) + 0.5);
  tmpvar_33 = texture2D (_LightTexture0, P_34);
  highp float tmpvar_35;
  tmpvar_35 = dot (xlv_TEXCOORD6.xyz, xlv_TEXCOORD6.xyz);
  lowp vec4 tmpvar_36;
  tmpvar_36 = texture2D (_LightTextureB0, vec2(tmpvar_35));
  mediump vec3 lightDir_37;
  lightDir_37 = lightDir_2;
  mediump vec3 viewDir_38;
  viewDir_38 = tmpvar_32;
  mediump float atten_39;
  atten_39 = ((float((xlv_TEXCOORD6.z > 0.0)) * tmpvar_33.w) * tmpvar_36.w);
  mediump vec4 res_40;
  highp float nh_41;
  mediump float tmpvar_42;
  tmpvar_42 = max (0.0, dot (tmpvar_30, normalize((lightDir_37 + viewDir_38))));
  nh_41 = tmpvar_42;
  mediump float arg1_43;
  arg1_43 = (tmpvar_13 * 128.0);
  res_40.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_37, tmpvar_30)));
  lowp float tmpvar_44;
  tmpvar_44 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_45;
  tmpvar_45 = (pow (nh_41, arg1_43) * tmpvar_44);
  res_40.w = tmpvar_45;
  mediump vec4 tmpvar_46;
  tmpvar_46 = (res_40 * (atten_39 * 2.0));
  res_40 = tmpvar_46;
  mediump vec4 c_47;
  c_47.xyz = ((tmpvar_11 * tmpvar_46.xyz) + (tmpvar_46.xyz * (tmpvar_46.w * tmpvar_12)));
  c_47.w = 1.0;
  c_1.xyz = c_47.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "SPOT" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 403
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 434
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 474
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec4 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform sampler2D _LightTextureB0;
#line 389
#line 393
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 397
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 401
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 414
#line 422
#line 445
#line 486
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return ((objSpaceLightPos.xyz * unity_Scale.w) - v.xyz);
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 488
v2f_surf vert_surf( in appdata_full v ) {
    #line 490
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 494
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 498
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    #line 502
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    o._LightCoord = (_LightMatrix0 * (_Object2World * v.vertex));
    #line 507
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out mediump vec3 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec4(xl_retval._LightCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 403
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 434
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 474
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec4 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform sampler2D _LightTextureB0;
#line 389
#line 393
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 397
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 401
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 414
#line 422
#line 445
#line 486
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 414
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 418
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 422
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 426
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 430
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 389
lowp float UnitySpotAttenuate( in highp vec3 LightCoord ) {
    return texture( _LightTextureB0, vec2( dot( LightCoord, LightCoord))).w;
}
#line 385
lowp float UnitySpotCookie( in highp vec4 LightCoord ) {
    return texture( _LightTexture0, ((LightCoord.xy / LightCoord.w) + 0.5)).w;
}
#line 445
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 449
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 453
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 457
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 461
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 465
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 469
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 509
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 511
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 515
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 519
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 523
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 lightDir = normalize(IN.lightDir);
    lowp vec4 c = LightingBlinnPhongEditor( o, lightDir, normalize(IN.viewDir), (((float((IN._LightCoord.z > 0.0)) * UnitySpotCookie( IN._LightCoord)) * UnitySpotAttenuate( IN._LightCoord.xyz)) * 1.0));
    #line 527
    c.w = 0.0;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in mediump vec3 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN._LightCoord = vec4(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES


#ifdef VERTEX

varying highp vec3 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_glesVertex.xyz - ((_World2Object * tmpvar_8).xyz * unity_Scale.w)));
  highp vec3 tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_11 = tmpvar_1.xyz;
  tmpvar_12 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_13;
  tmpvar_13[0].x = tmpvar_11.x;
  tmpvar_13[0].y = tmpvar_12.x;
  tmpvar_13[0].z = tmpvar_2.x;
  tmpvar_13[1].x = tmpvar_11.y;
  tmpvar_13[1].y = tmpvar_12.y;
  tmpvar_13[1].z = tmpvar_2.y;
  tmpvar_13[2].x = tmpvar_11.z;
  tmpvar_13[2].y = tmpvar_12.z;
  tmpvar_13[2].z = tmpvar_2.z;
  vec4 v_14;
  v_14.x = _Object2World[0].x;
  v_14.y = _Object2World[1].x;
  v_14.z = _Object2World[2].x;
  v_14.w = _Object2World[3].x;
  highp vec4 tmpvar_15;
  tmpvar_15.xyz = (tmpvar_13 * v_14.xyz);
  tmpvar_15.w = tmpvar_10.x;
  highp vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_15 * unity_Scale.w);
  tmpvar_4 = tmpvar_16;
  vec4 v_17;
  v_17.x = _Object2World[0].y;
  v_17.y = _Object2World[1].y;
  v_17.z = _Object2World[2].y;
  v_17.w = _Object2World[3].y;
  highp vec4 tmpvar_18;
  tmpvar_18.xyz = (tmpvar_13 * v_17.xyz);
  tmpvar_18.w = tmpvar_10.y;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_18 * unity_Scale.w);
  tmpvar_5 = tmpvar_19;
  vec4 v_20;
  v_20.x = _Object2World[0].z;
  v_20.y = _Object2World[1].z;
  v_20.z = _Object2World[2].z;
  v_20.w = _Object2World[3].z;
  highp vec4 tmpvar_21;
  tmpvar_21.xyz = (tmpvar_13 * v_20.xyz);
  tmpvar_21.w = tmpvar_10.z;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_21 * unity_Scale.w);
  tmpvar_6 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (tmpvar_13 * (((_World2Object * _WorldSpaceLightPos0).xyz * unity_Scale.w) - _glesVertex.xyz));
  tmpvar_7 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.w = 1.0;
  tmpvar_24.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_13 * (((_World2Object * tmpvar_24).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = tmpvar_7;
  xlv_TEXCOORD6 = (_LightMatrix0 * (_Object2World * _glesVertex)).xyz;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _LightTextureB0;
uniform samplerCube _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7.x = xlv_TEXCOORD2.w;
  tmpvar_7.y = xlv_TEXCOORD3.w;
  tmpvar_7.z = xlv_TEXCOORD4.w;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD2.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump float tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_4.z;
  tmpvar_21.y = tmpvar_5.z;
  tmpvar_21.z = tmpvar_6.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_3 - (2.0 * (dot (tmpvar_21, tmpvar_3) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_11 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_11 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_11 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_11 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_11 = tmpvar_28;
  tmpvar_13 = _Gloss;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_12 = tmpvar_29;
  mediump vec3 tmpvar_30;
  tmpvar_30 = normalize(vec3(0.0, 0.0, 1.0));
  mediump vec3 tmpvar_31;
  tmpvar_31 = normalize(xlv_TEXCOORD5);
  lightDir_2 = tmpvar_31;
  highp vec3 tmpvar_32;
  tmpvar_32 = normalize(xlv_TEXCOORD1);
  highp float tmpvar_33;
  tmpvar_33 = dot (xlv_TEXCOORD6, xlv_TEXCOORD6);
  lowp vec4 tmpvar_34;
  tmpvar_34 = texture2D (_LightTextureB0, vec2(tmpvar_33));
  lowp vec4 tmpvar_35;
  tmpvar_35 = textureCube (_LightTexture0, xlv_TEXCOORD6);
  mediump vec3 lightDir_36;
  lightDir_36 = lightDir_2;
  mediump vec3 viewDir_37;
  viewDir_37 = tmpvar_32;
  mediump float atten_38;
  atten_38 = (tmpvar_34.w * tmpvar_35.w);
  mediump vec4 res_39;
  highp float nh_40;
  mediump float tmpvar_41;
  tmpvar_41 = max (0.0, dot (tmpvar_30, normalize((lightDir_36 + viewDir_37))));
  nh_40 = tmpvar_41;
  mediump float arg1_42;
  arg1_42 = (tmpvar_13 * 128.0);
  res_39.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_36, tmpvar_30)));
  lowp float tmpvar_43;
  tmpvar_43 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_44;
  tmpvar_44 = (pow (nh_40, arg1_42) * tmpvar_43);
  res_39.w = tmpvar_44;
  mediump vec4 tmpvar_45;
  tmpvar_45 = (res_39 * (atten_38 * 2.0));
  res_39 = tmpvar_45;
  mediump vec4 c_46;
  c_46.xyz = ((tmpvar_11 * tmpvar_45.xyz) + (tmpvar_45.xyz * (tmpvar_45.w * tmpvar_12)));
  c_46.w = 1.0;
  c_1.xyz = c_46.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "POINT_COOKIE" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 395
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 426
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 466
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec3 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform samplerCube _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform sampler2D _LightTextureB0;
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
#line 388
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
#line 392
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 406
#line 414
#line 437
#line 478
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return ((objSpaceLightPos.xyz * unity_Scale.w) - v.xyz);
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 480
v2f_surf vert_surf( in appdata_full v ) {
    #line 482
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 486
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 490
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    #line 494
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    o._LightCoord = (_LightMatrix0 * (_Object2World * v.vertex)).xyz;
    #line 499
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out mediump vec3 xlv_TEXCOORD5;
out highp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec3(xl_retval._LightCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 395
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 426
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 466
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec3 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform samplerCube _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform sampler2D _LightTextureB0;
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
#line 388
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
#line 392
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 406
#line 414
#line 437
#line 478
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 406
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 410
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 414
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 418
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 422
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 437
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 441
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 445
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 449
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 453
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 457
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 461
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 501
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 503
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 507
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 511
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 515
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 lightDir = normalize(IN.lightDir);
    lowp vec4 c = LightingBlinnPhongEditor( o, lightDir, normalize(IN.viewDir), ((texture( _LightTextureB0, vec2( dot( IN._LightCoord, IN._LightCoord))).w * texture( _LightTexture0, IN._LightCoord).w) * 1.0));
    #line 519
    c.w = 0.0;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in mediump vec3 xlv_TEXCOORD5;
in highp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN._LightCoord = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES


#ifdef VERTEX

varying highp vec2 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_8;
  tmpvar_8.w = 1.0;
  tmpvar_8.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (_glesVertex.xyz - ((_World2Object * tmpvar_8).xyz * unity_Scale.w)));
  highp vec3 tmpvar_11;
  highp vec3 tmpvar_12;
  tmpvar_11 = tmpvar_1.xyz;
  tmpvar_12 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_13;
  tmpvar_13[0].x = tmpvar_11.x;
  tmpvar_13[0].y = tmpvar_12.x;
  tmpvar_13[0].z = tmpvar_2.x;
  tmpvar_13[1].x = tmpvar_11.y;
  tmpvar_13[1].y = tmpvar_12.y;
  tmpvar_13[1].z = tmpvar_2.y;
  tmpvar_13[2].x = tmpvar_11.z;
  tmpvar_13[2].y = tmpvar_12.z;
  tmpvar_13[2].z = tmpvar_2.z;
  vec4 v_14;
  v_14.x = _Object2World[0].x;
  v_14.y = _Object2World[1].x;
  v_14.z = _Object2World[2].x;
  v_14.w = _Object2World[3].x;
  highp vec4 tmpvar_15;
  tmpvar_15.xyz = (tmpvar_13 * v_14.xyz);
  tmpvar_15.w = tmpvar_10.x;
  highp vec4 tmpvar_16;
  tmpvar_16 = (tmpvar_15 * unity_Scale.w);
  tmpvar_4 = tmpvar_16;
  vec4 v_17;
  v_17.x = _Object2World[0].y;
  v_17.y = _Object2World[1].y;
  v_17.z = _Object2World[2].y;
  v_17.w = _Object2World[3].y;
  highp vec4 tmpvar_18;
  tmpvar_18.xyz = (tmpvar_13 * v_17.xyz);
  tmpvar_18.w = tmpvar_10.y;
  highp vec4 tmpvar_19;
  tmpvar_19 = (tmpvar_18 * unity_Scale.w);
  tmpvar_5 = tmpvar_19;
  vec4 v_20;
  v_20.x = _Object2World[0].z;
  v_20.y = _Object2World[1].z;
  v_20.z = _Object2World[2].z;
  v_20.w = _Object2World[3].z;
  highp vec4 tmpvar_21;
  tmpvar_21.xyz = (tmpvar_13 * v_20.xyz);
  tmpvar_21.w = tmpvar_10.z;
  highp vec4 tmpvar_22;
  tmpvar_22 = (tmpvar_21 * unity_Scale.w);
  tmpvar_6 = tmpvar_22;
  highp vec3 tmpvar_23;
  tmpvar_23 = (tmpvar_13 * (_World2Object * _WorldSpaceLightPos0).xyz);
  tmpvar_7 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24.w = 1.0;
  tmpvar_24.xyz = _WorldSpaceCameraPos;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_13 * (((_World2Object * tmpvar_24).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = tmpvar_7;
  xlv_TEXCOORD6 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

varying highp vec2 xlv_TEXCOORD6;
varying mediump vec3 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying lowp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
uniform sampler2D _LightTexture0;
uniform lowp vec4 _LightColor0;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7.x = xlv_TEXCOORD2.w;
  tmpvar_7.y = xlv_TEXCOORD3.w;
  tmpvar_7.z = xlv_TEXCOORD4.w;
  tmpvar_3 = tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD2.xyz;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  mediump vec3 tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump float tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_4.z;
  tmpvar_21.y = tmpvar_5.z;
  tmpvar_21.z = tmpvar_6.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_3 - (2.0 * (dot (tmpvar_21, tmpvar_3) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_11 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_11 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_11, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_11 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_11 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_11 = tmpvar_28;
  tmpvar_13 = _Gloss;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_12 = tmpvar_29;
  mediump vec3 tmpvar_30;
  tmpvar_30 = normalize(vec3(0.0, 0.0, 1.0));
  lightDir_2 = xlv_TEXCOORD5;
  highp vec3 tmpvar_31;
  tmpvar_31 = normalize(xlv_TEXCOORD1);
  lowp vec4 tmpvar_32;
  tmpvar_32 = texture2D (_LightTexture0, xlv_TEXCOORD6);
  mediump vec3 lightDir_33;
  lightDir_33 = lightDir_2;
  mediump vec3 viewDir_34;
  viewDir_34 = tmpvar_31;
  mediump float atten_35;
  atten_35 = tmpvar_32.w;
  mediump vec4 res_36;
  highp float nh_37;
  mediump float tmpvar_38;
  tmpvar_38 = max (0.0, dot (tmpvar_30, normalize((lightDir_33 + viewDir_34))));
  nh_37 = tmpvar_38;
  mediump float arg1_39;
  arg1_39 = (tmpvar_13 * 128.0);
  res_36.xyz = (_LightColor0.xyz * max (0.0, dot (lightDir_33, tmpvar_30)));
  lowp float tmpvar_40;
  tmpvar_40 = dot (_LightColor0.xyz, vec3(0.22, 0.707, 0.071));
  highp float tmpvar_41;
  tmpvar_41 = (pow (nh_37, arg1_39) * tmpvar_40);
  res_36.w = tmpvar_41;
  mediump vec4 tmpvar_42;
  tmpvar_42 = (res_36 * (atten_35 * 2.0));
  res_36 = tmpvar_42;
  mediump vec4 c_43;
  c_43.xyz = ((tmpvar_11 * tmpvar_42.xyz) + (tmpvar_42.xyz * (tmpvar_42.w * tmpvar_12)));
  c_43.w = 1.0;
  c_1.xyz = c_43.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 394
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 425
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 465
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec2 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 388
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 392
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 405
#line 413
#line 436
#line 477
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 81
highp vec3 ObjSpaceLightDir( in highp vec4 v ) {
    highp vec3 objSpaceLightPos = (_World2Object * _WorldSpaceLightPos0).xyz;
    return objSpaceLightPos.xyz;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 479
v2f_surf vert_surf( in appdata_full v ) {
    #line 481
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 485
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 489
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    highp vec3 lightDir = (rotation * ObjSpaceLightDir( v.vertex));
    #line 493
    o.lightDir = lightDir;
    highp vec3 viewDirForLight = (rotation * ObjSpaceViewDir( v.vertex));
    o.viewDir = viewDirForLight;
    o._LightCoord = (_LightMatrix0 * (_Object2World * v.vertex)).xy;
    #line 498
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out lowp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out mediump vec3 xlv_TEXCOORD5;
out highp vec2 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD5 = vec3(xl_retval.lightDir);
    xlv_TEXCOORD6 = vec2(xl_retval._LightCoord);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 394
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 425
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 465
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    mediump vec3 lightDir;
    highp vec2 _LightCoord;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform lowp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform sampler2D _LightTexture0;
uniform highp mat4 _LightMatrix0;
#line 384
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
#line 388
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
#line 392
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 405
#line 413
#line 436
#line 477
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 405
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 409
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 413
mediump vec4 LightingBlinnPhongEditor( in EditorSurfaceOutput s, in mediump vec3 lightDir, in mediump vec3 viewDir, in mediump float atten ) {
    mediump vec3 h = normalize((lightDir + viewDir));
    mediump float diff = max( 0.0, dot( lightDir, s.Normal));
    #line 417
    highp float nh = max( 0.0, dot( s.Normal, h));
    highp float spec = pow( nh, (s.Specular * 128.0));
    mediump vec4 res;
    res.xyz = (_LightColor0.xyz * diff);
    #line 421
    res.w = (spec * Luminance( _LightColor0.xyz));
    res *= (atten * 2.0);
    return LightingBlinnPhongEditor_PrePass( s, res);
}
#line 436
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 440
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 444
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 448
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 452
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 456
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 460
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 500
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 502
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 506
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 510
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 514
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 lightDir = IN.lightDir;
    lowp vec4 c = LightingBlinnPhongEditor( o, lightDir, normalize(IN.viewDir), (texture( _LightTexture0, IN._LightCoord).w * 1.0));
    #line 518
    c.w = 0.0;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in lowp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in mediump vec3 xlv_TEXCOORD5;
in highp vec2 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD4);
    xlt_IN.lightDir = vec3(xlv_TEXCOORD5);
    xlt_IN._LightCoord = vec2(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "POINT" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "SPOT" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "POINT_COOKIE" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES3"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" "QUEUE"="Geometry" "IGNOREPROJECTOR"="False" "RenderType"="Opaque" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform highp vec4 unity_Scale;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesTANGENT;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_3 = tmpvar_1.xyz;
  tmpvar_4 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_5;
  tmpvar_5[0].x = tmpvar_3.x;
  tmpvar_5[0].y = tmpvar_4.x;
  tmpvar_5[0].z = tmpvar_2.x;
  tmpvar_5[1].x = tmpvar_3.y;
  tmpvar_5[1].y = tmpvar_4.y;
  tmpvar_5[1].z = tmpvar_2.y;
  tmpvar_5[2].x = tmpvar_3.z;
  tmpvar_5[2].y = tmpvar_4.z;
  tmpvar_5[2].z = tmpvar_2.z;
  vec3 v_6;
  v_6.x = _Object2World[0].x;
  v_6.y = _Object2World[1].x;
  v_6.z = _Object2World[2].x;
  vec3 v_7;
  v_7.x = _Object2World[0].y;
  v_7.y = _Object2World[1].y;
  v_7.z = _Object2World[2].y;
  vec3 v_8;
  v_8.x = _Object2World[0].z;
  v_8.y = _Object2World[1].z;
  v_8.z = _Object2World[2].z;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((tmpvar_5 * v_6) * unity_Scale.w);
  xlv_TEXCOORD1 = ((tmpvar_5 * v_7) * unity_Scale.w);
  xlv_TEXCOORD2 = ((tmpvar_5 * v_8) * unity_Scale.w);
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD0;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp float _Gloss;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 res_1;
  lowp vec3 worldN_2;
  mediump vec3 tmpvar_3;
  highp vec2 tmpvar_4;
  highp vec2 tmpvar_5;
  highp vec3 tmpvar_6;
  highp vec3 tmpvar_7;
  mediump vec3 tmpvar_8;
  mediump float tmpvar_9;
  highp vec4 TexCUBE0_10;
  highp vec4 DamageMask_11;
  highp vec4 Tex2D2_12;
  highp vec4 Tex2D0_13;
  lowp vec4 tmpvar_14;
  tmpvar_14 = texture2D (_Diffuse, tmpvar_4);
  Tex2D0_13 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2D (_Mask, tmpvar_5);
  Tex2D2_12 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture2D (_DamageMask, tmpvar_5);
  DamageMask_11 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17 = textureCube (_Cube, tmpvar_6);
  TexCUBE0_10 = tmpvar_17;
  highp vec4 tmpvar_18;
  tmpvar_18 = vec4((1.0 - dot (normalize(tmpvar_7), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_19;
  tmpvar_19 = mix (((Tex2D0_13 + (Tex2D2_12 * (TexCUBE0_10 * tmpvar_18))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_11.x, (_Damage * 10.0)))).xyz;
  tmpvar_8 = tmpvar_19;
  highp vec3 tmpvar_20;
  tmpvar_20 = mix (tmpvar_8, _DamageColor.xyz, vec3(mix (0.0, DamageMask_11.y, (_Damage * 5.0))));
  tmpvar_8 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21 = mix (tmpvar_8, _DamageColor.xyz, vec3(mix (0.0, DamageMask_11.z, _Damage)));
  tmpvar_8 = tmpvar_21;
  highp vec3 tmpvar_22;
  tmpvar_22 = (tmpvar_8 + ((_RimColor * pow (tmpvar_18, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_8 = tmpvar_22;
  tmpvar_9 = _Gloss;
  mediump vec3 tmpvar_23;
  tmpvar_23 = normalize(vec3(0.0, 0.0, 1.0));
  highp float tmpvar_24;
  tmpvar_24 = dot (xlv_TEXCOORD0, tmpvar_23);
  worldN_2.x = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (xlv_TEXCOORD1, tmpvar_23);
  worldN_2.y = tmpvar_25;
  highp float tmpvar_26;
  tmpvar_26 = dot (xlv_TEXCOORD2, tmpvar_23);
  worldN_2.z = tmpvar_26;
  tmpvar_3 = worldN_2;
  mediump vec3 tmpvar_27;
  tmpvar_27 = ((tmpvar_3 * 0.5) + 0.5);
  res_1.xyz = tmpvar_27;
  res_1.w = tmpvar_9;
  gl_FragData[0] = res_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
};
#line 460
struct v2f_surf {
    highp vec4 pos;
    highp vec3 TtoW0;
    highp vec3 TtoW1;
    highp vec3 TtoW2;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 431
#line 468
#line 468
v2f_surf vert_surf( in appdata_full v ) {
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    #line 472
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    o.TtoW0 = ((rotation * xll_matrixindex_mf3x3_i (mat3( _Object2World), 0).xyz) * unity_Scale.w);
    o.TtoW1 = ((rotation * xll_matrixindex_mf3x3_i (mat3( _Object2World), 1).xyz) * unity_Scale.w);
    #line 476
    o.TtoW2 = ((rotation * xll_matrixindex_mf3x3_i (mat3( _Object2World), 2).xyz) * unity_Scale.w);
    return o;
}

out highp vec3 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec3(xl_retval.TtoW0);
    xlv_TEXCOORD1 = vec3(xl_retval.TtoW1);
    xlv_TEXCOORD2 = vec3(xl_retval.TtoW2);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
};
#line 460
struct v2f_surf {
    highp vec4 pos;
    highp vec3 TtoW0;
    highp vec3 TtoW1;
    highp vec3 TtoW2;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 431
#line 468
#line 431
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 435
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 439
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 443
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( IN.worldRefl, 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 447
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 451
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 455
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 479
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 481
    Input surfIN;
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    #line 485
    o.Specular = 0.0;
    o.Alpha = 0.0;
    surf( surfIN, o);
    lowp vec3 worldN;
    #line 489
    worldN.x = dot( IN.TtoW0, o.Normal);
    worldN.y = dot( IN.TtoW1, o.Normal);
    worldN.z = dot( IN.TtoW2, o.Normal);
    o.Normal = worldN;
    #line 493
    lowp vec4 res;
    res.xyz = ((o.Normal * 0.5) + 0.5);
    res.w = o.Specular;
    return res;
}
in highp vec3 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.TtoW0 = vec3(xlv_TEXCOORD0);
    xlt_IN.TtoW1 = vec3(xlv_TEXCOORD1);
    xlt_IN.TtoW2 = vec3(xlv_TEXCOORD2);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
}
Program "fp" {
SubProgram "gles " {
"!!GLES"
}
SubProgram "gles3 " {
"!!GLES3"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" "QUEUE"="Geometry" "IGNOREPROJECTOR"="False" "RenderType"="Opaque" }
  ZWrite Off
Program "vp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec3 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform highp vec4 _ProjectionParams;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  highp vec3 tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (glstate_matrix_mvp * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_glesVertex.xyz - ((_World2Object * tmpvar_9).xyz * unity_Scale.w)));
  highp vec3 tmpvar_12;
  highp vec3 tmpvar_13;
  tmpvar_12 = tmpvar_1.xyz;
  tmpvar_13 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_14;
  tmpvar_14[0].x = tmpvar_12.x;
  tmpvar_14[0].y = tmpvar_13.x;
  tmpvar_14[0].z = tmpvar_2.x;
  tmpvar_14[1].x = tmpvar_12.y;
  tmpvar_14[1].y = tmpvar_13.y;
  tmpvar_14[1].z = tmpvar_2.y;
  tmpvar_14[2].x = tmpvar_12.z;
  tmpvar_14[2].y = tmpvar_13.z;
  tmpvar_14[2].z = tmpvar_2.z;
  vec4 v_15;
  v_15.x = _Object2World[0].x;
  v_15.y = _Object2World[1].x;
  v_15.z = _Object2World[2].x;
  v_15.w = _Object2World[3].x;
  highp vec4 tmpvar_16;
  tmpvar_16.xyz = (tmpvar_14 * v_15.xyz);
  tmpvar_16.w = tmpvar_11.x;
  highp vec4 tmpvar_17;
  tmpvar_17 = (tmpvar_16 * unity_Scale.w);
  tmpvar_4 = tmpvar_17;
  vec4 v_18;
  v_18.x = _Object2World[0].y;
  v_18.y = _Object2World[1].y;
  v_18.z = _Object2World[2].y;
  v_18.w = _Object2World[3].y;
  highp vec4 tmpvar_19;
  tmpvar_19.xyz = (tmpvar_14 * v_18.xyz);
  tmpvar_19.w = tmpvar_11.y;
  highp vec4 tmpvar_20;
  tmpvar_20 = (tmpvar_19 * unity_Scale.w);
  tmpvar_5 = tmpvar_20;
  vec4 v_21;
  v_21.x = _Object2World[0].z;
  v_21.y = _Object2World[1].z;
  v_21.z = _Object2World[2].z;
  v_21.w = _Object2World[3].z;
  highp vec4 tmpvar_22;
  tmpvar_22.xyz = (tmpvar_14 * v_21.xyz);
  tmpvar_22.w = tmpvar_11.z;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_22 * unity_Scale.w);
  tmpvar_6 = tmpvar_23;
  highp vec4 o_24;
  highp vec4 tmpvar_25;
  tmpvar_25 = (tmpvar_8 * 0.5);
  highp vec2 tmpvar_26;
  tmpvar_26.x = tmpvar_25.x;
  tmpvar_26.y = (tmpvar_25.y * _ProjectionParams.x);
  o_24.xy = (tmpvar_26 + tmpvar_25.w);
  o_24.zw = tmpvar_8.zw;
  mat3 tmpvar_27;
  tmpvar_27[0] = _Object2World[0].xyz;
  tmpvar_27[1] = _Object2World[1].xyz;
  tmpvar_27[2] = _Object2World[2].xyz;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = (tmpvar_27 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_29;
  mediump vec4 normal_30;
  normal_30 = tmpvar_28;
  highp float vC_31;
  mediump vec3 x3_32;
  mediump vec3 x2_33;
  mediump vec3 x1_34;
  highp float tmpvar_35;
  tmpvar_35 = dot (unity_SHAr, normal_30);
  x1_34.x = tmpvar_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAg, normal_30);
  x1_34.y = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAb, normal_30);
  x1_34.z = tmpvar_37;
  mediump vec4 tmpvar_38;
  tmpvar_38 = (normal_30.xyzz * normal_30.yzzx);
  highp float tmpvar_39;
  tmpvar_39 = dot (unity_SHBr, tmpvar_38);
  x2_33.x = tmpvar_39;
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBg, tmpvar_38);
  x2_33.y = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBb, tmpvar_38);
  x2_33.z = tmpvar_41;
  mediump float tmpvar_42;
  tmpvar_42 = ((normal_30.x * normal_30.x) - (normal_30.y * normal_30.y));
  vC_31 = tmpvar_42;
  highp vec3 tmpvar_43;
  tmpvar_43 = (unity_SHC.xyz * vC_31);
  x3_32 = tmpvar_43;
  tmpvar_29 = ((x1_34 + x2_33) + x3_32);
  tmpvar_7 = tmpvar_29;
  highp vec4 tmpvar_44;
  tmpvar_44.w = 1.0;
  tmpvar_44.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_14 * (((_World2Object * tmpvar_44).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = o_24;
  xlv_TEXCOORD3 = tmpvar_4;
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = tmpvar_7;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _LightBuffer;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  mediump vec3 tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8.x = xlv_TEXCOORD3.w;
  tmpvar_8.y = xlv_TEXCOORD4.w;
  tmpvar_8.z = xlv_TEXCOORD5.w;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  lowp vec3 tmpvar_11;
  tmpvar_11 = xlv_TEXCOORD5.xyz;
  tmpvar_7 = tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump vec3 tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_5.z;
  tmpvar_21.y = tmpvar_6.z;
  tmpvar_21.z = tmpvar_7.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_4 - (2.0 * (dot (tmpvar_21, tmpvar_4) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_12 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_12, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_12 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_12, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_12 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_12 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_12 = tmpvar_28;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_13 = tmpvar_29;
  lowp vec4 tmpvar_30;
  tmpvar_30 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_3 = tmpvar_30;
  mediump vec4 tmpvar_31;
  tmpvar_31 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
  light_3.w = tmpvar_31.w;
  highp vec3 tmpvar_32;
  tmpvar_32 = (tmpvar_31.xyz + xlv_TEXCOORD6);
  light_3.xyz = tmpvar_32;
  mediump vec4 c_33;
  c_33.xyz = ((tmpvar_12 * light_3.xyz) + (light_3.xyz * (tmpvar_31.w * tmpvar_13)));
  c_33.w = 1.0;
  c_2.w = c_33.w;
  c_2.xyz = c_33.xyz;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 496
uniform sampler2D _LightBuffer;
uniform lowp vec4 unity_Ambient;
#line 283
highp vec4 ComputeScreenPos( in highp vec4 pos ) {
    #line 285
    highp vec4 o = (pos * 0.5);
    o.xy = (vec2( o.x, (o.y * _ProjectionParams.x)) + o.w);
    o.zw = pos.zw;
    return o;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 477
v2f_surf vert_surf( in appdata_full v ) {
    #line 479
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 483
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 487
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.screen = ComputeScreenPos( o.pos);
    #line 491
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    o.vlight = ShadeSH9( vec4( worldN, 1.0));
    o.viewDir = (rotation * ObjSpaceViewDir( v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec4 xlv_TEXCOORD5;
out highp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.screen);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD5 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 496
uniform sampler2D _LightBuffer;
uniform lowp vec4 unity_Ambient;
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 498
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 500
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 504
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 508
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 512
    o.Alpha = 0.0;
    surf( surfIN, o);
    mediump vec4 light = textureProj( _LightBuffer, IN.screen);
    light = max( light, vec4( 0.001));
    #line 516
    light = (-log2(light));
    light.xyz += IN.vlight;
    mediump vec4 c = LightingBlinnPhongEditor_PrePass( o, light);
    c.xyz += o.Emission;
    #line 520
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec4 xlv_TEXCOORD5;
in highp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.screen = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD4);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD7;
varying highp vec2 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp vec4 _ProjectionParams;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  highp vec4 tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (glstate_matrix_mvp * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_glesVertex.xyz - ((_World2Object * tmpvar_9).xyz * unity_Scale.w)));
  highp vec3 tmpvar_12;
  highp vec3 tmpvar_13;
  tmpvar_12 = tmpvar_1.xyz;
  tmpvar_13 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_14;
  tmpvar_14[0].x = tmpvar_12.x;
  tmpvar_14[0].y = tmpvar_13.x;
  tmpvar_14[0].z = tmpvar_2.x;
  tmpvar_14[1].x = tmpvar_12.y;
  tmpvar_14[1].y = tmpvar_13.y;
  tmpvar_14[1].z = tmpvar_2.y;
  tmpvar_14[2].x = tmpvar_12.z;
  tmpvar_14[2].y = tmpvar_13.z;
  tmpvar_14[2].z = tmpvar_2.z;
  vec4 v_15;
  v_15.x = _Object2World[0].x;
  v_15.y = _Object2World[1].x;
  v_15.z = _Object2World[2].x;
  v_15.w = _Object2World[3].x;
  highp vec4 tmpvar_16;
  tmpvar_16.xyz = (tmpvar_14 * v_15.xyz);
  tmpvar_16.w = tmpvar_11.x;
  highp vec4 tmpvar_17;
  tmpvar_17 = (tmpvar_16 * unity_Scale.w);
  tmpvar_4 = tmpvar_17;
  vec4 v_18;
  v_18.x = _Object2World[0].y;
  v_18.y = _Object2World[1].y;
  v_18.z = _Object2World[2].y;
  v_18.w = _Object2World[3].y;
  highp vec4 tmpvar_19;
  tmpvar_19.xyz = (tmpvar_14 * v_18.xyz);
  tmpvar_19.w = tmpvar_11.y;
  highp vec4 tmpvar_20;
  tmpvar_20 = (tmpvar_19 * unity_Scale.w);
  tmpvar_5 = tmpvar_20;
  vec4 v_21;
  v_21.x = _Object2World[0].z;
  v_21.y = _Object2World[1].z;
  v_21.z = _Object2World[2].z;
  v_21.w = _Object2World[3].z;
  highp vec4 tmpvar_22;
  tmpvar_22.xyz = (tmpvar_14 * v_21.xyz);
  tmpvar_22.w = tmpvar_11.z;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_22 * unity_Scale.w);
  tmpvar_6 = tmpvar_23;
  highp vec4 o_24;
  highp vec4 tmpvar_25;
  tmpvar_25 = (tmpvar_8 * 0.5);
  highp vec2 tmpvar_26;
  tmpvar_26.x = tmpvar_25.x;
  tmpvar_26.y = (tmpvar_25.y * _ProjectionParams.x);
  o_24.xy = (tmpvar_26 + tmpvar_25.w);
  o_24.zw = tmpvar_8.zw;
  tmpvar_7.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_7.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec4 tmpvar_27;
  tmpvar_27.w = 1.0;
  tmpvar_27.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_14 * (((_World2Object * tmpvar_27).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = o_24;
  xlv_TEXCOORD3 = tmpvar_4;
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD7 = tmpvar_7;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD7;
varying highp vec2 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapFade;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _LightBuffer;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  mediump vec3 tmpvar_8;
  mediump vec3 tmpvar_9;
  mediump vec3 tmpvar_10;
  lowp vec3 tmpvar_11;
  tmpvar_11.x = xlv_TEXCOORD3.w;
  tmpvar_11.y = xlv_TEXCOORD4.w;
  tmpvar_11.z = xlv_TEXCOORD5.w;
  tmpvar_7 = tmpvar_11;
  lowp vec3 tmpvar_12;
  tmpvar_12 = xlv_TEXCOORD3.xyz;
  tmpvar_8 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = xlv_TEXCOORD4.xyz;
  tmpvar_9 = tmpvar_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = xlv_TEXCOORD5.xyz;
  tmpvar_10 = tmpvar_14;
  mediump vec3 tmpvar_15;
  mediump vec3 tmpvar_16;
  highp vec4 TexCUBE0_17;
  highp vec4 DamageMask_18;
  highp vec4 Tex2D2_19;
  highp vec4 Tex2D0_20;
  lowp vec4 tmpvar_21;
  tmpvar_21 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_20 = tmpvar_21;
  lowp vec4 tmpvar_22;
  tmpvar_22 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_19 = tmpvar_22;
  lowp vec4 tmpvar_23;
  tmpvar_23 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_18 = tmpvar_23;
  highp vec3 tmpvar_24;
  tmpvar_24.x = tmpvar_8.z;
  tmpvar_24.y = tmpvar_9.z;
  tmpvar_24.z = tmpvar_10.z;
  highp vec4 tmpvar_25;
  tmpvar_25.w = 1.0;
  tmpvar_25.xyz = (tmpvar_7 - (2.0 * (dot (tmpvar_24, tmpvar_7) * tmpvar_24)));
  lowp vec4 tmpvar_26;
  tmpvar_26 = textureCube (_Cube, tmpvar_25.xyz);
  TexCUBE0_17 = tmpvar_26;
  highp vec4 tmpvar_27;
  tmpvar_27 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_28;
  tmpvar_28 = mix (((Tex2D0_20 + (Tex2D2_19 * (TexCUBE0_17 * tmpvar_27))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_18.x, (_Damage * 10.0)))).xyz;
  tmpvar_15 = tmpvar_28;
  highp vec3 tmpvar_29;
  tmpvar_29 = mix (tmpvar_15, _DamageColor.xyz, vec3(mix (0.0, DamageMask_18.y, (_Damage * 5.0))));
  tmpvar_15 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = mix (tmpvar_15, _DamageColor.xyz, vec3(mix (0.0, DamageMask_18.z, _Damage)));
  tmpvar_15 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (tmpvar_15 + ((_RimColor * pow (tmpvar_27, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_15 = tmpvar_31;
  highp vec3 tmpvar_32;
  tmpvar_32 = _SpecularColor.xyz;
  tmpvar_16 = tmpvar_32;
  lowp vec4 tmpvar_33;
  tmpvar_33 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_6 = tmpvar_33;
  mediump vec4 tmpvar_34;
  tmpvar_34 = -(log2(max (light_6, vec4(0.001, 0.001, 0.001, 0.001))));
  light_6.w = tmpvar_34.w;
  highp float tmpvar_35;
  tmpvar_35 = ((sqrt(dot (xlv_TEXCOORD7, xlv_TEXCOORD7)) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_35;
  lowp vec3 tmpvar_36;
  tmpvar_36 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD6).xyz);
  lmFull_4 = tmpvar_36;
  lowp vec3 tmpvar_37;
  tmpvar_37 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD6).xyz);
  lmIndirect_3 = tmpvar_37;
  light_6.xyz = (tmpvar_34.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  mediump vec4 c_38;
  c_38.xyz = ((tmpvar_15 * light_6.xyz) + (light_6.xyz * (tmpvar_34.w * tmpvar_16)));
  c_38.w = 1.0;
  c_2.w = c_38.w;
  c_2.xyz = c_38.xyz;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 lmapFadePos;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 476
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
#line 501
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
uniform lowp vec4 unity_Ambient;
#line 283
highp vec4 ComputeScreenPos( in highp vec4 pos ) {
    #line 285
    highp vec4 o = (pos * 0.5);
    o.xy = (vec2( o.x, (o.y * _ProjectionParams.x)) + o.w);
    o.zw = pos.zw;
    return o;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 479
v2f_surf vert_surf( in appdata_full v ) {
    #line 481
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 485
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 489
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.screen = ComputeScreenPos( o.pos);
    #line 493
    o.lmap.xy = ((v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    o.lmapFadePos.xyz = (((_Object2World * v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
    o.lmapFadePos.w = ((-(glstate_matrix_modelview0 * v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
    o.viewDir = (rotation * ObjSpaceViewDir( v.vertex));
    #line 497
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec4 xlv_TEXCOORD5;
out highp vec2 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.screen);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD5 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD6 = vec2(xl_retval.lmap);
    xlv_TEXCOORD7 = vec4(xl_retval.lmapFadePos);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];
float xll_saturate_f( float x) {
  return clamp( x, 0.0, 1.0);
}
vec2 xll_saturate_vf2( vec2 x) {
  return clamp( x, 0.0, 1.0);
}
vec3 xll_saturate_vf3( vec3 x) {
  return clamp( x, 0.0, 1.0);
}
vec4 xll_saturate_vf4( vec4 x) {
  return clamp( x, 0.0, 1.0);
}
mat2 xll_saturate_mf2x2(mat2 m) {
  return mat2( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0));
}
mat3 xll_saturate_mf3x3(mat3 m) {
  return mat3( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0), clamp(m[2], 0.0, 1.0));
}
mat4 xll_saturate_mf4x4(mat4 m) {
  return mat4( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0), clamp(m[2], 0.0, 1.0), clamp(m[3], 0.0, 1.0));
}
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 lmapFadePos;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 476
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
#line 501
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
uniform lowp vec4 unity_Ambient;
#line 176
lowp vec3 DecodeLightmap( in lowp vec4 color ) {
    #line 178
    return (2.0 * color.xyz);
}
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 504
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 506
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 510
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 514
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 518
    o.Alpha = 0.0;
    surf( surfIN, o);
    mediump vec4 light = textureProj( _LightBuffer, IN.screen);
    light = max( light, vec4( 0.001));
    #line 522
    light = (-log2(light));
    lowp vec4 lmtex = texture( unity_Lightmap, IN.lmap.xy);
    lowp vec4 lmtex2 = texture( unity_LightmapInd, IN.lmap.xy);
    mediump float lmFade = ((length(IN.lmapFadePos) * unity_LightmapFade.z) + unity_LightmapFade.w);
    #line 526
    mediump vec3 lmFull = DecodeLightmap( lmtex);
    mediump vec3 lmIndirect = DecodeLightmap( lmtex2);
    mediump vec3 lm = mix( lmIndirect, lmFull, vec3( xll_saturate_f(lmFade)));
    light.xyz += lm;
    #line 530
    mediump vec4 c = LightingBlinnPhongEditor_PrePass( o, light);
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec4 xlv_TEXCOORD5;
in highp vec2 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.screen = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD4);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD5);
    xlt_IN.lmap = vec2(xlv_TEXCOORD6);
    xlt_IN.lmapFadePos = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES


#ifdef VERTEX

varying highp vec3 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_SHC;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAr;
uniform highp vec4 _ProjectionParams;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  highp vec3 tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (glstate_matrix_mvp * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_glesVertex.xyz - ((_World2Object * tmpvar_9).xyz * unity_Scale.w)));
  highp vec3 tmpvar_12;
  highp vec3 tmpvar_13;
  tmpvar_12 = tmpvar_1.xyz;
  tmpvar_13 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_14;
  tmpvar_14[0].x = tmpvar_12.x;
  tmpvar_14[0].y = tmpvar_13.x;
  tmpvar_14[0].z = tmpvar_2.x;
  tmpvar_14[1].x = tmpvar_12.y;
  tmpvar_14[1].y = tmpvar_13.y;
  tmpvar_14[1].z = tmpvar_2.y;
  tmpvar_14[2].x = tmpvar_12.z;
  tmpvar_14[2].y = tmpvar_13.z;
  tmpvar_14[2].z = tmpvar_2.z;
  vec4 v_15;
  v_15.x = _Object2World[0].x;
  v_15.y = _Object2World[1].x;
  v_15.z = _Object2World[2].x;
  v_15.w = _Object2World[3].x;
  highp vec4 tmpvar_16;
  tmpvar_16.xyz = (tmpvar_14 * v_15.xyz);
  tmpvar_16.w = tmpvar_11.x;
  highp vec4 tmpvar_17;
  tmpvar_17 = (tmpvar_16 * unity_Scale.w);
  tmpvar_4 = tmpvar_17;
  vec4 v_18;
  v_18.x = _Object2World[0].y;
  v_18.y = _Object2World[1].y;
  v_18.z = _Object2World[2].y;
  v_18.w = _Object2World[3].y;
  highp vec4 tmpvar_19;
  tmpvar_19.xyz = (tmpvar_14 * v_18.xyz);
  tmpvar_19.w = tmpvar_11.y;
  highp vec4 tmpvar_20;
  tmpvar_20 = (tmpvar_19 * unity_Scale.w);
  tmpvar_5 = tmpvar_20;
  vec4 v_21;
  v_21.x = _Object2World[0].z;
  v_21.y = _Object2World[1].z;
  v_21.z = _Object2World[2].z;
  v_21.w = _Object2World[3].z;
  highp vec4 tmpvar_22;
  tmpvar_22.xyz = (tmpvar_14 * v_21.xyz);
  tmpvar_22.w = tmpvar_11.z;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_22 * unity_Scale.w);
  tmpvar_6 = tmpvar_23;
  highp vec4 o_24;
  highp vec4 tmpvar_25;
  tmpvar_25 = (tmpvar_8 * 0.5);
  highp vec2 tmpvar_26;
  tmpvar_26.x = tmpvar_25.x;
  tmpvar_26.y = (tmpvar_25.y * _ProjectionParams.x);
  o_24.xy = (tmpvar_26 + tmpvar_25.w);
  o_24.zw = tmpvar_8.zw;
  mat3 tmpvar_27;
  tmpvar_27[0] = _Object2World[0].xyz;
  tmpvar_27[1] = _Object2World[1].xyz;
  tmpvar_27[2] = _Object2World[2].xyz;
  highp vec4 tmpvar_28;
  tmpvar_28.w = 1.0;
  tmpvar_28.xyz = (tmpvar_27 * (tmpvar_2 * unity_Scale.w));
  mediump vec3 tmpvar_29;
  mediump vec4 normal_30;
  normal_30 = tmpvar_28;
  highp float vC_31;
  mediump vec3 x3_32;
  mediump vec3 x2_33;
  mediump vec3 x1_34;
  highp float tmpvar_35;
  tmpvar_35 = dot (unity_SHAr, normal_30);
  x1_34.x = tmpvar_35;
  highp float tmpvar_36;
  tmpvar_36 = dot (unity_SHAg, normal_30);
  x1_34.y = tmpvar_36;
  highp float tmpvar_37;
  tmpvar_37 = dot (unity_SHAb, normal_30);
  x1_34.z = tmpvar_37;
  mediump vec4 tmpvar_38;
  tmpvar_38 = (normal_30.xyzz * normal_30.yzzx);
  highp float tmpvar_39;
  tmpvar_39 = dot (unity_SHBr, tmpvar_38);
  x2_33.x = tmpvar_39;
  highp float tmpvar_40;
  tmpvar_40 = dot (unity_SHBg, tmpvar_38);
  x2_33.y = tmpvar_40;
  highp float tmpvar_41;
  tmpvar_41 = dot (unity_SHBb, tmpvar_38);
  x2_33.z = tmpvar_41;
  mediump float tmpvar_42;
  tmpvar_42 = ((normal_30.x * normal_30.x) - (normal_30.y * normal_30.y));
  vC_31 = tmpvar_42;
  highp vec3 tmpvar_43;
  tmpvar_43 = (unity_SHC.xyz * vC_31);
  x3_32 = tmpvar_43;
  tmpvar_29 = ((x1_34 + x2_33) + x3_32);
  tmpvar_7 = tmpvar_29;
  highp vec4 tmpvar_44;
  tmpvar_44.w = 1.0;
  tmpvar_44.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_14 * (((_World2Object * tmpvar_44).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = o_24;
  xlv_TEXCOORD3 = tmpvar_4;
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = tmpvar_7;
}



#endif
#ifdef FRAGMENT

varying highp vec3 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform sampler2D _LightBuffer;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  mediump vec3 tmpvar_6;
  mediump vec3 tmpvar_7;
  lowp vec3 tmpvar_8;
  tmpvar_8.x = xlv_TEXCOORD3.w;
  tmpvar_8.y = xlv_TEXCOORD4.w;
  tmpvar_8.z = xlv_TEXCOORD5.w;
  tmpvar_4 = tmpvar_8;
  lowp vec3 tmpvar_9;
  tmpvar_9 = xlv_TEXCOORD3.xyz;
  tmpvar_5 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = xlv_TEXCOORD4.xyz;
  tmpvar_6 = tmpvar_10;
  lowp vec3 tmpvar_11;
  tmpvar_11 = xlv_TEXCOORD5.xyz;
  tmpvar_7 = tmpvar_11;
  mediump vec3 tmpvar_12;
  mediump vec3 tmpvar_13;
  highp vec4 TexCUBE0_14;
  highp vec4 DamageMask_15;
  highp vec4 Tex2D2_16;
  highp vec4 Tex2D0_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_17 = tmpvar_18;
  lowp vec4 tmpvar_19;
  tmpvar_19 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_16 = tmpvar_19;
  lowp vec4 tmpvar_20;
  tmpvar_20 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_15 = tmpvar_20;
  highp vec3 tmpvar_21;
  tmpvar_21.x = tmpvar_5.z;
  tmpvar_21.y = tmpvar_6.z;
  tmpvar_21.z = tmpvar_7.z;
  highp vec4 tmpvar_22;
  tmpvar_22.w = 1.0;
  tmpvar_22.xyz = (tmpvar_4 - (2.0 * (dot (tmpvar_21, tmpvar_4) * tmpvar_21)));
  lowp vec4 tmpvar_23;
  tmpvar_23 = textureCube (_Cube, tmpvar_22.xyz);
  TexCUBE0_14 = tmpvar_23;
  highp vec4 tmpvar_24;
  tmpvar_24 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_25;
  tmpvar_25 = mix (((Tex2D0_17 + (Tex2D2_16 * (TexCUBE0_14 * tmpvar_24))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_15.x, (_Damage * 10.0)))).xyz;
  tmpvar_12 = tmpvar_25;
  highp vec3 tmpvar_26;
  tmpvar_26 = mix (tmpvar_12, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.y, (_Damage * 5.0))));
  tmpvar_12 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = mix (tmpvar_12, _DamageColor.xyz, vec3(mix (0.0, DamageMask_15.z, _Damage)));
  tmpvar_12 = tmpvar_27;
  highp vec3 tmpvar_28;
  tmpvar_28 = (tmpvar_12 + ((_RimColor * pow (tmpvar_24, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_12 = tmpvar_28;
  highp vec3 tmpvar_29;
  tmpvar_29 = _SpecularColor.xyz;
  tmpvar_13 = tmpvar_29;
  lowp vec4 tmpvar_30;
  tmpvar_30 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_3 = tmpvar_30;
  mediump vec4 tmpvar_31;
  tmpvar_31 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
  light_3.w = tmpvar_31.w;
  highp vec3 tmpvar_32;
  tmpvar_32 = (tmpvar_31.xyz + xlv_TEXCOORD6);
  light_3.xyz = tmpvar_32;
  mediump vec4 c_33;
  c_33.xyz = ((tmpvar_12 * light_3.xyz) + (light_3.xyz * (tmpvar_31.w * tmpvar_13)));
  c_33.w = 1.0;
  c_2.w = c_33.w;
  c_2.xyz = c_33.xyz;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 496
uniform sampler2D _LightBuffer;
uniform lowp vec4 unity_Ambient;
#line 283
highp vec4 ComputeScreenPos( in highp vec4 pos ) {
    #line 285
    highp vec4 o = (pos * 0.5);
    o.xy = (vec2( o.x, (o.y * _ProjectionParams.x)) + o.w);
    o.zw = pos.zw;
    return o;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 136
mediump vec3 ShadeSH9( in mediump vec4 normal ) {
    mediump vec3 x1;
    mediump vec3 x2;
    mediump vec3 x3;
    x1.x = dot( unity_SHAr, normal);
    #line 140
    x1.y = dot( unity_SHAg, normal);
    x1.z = dot( unity_SHAb, normal);
    mediump vec4 vB = (normal.xyzz * normal.yzzx);
    x2.x = dot( unity_SHBr, vB);
    #line 144
    x2.y = dot( unity_SHBg, vB);
    x2.z = dot( unity_SHBb, vB);
    highp float vC = ((normal.x * normal.x) - (normal.y * normal.y));
    x3 = (unity_SHC.xyz * vC);
    #line 148
    return ((x1 + x2) + x3);
}
#line 477
v2f_surf vert_surf( in appdata_full v ) {
    #line 479
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 483
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 487
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.screen = ComputeScreenPos( o.pos);
    #line 491
    highp vec3 worldN = (mat3( _Object2World) * (v.normal * unity_Scale.w));
    o.vlight = ShadeSH9( vec4( worldN, 1.0));
    o.viewDir = (rotation * ObjSpaceViewDir( v.vertex));
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec4 xlv_TEXCOORD5;
out highp vec3 xlv_TEXCOORD6;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.screen);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD5 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD6 = vec3(xl_retval.vlight);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];

#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec3 vlight;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 475
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
#line 496
uniform sampler2D _LightBuffer;
uniform lowp vec4 unity_Ambient;
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 498
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 500
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 504
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 508
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 512
    o.Alpha = 0.0;
    surf( surfIN, o);
    mediump vec4 light = textureProj( _LightBuffer, IN.screen);
    light = max( light, vec4( 0.001));
    #line 516
    light.xyz += IN.vlight;
    mediump vec4 c = LightingBlinnPhongEditor_PrePass( o, light);
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec4 xlv_TEXCOORD5;
in highp vec3 xlv_TEXCOORD6;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.screen = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD4);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD5);
    xlt_IN.vlight = vec3(xlv_TEXCOORD6);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES


#ifdef VERTEX

varying highp vec4 xlv_TEXCOORD7;
varying highp vec2 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 _Mask_ST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 unity_Scale;
uniform highp mat4 _World2Object;
uniform highp mat4 _Object2World;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp vec4 _ProjectionParams;
uniform highp vec3 _WorldSpaceCameraPos;
attribute vec4 _glesTANGENT;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesMultiTexCoord0;
attribute vec3 _glesNormal;
attribute vec4 _glesVertex;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  highp vec4 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 tmpvar_5;
  lowp vec4 tmpvar_6;
  highp vec4 tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (glstate_matrix_mvp * _glesVertex);
  tmpvar_3.xy = ((_glesMultiTexCoord0.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
  tmpvar_3.zw = ((_glesMultiTexCoord0.xy * _Mask_ST.xy) + _Mask_ST.zw);
  highp vec4 tmpvar_9;
  tmpvar_9.w = 1.0;
  tmpvar_9.xyz = _WorldSpaceCameraPos;
  mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (_glesVertex.xyz - ((_World2Object * tmpvar_9).xyz * unity_Scale.w)));
  highp vec3 tmpvar_12;
  highp vec3 tmpvar_13;
  tmpvar_12 = tmpvar_1.xyz;
  tmpvar_13 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_14;
  tmpvar_14[0].x = tmpvar_12.x;
  tmpvar_14[0].y = tmpvar_13.x;
  tmpvar_14[0].z = tmpvar_2.x;
  tmpvar_14[1].x = tmpvar_12.y;
  tmpvar_14[1].y = tmpvar_13.y;
  tmpvar_14[1].z = tmpvar_2.y;
  tmpvar_14[2].x = tmpvar_12.z;
  tmpvar_14[2].y = tmpvar_13.z;
  tmpvar_14[2].z = tmpvar_2.z;
  vec4 v_15;
  v_15.x = _Object2World[0].x;
  v_15.y = _Object2World[1].x;
  v_15.z = _Object2World[2].x;
  v_15.w = _Object2World[3].x;
  highp vec4 tmpvar_16;
  tmpvar_16.xyz = (tmpvar_14 * v_15.xyz);
  tmpvar_16.w = tmpvar_11.x;
  highp vec4 tmpvar_17;
  tmpvar_17 = (tmpvar_16 * unity_Scale.w);
  tmpvar_4 = tmpvar_17;
  vec4 v_18;
  v_18.x = _Object2World[0].y;
  v_18.y = _Object2World[1].y;
  v_18.z = _Object2World[2].y;
  v_18.w = _Object2World[3].y;
  highp vec4 tmpvar_19;
  tmpvar_19.xyz = (tmpvar_14 * v_18.xyz);
  tmpvar_19.w = tmpvar_11.y;
  highp vec4 tmpvar_20;
  tmpvar_20 = (tmpvar_19 * unity_Scale.w);
  tmpvar_5 = tmpvar_20;
  vec4 v_21;
  v_21.x = _Object2World[0].z;
  v_21.y = _Object2World[1].z;
  v_21.z = _Object2World[2].z;
  v_21.w = _Object2World[3].z;
  highp vec4 tmpvar_22;
  tmpvar_22.xyz = (tmpvar_14 * v_21.xyz);
  tmpvar_22.w = tmpvar_11.z;
  highp vec4 tmpvar_23;
  tmpvar_23 = (tmpvar_22 * unity_Scale.w);
  tmpvar_6 = tmpvar_23;
  highp vec4 o_24;
  highp vec4 tmpvar_25;
  tmpvar_25 = (tmpvar_8 * 0.5);
  highp vec2 tmpvar_26;
  tmpvar_26.x = tmpvar_25.x;
  tmpvar_26.y = (tmpvar_25.y * _ProjectionParams.x);
  o_24.xy = (tmpvar_26 + tmpvar_25.w);
  o_24.zw = tmpvar_8.zw;
  tmpvar_7.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_7.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec4 tmpvar_27;
  tmpvar_27.w = 1.0;
  tmpvar_27.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_8;
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = (tmpvar_14 * (((_World2Object * tmpvar_27).xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD2 = o_24;
  xlv_TEXCOORD3 = tmpvar_4;
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD7 = tmpvar_7;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_TEXCOORD7;
varying highp vec2 xlv_TEXCOORD6;
varying lowp vec4 xlv_TEXCOORD5;
varying lowp vec4 xlv_TEXCOORD4;
varying lowp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD0;
uniform highp vec4 unity_LightmapFade;
uniform sampler2D unity_LightmapInd;
uniform sampler2D unity_Lightmap;
uniform sampler2D _LightBuffer;
uniform highp vec4 _RimColor;
uniform highp float _Damage;
uniform sampler2D _DamageMask;
uniform sampler2D _Mask;
uniform samplerCube _Cube;
uniform highp vec4 _DamageColor;
uniform highp vec4 _SpecularColor;
uniform sampler2D _Diffuse;
uniform highp vec4 _MainColor;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  mediump vec3 tmpvar_8;
  mediump vec3 tmpvar_9;
  mediump vec3 tmpvar_10;
  lowp vec3 tmpvar_11;
  tmpvar_11.x = xlv_TEXCOORD3.w;
  tmpvar_11.y = xlv_TEXCOORD4.w;
  tmpvar_11.z = xlv_TEXCOORD5.w;
  tmpvar_7 = tmpvar_11;
  lowp vec3 tmpvar_12;
  tmpvar_12 = xlv_TEXCOORD3.xyz;
  tmpvar_8 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = xlv_TEXCOORD4.xyz;
  tmpvar_9 = tmpvar_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = xlv_TEXCOORD5.xyz;
  tmpvar_10 = tmpvar_14;
  mediump vec3 tmpvar_15;
  mediump vec3 tmpvar_16;
  highp vec4 TexCUBE0_17;
  highp vec4 DamageMask_18;
  highp vec4 Tex2D2_19;
  highp vec4 Tex2D0_20;
  lowp vec4 tmpvar_21;
  tmpvar_21 = texture2D (_Diffuse, xlv_TEXCOORD0.xy);
  Tex2D0_20 = tmpvar_21;
  lowp vec4 tmpvar_22;
  tmpvar_22 = texture2D (_Mask, xlv_TEXCOORD0.zw);
  Tex2D2_19 = tmpvar_22;
  lowp vec4 tmpvar_23;
  tmpvar_23 = texture2D (_DamageMask, xlv_TEXCOORD0.zw);
  DamageMask_18 = tmpvar_23;
  highp vec3 tmpvar_24;
  tmpvar_24.x = tmpvar_8.z;
  tmpvar_24.y = tmpvar_9.z;
  tmpvar_24.z = tmpvar_10.z;
  highp vec4 tmpvar_25;
  tmpvar_25.w = 1.0;
  tmpvar_25.xyz = (tmpvar_7 - (2.0 * (dot (tmpvar_24, tmpvar_7) * tmpvar_24)));
  lowp vec4 tmpvar_26;
  tmpvar_26 = textureCube (_Cube, tmpvar_25.xyz);
  TexCUBE0_17 = tmpvar_26;
  highp vec4 tmpvar_27;
  tmpvar_27 = vec4((1.0 - dot (normalize(xlv_TEXCOORD1), normalize(vec3(0.0, 0.0, 1.0)))));
  highp vec3 tmpvar_28;
  tmpvar_28 = mix (((Tex2D0_20 + (Tex2D2_19 * (TexCUBE0_17 * tmpvar_27))) * _MainColor), _DamageColor, vec4(mix (0.0, DamageMask_18.x, (_Damage * 10.0)))).xyz;
  tmpvar_15 = tmpvar_28;
  highp vec3 tmpvar_29;
  tmpvar_29 = mix (tmpvar_15, _DamageColor.xyz, vec3(mix (0.0, DamageMask_18.y, (_Damage * 5.0))));
  tmpvar_15 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = mix (tmpvar_15, _DamageColor.xyz, vec3(mix (0.0, DamageMask_18.z, _Damage)));
  tmpvar_15 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (tmpvar_15 + ((_RimColor * pow (tmpvar_27, vec4(2.0, 2.0, 2.0, 2.0))) * 3.0).xyz);
  tmpvar_15 = tmpvar_31;
  highp vec3 tmpvar_32;
  tmpvar_32 = _SpecularColor.xyz;
  tmpvar_16 = tmpvar_32;
  lowp vec4 tmpvar_33;
  tmpvar_33 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_6 = tmpvar_33;
  mediump vec4 tmpvar_34;
  tmpvar_34 = max (light_6, vec4(0.001, 0.001, 0.001, 0.001));
  light_6.w = tmpvar_34.w;
  highp float tmpvar_35;
  tmpvar_35 = ((sqrt(dot (xlv_TEXCOORD7, xlv_TEXCOORD7)) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_35;
  lowp vec3 tmpvar_36;
  tmpvar_36 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD6).xyz);
  lmFull_4 = tmpvar_36;
  lowp vec3 tmpvar_37;
  tmpvar_37 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD6).xyz);
  lmIndirect_3 = tmpvar_37;
  light_6.xyz = (tmpvar_34.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  mediump vec4 c_38;
  c_38.xyz = ((tmpvar_15 * light_6.xyz) + (light_6.xyz * (tmpvar_34.w * tmpvar_16)));
  c_38.w = 1.0;
  c_2.w = c_38.w;
  c_2.xyz = c_38.xyz;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_Color _glesColor
in vec4 _glesColor;
#define gl_Normal (normalize(_glesNormal))
in vec3 _glesNormal;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;
#define gl_MultiTexCoord1 _glesMultiTexCoord1
in vec4 _glesMultiTexCoord1;
#define TANGENT vec4(normalize(_glesTANGENT.xyz), _glesTANGENT.w)
in vec4 _glesTANGENT;
mat2 xll_transpose_mf2x2(mat2 m) {
  return mat2( m[0][0], m[1][0], m[0][1], m[1][1]);
}
mat3 xll_transpose_mf3x3(mat3 m) {
  return mat3( m[0][0], m[1][0], m[2][0],
               m[0][1], m[1][1], m[2][1],
               m[0][2], m[1][2], m[2][2]);
}
mat4 xll_transpose_mf4x4(mat4 m) {
  return mat4( m[0][0], m[1][0], m[2][0], m[3][0],
               m[0][1], m[1][1], m[2][1], m[3][1],
               m[0][2], m[1][2], m[2][2], m[3][2],
               m[0][3], m[1][3], m[2][3], m[3][3]);
}
vec2 xll_matrixindex_mf2x2_i (mat2 m, int i) { vec2 v; v.x=m[0][i]; v.y=m[1][i]; return v; }
vec3 xll_matrixindex_mf3x3_i (mat3 m, int i) { vec3 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; return v; }
vec4 xll_matrixindex_mf4x4_i (mat4 m, int i) { vec4 v; v.x=m[0][i]; v.y=m[1][i]; v.z=m[2][i]; v.w=m[3][i]; return v; }
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 lmapFadePos;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 476
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
#line 501
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
uniform lowp vec4 unity_Ambient;
#line 283
highp vec4 ComputeScreenPos( in highp vec4 pos ) {
    #line 285
    highp vec4 o = (pos * 0.5);
    o.xy = (vec2( o.x, (o.y * _ProjectionParams.x)) + o.w);
    o.zw = pos.zw;
    return o;
}
#line 90
highp vec3 ObjSpaceViewDir( in highp vec4 v ) {
    highp vec3 objSpaceCameraPos = ((_World2Object * vec4( _WorldSpaceCameraPos.xyz, 1.0)).xyz * unity_Scale.w);
    return (objSpaceCameraPos - v.xyz);
}
#line 479
v2f_surf vert_surf( in appdata_full v ) {
    #line 481
    v2f_surf o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.pack0.xy = ((v.texcoord.xy * _Diffuse_ST.xy) + _Diffuse_ST.zw);
    o.pack0.zw = ((v.texcoord.xy * _Mask_ST.xy) + _Mask_ST.zw);
    #line 485
    highp vec3 viewDir = (-ObjSpaceViewDir( v.vertex));
    highp vec3 worldRefl = (mat3( _Object2World) * viewDir);
    highp vec3 binormal = (cross( v.normal, v.tangent.xyz) * v.tangent.w);
    highp mat3 rotation = xll_transpose_mf3x3(mat3( v.tangent.xyz, binormal, v.normal));
    #line 489
    o.TtoW0 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 0).xyz), worldRefl.x) * unity_Scale.w);
    o.TtoW1 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 1).xyz), worldRefl.y) * unity_Scale.w);
    o.TtoW2 = (vec4( (rotation * xll_matrixindex_mf4x4_i (_Object2World, 2).xyz), worldRefl.z) * unity_Scale.w);
    o.screen = ComputeScreenPos( o.pos);
    #line 493
    o.lmap.xy = ((v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
    o.lmapFadePos.xyz = (((_Object2World * v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
    o.lmapFadePos.w = ((-(glstate_matrix_modelview0 * v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
    o.viewDir = (rotation * ObjSpaceViewDir( v.vertex));
    #line 497
    return o;
}

out highp vec4 xlv_TEXCOORD0;
out highp vec3 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
out lowp vec4 xlv_TEXCOORD3;
out lowp vec4 xlv_TEXCOORD4;
out lowp vec4 xlv_TEXCOORD5;
out highp vec2 xlv_TEXCOORD6;
out highp vec4 xlv_TEXCOORD7;
void main() {
    v2f_surf xl_retval;
    appdata_full xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.tangent = vec4(TANGENT);
    xlt_v.normal = vec3(gl_Normal);
    xlt_v.texcoord = vec4(gl_MultiTexCoord0);
    xlt_v.texcoord1 = vec4(gl_MultiTexCoord1);
    xlt_v.color = vec4(gl_Color);
    xl_retval = vert_surf( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec4(xl_retval.pack0);
    xlv_TEXCOORD1 = vec3(xl_retval.viewDir);
    xlv_TEXCOORD2 = vec4(xl_retval.screen);
    xlv_TEXCOORD3 = vec4(xl_retval.TtoW0);
    xlv_TEXCOORD4 = vec4(xl_retval.TtoW1);
    xlv_TEXCOORD5 = vec4(xl_retval.TtoW2);
    xlv_TEXCOORD6 = vec2(xl_retval.lmap);
    xlv_TEXCOORD7 = vec4(xl_retval.lmapFadePos);
}


#endif
#ifdef FRAGMENT

#define gl_FragData _glesFragData
layout(location = 0) out mediump vec4 _glesFragData[4];
float xll_saturate_f( float x) {
  return clamp( x, 0.0, 1.0);
}
vec2 xll_saturate_vf2( vec2 x) {
  return clamp( x, 0.0, 1.0);
}
vec3 xll_saturate_vf3( vec3 x) {
  return clamp( x, 0.0, 1.0);
}
vec4 xll_saturate_vf4( vec4 x) {
  return clamp( x, 0.0, 1.0);
}
mat2 xll_saturate_mf2x2(mat2 m) {
  return mat2( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0));
}
mat3 xll_saturate_mf3x3(mat3 m) {
  return mat3( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0), clamp(m[2], 0.0, 1.0));
}
mat4 xll_saturate_mf4x4(mat4 m) {
  return mat4( clamp(m[0], 0.0, 1.0), clamp(m[1], 0.0, 1.0), clamp(m[2], 0.0, 1.0), clamp(m[3], 0.0, 1.0));
}
#line 150
struct v2f_vertex_lit {
    highp vec2 uv;
    lowp vec4 diff;
    lowp vec4 spec;
};
#line 186
struct v2f_img {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 180
struct appdata_img {
    highp vec4 vertex;
    mediump vec2 texcoord;
};
#line 306
struct SurfaceOutput {
    lowp vec3 Albedo;
    lowp vec3 Normal;
    lowp vec3 Emission;
    mediump float Specular;
    lowp float Gloss;
    lowp float Alpha;
};
#line 392
struct EditorSurfaceOutput {
    mediump vec3 Albedo;
    mediump vec3 Normal;
    mediump vec3 Emission;
    mediump vec3 Gloss;
    mediump float Specular;
    mediump float Alpha;
    mediump vec4 Custom;
};
#line 423
struct Input {
    highp vec2 uv_Diffuse;
    highp vec2 uv_Mask;
    highp vec3 worldRefl;
    highp vec3 viewDir;
    mediump vec3 TtoW0;
    mediump vec3 TtoW1;
    mediump vec3 TtoW2;
};
#line 463
struct v2f_surf {
    highp vec4 pos;
    highp vec4 pack0;
    highp vec3 viewDir;
    highp vec4 screen;
    lowp vec4 TtoW0;
    lowp vec4 TtoW1;
    lowp vec4 TtoW2;
    highp vec2 lmap;
    highp vec4 lmapFadePos;
};
#line 66
struct appdata_full {
    highp vec4 vertex;
    highp vec4 tangent;
    highp vec3 normal;
    highp vec4 texcoord;
    highp vec4 texcoord1;
    lowp vec4 color;
};
uniform highp vec4 _Time;
uniform highp vec4 _SinTime;
#line 3
uniform highp vec4 _CosTime;
uniform highp vec4 unity_DeltaTime;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
#line 7
uniform highp vec4 _ScreenParams;
uniform highp vec4 _ZBufferParams;
uniform highp vec4 unity_CameraWorldClipPlanes[6];
uniform highp vec4 _WorldSpaceLightPos0;
#line 11
uniform highp vec4 _LightPositionRange;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
#line 15
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[4];
uniform highp vec4 unity_LightPosition[4];
uniform highp vec4 unity_LightAtten[4];
#line 19
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
#line 23
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp vec3 unity_LightColor0;
uniform highp vec3 unity_LightColor1;
uniform highp vec3 unity_LightColor2;
uniform highp vec3 unity_LightColor3;
#line 27
uniform highp vec4 unity_ShadowSplitSpheres[4];
uniform highp vec4 unity_ShadowSplitSqRadii;
uniform highp vec4 unity_LightShadowBias;
uniform highp vec4 _LightSplitsNear;
#line 31
uniform highp vec4 _LightSplitsFar;
uniform highp mat4 unity_World2Shadow[4];
uniform highp vec4 _LightShadowData;
uniform highp vec4 unity_ShadowFadeCenterAndType;
#line 35
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 glstate_matrix_invtrans_modelview0;
uniform highp mat4 _Object2World;
#line 39
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp mat4 glstate_matrix_transpose_modelview0;
uniform highp mat4 glstate_matrix_texture0;
#line 43
uniform highp mat4 glstate_matrix_texture1;
uniform highp mat4 glstate_matrix_texture2;
uniform highp mat4 glstate_matrix_texture3;
uniform highp mat4 glstate_matrix_projection;
#line 47
uniform highp vec4 glstate_lightmodel_ambient;
uniform highp mat4 unity_MatrixV;
uniform highp mat4 unity_MatrixVP;
uniform lowp vec4 unity_ColorSpaceGrey;
#line 76
#line 81
#line 86
#line 90
#line 95
#line 119
#line 136
#line 157
#line 165
#line 192
#line 205
#line 214
#line 219
#line 228
#line 233
#line 242
#line 259
#line 264
#line 290
#line 298
#line 302
#line 316
uniform lowp vec4 _LightColor0;
uniform lowp vec4 _SpecColor;
#line 329
#line 337
#line 351
uniform highp vec4 _MainColor;
uniform sampler2D _Diffuse;
#line 384
uniform highp float _Gloss;
uniform highp vec4 _SpecularColor;
uniform highp vec4 _DamageColor;
uniform samplerCube _Cube;
#line 388
uniform sampler2D _Mask;
uniform sampler2D _DamageMask;
uniform highp float _Damage;
uniform highp vec4 _RimColor;
#line 403
#line 411
#line 434
#line 476
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _Diffuse_ST;
uniform highp vec4 _Mask_ST;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
#line 501
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
uniform lowp vec4 unity_Ambient;
#line 176
lowp vec3 DecodeLightmap( in lowp vec4 color ) {
    #line 178
    return (2.0 * color.xyz);
}
#line 403
mediump vec4 LightingBlinnPhongEditor_PrePass( in EditorSurfaceOutput s, in mediump vec4 light ) {
    mediump vec3 spec = (light.w * s.Gloss);
    mediump vec4 c;
    #line 407
    c.xyz = ((s.Albedo * light.xyz) + (light.xyz * spec));
    c.w = s.Alpha;
    return c;
}
#line 434
void surf( in Input IN, inout EditorSurfaceOutput o ) {
    o.Normal = vec3( 0.0, 0.0, 1.0);
    o.Alpha = 1.0;
    #line 438
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Gloss = vec3( 0.0);
    o.Specular = 0.0;
    #line 442
    o.Custom = vec4( 0.0);
    highp vec4 Tex2D0 = texture( _Diffuse, IN.uv_Diffuse.xyxy.xy);
    highp vec4 Tex2D2 = texture( _Mask, IN.uv_Mask.xyxy.xy);
    highp vec4 DamageMask = texture( _DamageMask, IN.uv_Mask.xyxy.xy);
    #line 446
    highp vec4 WorldReflection0_0_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    highp vec4 WorldReflection0 = vec4( reflect( IN.worldRefl, vec3( dot( IN.TtoW0, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW1, vec3( WorldReflection0_0_NoInput)), dot( IN.TtoW2, vec3( WorldReflection0_0_NoInput)))), 1.0);
    highp vec4 TexCUBE0 = texture( _Cube, vec3( WorldReflection0));
    highp vec4 Fresnel_1_NoInput = vec4( 0.0, 0.0, 1.0, 1.0);
    #line 450
    highp vec4 Fresnel = vec4( (1.0 - dot( normalize(vec4( IN.viewDir.x, IN.viewDir.y, IN.viewDir.z, 1.0).xyz), normalize(Fresnel_1_NoInput.xyz))));
    highp vec4 Multiply0 = (TexCUBE0 * Fresnel);
    highp vec4 Multiply2 = (Tex2D2 * Multiply0);
    highp vec4 Add0 = (Tex2D0 + Multiply2);
    #line 454
    highp vec4 Multiply1 = (Add0 * _MainColor);
    o.Albedo = vec3( mix( Multiply1, _DamageColor, vec4( mix( 0.0, DamageMask.x, (_Damage * 10.0)))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.y, (_Damage * 5.0))));
    o.Albedo = mix( o.Albedo, vec3( _DamageColor), vec3( mix( 0.0, DamageMask.z, _Damage)));
    #line 458
    o.Albedo = (o.Albedo + vec3( ((_RimColor * pow( Fresnel, vec4( 2.0))) * 3.0)));
    o.Specular = float( vec4( _Gloss));
    o.Gloss = vec3( _SpecularColor);
    o.Normal = normalize(o.Normal);
}
#line 504
lowp vec4 frag_surf( in v2f_surf IN ) {
    #line 506
    Input surfIN;
    surfIN.uv_Diffuse = IN.pack0.xy;
    surfIN.uv_Mask = IN.pack0.zw;
    surfIN.worldRefl = vec3( IN.TtoW0.w, IN.TtoW1.w, IN.TtoW2.w);
    #line 510
    surfIN.TtoW0 = IN.TtoW0.xyz;
    surfIN.TtoW1 = IN.TtoW1.xyz;
    surfIN.TtoW2 = IN.TtoW2.xyz;
    surfIN.viewDir = IN.viewDir;
    #line 514
    EditorSurfaceOutput o;
    o.Albedo = vec3( 0.0);
    o.Emission = vec3( 0.0);
    o.Specular = 0.0;
    #line 518
    o.Alpha = 0.0;
    surf( surfIN, o);
    mediump vec4 light = textureProj( _LightBuffer, IN.screen);
    light = max( light, vec4( 0.001));
    #line 522
    lowp vec4 lmtex = texture( unity_Lightmap, IN.lmap.xy);
    lowp vec4 lmtex2 = texture( unity_LightmapInd, IN.lmap.xy);
    mediump float lmFade = ((length(IN.lmapFadePos) * unity_LightmapFade.z) + unity_LightmapFade.w);
    mediump vec3 lmFull = DecodeLightmap( lmtex);
    #line 526
    mediump vec3 lmIndirect = DecodeLightmap( lmtex2);
    mediump vec3 lm = mix( lmIndirect, lmFull, vec3( xll_saturate_f(lmFade)));
    light.xyz += lm;
    mediump vec4 c = LightingBlinnPhongEditor_PrePass( o, light);
    #line 530
    c.xyz += o.Emission;
    return c;
}
in highp vec4 xlv_TEXCOORD0;
in highp vec3 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
in lowp vec4 xlv_TEXCOORD3;
in lowp vec4 xlv_TEXCOORD4;
in lowp vec4 xlv_TEXCOORD5;
in highp vec2 xlv_TEXCOORD6;
in highp vec4 xlv_TEXCOORD7;
void main() {
    lowp vec4 xl_retval;
    v2f_surf xlt_IN;
    xlt_IN.pos = vec4(0.0);
    xlt_IN.pack0 = vec4(xlv_TEXCOORD0);
    xlt_IN.viewDir = vec3(xlv_TEXCOORD1);
    xlt_IN.screen = vec4(xlv_TEXCOORD2);
    xlt_IN.TtoW0 = vec4(xlv_TEXCOORD3);
    xlt_IN.TtoW1 = vec4(xlv_TEXCOORD4);
    xlt_IN.TtoW2 = vec4(xlv_TEXCOORD5);
    xlt_IN.lmap = vec2(xlv_TEXCOORD6);
    xlt_IN.lmapFadePos = vec4(xlv_TEXCOORD7);
    xl_retval = frag_surf( xlt_IN);
    gl_FragData[0] = vec4(xl_retval);
}


#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3"
}
}
 }
}
Fallback "CarShaderModel 2.0"
}