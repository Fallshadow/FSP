#define STB_IMAGE_IMPLEMENTATION
#include "Shader.h"
#include "stb_image.h"
#include "glm/glm.hpp"
#include "glm/gtc/matrix_transform.hpp"
#include "glm/gtc/type_ptr.hpp"
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

void processInput(GLFWwindow* window);
GLFWwindow* initWindowEnvironment(float width, float height);

const unsigned int screenWidth = 800;
const unsigned int screenHeight = 600;
int main() 
{
	GLFWwindow* window = initWindowEnvironment(800, 600);
	if (window == NULL) return -1;

	float vertices[] =
	{
		0.5f, 0.5f, 0.0f,     1.0f, 0.0f, 0.0f,	1.0f, 1.0f, // 右上角 0.55f, 0.55f,
		0.5f, -0.5f, 0.0f,    0.0f, 1.0f, 0.0f,	1.0f, 0.0f, // 右下角 0.55f, 0.45f,
		-0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,	0.0f, 0.0f, // 左下角 0.45f, 0.45f,
		-0.5f, 0.5f, 0.0f,    0.0f, 0.0f, 0.0f,	0.0f, 1.0f, // 左上角 0.45f, 0.55f 
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

	unsigned int indices2[] = {
		0, 1, 3, // 第一个三角形
		1, 2, 3  // 第二个三角形
	};

	unsigned int VAOs[2], VBOs[2], EBOs[2];
	glGenVertexArrays(2, VAOs);
	glGenBuffers(2, VBOs);
	glGenBuffers(2, EBOs);

	glBindVertexArray(VAOs[0]);
	glBindBuffer(GL_ARRAY_BUFFER, VBOs[0]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBOs[0]);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	glBindVertexArray(VAOs[1]);
	glBindBuffer(GL_ARRAY_BUFFER, VBOs[1]);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices2), vertices2, GL_STATIC_DRAW);
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
	glEnableVertexAttribArray(1);
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
	glEnableVertexAttribArray(2);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBOs[1]);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices2), indices2, GL_STATIC_DRAW);

	unsigned int tex_test[2];
	glGenTextures(2, tex_test);


	glBindTexture(GL_TEXTURE_2D, tex_test[0]);
	int weight, height, nrChannels;
	stbi_set_flip_vertically_on_load(true);
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

	glBindTexture(GL_TEXTURE_2D, tex_test[1]);
	stbi_set_flip_vertically_on_load(true);
	data = stbi_load("awesomeface.png", &weight, &height, &nrChannels, 0);
	if (data)
	{
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, weight, height, 0, GL_RGBA, GL_UNSIGNED_BYTE, data);
		glGenerateMipmap(GL_TEXTURE_2D);
	}
	else
	{
		std::cout << "Failed to load texture" << std::endl;
	}
	stbi_image_free(data);

	Shader MYShader_MixT("Vertex_0P1C2T_Transform.Shader", "Fragment_InCT.Shader");

	MYShader_MixT.Use();
	glUniform1i(glGetUniformLocation(MYShader_MixT.ID, "UniformTexture2D"), 0);
	MYShader_MixT.SetInt("UniformTexture2D", 0);
	
	
	while (!glfwWindowShouldClose(window))
	{
		processInput(window);
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		// 激活程序
		glActiveTexture(GL_TEXTURE0);
		glBindTexture(GL_TEXTURE_2D, tex_test[1]);

		glm::mat4 model = glm::mat4(1.0f);
		model = glm::rotate(model, -30.0f, glm::vec3(1.0f, 0.0f, 0.0f));

		glm::mat4 view;
		view = glm::translate(view, glm::vec3(0.0f, 0.0f, -3.0f));

		glm::mat4 projection;
		projection = glm::perspective(glm::radians(45.0f), (float)screenWidth / (float)screenHeight, 0.1f, 100.0f);

		MYShader_MixT.SetMat4("model", model);
		MYShader_MixT.SetMat4("view", view);
		MYShader_MixT.SetMat4("projection", projection);
		MYShader_MixT.Use();
		glBindVertexArray(VAOs[0]);
		glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);



		glfwSwapBuffers(window);
		glfwPollEvents();
	}

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

	GLFWwindow* window = glfwCreateWindow(screenWidth, screenHeight, "LearnOpenGL", NULL, NULL);
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