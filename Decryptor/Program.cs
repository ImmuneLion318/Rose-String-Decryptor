using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Text.RegularExpressions;

namespace Decryptor
{
    public class Program
    {
        public static string Decrypt(string Input)
        {
            short Number = 0;
            do
            {
                if (Number == 0)
                    Number = 1;
            }
            while (Number != 1);

            try
            {
                if (IsBase64String(Input))
                    return Encoding.UTF8.GetString(Convert.FromBase64String(Input));
                else
                    return Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(Input));
            }
            catch
            {
                return Input;
            }
        }

        public static string Decode(string Input)
        {
            byte[] Bytes = Encoding.ASCII.GetBytes(Input);
            for (int i = 0; i < Bytes.Length; i += 1)
            {
                byte[] Array = Bytes;
                int Number = i;
                Array[Number] = (byte)((int)Array[Number] - 1);
            }
            return Encoding.ASCII.GetString(Bytes);
        }

        public static bool IsBase64String(string Input)
        {
            Input = Input.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");

            if (Input.Length % 4 != 0)
                return false;

            for (int i = 0; i < Input.Length; i++)
            {
                char c = Input[i];
                if (!(char.IsLetterOrDigit(c) || c == '+' || c == '/' || c == '='))
                    return false;

                if (c == '=')
                    if (i < Input.Length - 2 || (i == Input.Length - 2 && Input[Input.Length - 1] != '='))
                        return false;
            }

            try
            {
                byte[] Data = Convert.FromBase64String(Input);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        
        public static void Main(string[] Args)
        {
            Console.Title = "Rose String Decryptor";
            Console.SetWindowSize(80, 20);
            
            ModuleContext Context = ModuleDef.CreateModuleContext();
            ModuleDefMD Module = ModuleDefMD.Load(Args[0], Context);
            
            List<string> Strings = new List<string>();

            foreach (TypeDef Type in Module.GetTypes())
            {
                foreach (MethodDef Method in Type.Methods)
                {
                    if (!Method.HasBody) continue;

                    foreach (Instruction Instruction in Method.Body.Instructions)
                        if (Instruction.OpCode == OpCodes.Ldstr)
                            Strings.Add(Instruction.Operand as string);

                }
            }
            
            foreach (string String in Strings.ToList())
            {
                try
                {
                    Console.WriteLine("Found String : " + Decrypt(Decode(String)), Console.ForegroundColor = ConsoleColor.Green);
                    Strings.Remove(String);
                }
                catch (FormatException)
                {
                    Strings.Remove(String);
                    continue;
                }
            }

            Console.ReadLine();
        }
    }
}
