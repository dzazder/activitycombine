using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tcxengine
{
    
    public class XmlConvertBase
    {
        /// <summary>
        /// Returns all children with specified name
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Child name</param>
        /// <returns></returns>
        protected static List<XmlNode> GetChildren(XmlNode parentNode, string name)
        {
            var result = new List<XmlNode>();

            foreach (XmlNode child in parentNode.ChildNodes)
            {
                if (child.Name.Equals(name))
                {
                    result.Add(child);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns first child node with specified name or null
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Child name</param>
        /// <returns></returns>
        protected static XmlNode GetChild(XmlNode parentNode, string name)
        {
            var children = GetChildren(parentNode, name);
            return children.Count > 0 ? children[0] : null;
        }

        /// <summary>
        /// Reteurns value of node with specified name when value is saved like as <Node>value</Node>
        /// </summary>
        /// <param name="parentNode">Parent node</param>
        /// <param name="name">Name of node with value</param>
        /// <returns>Value of node or null</returns>
        protected static string GetChildValue(XmlNode parentNode, string name)
        {
            var childNode = GetChild(parentNode, name);
            if (childNode != null)
            {
                var childNodes = childNode.ChildNodes;
                if (childNodes != null && childNodes.Count > 0)
                {
                    return childNodes[0].Value;
                }
            }

            return null;
        }

        protected static double? GetDoubleChildValue(XmlNode parentNode, string name)
        {
            var val = GetChildValue(parentNode, name);
            if (!string.IsNullOrEmpty(val))
            {
                double d;
                if (Double.TryParse(val.Replace('.', ','), out d))  // todo localization
                {
                    return d;
                }
            }

            return null;
        }

        protected static DateTime? GetDateTimeChildValue(XmlNode parentNode, string name)
        {
            var val = GetChildValue(parentNode, name);
            if (!string.IsNullOrEmpty(val))
            {
                DateTime dt;
                if (DateTime.TryParse(val, out dt))
                {
                    return dt;
                }
            }

            return null;
        }

        protected static string GetAttributeValue(XmlNode node, string name)
        {
            if (node.Attributes != null)
            {
                if (node.Attributes[name] != null)
                {
                    return node.Attributes[name].Value;
                }
            }

            return null;
        }

        protected static double? GetDoubleAttributeValue(XmlNode node, string name)
        {
            var val = GetAttributeValue(node, name);
            double d;
            if (Double.TryParse(val.Replace('.', ','), out d))
            {
                return d;
            }

            return null;
        }
        
    }
}
