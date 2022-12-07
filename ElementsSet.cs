using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGltfExporter
{
    public class ElementsSet<T>
    {
        private List<T> elements = new List<T>();

        public ElementsSet()
        {
        }

        public int add(T element)
        {
            elements.Add(element);
            return elements.Count - 1;
        }

        public void add(List<T> elements)
        {
            this.elements.AddRange(elements);
        }

        public T at(int index)
        {
            Debug.Assert((0 <= index) && (index < elements.Count), "Elementset::get: Element index out of range");
            return elements[index];
        }

        public List<T> toList()
        {
            return elements;
        }

        virtual public void dispose()
        {
            elements.Clear();
        }
    }
}
