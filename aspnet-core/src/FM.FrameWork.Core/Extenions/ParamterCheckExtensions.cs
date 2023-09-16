using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Extenions
{
    /// <summary>
    /// 用于参数检查的扩展方法
    /// </summary>
    public static class ParamterCheckExtensions
    {
        /// <summary>
        /// 验证指定值的断言assertion是否为真，如果不为真，抛出指定消息message的指定类型 TException异常
        /// </summary>
        /// <typeparam name="TException">异常类型</typeparam>
        /// <param name="assertion">要验证的断言</param>
        /// <param name="message">异常消息</param>
        private static void Require<TException>(bool assertion, string message) where TException : Exception
        {
            if (assertion)
            {
                return;
            }

            throw (TException)Activator.CreateInstance(typeof(TException), message);
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真，不为值抛出System.Exception异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T>(this T value, Func<T, bool> assertionFunc, string message)
        {
            Require<Exception>(assertionFunc(value), message);
        }

        /// <summary>
        /// 验证指定值的断言表达式是否为真，不为真抛出 异常
        /// </summary>
        /// <typeparam name="T">要判断的值的类型</typeparam>
        /// <typeparam name="TException"></typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="assertionFunc">要验证的断言表达式</param>
        /// <param name="message">异常消息</param>
        public static void Required<T, TException>(this T value, Func<T, bool> assertionFunc, string message) where TException : Exception
        {
            Require<TException>(assertionFunc(value), message);
        }

        /// <summary>
        /// 检查参数不能为空引用，否则抛出System.ArgumentNullException异常。
        /// </summary>
        /// <typeparam name="T">System.ArgumentNullException:</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNull<T>(this T value, string paramName) where T : class
        {
            Require<ArgumentNullException>(value != null, "参数“" + paramName + "”不能为空引用。");
        }

        /// <summary>
        /// 检查字符串不能为空引用或空字符串，否则抛出System.ArgumentNullException异常或System.ArgumentException异常。
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNullOrEmpty(this string value, string paramName)
        {
            value.CheckNotNull(paramName);
            Require<ArgumentException>(value.Length > 0, "参数“" + paramName + "”不能为空引用或空字符串。");
        }

        /// <summary>
        /// 检查Guid值不能为Guid.Empty，否则抛出System.ArgumentException异常。
        /// </summary>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotEmpty(this Guid value, string paramName)
        {
            Require<ArgumentException>(value != Guid.Empty, "参数“" + paramName + "”的值不能为Guid.Empty");
        }

        /// <summary>
        ///  检查集合不能为空引用或空集合，否则抛出System.ArgumentNullException异常或System.ArgumentException异常。
        /// </summary>
        /// <typeparam name="T">集合项的类型</typeparam>
        /// <param name="collection"></param>
        /// <param name="paramName">参数名称</param>
        public static void CheckNotNullOrEmpty<T>(this IEnumerable<T> collection, string paramName)
        {
            collection.CheckNotNull(paramName);
            Require<ArgumentException>(collection.Any(), "参数“" + paramName + "”不能为空引用或空集合。");
        }

        /// <summary>
        /// 检查参数必须小于[或可等于，参数canEqual]指定值，否则抛出System.ArgumentOutOfRangeException异常。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="target">要比较的值</param>
        /// <param name="canEqual">是否可等于</param>
        public static void CheckLessThan<T>(this T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool assertion = (canEqual ? (value.CompareTo(target) <= 0) : (value.CompareTo(target) < 0));
            string format = (canEqual ? "参数“{0}”的值必须小于或等于“{1}”。" : "参数“{0}”的值必须小于“{1}”。");
            Require<ArgumentOutOfRangeException>(assertion, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 检查参数必须大于[或可等于，参数canEqual]指定值，否则抛出System.ArgumentOutOfRangeException异常。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="target">要比较的值</param>
        /// <param name="canEqual">是否可等于</param>
        public static void CheckGreaterThan<T>(this T value, string paramName, T target, bool canEqual = false) where T : IComparable<T>
        {
            bool assertion = (canEqual ? (value.CompareTo(target) >= 0) : (value.CompareTo(target) > 0));
            string format = (canEqual ? "参数“{0}”的值必须大于或等于“{1}”。" : "参数“{0}”的值必须大于“{1}”。");
            Require<ArgumentOutOfRangeException>(assertion, string.Format(format, paramName, target));
        }

        /// <summary>
        /// 检查参数必须在指定范围之间，否则抛出System.ArgumentOutOfRangeException异常。
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">要判断的值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="start">比较范围的起始值</param>
        /// <param name="end">比较范围的结束值</param>
        /// <param name="startEqual">是否可等于起始值</param>
        /// <param name="endEqual">是否可等于结束值</param>
        public static void CheckBetween<T>(this T value, string paramName, T start, T end, bool startEqual = false, bool endEqual = false) where T : IComparable<T>
        {
            bool assertion = (startEqual ? (value.CompareTo(start) >= 0) : (value.CompareTo(start) > 0));
            string message = (startEqual ? $"参数“{paramName}”的值必须在“{start}”与“{end}”之间，且不能等于“{start}”。" : $"参数“{paramName}”的值必须在“{start}”与“{end}”之间。");
            Require<ArgumentOutOfRangeException>(assertion, message);
            assertion = (endEqual ? (value.CompareTo(end) <= 0) : (value.CompareTo(end) < 0));
            message = (endEqual ? $"参数“{paramName}”的值必须在“{start}”与“{end}”之间，且不能等于“{end}”。" : $"参数“{paramName}”的值必须在“{start}”与“{end}”之间。");
            Require<ArgumentOutOfRangeException>(assertion, message);
        }

        /// <summary>
        /// 检查指定路径的文件夹必须存在，否则抛出System.IO.DirectoryNotFoundException异常。
        /// </summary>
        /// <param name="directory">目录路径</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckDirectoryExists(this string directory, string paramName = null)
        {
            directory.CheckNotNull(paramName);
            Require<DirectoryNotFoundException>(Directory.Exists(directory), "指定的目录路径“" + directory + "”不存在。");
        }

        /// <summary>
        /// 检查指定路径的文件必须存在，否则抛出System.IO.FileNotFoundException异常。
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <param name="paramName">参数名称</param>
        public static void CheckFileExists(this string filename, string paramName = null)
        {
            filename.CheckNotNull(paramName);
            Require<FileNotFoundException>(File.Exists(filename), "指定的文件路径“" + filename + "”不存在。");
        }
    }
}
