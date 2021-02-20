using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Reflection
{
    class Program 
    {
        static void Main(string[] args)
        {
            var firstUrl = "/Customer/List?limit=20";
            var url = @"/Customer/Add?Name=Pepa&Age=30&IsActive=true";
            Execute(url);
            Execute(firstUrl);
        }

        private static void Execute(string url)
        {
            var parts = url.Split("/", StringSplitOptions.RemoveEmptyEntries);

            var nameOfClass = $"Library.Controllers.{parts[0]}Controller";
            var minorParts = parts[1].Split("?");

            var nameOfMethod = minorParts[0];
            var lastSplitting = minorParts[1].Split("&");
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            foreach (var parameters in lastSplitting)
            {
                string[] keyValue = parameters.Split('=');
                queryParams.Add(keyValue[0], keyValue[1]);
            }

            //Path to dll where is store my class library
            //const string PATH = @"C:\Users\jakub\Source\Repos\Reflection\Library\bin\Debug\netcoreapp3.1\Library.dll";

            const string PATH = @"..\..\..\..\Library\bin\Debug\netcoreapp3.1\Library.dll";
            Assembly assembly = Assembly.LoadFile(Path.GetFullPath(PATH));

            //load my class from library
            Type controllerType = assembly.GetType(nameOfClass);
            if (controllerType == null)
            {
                Console.WriteLine("Page was not found");
                return;
            }

          
            object controllerInstance = Activator.CreateInstance(controllerType);


            //Method from class
            MethodInfo method = controllerType.GetMethod(nameOfMethod);
            if (method == null || method.ReturnType != typeof(string))
            {
                Console.WriteLine("Page was nout found");
                return;
            }

            ParameterInfo[] param = method.GetParameters();
            object[] arguments = new object[param.Length];

            #region dictionary
            //Can be done using dictrionary
            //Dictionary<Type, Func<string, object>> converters = new Dictionary<Type, Func<string, object>>
            //{
            //    [typeof(int)] = (string val) => int.Parse(val),
            //    [typeof(string)] = (string val) => val,
            //    [typeof(bool)] = (string val) => bool.Parse(val)
            //    [typeof()]
            //};
            #endregion
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i].ParameterType.IsClass)
                {
                    object parObject = Activator.CreateInstance(param[i].ParameterType);
                    PropertyInfo[] props = param[i].ParameterType.GetProperties();
                    foreach (var prop in props)
                    {
                        bool ignore = prop.GetCustomAttributes().Any(x => x.GetType().Name == "IgnoreAttribute");
                        if (ignore)
                        {
                            continue;
                        }
                        string value = queryParams.ContainsKey(prop.Name) ? queryParams[prop.Name] : null;
                        if (value == null)
                        {
                            continue;
                             
                        }
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(parObject,int.Parse(value));
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(parObject, value);
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            prop.SetValue(parObject, bool.Parse(value));
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    arguments[i] = parObject;
                }
                else
                {
                    string value = queryParams.ContainsKey(param[i].Name) ? queryParams[param[i].Name] : null;

                    //arguments[i] = converters[param[i].ParameterType](value);

                    if (param[i].ParameterType == typeof(int))
                    {
                        arguments[i] = int.Parse(value);
                    }
                    else if (param[i].ParameterType == typeof(string))
                    {
                        arguments[i] = value;
                    }
                    else if (param[i].ParameterType == typeof(bool))
                    {
                        arguments[i] = bool.Parse(value);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                
            }


            //invoke method
            var methodObject = (string) method?.Invoke(controllerInstance, arguments);


            //if method is private
            //MethodInfo privateMethod = type.GetMethod("List", BindingFlags.NonPublic | BindingFlags.Instance)


            Console.WriteLine(methodObject);
        }
    }
}
