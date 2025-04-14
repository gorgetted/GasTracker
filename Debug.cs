namespace GasTracker {
    internal class Debug {
        private static string _path = @"D:\debug.txt";

        public static void write(string message) {
            File.AppendAllText(_path, $"{DateTime.Now:T} | {message}\n");
        }
    }
}
