using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace RevitGltfExporter
{
    public class Accessor
    {
        [JsonIgnore]
        public string id;
        private int _meshId;
        public ComponentType componentType;
        public ElementType type;
        public object min;
        public object max;
        public int bufferView => Gltf.Instance.indexOfBufferView(bufferViewId);
        public long byteOffset = 0;
        public long count => _data.Length / ((int)type * countBytes(componentType));
        private byte[] _data;
        [JsonIgnore]
        public string bufferViewId => String.Format("{0}-{1}-{2}", _meshId, type, componentType);

        public Accessor(int meshId, AccessorType accessorType, object data, ComponentType componentType, ElementType type, object max = null, object min = null)
        {
            this._meshId = meshId;
            this.componentType = componentType;
            this.type = type;
            this.max = max;
            this.min = min;
            this._data = toBinary(data);
            SHA256 sha = SHA256.Create();
            id = accessorType.ToString() + type.ToString() + componentType.ToString() + Convert.ToBase64String( sha.ComputeHash(_data));
        }

        private int countBytes(ComponentType componentType)
        {
            switch(componentType)
            {
                case ComponentType.UNSIGNED_BYTE:
                case ComponentType.BYTE:
                    return 1;

                case ComponentType.UNSIGNED_SHORT:
                case ComponentType.SHORT:
                    return 2;

                default: return 4;
            }
        }

        private byte[] toBinary(object data)
        {
            switch(componentType)
            {
                case ComponentType.UNSIGNED_INT:
                    return ((int[])data).SelectMany(item => BitConverter.GetBytes(item)).ToArray();

                case ComponentType.FLOAT:
                    return ((float[])data).SelectMany(item => BitConverter.GetBytes(item)).ToArray();

                case ComponentType.UNSIGNED_SHORT:
                    return ((int[])data).SelectMany(item => BitConverter.GetBytes((ushort)item)).ToArray();

                case ComponentType.UNSIGNED_BYTE: 
                    return ((int[])data).Select(item => (byte)item).ToArray();

                default: return null;
            }
        }

        [JsonIgnore]
        public int byteLength
        {
            get
            {
                return (int)Math.Ceiling(_data.Length / 4.0) * 4;
            }
        }

        [JsonIgnore]
        public long offset
        {
            get
            {
                BufferView view = Gltf.Instance.getBufferView(bufferView);
                return view.byteOffset + byteOffset;
            }
        }

        [JsonIgnore]
        public byte[] bytes => _data;

        public void saveBinary()
        {
            BufferView view = Gltf.Instance.getBufferView(bufferView);
            byteOffset = view.byteLength;
            view.byteLength += byteLength;
            //ms.Position = 4;
            //view.Add(ms);
            //ms = null;
            //ms.Dispose();
            //ms.Close();
        }
    }

    public enum ComponentType
    {
        BYTE = 5120,
        UNSIGNED_BYTE = 5121,
        SHORT = 5122,
        UNSIGNED_SHORT = 5123,
        UNSIGNED_INT = 5125,
        FLOAT = 5126
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ElementType
    {
        SCALAR = 1,
        VEC2 = 2,
        VEC3 = 3,
        VEC4 = 4,
        MAT2 = 4,
        MAT3 = 9,
        MAT4 = 16
    }

    public enum AccessorType
    {
        INDICES,
        POINTS,
        NORMALS,
        UVS
    }
}
