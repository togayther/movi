using System;
using System.Collections.Generic;
using System.Linq;

namespace Movi.Common
{
    public class FilterHelper : TrieNode
    {
        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="key"></param>
        public void AddKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            TrieNode node = this;
            for (int i = 0; i < key.Length; i++)
            {
                char c = (key[i]);
                TrieNode subnode;
                if (!node.m_values.TryGetValue(c, out subnode))
                {
                    subnode = new TrieNode();
                    node.m_values.Add(c, subnode);
                }
                node = subnode;
            }
            node.m_end = true;
        }

        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public bool HasBadWord(string text)
        {
            for (int head = 0; head < text.Length; head++)
            {
                int index = head;
                TrieNode node = this;
                while (node.TryGetValue(text[index], out node))
                {
                    if (node.m_end)
                    {
                        return true;
                    }
                    if (text.Length == ++index)
                    {
                        break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 检查是否包含非法字符
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <returns>找到的第1个非法字符.没有则返回string.Empty</returns>
        public string FindOne(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = (text[i]);
                TrieNode node = this;
                int j = i;
                while (node.m_values.TryGetValue(c, out node))
                {
                    if (node.m_end)
                    {
                        return text.Substring(i, j - i + 1);
                    }
                    if (text.Length == ++j)
                    {
                        break;
                    }
                    c = (text[j]);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 查找所有非法字符
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <returns></returns>
        public IEnumerable<string> FindAll(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = (text[i]);
                TrieNode node = this;
                int j = i;
                while (node.m_values.TryGetValue(c, out node))
                {
                    if (node.m_end)
                    {
                        yield return text.Substring(i, j - i + 1);
                    }
                    if (text.Length == ++j)
                    {
                        break;
                    }
                    c = (text[j]);
                }
            }
        }

        /// <summary>
        /// 替换非法字符
        /// </summary>
        /// <param name="text">原始文本</param>
        /// <param name="mask">用于代替非法字符，默认为'*'</param>
        /// <returns>替换后的字符串</returns>
        public string Replace(string text, char mask = '*')
        {
            char[] chars = null;
            for (int i = 0; i < text.Length; i++)
            {
                char c = (text[i]);
                int j = i;
                TrieNode node = this;
                while (node.m_values.TryGetValue(c, out node))
                {
                    if (node.m_end)
                    {
                        if (chars == null) chars = text.ToArray();
                        for (int t = i; t <= j; t++)
                        {
                            chars[t] = mask;
                        }
                        i = j;
                    }
                    if (text.Length == ++j)
                    {
                        break;
                    }
                    c = (text[j]);
                }
            }
            return chars == null ? text : new string(chars);
        }
    }

    /// <summary>
    /// 字典树
    /// </summary>
    public class TrieNode
    {
        public bool m_end;
        public Dictionary<char, TrieNode> m_values;
        public TrieNode()
        {
            m_values = new Dictionary<char, TrieNode>();
        }

        public bool TryGetValue(char c, out TrieNode node)
        {
            return m_values.TryGetValue(c, out node);
        }
        public TrieNode Add(char c)
        {
            TrieNode subnode;
            if (!m_values.TryGetValue(c, out subnode))
            {
                subnode = new TrieNode();
                m_values.Add(c, subnode);
            }
            return subnode;
        }
    }
}
