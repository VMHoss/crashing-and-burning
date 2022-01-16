Shader "Hidden/MultipassHollywoodFlares" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
 _NonBlurredTex ("Base (RGB)", 2D) = "" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

varying mediump vec2 xlv_TEXCOORD0;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _NonBlurredTex;
uniform sampler2D _MainTex;
uniform mediump vec4 tintColor;
void main ()
{
  mediump vec4 colorNb_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_NonBlurredTex, xlv_TEXCOORD0);
  colorNb_1 = tmpvar_4;
  gl_FragData[0] = (((color_2 * tintColor) * 0.5) + ((colorNb_1 * normalize(tintColor)) * 0.5));
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 325
v2f vert( in appdata_img v ) {
    #line 327
    v2f o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.uv = v.texcoord.xy;
    return o;
}
out mediump vec2 xlv_TEXCOORD0;
void main() {
    v2f xl_retval;
    appdata_img xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.texcoord = vec2(gl_MultiTexCoord0);
    xl_retval = vert( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.uv);
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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 359
mediump vec4 fragPrepare( in v2f i ) {
    #line 361
    mediump vec4 color = texture( _MainTex, i.uv);
    mediump vec4 colorNb = texture( _NonBlurredTex, i.uv);
    return (((color * tintColor) * 0.5) + ((colorNb * normalize(tintColor)) * 0.5));
}
in mediump vec2 xlv_TEXCOORD0;
void main() {
    mediump vec4 xl_retval;
    v2f xlt_i;
    xlt_i.pos = vec4(0.0);
    xlt_i.uv = vec2(xlv_TEXCOORD0);
    xl_retval = fragPrepare( xlt_i);
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
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform mediump float stretchWidth;
uniform mediump vec4 offsets;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD0_1 = (_glesMultiTexCoord0.xy + ((stretchWidth * 2.0) * offsets.xy));
  xlv_TEXCOORD0_2 = (_glesMultiTexCoord0.xy - ((stretchWidth * 2.0) * offsets.xy));
  xlv_TEXCOORD0_3 = (_glesMultiTexCoord0.xy + ((stretchWidth * 4.0) * offsets.xy));
  xlv_TEXCOORD0_4 = (_glesMultiTexCoord0.xy - ((stretchWidth * 4.0) * offsets.xy));
  xlv_TEXCOORD0_5 = (_glesMultiTexCoord0.xy + ((stretchWidth * 6.0) * offsets.xy));
  xlv_TEXCOORD0_6 = (_glesMultiTexCoord0.xy - ((stretchWidth * 6.0) * offsets.xy));
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  mediump vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_1 = tmpvar_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0_1);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0_2);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0_3);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD0_4);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0_5);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0_6);
  mediump vec4 tmpvar_9;
  tmpvar_9 = max (max (max (max (max (max (color_1, tmpvar_3), tmpvar_4), tmpvar_5), tmpvar_6), tmpvar_7), tmpvar_8);
  color_1 = tmpvar_9;
  gl_FragData[0] = tmpvar_9;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 332
v2f_opts vertStretch( in appdata_img v ) {
    v2f_opts o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    #line 336
    mediump float b = stretchWidth;
    o.uv[0] = v.texcoord.xy;
    o.uv[1] = (v.texcoord.xy + ((b * 2.0) * offsets.xy));
    o.uv[2] = (v.texcoord.xy - ((b * 2.0) * offsets.xy));
    #line 340
    o.uv[3] = (v.texcoord.xy + ((b * 4.0) * offsets.xy));
    o.uv[4] = (v.texcoord.xy - ((b * 4.0) * offsets.xy));
    o.uv[5] = (v.texcoord.xy + ((b * 6.0) * offsets.xy));
    o.uv[6] = (v.texcoord.xy - ((b * 6.0) * offsets.xy));
    #line 344
    return o;
}
out mediump vec2 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD0_1;
out mediump vec2 xlv_TEXCOORD0_2;
out mediump vec2 xlv_TEXCOORD0_3;
out mediump vec2 xlv_TEXCOORD0_4;
out mediump vec2 xlv_TEXCOORD0_5;
out mediump vec2 xlv_TEXCOORD0_6;
void main() {
    v2f_opts xl_retval;
    appdata_img xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.texcoord = vec2(gl_MultiTexCoord0);
    xl_retval = vertStretch( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.uv[0]);
    xlv_TEXCOORD0_1 = vec2(xl_retval.uv[1]);
    xlv_TEXCOORD0_2 = vec2(xl_retval.uv[2]);
    xlv_TEXCOORD0_3 = vec2(xl_retval.uv[3]);
    xlv_TEXCOORD0_4 = vec2(xl_retval.uv[4]);
    xlv_TEXCOORD0_5 = vec2(xl_retval.uv[5]);
    xlv_TEXCOORD0_6 = vec2(xl_retval.uv[6]);
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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 376
mediump vec4 fragStretch( in v2f_opts i ) {
    #line 378
    mediump vec4 color = texture( _MainTex, i.uv[0]);
    color = max( color, texture( _MainTex, i.uv[1]));
    color = max( color, texture( _MainTex, i.uv[2]));
    color = max( color, texture( _MainTex, i.uv[3]));
    #line 382
    color = max( color, texture( _MainTex, i.uv[4]));
    color = max( color, texture( _MainTex, i.uv[5]));
    color = max( color, texture( _MainTex, i.uv[6]));
    return color;
}
in mediump vec2 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD0_1;
in mediump vec2 xlv_TEXCOORD0_2;
in mediump vec2 xlv_TEXCOORD0_3;
in mediump vec2 xlv_TEXCOORD0_4;
in mediump vec2 xlv_TEXCOORD0_5;
in mediump vec2 xlv_TEXCOORD0_6;
void main() {
    mediump vec4 xl_retval;
    v2f_opts xlt_i;
    xlt_i.pos = vec4(0.0);
    xlt_i.uv[0] = vec2(xlv_TEXCOORD0);
    xlt_i.uv[1] = vec2(xlv_TEXCOORD0_1);
    xlt_i.uv[2] = vec2(xlv_TEXCOORD0_2);
    xlt_i.uv[3] = vec2(xlv_TEXCOORD0_3);
    xlt_i.uv[4] = vec2(xlv_TEXCOORD0_4);
    xlt_i.uv[5] = vec2(xlv_TEXCOORD0_5);
    xlt_i.uv[6] = vec2(xlv_TEXCOORD0_6);
    xl_retval = fragStretch( xlt_i);
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
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform mediump vec4 _MainTex_TexelSize;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD0_1 = (_glesMultiTexCoord0.xy + (vec2(0.0, 0.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_2 = (_glesMultiTexCoord0.xy - (vec2(0.0, 0.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_3 = (_glesMultiTexCoord0.xy + (vec2(0.0, 1.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_4 = (_glesMultiTexCoord0.xy - (vec2(0.0, 1.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_5 = (_glesMultiTexCoord0.xy + (vec2(0.0, 2.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_6 = (_glesMultiTexCoord0.xy - (vec2(0.0, 2.5) * _MainTex_TexelSize.xy));
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
uniform mediump vec2 _Threshhold;
uniform mediump vec4 tintColor;
void main ()
{
  mediump vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_1 = tmpvar_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0_1);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0_2);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0_3);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD0_4);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0_5);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0_6);
  mediump vec4 tmpvar_9;
  tmpvar_9 = ((((((color_1 + tmpvar_3) + tmpvar_4) + tmpvar_5) + tmpvar_6) + tmpvar_7) + tmpvar_8);
  color_1 = tmpvar_9;
  gl_FragData[0] = ((max (((tmpvar_9 / 7.0) - _Threshhold.x), vec4(0.0, 0.0, 0.0, 0.0)) * _Threshhold.y) * tintColor);
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 346
v2f_opts vertVerticalCoords( in appdata_img v ) {
    #line 348
    v2f_opts o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.uv[0] = v.texcoord.xy;
    o.uv[1] = (v.texcoord.xy + ((0.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    #line 352
    o.uv[2] = (v.texcoord.xy - ((0.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[3] = (v.texcoord.xy + ((1.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[4] = (v.texcoord.xy - ((1.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[5] = (v.texcoord.xy + ((2.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    #line 356
    o.uv[6] = (v.texcoord.xy - ((2.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    return o;
}
out mediump vec2 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD0_1;
out mediump vec2 xlv_TEXCOORD0_2;
out mediump vec2 xlv_TEXCOORD0_3;
out mediump vec2 xlv_TEXCOORD0_4;
out mediump vec2 xlv_TEXCOORD0_5;
out mediump vec2 xlv_TEXCOORD0_6;
void main() {
    v2f_opts xl_retval;
    appdata_img xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.texcoord = vec2(gl_MultiTexCoord0);
    xl_retval = vertVerticalCoords( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.uv[0]);
    xlv_TEXCOORD0_1 = vec2(xl_retval.uv[1]);
    xlv_TEXCOORD0_2 = vec2(xl_retval.uv[2]);
    xlv_TEXCOORD0_3 = vec2(xl_retval.uv[3]);
    xlv_TEXCOORD0_4 = vec2(xl_retval.uv[4]);
    xlv_TEXCOORD0_5 = vec2(xl_retval.uv[5]);
    xlv_TEXCOORD0_6 = vec2(xl_retval.uv[6]);
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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 365
mediump vec4 fragPreAndCut( in v2f_opts i ) {
    mediump vec4 color = texture( _MainTex, i.uv[0]);
    color += texture( _MainTex, i.uv[1]);
    #line 369
    color += texture( _MainTex, i.uv[2]);
    color += texture( _MainTex, i.uv[3]);
    color += texture( _MainTex, i.uv[4]);
    color += texture( _MainTex, i.uv[5]);
    #line 373
    color += texture( _MainTex, i.uv[6]);
    return ((max( ((color / 7.0) - _Threshhold.x), vec4( 0.0)) * _Threshhold.y) * tintColor);
}
in mediump vec2 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD0_1;
in mediump vec2 xlv_TEXCOORD0_2;
in mediump vec2 xlv_TEXCOORD0_3;
in mediump vec2 xlv_TEXCOORD0_4;
in mediump vec2 xlv_TEXCOORD0_5;
in mediump vec2 xlv_TEXCOORD0_6;
void main() {
    mediump vec4 xl_retval;
    v2f_opts xlt_i;
    xlt_i.pos = vec4(0.0);
    xlt_i.uv[0] = vec2(xlv_TEXCOORD0);
    xlt_i.uv[1] = vec2(xlv_TEXCOORD0_1);
    xlt_i.uv[2] = vec2(xlv_TEXCOORD0_2);
    xlt_i.uv[3] = vec2(xlv_TEXCOORD0_3);
    xlt_i.uv[4] = vec2(xlv_TEXCOORD0_4);
    xlt_i.uv[5] = vec2(xlv_TEXCOORD0_5);
    xlt_i.uv[6] = vec2(xlv_TEXCOORD0_6);
    xl_retval = fragPreAndCut( xlt_i);
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
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform mediump vec4 _MainTex_TexelSize;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD0_1 = (_glesMultiTexCoord0.xy + (vec2(0.0, 0.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_2 = (_glesMultiTexCoord0.xy - (vec2(0.0, 0.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_3 = (_glesMultiTexCoord0.xy + (vec2(0.0, 1.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_4 = (_glesMultiTexCoord0.xy - (vec2(0.0, 1.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_5 = (_glesMultiTexCoord0.xy + (vec2(0.0, 2.5) * _MainTex_TexelSize.xy));
  xlv_TEXCOORD0_6 = (_glesMultiTexCoord0.xy - (vec2(0.0, 2.5) * _MainTex_TexelSize.xy));
}



#endif
#ifdef FRAGMENT

varying mediump vec2 xlv_TEXCOORD0_6;
varying mediump vec2 xlv_TEXCOORD0_5;
varying mediump vec2 xlv_TEXCOORD0_4;
varying mediump vec2 xlv_TEXCOORD0_3;
varying mediump vec2 xlv_TEXCOORD0_2;
varying mediump vec2 xlv_TEXCOORD0_1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  mediump vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_1 = tmpvar_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0_1);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0_2);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0_3);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD0_4);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0_5);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0_6);
  mediump vec4 tmpvar_9;
  tmpvar_9 = ((((((color_1 + tmpvar_3) + tmpvar_4) + tmpvar_5) + tmpvar_6) + tmpvar_7) + tmpvar_8);
  color_1 = tmpvar_9;
  lowp vec3 c_10;
  c_10 = tmpvar_9.xyz;
  lowp float tmpvar_11;
  tmpvar_11 = dot (c_10, vec3(0.22, 0.707, 0.071));
  gl_FragData[0] = (tmpvar_9 / (7.5 + tmpvar_11));
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX

#define gl_Vertex _glesVertex
in vec4 _glesVertex;
#define gl_MultiTexCoord0 _glesMultiTexCoord0
in vec4 _glesMultiTexCoord0;

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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 346
v2f_opts vertVerticalCoords( in appdata_img v ) {
    #line 348
    v2f_opts o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.uv[0] = v.texcoord.xy;
    o.uv[1] = (v.texcoord.xy + ((0.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    #line 352
    o.uv[2] = (v.texcoord.xy - ((0.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[3] = (v.texcoord.xy + ((1.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[4] = (v.texcoord.xy - ((1.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    o.uv[5] = (v.texcoord.xy + ((2.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    #line 356
    o.uv[6] = (v.texcoord.xy - ((2.5 * _MainTex_TexelSize.xy) * vec2( 0.0, 1.0)));
    return o;
}
out mediump vec2 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD0_1;
out mediump vec2 xlv_TEXCOORD0_2;
out mediump vec2 xlv_TEXCOORD0_3;
out mediump vec2 xlv_TEXCOORD0_4;
out mediump vec2 xlv_TEXCOORD0_5;
out mediump vec2 xlv_TEXCOORD0_6;
void main() {
    v2f_opts xl_retval;
    appdata_img xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.texcoord = vec2(gl_MultiTexCoord0);
    xl_retval = vertVerticalCoords( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.uv[0]);
    xlv_TEXCOORD0_1 = vec2(xl_retval.uv[1]);
    xlv_TEXCOORD0_2 = vec2(xl_retval.uv[2]);
    xlv_TEXCOORD0_3 = vec2(xl_retval.uv[3]);
    xlv_TEXCOORD0_4 = vec2(xl_retval.uv[4]);
    xlv_TEXCOORD0_5 = vec2(xl_retval.uv[5]);
    xlv_TEXCOORD0_6 = vec2(xl_retval.uv[6]);
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
struct v2f {
    highp vec4 pos;
    mediump vec2 uv;
};
#line 312
struct v2f_opts {
    highp vec4 pos;
    mediump vec2 uv[7];
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
#line 318
uniform mediump vec4 offsets;
uniform mediump vec4 tintColor;
uniform mediump float stretchWidth;
uniform mediump vec2 _Threshhold;
#line 322
uniform mediump vec4 _MainTex_TexelSize;
uniform sampler2D _MainTex;
uniform sampler2D _NonBlurredTex;
#line 332
#line 365
#line 387
#line 172
lowp float Luminance( in lowp vec3 c ) {
    #line 174
    return dot( c, vec3( 0.22, 0.707, 0.071));
}
#line 387
mediump vec4 fragPost( in v2f_opts i ) {
    mediump vec4 color = texture( _MainTex, i.uv[0]);
    color += texture( _MainTex, i.uv[1]);
    #line 391
    color += texture( _MainTex, i.uv[2]);
    color += texture( _MainTex, i.uv[3]);
    color += texture( _MainTex, i.uv[4]);
    color += texture( _MainTex, i.uv[5]);
    #line 395
    color += texture( _MainTex, i.uv[6]);
    return ((color * 1.0) / ((7.0 + Luminance( color.xyz)) + 0.5));
}
in mediump vec2 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD0_1;
in mediump vec2 xlv_TEXCOORD0_2;
in mediump vec2 xlv_TEXCOORD0_3;
in mediump vec2 xlv_TEXCOORD0_4;
in mediump vec2 xlv_TEXCOORD0_5;
in mediump vec2 xlv_TEXCOORD0_6;
void main() {
    mediump vec4 xl_retval;
    v2f_opts xlt_i;
    xlt_i.pos = vec4(0.0);
    xlt_i.uv[0] = vec2(xlv_TEXCOORD0);
    xlt_i.uv[1] = vec2(xlv_TEXCOORD0_1);
    xlt_i.uv[2] = vec2(xlv_TEXCOORD0_2);
    xlt_i.uv[3] = vec2(xlv_TEXCOORD0_3);
    xlt_i.uv[4] = vec2(xlv_TEXCOORD0_4);
    xlt_i.uv[5] = vec2(xlv_TEXCOORD0_5);
    xlt_i.uv[6] = vec2(xlv_TEXCOORD0_6);
    xl_retval = fragPost( xlt_i);
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
}
Fallback Off
}