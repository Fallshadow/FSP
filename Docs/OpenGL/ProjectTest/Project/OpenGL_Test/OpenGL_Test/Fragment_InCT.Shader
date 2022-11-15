#version 330 core
out vec4 FragColor;

in vec3 VertexOutColor;
in vec2 VertexOutTexture;

uniform sampler2D UniformTexture2D;

void main()
{
	FragColor = texture(UniformTexture2D, VertexOutTexture);
}