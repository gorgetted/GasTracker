using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasTracker {
    internal class Debug {
        private static string _path = @"D:\debug.txt";

        public static void write(string message) {
            File.AppendAllText(_path, $"{DateTime.Now:T} | {message}\n");
        }
    }
}
