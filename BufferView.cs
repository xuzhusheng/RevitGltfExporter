using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGltfExporter
{
    public class BufferView
    {
        [JsonIgnore]
        public readonly string id;
        public int buffer = 0;
        public long byteOffset = 0;
        public readonly int? byteStride;
        public readonly BufferViewTarget target;

        public long byteLength = 0;

        public BufferView(string id, BufferViewTarget target, int? byteStride = null)
        {
            this.id = id;
            this.byteStride = byteStride;
            this.target = target;
        }
    }

    public enum BufferViewTarget
    {
        ARRAY_BUFFER = 34962,
        ELEMENT_ARRAY_BUFFER = 34963
    }
}
