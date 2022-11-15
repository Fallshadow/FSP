#define STB_IMAGE_IMPLEMENTATION
#include "Shader.h"
#include "stb_image.h"
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

void processInput(GLFWwindow* window);
GLFWwindow* initWindowEnvironment(float width, float height);

int main() 
{
	GLFWwindow* window = initWindowEnvironment(800, 600);
	if (window == NULL) return -1;

	float vertices[] =
	{
		0.5f, 0.5f, 0.0f,   // 右上角
		0.5f, -0.5f, 0.0f,  // 右下角
		-0.5f, -0.5f, 0.0f, // 左下角
		-0.5f, 0.5f, 0.0f   // 左上角
	};

	float vertices2[] =
	{
		0.0f, 0.0f, 0.0f,	1.0f, 0.0f, 0.0f,	0.0f, 0.0f,
		0.5f, 0.5f, 0.0f,	0.0f, 1.0f, 0.0f,	0.5f, 0.5f,
		0.0f, 1.0f, 0.0f,	0.0f, 0.0f, 1.0f,	1.0f, 0.0f,
		1.0f, 0.5f, 0.0f,	0.0f, 0.0f, 0.0f,	0.5f, 1.0f,
	};

	unsigned int indices[] = {
		// 注意索引从0开始! 
		// 此例的索引(0,1,2,3)就是顶点数组vertices的下标，
		// 这样可以由下标代表顶点组合成矩形

		0, 1, 3, // 第一个三角形
		1, 2, 3  // 第二个三角形
	};

	unsigned int VAOs[2], VBOs[2], EBOs[2];
	glGenVertexArrays(2, VAOs);
	glGenBuffers(2, VBOs);
	glGenBuffers(2, EBOs);

	glBindVertexArray(VAOs[0]);
	glBindBuffer(GL_ARRAY_BUFFER, VBOs[1]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices2), vertices2, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBOs[0]);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);


	unsigned int tex_test1;
	glGenTextures(1, &tex_test1);

	glBindTexture(GL_TEXTURE_2D, tex_test1);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	int weight, height, nrChannels;
	unsigned char* data = stbi_load("wall.jpg", &weight, &height, &nrChannels, 0);
	if(data)
	{
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, weight, height, 0, GL_RGB, GL_UNSIGNED_BYTE, data);
		glGenerateMipmap(GL_TEXTURE_2D);
	}
	else
	{
		std::cout << "Failed to load texture" << std::endl;
	}
	stbi_image_free(data);

	Shader MYShader("Vertex_0P1C2T_OutColorAndTexture.Shader", "Fragment_InCT.Shader");
	Shader MYShader_UniformColor("Vertex_0P1C2T_OutColorAndTexture.Shader", "Fragment_InCT_MixTime.Shader");
	Shader MYShader_UniformColor_NoT("Vertex_0P1C2T_OutColorAndTexture.Shader", "Fragment_UniformColor.Shader");

	while (!glfwWindowShouldClose(window))
	{
		processInput(window);

		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		// 激活程序
		MYShader.Use();
		glBindTexture(GL_TEXTURE_2D, tex_test1);
		glBindVertexArray(VAOs[0]);
		glDrawArrays(GL_TRIANGLES, 0, 3);

		float timeValue = glfwGetTime();
		float greenValue = (sin(timeValue) / 2.0f) + 0.5f;

		MYShader_UniformColor_NoT.Use();
		int vertexColorLocation_noT = glGetUniformLocation(MYShader_UniformColor_NoT.ID, "UniformInColor");
		glUniform4f(vertexColorLocation_noT, 1 - greenValue, greenValue, 0.5f - greenValue, 1.0f);
		glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);

		MYShader_UniformColor.Use();
		
		int vertexColorLocation = glGetUniformLocation(MYShader_UniformColor.ID, "UniformInColor");
		glUniform4f(vertexColorLocation, 1 - greenValue, greenValue, 0.5f - greenValue, 1.0f);
		glDrawArrays(GL_TRIANGLES, 1, 3);
		
		glfwSwapBuffers(window);
		glfwPollEvents();
	}

	MYShader.Delete();

	glfwTerminate();
	return 0;
}

void processInput(GLFWwindow *window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		glfwSetWindowShouldClose(window, true);
}

GLFWwindow* initWindowEnvironment(float width, float height)
{
	glfwInit();
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	GLFWwindow* window = glfwCreateWindow(800, 600, "LearnOpenGL", NULL, NULL);
	if (window == NULL) {
		std::cout << "Failed to create GLFW window" << std::endl;
		glfwTerminate();
		return NULL;
	}
	glfwMakeContextCurrent(window);

	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
		std::cout << "Failed to initialize GLAD" << std::endl;
		return NULL;
	}

	glViewport(0, 0, 800, 600);
	return window;
}