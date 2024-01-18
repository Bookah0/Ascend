Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" { }
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline width", Range(0.002, 0.03)) = 0.005
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    ENDCG

    SubShader
    {
        Tags
        {
            "Queue" = "Overlay"
        }

        Pass
        {
            Name "OUTLINE"
            Tags
            {
                "LightMode" = "Always"
            }

            ColorMask RGB
            Cull Front

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest LEqual
            Fog { Mode Off }
            Offset 5,5

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex]
            {
                combine primary
            }
        }

        Pass
        {
            Name "OUTLINEREMOVE"
            Tags
            {
                "LightMode" = "Always"
            }

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest LEqual
            Fog { Mode Off }
            Offset 5,5

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma exclude_renderers gles xbox360 ps3
            ENDCG

            SetTexture[_MainTex]
            {
                combine add
            }
        }
    }
}