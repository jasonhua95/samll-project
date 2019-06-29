# small-project
平时创建的小项目，项目名称【samll-project】创建错误，不做修改

### Github上指定项目的下载
 > 1. 点击[下载](https://minhaskamal.github.io/DownGit/#/home)
 > 2. 输入下载的地址
 > 3. 例如：输入：https://github.com/jasonhua95/samll-project/tree/master/DistributedSession   下载DistributedSession项目

### MoreLanguage
 > 1. 这是一个ASP.NET Core Web多语言项目，主要展示项目的不同：
 > 2. 第一种：www.xxx.com/en/index; www.xxx.com/zh/index; ，这种事通过路由来处理的
 > 3. 第二种: www.xxx.com/index/en; www.xxx.com/index/zh ，这种可以用cookie保存，通过cookie来判断
 > 4. 第三种：www.en.xxx.com; www.zh.xxx.com, 这种方案就是发布两个项目，属于比较简单的，有多少种语言就发布多少种，上面的两种发布的是同一个版本的
 > 5. 第一种，第三种一般用于两个项目差别比较大的情况，第二种一般用于项目只有内容不同，其他的都相同的情况
 > 6. 增加session和cookie的使用
 ![如图](https://github.com/jasonhua95/samll-project/blob/master/image/001.PNG)
 
### DistributedSession
> 1. 这是一个ASP.NET Core Web项目，主要展示redis的分布式缓存和负载均衡
> 2. 项目发布两次到不同的文件夹，IIS上配置
> 3. 通过nginx来做负载均衡，实现一个网址调用不同的服务器
![配置](https://github.com/jasonhua95/samll-project/blob/master/image/00201.png)
![效果图](https://github.com/jasonhua95/samll-project/blob/master/image/00202.png)
> 4. 缓存将可以替换session，完全不用session
![如图](https://github.com/jasonhua95/samll-project/blob/master/image/002.PNG)

### OcelotStudy
Ocelot是一个用.NET Core实现并且开源的API网关，它功能强大，包括了：路由、认证、鉴权、简单缓存、限流熔断、负载均衡器等。简单的来说Ocelot是一堆的asp.net core middleware组成的一个有顺序的管道。当它拿到请求之后会用一个request builder来构造一个HttpRequestMessage发到下游的真实服务器，等下游的服务返回response之后再由一个middleware将它返回的HttpResponseMessage映射到HttpResponse上。
> 1. 这是一个ASP.NET Core Web项目，主要展示Ocelot的使用
> 2. 实现Ocelot来控制访问
> 3. 实现Ocelot来控制负载均衡
![如图](https://github.com/jasonhua95/samll-project/blob/master/image/003.PNG)

### RabbitMQDemo
生成者就是发送信息，消费者就是接收信息，队列就是存储数据的排队。消息通过你的应用程序和RabbitMQ进行传输，它们只能存储在队列中，队列容量没有限制，你要存储多少消息都可以——基本上是一个无限的缓冲区。多个生产者（producers）能够把消息发送给同一个队列，同样，多个消费者（consumers）也能从同一个队列（queue）中获取数据。
MQ全称为Message Queue, 消息队列（MQ）是一种应用程序对应用程序的通信方法。应用程序通过读写出入队列的消息（针对应用程序的数据）来通信，而无需专用连接来链接它们。
RabbitMQ是实现了高级消息队列协议（AMQP）的开源消息代理软件（亦称面向消息的中间件）。RabbitMQ服务器是用Erlang语言编写的，而集群和故障转移是构建在开放电信平台框架上的。所有主要的编程语言均有与代理接口通讯的客户端库。消息传递相较文件传递与远程过程调用（RPC）而言，似乎更胜一筹，因为它具有更好的平台无关性，并能够很好地支持并发与异步调用。对操作的实时性要求不高，而需要执行的任务极为耗时，存在异构系统间的整合等效果更好。使用工作队列的一个好处就是它能够并行的处理队列。如果堆积了很多任务，我们只需要添加更多的消费者（Consuming）就可以了，扩展很简单。

> 1. 这是一个ASP.NET Core Web项目，主要展示RabbitMQ的使用
> 2. ReceiveMQ接收消息项目
> 3. SendMQ发送消息项目
> 4. 如果发送的信息很多的时候，可以启动多个消费者（ReceiveMQ）
![如图](https://github.com/jasonhua95/samll-project/blob/master/image/004.PNG)

### DynamicNews
门户网站经常会遇到有公告、新闻之类的，这类数据每次查询会浪费服务器性能，所有产生了这样一个项目，可以根据模板自动生成所需要的html文件

> 1. 这是一个ASP.NET Core Web项目，主要展示根据模板html自动生成需要的html页面
> 2. 初步预想步骤：
 >> 1. 根据模板生成tag
 >> 2. 填写新闻
 >> 3. 生成新闻页面
 
### GrpcDemo
这是一个RPC的项目，包括服务端和客户端，例子引用的[Grpc](https://github.com/grpc/grpc/tree/master/examples/csharp/Helloworld)中的例子
> 1. 项目用到了.proto的文件，项目需要先编译，之后，.proto属性设置编译方式Protobuf compiler
> 2. GreeterServer服务器，GreeterClient客户端
 
### Utils
.NET Core项目的工具类

> 1. ReplaceRenderer.cs 替换渲染器，使用方法：await ReplaceRenderer.ParseAsync("hi @UserName,年龄： @Age，小名：@XiaoName 。", new { UserName = "测试", Age = 19, XiaoName = "小明" })

### TestProject
.NET Core测试项目和.Framework测试项目。在工作和学习上，我们经常需要创建项目，引用些其他的库，测试下自己的想法是否正确，需要捕捉一下异常、调用一下异步方法、记录一下log等等，这样的项目都是需要花费时间，为了较少花费的时间，产生了一个这样的项目。

### SimpleEmail 邮件发送功能
用linq的方式发送邮件，简单直接。
``` C#
var smtp = new SmtpClient("smtp.exmail.qq.com", 25);
smtp.UseDefaultCredentials = true;
smtp.Credentials = new NetworkCredential(EmailFrom, EmailPassword);
smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
smtp.Port = EmailPort;
smtp.EnableSsl = true;

SimpleEmail.DefaultSender = smtp;
SimpleEmail.From(EmailFrom, "系统邮件").To(EmailTo).Subject("主题").Body(content).Send();

smtp.Dispose();
```



