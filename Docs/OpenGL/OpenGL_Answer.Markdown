## OpenGL
### 第一课
#### 1：OpenGL是什么？
是一种规范，由XXX团队指定的，不是API，只指定了表现和输出，具体实现由对应开发者决定。
#### 2：OpenGL的制作者？
#### 3：显卡与OpenGL的关系？显卡驱动更新一般意味着什么？
显卡厂商实现具体逻辑，驱动更新一般意味着新特性或者修复BUG。
#### 4：OpenGL两种模式？
立即渲染模式 更快速，隐藏细节 
核心模式    更现代化，要求编写人员了解渲染原理
#### 5：OpenGL一大特性？
扩展
#### 6：OpenGL本质是什么？编码风格是什么？什么语言编写的？
状态机，设置上下文，然后应用上下文，C语言

### 第二课
#### 1：如何在你的电脑上利用opengl？需要做哪些步骤？配置环境？
#### 2:GLFW?GLAD?
GLFW是一个免费的开源多平台库，用于OpenGL、OpenGL SE 和 Vulkan应用程序开发。它提供一个简单的、独立于平台的API，用于创建窗口、上下文和表面、读取输入、处理事件等。  
GLAD用于解决OpenGL函数调用的问题。OpenGL由于驱动版本过多，大多数函数都无法在编译时确定下来，开发者需要在运行时确定函数的位置，取得地址的方法因平台而已比较复杂，GLAD就是用来管理函数指针的。
#### 3：双缓冲？
单缓冲，绘图指令直接在窗口上执行，最终图像显示给用户的会是一个过程，按照从左到右、从上到下逐像素渲染而成。
双缓冲，绘图指令在后台生成下一帧图像，然后通过交换指令直接呈现给用户。


### 第三课
#### 1：翻译翻译图形渲染管线？渲染管线可以划分为的几个步骤？
将一系列3D坐标转换为2D像素的过程。  
从数据层面简单拆分为两步：3D坐标到2D坐标，2D坐标到2D像素。  
从OpenGL管线来说：输入顶点数据 到 顶点着色器 到 图元装配 到 几何着色器 到 光栅化 到 片段着色器 到 测试与混合
#### 2：3D坐标、2D坐标、2D像素？
#### 3：典型的着色器代码结构？
``` C++
#version version_number
in type in_type_name
out type out_type_name
uniform type uniform_type_name
void main()
{
    // 自定义操作变量
    // 最终输出
    out_type_name = xxxxxxx;
}
```
#### 4：顶点着色器的数据来源？
直接来源于顶点数据，使用layout(position = 0)之类的修饰去寻找指定属性解析
#### 5：顶点属性最大值由什么决定？
由硬件决定，我们可以使用这个函数查看最大值
``` C++
int nrAttributes;
glGetIntegerv(GL_MAX_VERTEX_ATTRIBS, &nrAttributes);
std::cout << "Maximum nr of vertex attributes supported: " << nrAttributes << std::endl;
```
一般我们会得到16 这个基本够用了
#### 6：GLSL的数据类型和容器？
vecn,bvecn,ivecn,uvecn,dvecn
Vector Matrix
#### 7：向量的特质？
重组：Swizzling。即向量允许这样的写法
```C++
vec2 myVec2;
vec3 myVec3;
vec4 myVec4 = myVec2.yy + myVec3.zx;
myVec4 = myVec3.zzzx;
myVec4 = myVec2.xxyy;
```
#### 8：着色器之间的信息传递？
需要同名同类型的In 和 Out相连。
#### 9：uniform是什么？怎么用？使用时需要注意什么？
uniform 是一个声明在Shader里的全局变量。所有Shader都共享这份名称的uniform。  
glGetUniformLocation从指定程序中获取到指定名称的uniform。
glUniformXT赋值给指定uniform（X为几位，T为什么类型，比如uniform vec4 Uniform_Color为4位float，即4f）
#### 10：怎么理解VAO,VBO,EBO?使用上的注意事项？
首先，VBO是顶点缓冲对象，它保存着我们传入的顶点数据，保存在显存上。一般来说这份数据是float数组，OpenGL并不知道该如何解析他们，VAO就是用来解释数据的，称为顶点数组对象。为了避免浪费空间，我们引入了EBO，元素缓冲对象（别名IBO 索引缓冲对象）EBO指定构成几何图元的顶点顺序。  
使用上，我们需要先生成以上三类，然后先绑定指定的VAO，然后再将对应的VBO，EBO绑定到VAO上。然后我们可以继续绑定其他VAO，再将对应的VBO，EBO绑定到VAO。  
最后，我们在渲染时，一样先选择心仪的VAO进行绑定，然后可以用VAO画也可以用EBO画，但是不可以用VBO画，VBO纯纯是数据。（追本溯源还是用VAO画的）
#### 11：标准化设备坐标？
#### 12：如何创建并使用一个着色器程序？
编译顶点着色器，编译片段着色器，链接它们到程序，然后释放它们，最后使用链接好的程序。

### 第四课
#### 1：纹理的好处？
#### 2：如何导入一个图像？
#### 3：什么叫做采样？
#### 4：纹理坐标的范围？超出范围会怎样？