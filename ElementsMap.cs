using System.Collections.Generic;
using System.Diagnostics;

namespace RevitGltfExporter
{
    public class ElementsMap<ID, T> : ElementsSet<T>
    {
        private Dictionary<ID, int> map = new Dictionary<ID, int>();

        public int indexOf(ID id)
        {
            if(map.ContainsKey(id))
                return map[id];

            return -1;
        }

        public T getById(ID id)
        {
            return at(map[id]);
        }

        public bool exists(ID id)
        {
            return map.ContainsKey(id);
        }

        public int add(ID id, T element)
        {
            if (map.ContainsKey(id))
            {
                return indexOf(id);
            }

            int index = base.add(element);
            map.Add(id, index);
            return index;

        }

        override public void dispose()
        {
            map.Clear();
            base.dispose();
        }

    }
}
