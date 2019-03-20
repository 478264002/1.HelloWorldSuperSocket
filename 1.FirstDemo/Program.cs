using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace First_TelnetDemo
{
    /// <summary>
    /// 服务器启动和停止代码
    /// 参考: http://docs.supersocket.net/v1-6/zh-CN/A-Telnet-Example
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            var appServer = new AppServer();

            //Setup the appServer
            if (!appServer.Setup(2012)) //Setup with listening port
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();

            //Try to start the appServer
            if (!appServer.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("The server started successfully, press key 'q' to stop it!");


            //1.注册回话新建事件处理方法
            appServer.NewSessionConnected += AppServer_NewSessionConnected;

            //2.处理请求
            //2.1 注册请求处理方法   appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);
            //appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);

            /* 3. Command的用法
             *  在本文档的前半部分，你可能已经了解到了如何在SuperSocket处理客户端请求。 但是同时你可能会发现一个问题，
             *  如果你的服务器端包含有很多复杂的业务逻辑，这样的switch/case代码将会很长而且非常难看，并且没有遵循OOD的原则。 
             *  在这种情况下，SuperSocket提供了一个让你在多个独立的类中处理各自不同的请求的命令框架。
             *  同时你要移除请求处理方法的注册，因为它和命令不能同时被支持： 删除: 2.1 注册请求处理方法
             */




            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            //Stop the appServer
            appServer.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        static void AppServer_NewSessionConnected(AppSession session)
        {
            session.Send("Hello world!  Welcome to SuperSocket Telnet Server");
        }

        #region --- 2.处理请求: 2.1 注册请求处理方法 ---

        //static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        //{
        //    //requestInfo.Key 是请求的命令行用空格分隔开的第一部分
        //    //requestInfo.Parameters 是用空格分隔开的其余部分

        //    switch (requestInfo.Key.ToUpper())
        //    {
        //        case ("ECHO"):
        //            session.Send(requestInfo.Body);
        //            break;

        //        case ("ADD"):
        //            session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
        //            break;

        //        case ("MULT"):

        //            var result = 1;

        //            foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
        //            {
        //                result *= factor;
        //            }

        //            session.Send(result.ToString());
        //            break;
        //    }

        #endregion
    }
}
