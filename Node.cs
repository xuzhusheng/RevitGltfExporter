using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace RevitGltfExporter
{
    public class Node 
    {
        private int _id;
        [JsonIgnore]
        public int id
        {
            get => _id;
            set
            {
                if (isMesh && !_meshId.HasValue) _meshId = _id;
                _id = value;
            }
        }
        private string _name;
        [JsonIgnore]
        public string shortname
        {
            get
            {
                return _name;
            }
        }

        [JsonIgnore]
        public string uuid;

        [JsonIgnore]
        public NodeType type;

        [JsonIgnore]
        public Transform transform
        {
            set; get;
        }

        public int? camera;

        private List<Node> _children;

        public bool hasChild()
        {
            return (_children != null) && 0 < (_children.Count);
        }

        public int? mesh
        {
            get
            {
                if (isMesh) return Gltf.Instance.indexOfMesh(_meshId.HasValue ? _meshId.Value : id);

                return null;
            }
        }

        private int? _meshId;

        [JsonIgnore]
        public bool isMesh = false;


        public object extras
        {
            get
            {
                if (type != NodeType.Element) return null;
                if (-1 == id) return null;
                return new Dictionary<string, object>() { { "id", id }, {"uuid", uuid } };
            }
        }

        public string name
        {
            get
            {
                if ( id == -1 ) return _name;

                return string.Format("{0}-{1}", _name, id);
            }
            set
            {
                _name = value;
            }
        }

        [JsonIgnore]
        public bool isEmpty
        {
            get
            {
                return ((!isMesh) && (null == _children) && (type != NodeType.Camera));
            }
        }
        public double[] matrix
        {
            get
            {
                if ((null == transform) || transform.IsIdentity || (transform.Determinant == 0))  //Determinat is 0, the fransform could not decompose to trs, probably, should ignore the node
                    return null;

                return new double[16] { transform.BasisX.X, transform.BasisX.Y, transform.BasisX.Z, 0, transform.BasisY.X, transform.BasisY.Y, transform.BasisY.Z, 0, transform.BasisZ.X, transform.BasisZ.Y, transform.BasisZ.Z, 0, transform.Origin.X, transform.Origin.Y, transform.Origin.Z, 1 };
            }
        }

        public Node(int id, string uuid, string name, NodeType type, Transform transform = null)
        {
            this._id = id;
            this.uuid = uuid;
            this._name = name;
            this.type = type;
            this.transform = transform;
        }

        public Node(Node node)
        {
            this._id = node.id;
            this.uuid = node.uuid;
            this._name = node._name;
            this.type = node.type;
            this.transform = node.transform;
            this._meshId = node._meshId;
            this.isMesh = node.isMesh;
            if (!node.hasChild()) return;

            _children = new List<Node>();
            foreach (Node child in node.getChildren())
            {
                _children.Add(new Node(child));
            }
        }

        public List<Node> toList()
        {
            List<Node> ret = new List<Node>();
            ret.Add(this);

            if (!this.hasChild()) return ret;

            foreach (Node child in this.getChildren())
            {
                ret.AddRange(child.toList());
            }

            return ret;
        }

        public Node AddChild(Node node)
        {
            if (null == _children)
                _children = new List<Node>();

            _children.Add(node);

            return this;
        }

        [JsonIgnore]
        public int index;

        public void filterEmptyChildren()
        {
            if (null == _children) return;

            _children = _children.Where((node) => !(node as Node).isEmpty).ToList();
        }

        public Node GetFirstChild()
        {
            return _children[0];
        }

        List<Node> getChildren()
        {
            return _children;
        }

        public List<int> children
        {
            get
            {
                if ((null == _children) || (_children.Count == 0)) return null;

                return _children.Select(node => (node as Node).index).ToList();
            }
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum NodeType
    {
        Element,
        FamilyInstance,
        Link,
        Camera
    }
}