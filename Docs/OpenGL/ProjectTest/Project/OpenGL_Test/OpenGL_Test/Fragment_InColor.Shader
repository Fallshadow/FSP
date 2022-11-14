#version 330 core
out vec4 FragColor;

in vec3 VertexOutColor;

void main()
{
	FragColor = vec4(VertexOutColor, 1.0f);
}