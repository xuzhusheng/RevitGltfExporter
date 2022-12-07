using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RevitGltfExporter
{
    public class Primitive
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Attribute
        {
            POSITION,
            NORMAL,
            TEXCOORD_0
        };

        [JsonIgnore]
        public readonly int meshId;
        private string _indicesId;
        private string _positionId;
        private string _normalId;
        private string _uvId;

        public int indices => Gltf.Instance.indexOfAccessor(_indicesId);
        private int position => Gltf.Instance.indexOfAccessor(_positionId);
        private int normal => Gltf.Instance.indexOfAccessor(_normalId);
        public int? material;
        private int? uv
        {
            get
            {
                if (_uvId != null) return Gltf.Instance.indexOfAccessor(_uvId);
                return null;
            }
        }
        public Dictionary<Attribute, int> attributes
        {
            get
            {
                Dictionary<Attribute, int> ret = new Dictionary<Attribute, int> { { Attribute.POSITION, position }, { Attribute.NORMAL, normal } };
                if (material.HasValue && uv.HasValue)
                {
                    ret.Add(Attribute.TEXCOORD_0, uv.Value);
                }

                return ret;
            }
        }

        public Primitive(int meshId, string indicesId, string positionId, string normalId, int material = -1, string uvId = null)
        {
            this.meshId = meshId;
            this._indicesId = indicesId;
            this._positionId = positionId;
            this._normalId = normalId;
            if (material == -1) 
                this.material = null;
            else
            {
                this.material = material;
            }
            this._uvId = uvId;
        }
    }
}
