using System.Collections.Generic;

namespace MazeDemo
{
    public class CustomCollection<T>
    {
        public List<T> List { get; set; }

        public List<bool> Ready { get; set; }

        public CustomCollection()
        {
            List = new List<T>();
            Ready = new List<bool>();
        }

        public void Refresh()
        {
            List<bool> ready = new List<bool>();
            List<T> list = new List<T>();

            for (int i = 0; i < List.Count; i++)
            {
                if (Ready[i] == true)
                {
                    ready.Add(Ready[i]);
                    list.Add(List[i]);
                }
            }
            Ready = ready;
            this.List = list;
        }

        public void Add(T element)
        {
            this.List.Add(element);
            Ready.Add(true);
        }

        public void RemoveAll()
        {
            List.Clear();
            Ready.Clear();
        }
    }
}
