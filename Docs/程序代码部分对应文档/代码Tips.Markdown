# 一些代码小知识

* Flags去制作一些筛选 10110101 1意义为含有 0为不含有

```
[Flags]
public enum PrintBy : int
{
    SunShuchao = 1 << 1,
    
    unknown = 1 << 31,
}
```

这种方法需要注意越界！int 31为止  想扩充还是用long

* [System.Diagnostics.Conditional("PRINT_SYSTEM_DEBUG")]在打包时去除一些方法