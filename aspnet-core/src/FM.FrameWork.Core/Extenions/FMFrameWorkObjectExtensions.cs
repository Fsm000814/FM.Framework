using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    public static class FMFrameWorkObjectExtensions
    {
        public static IFMFrameWorkObjectExtensions Instance = new DefaultObjectExtensions();

        /// <summary>
        /// json 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T JsonClone<T>(this T obj)
        {
            return Instance.JsonClone(obj);
        }

        /// <summary>
        /// json 非深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T JsonCloneNone<T>(this T obj)
        {
            return Instance.JsonCloneNone(obj);
        }

        /// <summary>
        /// 序列化Json字符串，驼峰命名
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public static string SerializeJsonCamelCase(this object jsonObj)
        {
            return Instance.SerializeJsonCamelCase(jsonObj);
        }

        /// <summary>
        /// 序列化为Json字符串
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public static string SerializeJson(this object jsonObj)
        {
            return Instance.SerializeJson(jsonObj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string jsonContent)
        {
            return Instance.Deserialize<T>(jsonContent);
        }
    }
}
