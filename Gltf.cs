using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitGltfExporter
{
    public class Gltf
    {
        private ElementsSet<Node> _nodes;
        private ElementsMap<int, Mesh> _meshes;
        private ElementsMap<string, Accessor> _accessors;
        private ElementsMap<string, BufferView> _bufferViews;
        private ElementsMap<int, RenderingMaterial> _materials;
        private ElementsMap<string, Image> _images;
        private ConcurrentDictionary <int, List<Primitive>> _primitives;

        public int scene = 0;

        public Dictionary<string, string> asset
        {
            get
            {
                return new Dictionary<string, string> { { "generator", "gltf generator" }, { "version", "2.0" }, { "copyright", "2019 (p) jason" } };
            }
        }

        public string uri { set; private get; }

        private long byteLength;

        public Buffer[] buffers => new Buffer[1] { new Buffer(uri, byteLength) };

        public Dictionary<string, object>[] scenes
        {
            get
            {
                return new Dictionary<string, object>[] { new Dictionary<string, object> { { "nodes", new int[] { 0 } }, { "name", "Scene" } } };
            }
        }
        
        public int add(Image img)
        {
            return _images.add(img.source, img);
        }

        public int add(Node node)
        {
            return _nodes.add(node);
        }

        public void add(List<Node> nodes)
        {
            _nodes.add(nodes);
            return;
        }

        public int add(Accessor accessor)
        {
            return _accessors.add(accessor.id, accessor);
        }

        public int indexOfAccessor(string id)
        {
            return _accessors.indexOf(id);
        }

        public bool existAccessor(string id)
        {
            return _accessors.exists(id);
        }

        public int add(BufferView bufferView)
        {
            return _bufferViews.add(bufferView.id, bufferView);
        }

        public BufferView getBufferView(int index)
        {
            return _bufferViews.at(index);
        }

        public int indexOfBufferView(string id)
        {
            return _bufferViews.indexOf(id);
        }

        public int add(Mesh mesh)
        {
            return _meshes.add(mesh.id, mesh);
        }

        public int indexOfMesh(int id)
        {
            return _meshes.indexOf(id);
        }

        public int indexOfTexture(string id)
        {
            return _images.indexOf(id);
        }
        public Mesh meshAt(int index)
        {
            return _meshes.at(index);
        }

        public int add(RenderingMaterial material)
        {
            return _materials.add(material.id, material);
        }

        public RenderingMaterial getMaterial(int index)
        {
            return _materials.at(index);
        }

        public int getMaterialIndex(int id)
        {
            return _materials.indexOf(id);
        }

        public void add(Primitive primitive)
        {
            if (!_primitives.ContainsKey(primitive.meshId))
                _primitives.TryAdd(primitive.meshId, new List<Primitive>());

            _primitives[primitive.meshId].Add(primitive);
        }

        public List<Primitive> primitivesOf(int meshId)
        {
            if(_primitives.ContainsKey(meshId))
                return _primitives[meshId];

            return null;
        }

        private Gltf()
        {
            _nodes = new ElementsSet<Node>();
            _meshes = new ElementsMap<int, Mesh>();
            _accessors = new ElementsMap<string, Accessor>();
            _bufferViews = new ElementsMap<string, BufferView>();
            _materials = new ElementsMap<int, RenderingMaterial>();
            _images = new ElementsMap<string, Image>();
            _primitives = new ConcurrentDictionary<int, List<Primitive>>();
        }

        static private Gltf _instance;
        static public Gltf Instance
        {
            get
            {
                return _instance;
            }
        }
        static public void reset()
        {
            _instance = new Gltf();
        }

        private void generateModel()
        {

            //foreach (Mesh mesh in _meshes.toList())
            //{
            //    mesh.generateMesh();
            //}

            //foreach(Node node in nodes.toList())
            //{
            //    node.filterEmptyChildren();
            //}

            //filteredNodes = nodes.toList().Where((node) => !(node as Node).isEmpty).ToList();
            //filteredNodes = _nodes.toList();

            //int i = 0;
            //foreach (Node node in filteredNodes)
            //{
            //    node.index = i;
            //    i++;
            //}

            //Debug.WriteLine("nodes count: " + filteredNodes.Count);

            Debug.WriteLine("accessors count: " + _accessors.toList().Count);
            foreach(Accessor accessor in _accessors.toList())
            {
                accessor.saveBinary();
            }

            //foreach (BufferView bufferView in bufferViews.toList())
            //{
            //    bufferView.saveToBuffer(bufferStream);
            //}

            //byteLength = bufferStream.Length;
        }

        public void CopyImages(string targetPath)
        {
            if (_images.toList().Count == 0) return;

            Directory.CreateDirectory(Path.Combine(targetPath, "textures"));
            List<string> missingImages = new List<string>();

            foreach (Image image in _images.toList())
            {
                string dest = Path.Combine(targetPath, image.uri);
                try
                {
                    File.Copy(image.source, dest, true);
                }
                catch (IOException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public void save(string path, string name)
        {
            uri = name + ".bin";
            generateModel();
            CopyImages(path);
            saveBin(path);
            //saveBuffers(path, name);
            saveGltf(path, name);
            dispose();
        }

        public void dispose()
        {
            _nodes.dispose();
            _nodes = null;
            _meshes.dispose();
            _meshes = null;
            _accessors.dispose();
            _accessors = null;
            _bufferViews.dispose();
            _bufferViews = null;
            _materials.dispose();
            _materials = null;
            //images.dispose();
            _images = null;
            _instance = null;
        }

        private void saveBin(string path)
        {
            string file = Path.Combine(path, uri);
            foreach (BufferView bufferView in _bufferViews.toList())
            {
                bufferView.byteOffset = byteLength;
                byteLength += bufferView.byteLength;
            }
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(file, FileMode.Create, "null", byteLength))
            {
                foreach (Accessor accessor in _accessors.toList())
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream(accessor.offset, accessor.byteLength))
                    {
                        stream.Write(accessor.bytes, 0, accessor.bytes.Length);
                    }
                }
            }
        }

        public List<Accessor> accessors => _accessors.toList();
        public List<Mesh> meshes => _meshes.toList();
        public List<BufferView> bufferViews => _bufferViews.toList();
        public List<RenderingMaterial> materials => _materials.toList();
        public List<Node> nodes
        {
            get
            {
                List<Node> ret = _nodes.toList();
                int i = 0;
                foreach (Node node in ret)
                {
                    node.index = i;
                    i++;
                }

                return ret;
            }
        }

        public List<Image> images
        {
            get
            {
                if (0 < _images.toList().Count) return _images.toList();

                return null;
            }
        }

        public List<Dictionary<string, int>> textures
        {
            get
            {
                if (_images.toList().Count == 0) return null;

                List<Dictionary<string, int>> ret = new List<Dictionary<string, int>>();
                for (int i = 0; i < _images.toList().Count; i++)
                {
                    ret.Add(new Dictionary<string, int> { { "source", i } });
                }

                return ret;
            }
        }

        public List<Camera> cameras => new List<Camera>() { new Camera() };

        private void saveGltf(string path, string name)
        {
            Debug.WriteLine("meshes count: " + this.meshes.Count);
            string f = Path.Combine(path, name + ".gltf");
            using (StreamWriter file = File.CreateText(f))
            {
                
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                settings.ContractResolver = new InterfaceContractResolver(typeof(IPbrMaterial));
                //settings.Formatting = Formatting.Indented;

                file.Write(JsonConvert.SerializeObject(this, settings));
            }
        }
    }

    public struct Buffer
    {
        public string uri;
        public long byteLength;

        public Buffer(string uri, long byteLength)
        {
            this.uri = uri;
            this.byteLength = byteLength;
        }
    }
}
