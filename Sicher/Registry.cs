using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akant
{
   public class Registry
    {
        public static void  createRegistry (string b,string software) {
           Microsoft.Win32.RegistryKey key,key1;
           key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("vr");
           key1 = key.CreateSubKey(software);
           key1.SetValue("Junk", b);
           key.Close();
       }

        public static string readRegistry(string software)
        {
            Microsoft.Win32.RegistryKey key,key1;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("vr");
            key1 = key.OpenSubKey(software);
            object name = key1.GetValue("Junk");
            string n = name.ToString();
            key.Close();
            return n;
        }

        public static void deleteRegistry(string software)
        {
            Microsoft.Win32.RegistryKey key,key1;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("vr");
            key1 = key.OpenSubKey(software,true);
            key1.DeleteValue("Junk");
        }

    }
}
