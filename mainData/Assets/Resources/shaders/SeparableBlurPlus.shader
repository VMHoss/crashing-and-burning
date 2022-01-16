Shader "Hidden/SeparableBlurPlus" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
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

varying mediump vec4 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform mediump vec4 offsets;
uniform highp mat4 glstate_matrix_mvp;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesVertex;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD1 = (_glesMultiTexCoord0.xyxy + (offsets.xyxy * vec4(1.0, 1.0, -1.0, -1.0)));
  xlv_TEXCOORD2 = (_glesMultiTexCoord0.xyxy + (vec4(2.0, 2.0, -2.0, -2.0) * offsets.xyxy));
  xlv_TEXCOORD3 = (_glesMultiTexCoord0.xyxy + (vec4(3.0, 3.0, -3.0, -3.0) * offsets.xyxy));
  xlv_TEXCOORD4 = (_glesMultiTexCoord0.xyxy + (vec4(6.5, 6.5, -6.5, -6.5) * offsets.xyxy));
}



#endif
#ifdef FRAGMENT

varying mediump vec4 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD0;
uniform sampler2D _MainTex;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD1.xy);
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD1.zw);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD2.xy);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD2.zw);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD3.xy);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD3.zw);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD4.xy);
  lowp vec4 tmpvar_9;
  tmpvar_9 = texture2D (_MainTex, xlv_TEXCOORD4.zw);
  gl_FragData[0] = (((((((((0.225 * tmpvar_1) + (0.15 * tmpvar_2)) + (0.15 * tmpvar_3)) + (0.11 * tmpvar_4)) + (0.11 * tmpvar_5)) + (0.075 * tmpvar_6)) + (0.075 * tmpvar_7)) + (0.0525 * tmpvar_8)) + (0.0525 * tmpvar_9));
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
    mediump vec4 uv01;
    mediump vec4 uv23;
    mediump vec4 uv45;
    mediump vec4 uv67;
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
uniform mediump vec4 offsets;
uniform sampler2D _MainTex;
#line 318
v2f vert( in appdata_img v ) {
    #line 320
    v2f o;
    o.pos = (glstate_matrix_mvp * v.vertex);
    o.uv.xy = v.texcoord.xy;
    o.uv01 = (v.texcoord.xyxy + (offsets.xyxy * vec4( 1.0, 1.0, -1.0, -1.0)));
    #line 324
    o.uv23 = (v.texcoord.xyxy + ((offsets.xyxy * vec4( 1.0, 1.0, -1.0, -1.0)) * 2.0));
    o.uv45 = (v.texcoord.xyxy + ((offsets.xyxy * vec4( 1.0, 1.0, -1.0, -1.0)) * 3.0));
    o.uv67 = (v.texcoord.xyxy + ((offsets.xyxy * vec4( 1.0, 1.0, -1.0, -1.0)) * 4.5));
    o.uv67 = (v.texcoord.xyxy + ((offsets.xyxy * vec4( 1.0, 1.0, -1.0, -1.0)) * 6.5));
    #line 328
    return o;
}
out mediump vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
out mediump vec4 xlv_TEXCOORD2;
out mediump vec4 xlv_TEXCOORD3;
out mediump vec4 xlv_TEXCOORD4;
void main() {
    v2f xl_retval;
    appdata_img xlt_v;
    xlt_v.vertex = vec4(gl_Vertex);
    xlt_v.texcoord = vec2(gl_MultiTexCoord0);
    xl_retval = vert( xlt_v);
    gl_Position = vec4(xl_retval.pos);
    xlv_TEXCOORD0 = vec2(xl_retval.uv);
    xlv_TEXCOORD1 = vec4(xl_retval.uv01);
    xlv_TEXCOORD2 = vec4(xl_retval.uv23);
    xlv_TEXCOORD3 = vec4(xl_retval.uv45);
    xlv_TEXCOORD4 = vec4(xl_retval.uv67);
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
    mediump vec4 uv01;
    mediump vec4 uv23;
    mediump vec4 uv45;
    mediump vec4 uv67;
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
uniform mediump vec4 offsets;
uniform sampler2D _MainTex;
#line 330
mediump vec4 frag( in v2f i ) {
    #line 332
    mediump vec4 color = vec4( 0.0, 0.0, 0.0, 0.0);
    color += (0.225 * texture( _MainTex, i.uv));
    color += (0.15 * texture( _MainTex, i.uv01.xy));
    color += (0.15 * texture( _MainTex, i.uv01.zw));
    #line 336
    color += (0.11 * texture( _MainTex, i.uv23.xy));
    color += (0.11 * texture( _MainTex, i.uv23.zw));
    color += (0.075 * texture( _MainTex, i.uv45.xy));
    color += (0.075 * texture( _MainTex, i.uv45.zw));
    #line 340
    color += (0.0525 * texture( _MainTex, i.uv67.xy));
    color += (0.0525 * texture( _MainTex, i.uv67.zw));
    return color;
}
in mediump vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
in mediump vec4 xlv_TEXCOORD2;
in mediump vec4 xlv_TEXCOORD3;
in mediump vec4 xlv_TEXCOORD4;
void main() {
    mediump vec4 xl_retval;
    v2f xlt_i;
    xlt_i.pos = vec4(0.0);
    xlt_i.uv = vec2(xlv_TEXCOORD0);
    xlt_i.uv01 = vec4(xlv_TEXCOORD1);
    xlt_i.uv23 = vec4(xlv_TEXCOORD2);
    xlt_i.uv45 = vec4(xlv_TEXCOORD3);
    xlt_i.uv67 = vec4(xlv_TEXCOORD4);
    xl_retval = frag( xlt_i);
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