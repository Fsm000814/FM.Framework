using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    /// <summary>
    /// 对象扩展接口
    /// </summary>
    public interface IFMFrameWorkObjectExtensions
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        T Deserialize<T>(string jsonContent);

        /// <summary>
        /// json 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T JsonClone<T>(T obj);

        /// <summary>
        ///  json 非深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        T JsonCloneNone<T>(T obj);

        /// <summary>
        ///  序列化Json字符串，驼峰命名
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        string SerializeJsonCamelCase(object jsonObj);

        /// <summary>
        /// 序列化为Json字符串
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        string SerializeJson(object jsonObj);
    }
}
