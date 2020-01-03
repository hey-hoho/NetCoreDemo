using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.ScoreRank
{
    public class TreeTester
    {
        public static void Run()
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            RankBinaryTree tree = new RankBinaryTree(1000000);
            stopwatch.Stop();
            Console.WriteLine($"构造二叉树耗时：{stopwatch.ElapsedMilliseconds}ms");

            //模拟100W用户的积分数据
            //假设50W用户积分都在100以内，30W用户积分在101-10000，15W用户积分在10001-50000,5W用户积分在50000以上
            stopwatch.Restart();
            for (int i = 0; i < 500000; i++)
            {
                tree.Insert(new Random().Next(0, 100));
            }
            for (int i = 0; i < 300000; i++)
            {
                tree.Insert(new Random().Next(100, 10000));
            }
            for (int i = 0; i < 150000; i++)
            {
                tree.Insert(new Random().Next(10000, 50000));
            }
            for (int i = 0; i < 50000; i++)
            {
                tree.Insert(new Random().Next(50000, 1000000));
            }
            stopwatch.Stop();
            Console.WriteLine($"插入100W条数据耗时：{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine("----------------------------------------------------");
            //for (int i = 0; i < 5; i++)
            {
                //tree.Insert(1);
                //tree.Insert(2);
                //tree.Insert(2);
                //tree.Insert(3);
                //tree.Insert(3);
                //tree.Insert(3);
                //tree.Insert(5);
                //tree.Insert(7);
                //tree.Insert(7);
                //tree.Insert(9);
                //tree.Insert(8);
                //Console.WriteLine("获取排名：" + tree.GetRank(5));
                //tree.Remove(7);
                //Console.WriteLine("获取排名：" + tree.GetRank(5));
                //tree.Update(5, 3);
                //Console.WriteLine("获取排名：" + tree.GetRank(7));
            }

            while (true)
            {
                Console.Write("输入要获取排名的积分值：");
                int value = int.Parse(Console.ReadLine());
                stopwatch.Restart();
                int rank = tree.GetRank(value);
                stopwatch.Stop();
                Console.WriteLine("获取排名：{0}，是否存在：{1}，耗时{2}ms", rank, tree.ContainValue(value), stopwatch.ElapsedMilliseconds);
                Console.WriteLine("");
            }
        }
    }
}
