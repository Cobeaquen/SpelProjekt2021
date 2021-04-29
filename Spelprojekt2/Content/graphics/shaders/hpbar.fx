#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

int frameWidth;
int frameHeight;
float value;
Texture2D SpriteTexture;

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

	int2 realUV = int2(round(UV.x * frameWidth), round(UV.y * frameHeight));

	float4 texColor = tex2D(SpriteTextureSampler, input.TextureCoordinates) * input.Color;
	float4 barColor = UV.x <= value ? lerp(float4(1, 0, 0, 1), float4(0, 1, 0, 1), value) : float4(0, 0, 0, 0);
	if (texColor.r == 0.0 && texColor.g == 0.0 && texColor.b == 0.0 && texColor.a == 1) {
		return float4(0, 0, 0, 0);
	}
	else if (texColor.a == 1.0) {
		return texColor;
	}
	else if (texColor.a < 1.0 && texColor.a > 0.0) {
		texColor.a = 1.0;
		return texColor * barColor;
	}
	return barColor;
	//return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};