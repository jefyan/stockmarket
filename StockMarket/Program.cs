﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StockMarket
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
#if DEBUG
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainDialog());
#else
            /**
             * 当前用户是管理员的时候，直接启动应用程序
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
             */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            Application.EnableVisualStyles();

            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainDialog());
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                //设置启动参数
                startInfo.Arguments = String.Join(" ", Args);
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
                //退出
                System.Windows.Forms.Application.Exit();
            }
#endif
        }
    }
}