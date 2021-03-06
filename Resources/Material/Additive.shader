
Shader "TYImage/Particles/Additive" {
	Properties{
		_MainTex("Particle Texture", 2D) = "white" {}
	}

	SubShader{
		 Tags { "Queue" = "Opaque + 10" "RenderType" = "Opaque + 10" }
		 Pass {
			  Tags { "Queue" = "Opaque + 10" "RenderType" = "Opaque + 10" }
			  BindChannels {
			   Bind "vertex", Vertex
			   Bind "color", Color
			   Bind "texcoord", TexCoord
		 }
		 ZWrite Off
		 Cull Off

		Fog{ Mode Off }
		Blend SrcAlpha One

		SetTexture[_MainTex]{ combine texture * primary }
		 /*SetTexture[_MainTex]{
			constantColor[_TintColor]
			combine constant * primary
		 }
		 SetTexture[_MainTex]{
			combine texture * previous DOUBLE
		 }*/
	   }
	}
}