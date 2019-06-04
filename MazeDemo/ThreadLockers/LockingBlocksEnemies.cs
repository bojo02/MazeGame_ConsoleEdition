using System.Threading.Tasks;

namespace MazeDemo
{
    public class LockingBlocksEnemies
    {
        private static readonly object obj = new object();

        private static bool _doing;

        public static void Start()
        {
            lock (obj)
            {
                _doing = true;
                Task tsk = new Task(() =>
                {
                    while (true)
                    {

                        lock (obj)
                        {
                            while (_doing)
                            {

                            }
                        }
                    }
                });
            }
        }

        public static void End()
        {
            lock (obj)
            {
                _doing = false;
            }
        }

        public static bool State()
        {
            return _doing;
        }
    }
}