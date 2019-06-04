
namespace MazeDemo
{
    public class Sequence
    {
        private static readonly object obj = new object();

        private static bool locker;
        private static void Locking()
        {
            lock (obj)
            {
                while (locker)
                {
                   //JUST LOCKING OTHER PROCESS
                }
            }
        }

        public static void Lock()
        {
            locker = true;
        }

        public static void Unlock()
        {
            locker = false;
        }
    }
}
