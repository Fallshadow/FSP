#version 330 core
out vec4 FragColor;
in vec3 VertexOutColor;
uniform vec4 UniformInColor;

void main()
{
	FragColor = UniformInColor + vec4(VertexOutColor, 1.0f);
}