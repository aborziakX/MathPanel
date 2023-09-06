//MathPanel, функции для работы со скриптами, написанными на C#.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//for dynamo
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace MathPanel
{
    public partial class Dynamo //: Window
    {
        public static string frmPath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\";
        static object dynClassInstance = null; //объект типа скрипт    
        static System.Threading.Thread my_thread = null; //текущий поток со скриптом   
        readonly static List<System.Threading.Thread> lstThr = new List<Thread>(); //список всех потоков со скриптами

        //компилировать и выполнить скрипт из окна "комманд"
        public static void Process(string s)
        {
            //сборки
            string[] includeAssemblies = { "MathPanel.exe",
                frmPath + "System.dll",
                frmPath + "System.Xaml.dll",
                frmPath + "WindowsBase.dll",
                frmPath + "PresentationFramework.dll",
                frmPath + "PresentationCore.dll"
                , frmPath + "System.Drawing.dll"
                , frmPath + "System.Net.dll"
                , frmPath + "System.Net.Http.dll"
                //, frmPath + "System.Collections.dll"
            };
            //пространства имен
            string[] includeNamespaces = { "MathPanel", "MathPanelExt", "System.Net.Sockets", "System.Collections.Generic" };
            keyConsole = "";
            CompileDynamo(s, null, includeNamespaces, includeAssemblies);
            //создаем новый поток
            try
            {
                if (dynClassInstance != null)
                {
                    my_thread = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                        Type type = dynClassInstance.GetType();
                        MethodInfo methodInfo = type.GetMethod("Execute");
                        try
                        {
                            methodInfo.Invoke(dynClassInstance, null);
                            Dynamo.Console("Скрипт выполнен.");
                        }
                        catch (Exception yyy) { Dynamo.Console(yyy.ToString()); }
                        //my_thread = null;
                    }));
                    my_thread.Start();
                    lstThr.Add(my_thread);
                }
            }
            catch (Exception xxx) { Console(xxx.ToString()); }
        }

        //метод для тестирования
        static object Eval(string code, Type outType = null, string[] includeNamespaces = null, string[] includeAssemblies = null)
        {
            object methodResult = null;
            CompileDynamo(code, outType, includeNamespaces, includeAssemblies);
            if (dynClassInstance != null)
            {
                Type type = dynClassInstance.GetType();
                MethodInfo methodInfo = type.GetMethod("Execute");
                methodResult = methodInfo.Invoke(dynClassInstance, null);
            }
            return methodResult;
        }

        //создать объект типа скрипт
        static object CompileDynamo(string code, Type outType = null, string[] includeNamespaces = null, string[] includeAssemblies = null)
        {
            StringBuilder namespaces = null;
            object methodResult = null;
            dynClassInstance = null;
            using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
            {
                //ICodeCompiler codeCompiler = codeProvider.CreateCompiler();//obsolete!
                CompilerParameters compileParams = new CompilerParameters
                {
                    CompilerOptions = "/t:library",
                    GenerateInMemory = true
                };

                int ipos = code.IndexOf("///[DLL]");
                if (ipos > 0)
                {
                    int ipos2 = code.IndexOf("[/DLL]", ipos);
                    if (ipos2 > 0)
                    {
                        compileParams.ReferencedAssemblies.Add("MathPanel.exe");
                        string ass = code.Substring(ipos + 8, ipos2 - ipos - 8);
                        var arr = ass.Split(',');
                        foreach (string _assembly in arr)
                        {
                            compileParams.ReferencedAssemblies.Add(frmPath + _assembly.Trim());
                        }
                    }
                }
                else if (includeAssemblies != null && includeAssemblies.Length > 0)
                {
                    foreach (string _assembly in includeAssemblies)
                    {
                        compileParams.ReferencedAssemblies.Add(_assembly);
                    }
                }

                if (includeNamespaces != null && includeNamespaces.Length > 0)
                {
                    namespaces = new StringBuilder();
                    foreach (string _namespace in includeNamespaces)
                    {
                        namespaces.Append(string.Format("using {0};\n", _namespace));
                    }
                }

                if (code.IndexOf("namespace DynamoCode") < 0)
                    code = string.Format(
                        @"{1}  
                using System;  
                namespace DynamoCode{{  
                    public class Script{{  
                        public {2} Execute(){{  
                            {3} {0};  
                        }}  
                    }}  
                }}",
                        code,
                        namespaces != null ? namespaces.ToString() : null,
                        outType != null ? outType.FullName : "void",
                        outType != null ? "return" : string.Empty
                        );
                CompilerResults compileResult = codeProvider.CompileAssemblyFromSource(compileParams, code);
                //codeCompiler.CompileAssemblyFromSource(compileParams, code);

                if (compileResult.Errors.Count > 0)
                {
                    //throw new Exception(compileResult.Errors[0].ErrorText);
                    Console("compile error: " + compileResult.Errors[0].ErrorText);
                    return "compile error: " + compileResult.Errors[0].ErrorText;
                }
                System.Reflection.Assembly assembly = compileResult.CompiledAssembly;
                dynClassInstance = assembly.CreateInstance("DynamoCode.Script");
                /*Type type = dynClassInstance.GetType();
                MethodInfo methodInfo = type.GetMethod("Execute");
                methodResult = methodInfo.Invoke(dynClassInstance, null);*/
            }
            return methodResult;
        }
    }
}
