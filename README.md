# small-project
平时创建的小项目，项目名称【samll-project】创建错误，不做修改

### MoreLanguage
 > 1. 这是一个ASP.NET Core Web多语言项目，主要展示项目的不同：
 > 2. 第一种：www.xxx.com/en/index; www.xxx.com/zh/index; ，这种事通过路由来处理的
 > 3. 第二种: www.xxx.com/index/en; www.xxx.com/index/zh ，这种可以用cookie保存，通过cookie来判断
 > 4. 第三种：www.en.xxx.com; www.zh.xxx.com,这种方案就是发布两个项目，属于比较简单的，有多少种语言就发布多少种，上面的两种发布的是同一个版本的
 > 5. 第一种，第三种一般用于两个项目差别比较大的情况，第二种一般用于项目只有内容不同，其他的都相同的情况
 > 6. 增加session和cookie的使用
 ![如图](https://github.com/jasonhua95/samll-project/blob/master/image/001.PNG)
 
### DistributedRedis
> 1. 这是一个ASP.NET Core Web项目，主要展示redis的分布式缓存
> 2. 项目发布两次到不同的文件夹，IIS上配置
> 3. 通过nginx来做负载均衡，实现一个网址调用不同的服务器
> 4. 缓存将可以替换session，完全不用session
![如图](https://github.com/jasonhua95/samll-project/blob/master/image/002.PNG)

### OcelotStudy
Ocelot是一个用.NET Core实现并且开源的API网关，它功能强大，包括了：路由、认证、鉴权、简单缓存、限流熔断、负载均衡器等。简单的来说Ocelot是一堆的asp.net core middleware组成的一个有顺序的管道。当它拿到请求之后会用一个request builder来构造一个HttpRequestMessage发到下游的真实服务器，等下游的服务返回response之后再由一个middleware将它返回的HttpResponseMessage映射到HttpResponse上。
> 1. 这是一个ASP.NET Core Web项目，主要展示Ocelot的使用
> 2. 实现Ocelot来控制访问
> 3. 实现Ocelot来控制负载均衡
![如图](https://github.com/jasonhua95/samll-project/blob/master/image/003.PNG)