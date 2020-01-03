using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.ScoreRank
{
    /// <summary>
    /// 平衡二叉树
    /// </summary>
    public class RankBinaryTree
    {
        /// <summary>
        /// 根节点
        /// </summary>
        private TreeNode _root;

        /// <summary>
        /// 保存的数据
        /// </summary>
        private List<int> _data;

        /// <summary>
        /// 构造函数初始化根节点
        /// </summary>
        /// <param name="max"></param>
        public RankBinaryTree(int max)
        {
            _root = new TreeNode() { ValueFrom = 0, ValueTo = max, Height = 1 };
            _root.LeftChildNode = CreateChildNode(_root, 0, max / 2);
            _root.RightChildNode = CreateChildNode(_root, max / 2 + 1, max);
            _data = new List<int>();
        }

        /// <summary>
        /// 遍历创建子节点
        /// </summary>
        /// <param name="current"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private TreeNode CreateChildNode(TreeNode current, int min, int max)
        {
            if (min == max) return null;
            var node = new TreeNode() { ValueFrom = min, ValueTo = max, Height = current.Height + 1 };
            node.Parent = current;
            int center = (min + max) / 2;
            if (min < max - 1)
            {
                node.LeftChildNode = CreateChildNode(node, min, center);
                node.RightChildNode = CreateChildNode(node, center + 1, max);
            }
            return node;
        }

        /// <summary>
        /// 往树中插入一个值
        /// </summary>
        /// <param name="value"></param>
        public void Insert(int value)
        {
            InnerInsert(_root, value);
            _data.Add(value);
        }

        /// <summary>
        /// 子节点判断范围遍历插入
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        private void InnerInsert(TreeNode node, int value)
        {
            if (node == null) return;
            //判断是否在这个节点范围内
            if (value >= node.ValueFrom && value <= node.ValueTo)
            {
                //更新节点总数信息
                node.Count++;
                //更新左子节点
                InnerInsert(node.LeftChildNode, value);
                //更新右子节点
                InnerInsert(node.RightChildNode, value);
            }
        }

        /// <summary>
        /// 更新值，先移除再插入新值
        /// </summary>
        /// <param name="original"></param>
        /// <param name="diff"></param>
        public void Update(int original, int diff)
        {
            Remove(original);
            int latest = original + diff;
            Insert(latest);
        }

        /// <summary>
        /// 从树中移除一个值
        /// </summary>
        /// <param name="value"></param>
        public void Remove(int value)
        {
            if (!_data.Contains(value)) return;
            InnerRemove(_root, value);
        }

        /// <summary>
        /// 移除值更新子节点信息
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public void InnerRemove(TreeNode node, int value)
        {
            if (node == null) return;
            //判断是否在这个节点范围内
            if (value >= node.ValueFrom && value <= node.ValueTo)
            {
                //更新总数信息
                node.Count--;
                //更新左子节点
                InnerRemove(node.LeftChildNode, value);
                //更新右子节点
                InnerRemove(node.RightChildNode, value);
            }
        }

        /// <summary>
        /// 从树中获取总排名
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetRank(int value)
        {
            if (value < 0) return 0;
            return InnerGet(_root, value);
        }

        /// <summary>
        /// 遍历子节点获取累计排名
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int InnerGet(TreeNode node, int value)
        {
            if (node.LeftChildNode == null || node.RightChildNode == null) return 1;
            if (value >= node.LeftChildNode.ValueFrom && value <= node.LeftChildNode.ValueTo)
            {
                //当这个值存在于左子节点中时，要累加右子节点的总数（表示这个数在多少名之后）
                return node.RightChildNode.Count + InnerGet(node.LeftChildNode, value);
            }
            else
            {
                //如果在右子节点中就继续遍历
                return InnerGet(node.RightChildNode, value);
            }
        }

        /// <summary>
        /// 是否存在指定的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainValue(int value)
        {
            return _data.Contains(value);
        }
    }

    /// <summary>
    /// 树节点对象
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 节点的最小值
        /// </summary>
        public int ValueFrom { get; set; }

        /// <summary>
        /// 节点的最大值
        /// </summary>
        public int ValueTo { get; set; }

        /// <summary>
        /// 在节点范围内的数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 节点高度（树的层级）
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public TreeNode Parent { get; set; }

        /// <summary>
        /// 左子节点
        /// </summary>
        public TreeNode LeftChildNode { get; set; }

        /// <summary>
        /// 右子节点
        /// </summary>
        public TreeNode RightChildNode { get; set; }
    }
}
