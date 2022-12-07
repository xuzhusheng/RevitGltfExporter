using Autodesk.Revit.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGltfExporter
{
    public class Mesh
    {
        [JsonIgnore]
        public int id { get; private set; }
        public List<Primitive> primitives => Gltf.Instance.primitivesOf(id);

        public Mesh(int id) { this.id = id; }
    }
}
