using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    /// <summary>
    /// 默认对象扩展类
    /// </summary>
    public class DefaultObjectExtensions: IFMFrameWorkObjectExtensions
    {
        /// <summary>
        /// 默认json序列化设置
        /// </summary>
        protected virtual JsonSerializerSettings DefaultJsonSerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ContractResolver = new NonPublicSetterContractResolver(),
            PreserveReferencesHandling = PreserveReferencesHandling.All
        };


        /// <summary>
        /// 驼峰命名JSON序列化配置
        /// </summary>
        protected virtual JsonSerializerSettings CamelCaseJsonSerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonContent"></param>
        /// <returns></returns>
        public virtual T Deserialize<T>(string jsonContent)
        {
            return JsonConvert.DeserializeObject<T>(jsonContent, DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// json 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T JsonClone<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj, DefaultJsonSerializerSettings), DefaultJsonSerializerSettings);
        }

        /// <summary>
        /// json 非深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual T JsonCloneNone<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// 序列化Json字符串，驼峰命名
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public virtual string SerializeJsonCamelCase(object jsonObj)
        {
            if (jsonObj == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(jsonObj, CamelCaseJsonSerializerSettings);
        }

        /// <summary>
        /// 序列化Json字符串
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
        public virtual string SerializeJson(object jsonObj)
        {
            if (jsonObj == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(jsonObj, DefaultJsonSerializerSettings);
        }
    }
}
