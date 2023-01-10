#version 330 core
out vec4 FragColor;

in vec3 VertexOutColor;
in vec2 VertexOutTexture;

uniform sampler2D UniformTexture2D_1;
uniform sampler2D UniformTexture2D_2;
uniform float UniformMix;

void main()
{
	FragColor = mix(texture(UniformTexture2D_1, VertexOutTexture * 2), texture(UniformTexture2D_2, VertexOutTexture * 2), UniformMix);
}