using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    internal class NonPublicSetterContractResolver: DefaultContractResolver
    {
        /// <summary>
        /// 确保只读属性仍然被序列化和反序列化，而不会导致异常。
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            //调用基类的CreateProperty方法来创建基本的JsonProperty对象。
            JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);

            // 判断是否是只读属性。
            if (!jsonProperty.Writable)
            {
                // 通过获取成员信息的方式判断是否是一个属性。
                PropertyInfo propertyInfo = member as PropertyInfo;
                if (propertyInfo != null)
                {
                    // 通过反射获取该属性的非公共设置方法（set方法）并判断是否存在。
                    bool flag2 = (jsonProperty.Writable = propertyInfo.GetSetMethod(nonPublic: true) != null);
                }
            }
            // 返回更新后的JsonProperty对象
            return jsonProperty;
        }
    }
}
