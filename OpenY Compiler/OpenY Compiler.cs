using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace OpenY_Compiler
{
    public class Compiler
    {
     public static   Dictionary<string, int> Integers32dict = new Dictionary<string, int>();
     public static Dictionary<string, string> Stringsdict = new Dictionary<string, string>();
        string workString = string.Empty;
        string stringConstructer = string.Empty;
        bool haltPadding = false;
        int charnum = -1;
        string durationBuilder = string.Empty;
        string IncludeBuilder = string.Empty;
        string Int32VarBuilder = string.Empty;
        bool IsInt32VarBuildingName = true;
        string Int32VarName = string.Empty;
        string Int32VarValue = string.Empty;
        string StringVarBuilder = string.Empty;
        bool IsStringBuildingName = true;
        string StringVarName = string.Empty;
        string StringVarValue = string.Empty;
        string TitleBuilder = string.Empty;
        string CMDValue = string.Empty;

        public void Lexxer(string Lex)
        {
            foreach (char LexChar in Lex)
            {
                charnum++;
                if (!haltPadding)
                {
                    workString = workString + LexChar;
                    workString = workString.Replace("\n", "");
                }
                #region <Output>
                if (workString.Contains("<Screen>"))
                {
                    haltPadding = true;
                    if (!stringConstructer.Contains("</Screen>"))
                    {
                        try
                        {
                            stringConstructer = stringConstructer + Lex[charnum + 1];
                           
                        }
                        catch (OverflowException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Warning Missing End");
                            Console.ResetColor();
                            break;
                        }
                    }
                    else
                    {
                        stringConstructer = stringConstructer.Replace("</Screen>", string.Empty);
                        stringConstructer = ProccessText(stringConstructer);
                        Parse(stringConstructer, "PRINT");
                        workString = string.Empty;
                        stringConstructer = string.Empty;
                        haltPadding = false;

                    }
                }
                #endregion
                #region <ClearOutput>
                if (workString.Contains("<ClearScreen>"))
                {
                    haltPadding = true;
                    try
                    {

                        Console.Clear();

                    }
                    catch (IOException)
                    {
                        Console.WriteLine("CLEARING SCREEN ISN'T SUPPORTED\n");
                    }
                    workString = string.Empty;
                    haltPadding = false;
                }
                #endregion
                #region <Halt>

                if (workString.Contains("<Halt>"))
                {

                    if (!durationBuilder.Contains("</Halt>"))
                    {

                        durationBuilder = durationBuilder + Lex[charnum + 1];

                    }
                    else
                    {
                        durationBuilder = durationBuilder.Replace("</Halt>", string.Empty);
                        System.Threading.Thread.Sleep(Convert.ToInt32(durationBuilder));
                        workString = string.Empty;
                        stringConstructer = string.Empty;
                        durationBuilder = string.Empty;
                    }
                }

                #endregion
                string IncludeResult;
                if (workString.Contains("<Procces>"))
                {

                    if (!IncludeBuilder.Contains("</Procces>"))
                    {

                        IncludeBuilder += Lex[charnum + 1];

                    }
                    else
                    {
                        IncludeResult = IncludeBuilder.Replace("</Procces>", string.Empty);
                        workString = string.Empty;
                        stringConstructer = string.Empty;
                        IncludeBuilder = string.Empty;
                        Compiler compiler = new Compiler();
                        compiler.Lexxer(File.ReadAllText(IncludeResult));
                        IncludeResult = string.Empty;
                    }
                }
                if (workString.Contains("<CMD>"))
                {

                    if (!CMDValue.Contains("</CMD>"))
                    {

                        CMDValue += Lex[charnum + 1];

                    }
                    else
                    {
                        CMDValue = CMDValue.Replace("</CMD>", string.Empty);
                        CMDValue = ProccessText(CMDValue);
                        ExecuteCommand(CMDValue);
                        CMDValue = string.Empty;
                        workString = string.Empty;
                    }
                }
                if (workString.Contains("<Title>"))
                {

                    if (!TitleBuilder.Contains("</Title>"))
                    {

                        TitleBuilder += Lex[charnum + 1];

                    }
                    else
                    {
                        TitleBuilder = TitleBuilder.Replace("</Title>", string.Empty);
                        TitleBuilder = ProccessText(TitleBuilder);
                        Console.Title = TitleBuilder;
                        TitleBuilder = string.Empty;
                        workString = string.Empty;
                    }
                }
                if (workString.Contains("<Int32>"))
                {

                    if (Int32VarName == string.Empty)
                    {
                        if (!Int32VarBuilder.Contains("="))
                        {
                            Int32VarBuilder += Lex[charnum + 1];
                        }
                        else
                        {
                            Int32VarName = Int32VarBuilder.Replace("=", string.Empty);
                            Int32VarName = Int32VarName.Replace("\r", string.Empty);
                            Int32VarName = Int32VarName.Replace("\n", string.Empty);
                            Int32VarName = Int32VarName.Replace(" ", string.Empty);
                            Int32VarBuilder = string.Empty;
                        }
                    }
                    else
                    {
                        if (!Int32VarBuilder.Contains("</Int32>"))
                        {
                            try
                            {
                                Int32VarBuilder += Lex[charnum + 1];
                            }
                            catch (StackOverflowException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Warning Missing End");
                                Console.ResetColor();
                                break;
                            }
                        }
                        else
                        {
                            Int32VarValue = Int32VarBuilder.Replace("</Int32>", string.Empty);
                            Int32VarValue = Int32VarValue.Replace("\r", string.Empty);
                            Int32VarValue = Int32VarValue.Replace("\n", string.Empty);
                            try
                            {
                                Integers32dict.Add(Int32VarName, Convert.ToInt32(Int32VarValue));
                            }
                            catch (ArgumentException)
                            {
                                Integers32dict[Int32VarName] = Convert.ToInt32(Int32VarValue);
                            }
                            IncludeBuilder = string.Empty;
                            Int32VarBuilder = string.Empty;
                            IsInt32VarBuildingName = true;
                            Int32VarName = string.Empty;
                            Int32VarValue = string.Empty;
                            workString = string.Empty;
                        }
                    }

                }
                if (workString.Contains("<String>"))
                {

                    if (StringVarName == string.Empty)
                    {
                        if (!StringVarBuilder.Contains("="))
                        {
                            StringVarBuilder += Lex[charnum + 1];
                        }
                        else
                        {
                            StringVarName = StringVarBuilder.Replace("=", string.Empty);
                            StringVarName = StringVarName.Replace(" ", string.Empty);
                            StringVarName = StringVarName.Replace("\n", string.Empty);
                            StringVarBuilder = string.Empty;
                        }
                    }
                    else
                    {
                        if (!StringVarBuilder.Contains("</String>"))
                        {
                            try
                            {
                                StringVarBuilder += Lex[charnum + 1];
                            }
                            catch (StackOverflowException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Warning Missing End");
                                Console.ResetColor();
                                break;
                            }
                        }
                        else
                        {
                            StringVarValue = StringVarBuilder.Replace("</String>", string.Empty);
                            try
                            {
                                Stringsdict.Add(StringVarName, ProccessText(StringVarValue));
                            }
                            catch (ArgumentException)
                            {
                                Stringsdict[StringVarName] = StringVarValue;
                            }
                            StringVarBuilder = string.Empty;
                            IsStringBuildingName = true;
                            StringVarName = string.Empty;
                            StringVarValue = string.Empty;
                            workString = string.Empty;
                        }
                    }

                }
            }

        }
  
        private static string ProccessText(string stringConstructer)
        {
            string Int32VarName = string.Empty;
            string Int32VarValue = string.Empty;
            string StringVarBuilder = string.Empty;
            bool IsStringBuildingName = true;
            string StringVarName = string.Empty;
            string StringVarValue = string.Empty;
            int charnum = -1;
            string workString = string.Empty;
            stringConstructer = stringConstructer.Replace("<nl>", "\n");
            foreach (char LexChar in stringConstructer)
            {
                charnum++;
                workString = workString + LexChar;
                workString = workString.Replace("\n", "");
                if (workString.Contains("<rString>"))
                {
                    StringVarName += workString[charnum + 1];
                    if (StringVarName.Contains("</rString>"))
                    {
                        StringVarName = StringVarName.Replace("</rString>", string.Empty);
                        StringVarName = StringVarName.Replace(">", string.Empty);
                        StringVarName = StringVarName.Replace("rString", string.Empty);
                        string VarValue =
                        Stringsdict[StringVarName];
                        workString = workString.Replace(("<rString>" + StringVarName + "</rString>"), VarValue);
                        StringVarName = string.Empty;
                    }
                }
                if (workString.Contains("<rInt32>"))
                {
                    Int32VarName += workString[charnum + 1];
                    if (Int32VarName.Contains("</rInt32>"))
                    {
                        Int32VarName = Int32VarName.Replace("</rInt32>", string.Empty);
                        Int32VarName = Int32VarName.Replace(">", string.Empty);
                        StringVarName = StringVarName.Replace("rInt32", string.Empty);
                        int VarValue =
                        Integers32dict[Int32VarName];
                        workString = workString.Replace(("<rInt32>" + Int32VarName + "</rInt32>"), Convert.ToString(VarValue));
                        Int32VarName = string.Empty;
                    }

                }
            }
            
            if (stringConstructer.Contains("<InputChar>"))
            {
                Console.ReadKey();
                stringConstructer = stringConstructer.Replace("<InputChar>", string.Empty);
            }
            if (stringConstructer.Contains("<InputLine>"))
            {
                stringConstructer = stringConstructer.Replace("<InputLine>", Console.ReadLine());
            }


            return stringConstructer;
        }
        private static void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/k " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine( e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine( e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            process.Close();
        }
        private static void Parse(string workingString, string Action)
        {
            if (Action == "PRINT")
            {
                Console.Write(workingString);
            }
        }

    }
}
