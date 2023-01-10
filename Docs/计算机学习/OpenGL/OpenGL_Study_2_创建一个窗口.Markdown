## 构建环境
想要自己动手利用opengl实现一些效果，我们需要创建一个opengl上下文和一个用于显示的窗口。这些操作在每个系统上都是不同的，opengl也不便于为所有系统适配，所以我们要自己去处理这两项。好在有一些库针对opengl提供了以上功能，节省我们的时间。这里我们使用GLFW。
>GLFW是一个专门针对OpenGL的C语言库，它提供了一些渲染物体所需的最低限度的接口。它允许用户创建OpenGL上下文、定义窗口参数以及处理用户输入，对我们来说这就够了。

>GLFW是继GLUT，FreeGLUT之后，当前最新的用来创建OpenGL上下文，以及操作窗口的第三方库。官方网址为：https://www.glfw.org/documentation.html

1：下载[GLFW源代码包](https://www.glfw.org/download.html)。这个代码包包含很多内容，但最终我们只需要编译生成的库和include文件夹。那要将源代码编译，就要将源代码导入工程，但是不同IDE不同构建系统导入工程的设置都是不同的，这就需要我们借助cmake去自动配置生成我们心仪的工程文件。  
2：下载[CMake](https://cmake.org/download/)并生成工程。  
3：编译工程。直接vs打开后生成解决方案就会在build/src/Debug里出现编译出的库文件glfw3.lib。如何让我们的测试工程可以用到这个库呢？  
推荐的方式是建立一个新的目录包含所有的第三方库文件和头文件，并且在你的IDE或编译器中指定这些文件夹。我个人会使用一个单独的文件夹，里面包含Libs和Include文件夹，在这里存放OpenGL工程用到的所有第三方库和头文件。这样我的所有第三方库都在同一个位置（并且可以共享至多台电脑）。然而这要求你每次新建一个工程时都需要告诉IDE/编译器在哪能找到这些目录。  
4：创建工程  
5：GLAD（这部分自己看教程吧）（不知为何4.6似乎更稳定）  
>由于OpenGL驱动版本众多，它大多数函数的位置都无法在编译时确定下来，需要在运行时查询。所以任务就落在了开发者身上，开发者需要在运行时获取函数地址并将其保存在一个函数指针中供以后使用。取得地址的方法因平台而异,代码非常复杂，而且很繁琐，我们需要对每个可能使用的函数都要重复这个过程。幸运的是，有些库能简化此过程，其中GLAD是目前最新，也是最流行的库。GLAD是用来管理OpenGL的函数指针的，所以在调用任何OpenGL的函数之前我们需要初始化GLAD。GLAD也可以使OpenGL基础渲染变得简单。  
简单点说，GLFW用来创建窗口给opengl的上下文(context)绘图，glad用来调用上下文中的函数

6：检查一下opengl版本：[工具下载](https://download.cnet.com/OpenGL-Extensions-Viewer/3000-18487_4-34442.html)
## 第一个用glfw创建的窗口
1：初始化glfw  
2：设置参数：Hint参数[网址](https://www.glfw.org/docs/latest/window.html#window_hints)  
3：创建窗口  
4：设置glad  
5：开始调用函数  
>OpenGL幕后使用glViewport中定义的位置和宽高进行2D坐标的转换，将OpenGL中的位置坐标转换为你的屏幕坐标。例如，OpenGL中的坐标(-0.5, 0.5)有可能（最终）被映射为屏幕中的坐标(200,450)。注意，处理过的OpenGL坐标范围只为-1到1，因此我们事实上将(-1到1)范围内的坐标映射到(0, 800)和(0, 600)。  

具体函数学习请到test项目中去。

## 一些知识点

>双缓冲(Double Buffer)：应用程序使用单缓冲绘图时可能会存在图像闪烁的问题。 这是因为生成的图像不是一下子被绘制出来的，而是按照从左到右，由上而下逐像素地绘制而成的。最终图像不是在瞬间显示给用户，而是通过一步一步生成的，这会导致渲染的结果很不真实。为了规避这些问题，我们应用双缓冲渲染窗口应用程序。前缓冲保存着最终输出的图像，它会在屏幕上显示；而所有的的渲染指令都会在后缓冲上绘制。当所有的渲染指令执行完毕后，我们交换(Swap)前缓冲和后缓冲，这样图像就立即呈显出来，之前提到的不真实感就消除了。

```C++
#include <glad/glad.h>
#include <GLFW/glfw3.h>
#include <iostream>

void processInput(GLFWwindow* window);

int main() 
{
	// 初始化
	glfwInit();
	// glfwWindowHint设置一些参数
	// 将主版本号(Major)和次版本号(Minor)都设为3
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	// 明确告知使用核心模式
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	GLFWwindow* window = glfwCreateWindow(800, 600, "LearnOpenGL", NULL, NULL);
	if (window == NULL) {
		std::cout << "Failed to create GLFW window" << std::endl;
		glfwTerminate();
		return -1;
	}
	glfwMakeContextCurrent(window);

	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress)) {
		std::cout << "Failed to initialize GLAD" << std::endl;
		return -1;
	}

	glViewport(0, 0, 800, 600);

	while (!glfwWindowShouldClose(window))
	{
		processInput(window);

		// 在每个新的渲染迭代开始的时候我们总是希望清屏，否则我们仍能看见上一次迭代的渲染结果（这可能是你想要的效果，但通常这不是）。
		// 我们可以通过调用glClear函数来清空屏幕的颜色缓冲，它接受一个缓冲位(Buffer Bit)来指定要清空的缓冲
		// 可能的缓冲位有GL_COLOR_BUFFER_BIT，GL_DEPTH_BUFFER_BIT和GL_STENCIL_BUFFER_BIT。由于现在我们只关心颜色值，所以我们只清空颜色缓冲。
		glClearColor(0.2f, 0.3f, 0.3f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		// 交换颜色缓冲（它是一个储存着GLFW窗口每一个像素颜色值的大缓冲），它在这一迭代中被用来绘制，并且将会作为输出显示在屏幕上。
		glfwSwapBuffers(window);
		// 检查有没有触发什么事件（比如键盘输入、鼠标移动等）、更新窗口状态，并调用对应的回调函数（可以通过回调方法手动设置）。
		glfwPollEvents();
	}


	// 正确释放/删除之前的分配的所有资源
	glfwTerminate();
	return 0;
}

void processInput(GLFWwindow *window)
{
	if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS)
		glfwSetWindowShouldClose(window, true);
}
```