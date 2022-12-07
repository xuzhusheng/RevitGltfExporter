using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RevitGltfExporter
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AlphaMode
    {
        OPAQUE,
        BLEND
    }
    public class ColorTexture
    {
        public int index;

        public ColorTexture(int index)
        {
            this.index = index;
        }
    }
    public struct PbrMetallicRoughness
    {
        public double metallicFactor;
        public double roughnessFactor;
        public ColorTexture baseColorTexture;
        public double[] baseColorFactor;
    }
    interface IPbrMaterial
    {
        string name { get; }

        bool doubleSided { get; }

        AlphaMode alphaMode { get; }

        PbrMetallicRoughness pbrMetallicRoughness { get; }
    }

    partial class RenderingMaterial : IPbrMaterial
    {
        public string name => null != _name ? _name : _id.ToString();

        public bool doubleSided => !backfaceCull;

        public AlphaMode alphaMode => transparency < Double.PositiveInfinity ? AlphaMode.OPAQUE : AlphaMode.BLEND;

        public PbrMetallicRoughness pbrMetallicRoughness
        {
            get
            {
                var ret = new PbrMetallicRoughness();
                ret.metallicFactor = isMetal ? 1.0 : 0.0;
                ret.roughnessFactor = 1 - glossiness;
                if (null != diffuseImageTexture)
                {
                    ret.baseColorTexture = new ColorTexture(Gltf.Instance.indexOfTexture(diffuseImageTexture.path));
                }
                else
                {
                    var color = diffuseColor != null ? diffuseColor : _color;
                    ret.baseColorFactor = new double[] { color[0], color[1], color[2], 1 - transparency };
                }
                return ret;
            }
        }
    }
}