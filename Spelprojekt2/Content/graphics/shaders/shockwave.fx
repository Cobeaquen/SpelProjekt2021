#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

float time;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float2 UV = input.TextureCoordinates;
	float2 center = float2(0.5f, 0.5f);
	float dist = distance(center, UV);

	if (dist < 0.5f && dist > time - 0.1f && dist < time) {
		return lerp(float4(1, 1, 1, 1), input.Color, 1.5f*time) * (0.5f - dist) * 10 * (0.1f - (time - dist));
	}

	return float4(0, 0, 0, 0);
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};