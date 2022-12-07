using Autodesk.Revit.DB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RevitGltfExporter
{
    class Geometry
    {
        static private Dictionary<int, Dictionary<int, List<Geometry>>> geometries = new Dictionary<int, Dictionary<int, List<Geometry>>>();

        static public void addGeometry(int id, int material, PolymeshTopology node)
        {
            if (!geometries.ContainsKey(id)) geometries.Add(id, new Dictionary<int, List<Geometry>>());

            if (!geometries[id].ContainsKey(material))
                geometries[id].Add(material, new List<Geometry>());

            if((geometries[id][material].Count == 0) || geometries[id][material].Last().isTooLarge())
                geometries[id][material].Add(new Geometry(material));

            geometries[id][material].Last().Add(node);
        }

        static public void export()
        {
            //List<Task> tasks = new List<Task>();
            foreach(var g in geometries)
            {
                if (g.Value.Count == 0) continue;

                export(g.Key, g.Value);
            }
            geometries.Clear();
        }

        static void exportGeometry(int id, int material, Geometry geometry)
        {
            if (geometry.indices.Length == 0) return;
            List<Task> tasks = new List<Task>();
            Task<Accessor> indicesAccessorTask = Task.Run(() => new Accessor(id, AccessorType.INDICES, geometry.indices, geometry.indeiceComponentType, ElementType.SCALAR, geometry.maxIndex, geometry.minIndex));
            tasks.Add(indicesAccessorTask);
            Task<Accessor> pointAccessorTask = Task.Run(() => new Accessor(id, AccessorType.POINTS, geometry.verters, ComponentType.FLOAT, ElementType.VEC3, geometry.maxVerter, geometry.minVerter));
            tasks.Add(pointAccessorTask);
            Task<Accessor> normalAccessorTask = Task.Run(() => new Accessor(id, AccessorType.NORMALS, geometry.normals, ComponentType.FLOAT, ElementType.VEC3));
            tasks.Add(normalAccessorTask);
            Task<Accessor> uvAccessorTask = null;
            if ((material != -1) && Gltf.Instance.getMaterial(material).hasTexture)
            {
                uvAccessorTask = Task.Run(() => new Accessor(id, AccessorType.UVS, geometry.uvs, ComponentType.FLOAT, ElementType.VEC2));
                tasks.Add(uvAccessorTask);
            }

            Task.WaitAll(tasks.ToArray());
            Accessor indicesAccessor = indicesAccessorTask.Result;
            if (!Gltf.Instance.existAccessor(indicesAccessor.id))
            {
                Gltf.Instance.add(new BufferView(indicesAccessor.bufferViewId, BufferViewTarget.ELEMENT_ARRAY_BUFFER));
                Gltf.Instance.add(indicesAccessor);
            }

            Accessor positionAccessor = pointAccessorTask.Result;
            if (!Gltf.Instance.existAccessor(positionAccessor.id))
            {
                Gltf.Instance.add(new BufferView(positionAccessor.bufferViewId, BufferViewTarget.ARRAY_BUFFER, 12));
                Gltf.Instance.add(positionAccessor);
            }

            Accessor normalAccessor = normalAccessorTask.Result;
            if (!Gltf.Instance.existAccessor(normalAccessor.id))
            {
                //Gltf.Instance.add(new BufferView(positionAccessor.bufferViewId, BufferViewTarget.ARRAY_BUFFER, 12));
                Gltf.Instance.add(normalAccessor);
            }

            Accessor uvAccessor = null;
            if ((material != -1) && Gltf.Instance.getMaterial(material).hasTexture)
            {
                uvAccessor = uvAccessorTask.Result;
                if (!Gltf.Instance.existAccessor(uvAccessor.id))
                {
                    Gltf.Instance.add(new BufferView(uvAccessor.bufferViewId, BufferViewTarget.ARRAY_BUFFER, 8));
                    Gltf.Instance.add(uvAccessor);
                }
            }

            Gltf.Instance.add(new Primitive(id, indicesAccessor.id, positionAccessor.id, normalAccessor.id, material, uvAccessor != null ? uvAccessor.id : null));
        }

        static public void export(int id, Dictionary<int, List<Geometry>> geometries)
        {
            foreach (int material in geometries.Keys)
            {
                foreach(Geometry geometry in geometries[material])
                {
                    exportGeometry(id, material, geometry);
                }
            }
        }

        //static public void export(int id, Dictionary<int, Geometry> geometries)
        //{
        //    //List<Task> tasks = new List<Task>();
        //    foreach (int material in geometries.Keys)
        //    {
        //        exportGeometry(id, material, geometries);
        //    }
        //}

        public int material;
        private List<int[]> _indices = new List<int[]>();
        private List<float[]> _verters = new List<float[]>();
        private List<float[]> _normals = new List<float[]>();
        private List<float[]> _uvs = new List<float[]>();
        private int vertersCount = 0;
        private int facetsCount = 0;
        private const int MAX_FACETS_COUNT = 10000000;

        private Geometry(int material)
        {
            this.material = material;
        }

        public int[] indices => _indices.SelectMany(indices => indices).ToArray();

        public float[] verters => _verters.SelectMany(verters => verters).ToArray();
        public float[] normals => _normals.SelectMany(normals => normals).ToArray();

        public float[] uvs => _uvs.SelectMany(uvs => uvs).ToArray();
        public float[] maxVerter => new[] { verters.Where((item, index) => 0 == index % 3).Max(), verters.Where((item, index) => 1 == index % 3).Max(), verters.Where((item, index) => 2 == index % 3).Max() };
        public float[] minVerter => new[] { verters.Where((item, index) => 0 == index % 3).Min(), verters.Where((item, index) => 1 == index % 3).Min(), verters.Where((item, index) => 2 == index % 3).Min() };
        public int[] maxIndex => new int[] { indices.Max() };
        public int[] minIndex => new int[] { indices.Min() };

        private ComponentType indeiceComponentType
        {
            get
            {
                if (maxIndex[0] < byte.MaxValue) return ComponentType.UNSIGNED_BYTE;
                return maxIndex[0] < ushort.MaxValue ? ComponentType.UNSIGNED_SHORT : ComponentType.UNSIGNED_INT;
            }
        }

        private bool isTooLarge()
        {
            return MAX_FACETS_COUNT < facetsCount;
        }

        //sum x, y, z seperatly instead of call XYZ.add for better performance
        XYZ sum(HashSet<XYZ> set)
        {

            double x = 0;
            double y = 0;
            double z = 0;

            foreach(XYZ value in set)
            {
                x += value.X;
                y += value.Y;
                z += value.Z;
            }

            return new XYZ(x, y, z);
        }
        private Task<float[]> getNormals(PolymeshTopology topology)
        {
            return Task.Run(() =>
            {
                switch (topology.DistributionOfNormals)
                {
                    case DistributionOfNormals.OnEachFacet:
                        Debug.Assert(topology.NumberOfFacets == topology.NumberOfNormals);
                        HashSet<XYZ>[] normals = Enumerable.Repeat(new HashSet<XYZ>(new XYZComparer()), topology.NumberOfPoints).ToArray();
                        IList<PolymeshFacet> facets = topology.GetFacets();
                        IList<XYZ> facetsNormals = topology.GetNormals();

                        for (int i = 0; i < facets.Count; i++)
                        {
                            PolymeshFacet facet = facets[i];
                            XYZ normal = facetsNormals[i].Normalize();
                            normals[facet.V1].Add(normal);
                            normals[facet.V2].Add(normal);
                            normals[facet.V3].Add(normal);
                        }

                        return normals.AsParallel().AsOrdered().Select(set => sum(set).Normalize()).SelectMany(normal => new[] { (float)normal.X, (float)normal.Y, (float)normal.Z }).ToArray();


                    case DistributionOfNormals.OnePerFace:
                        Debug.Assert(1 == topology.NumberOfNormals);
                        //return Enumerable.Range(0, topology.NumberOfPoints).Select(_ => topology.GetNormal(0).Normalize()).SelectMany(normal => new[] { (float)normal.X, (float)normal.Y, (float)normal.Z }).ToArray();
                        return Enumerable.Repeat(topology.GetNormal(0).Normalize(), topology.NumberOfPoints).SelectMany(normal => new[] { (float)normal.X, (float)normal.Y, (float)normal.Z }).ToArray();

                    default:
                        Debug.Assert(topology.NumberOfPoints == topology.NumberOfNormals);
                        return topology.GetNormals().Select(normal => normal.Normalize()).SelectMany(normal => new[] { (float)normal.X, (float)normal.Y, (float)normal.Z }).ToArray();
                }
            });
        }

        private void Add(PolymeshTopology topology)
        {
            facetsCount += topology.NumberOfFacets;
            var facets = topology.GetFacets();
            var points = topology.GetPoints();
            var uvs = topology.GetUVs();
            int count = topology.NumberOfPoints;

            List<Task> tasks = new List<Task>();
            Task<int[]> indicesTask = Task.Run(() => facets.SelectMany(facet => new[] { facet.V1, facet.V2, facet.V3 }).Select(index => index + vertersCount).ToArray());
            tasks.Add(indicesTask);
            Task<float[]> vertersTask = Task.Run(() => points.SelectMany(point => new[] { (float)point.X, (float)point.Y, (float)point.Z }).ToArray());
            tasks.Add(vertersTask);
            Task<float[]> normalsTask = getNormals(topology);
            tasks.Add(normalsTask);
            Task<float[]> uvsTask = null;
            if ((-1 != material) && Gltf.Instance.getMaterial(material).hasTexture)
            {
                uvsTask = Task.Run(() =>
                {
                    Texture t = Gltf.Instance.getMaterial(material).diffuseImageTexture;
                    //double w = UnitUtils.ConvertToInternalUnits(t.width, Texture.unit);
                    //double h = UnitUtils.ConvertToInternalUnits(t.height, Texture.unit);
                    return uvs.SelectMany(uv => new[] { (float)(uv.U / t.width), -(float)(uv.V / t.height) }).ToArray();
                });
                tasks.Add(uvsTask);
            }

            Task.WaitAll(tasks.ToArray());

            _indices.Add(indicesTask.Result);

            _verters.Add(vertersTask.Result);
            _normals.Add(normalsTask.Result);
            vertersCount += count;

            if(null != uvsTask) _uvs.Add(uvsTask.Result);
        }
    }

    class XYZComparer : IEqualityComparer<XYZ>
    {
        public bool Equals(XYZ x, XYZ y)
        {
            return x.IsAlmostEqualTo(y);
        }

        public int GetHashCode(XYZ obj)
        {
            return obj.GetHashCode();
        }
    }
}