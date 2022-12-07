using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RevitElement = Autodesk.Revit.DB.Element;
using System.Threading;

namespace RevitGltfExporter
{
    public class ExportContext : IExportContext
    {
        private Document doc;
        public Node root;
        public Stack<Node> nodeStack;
        private FinishHandler onFinish;
        public delegate void FinishHandler();
        private int currentMaterial;
        private Gltf gltf;
        private Node camera = new Node(-1, null, "camera", NodeType.Camera);
        private Node scene = new Node(-1, null, "scene", NodeType.Element);
        //private Dictionary<int, Node> instances = new Dictionary<int, Node>();
        private Dictionary<int, Node> elements = new Dictionary<int, Node>();
        private int levelOfDetail = -1;

        private Node currentNode => nodeStack.Peek();

        public ExportContext(Document doc, Gltf gltf, FinishHandler onFinish, int levelOfDetail = -1)
        {
            this.doc = doc;
            this.gltf = gltf;
            this.onFinish = onFinish;
            this.levelOfDetail = levelOfDetail;
        }

        public void Finish()
        {
            onFinish();
            this.gltf = null;
            this.doc = null;
            //throw new NotImplementedException();
        }

        public bool IsCanceled()
        {
            return false;
        }

#if REVIT2016
        public void OnDaylightPortal(DaylightPortalNode node)
        {
            //throw new NotImplementedException();
        }
#endif

        public RenderNodeAction OnElementBegin(ElementId elementId)
        {
            if (elements.ContainsKey(elementId.IntegerValue)) return RenderNodeAction.Skip;

            RevitElement e = doc.GetElement(elementId);
            
            Node node = new Node(e.Id.IntegerValue, e.UniqueId, e.Name, NodeType.Element);

            nodeStack.Push(node);
            
            int index = Gltf.Instance.indexOfMesh(e.Id.IntegerValue);
            if (-1 != index)
            {
                node.isMesh = true;
                return RenderNodeAction.Skip;
            }

            return RenderNodeAction.Proceed;
        }

        public void OnElementEnd(ElementId elementId)
        {
            if (elements.ContainsKey(elementId.IntegerValue)) return;

            Geometry.export();

            Node n = nodeStack.Pop();
            elements.Add(elementId.IntegerValue, n);
            if (n.isEmpty) return;

            //if ((!n.isMesh) && (n.children.Count == 1))
            //{
            //    Node child = n.GetFirstChild();
            //    child.id = n.id;
            //    child.uuid = n.uuid;
            //    child.type = n.type;
            //    child.name = n.shortname;
            //    if ((null != n.transform) && !n.transform.IsIdentity)
            //        child.transform = n.transform.Multiply(child.transform);
            //    currentNode.AddChild(child);
            //    return;
            //}

            gltf.add(n);     
            currentNode.AddChild(n);
        }

        public RenderNodeAction OnFaceBegin(FaceNode node)
        {
            return RenderNodeAction.Proceed;
        }

        public void OnFaceEnd(FaceNode node)
        {
            //throw new NotImplementedException();
        }

        public RenderNodeAction OnInstanceBegin(InstanceNode node)
        {
            ElementId symbolId = node.GetSymbolId();
            if (elements.ContainsKey(symbolId.IntegerValue))
            {
                if(elements[symbolId.IntegerValue].isEmpty) return RenderNodeAction.Skip;

                Node copied = new Node(elements[symbolId.IntegerValue]);
                copied.transform = node.GetTransform();
                gltf.add(copied.toList());
                currentNode.AddChild(copied);
                return RenderNodeAction.Skip;
            }

            //if (instances.ContainsKey(symbolId.IntegerValue)) return RenderNodeAction.Skip;

            FamilySymbol famSymbol = doc.GetElement(symbolId) as FamilySymbol;
            string name = "COMPOSEDINSTANCE";
            string uuid = null;
            if(null != famSymbol)
            {
                name = famSymbol.Name;
                uuid = famSymbol.UniqueId;
            }

            Node n = new Node(node.GetSymbolId().IntegerValue, uuid, name, NodeType.FamilyInstance, node.GetTransform());

            nodeStack.Push(n);
            int index = Gltf.Instance.indexOfMesh(symbolId.IntegerValue);
            if (-1 != index)
            {
                n.isMesh = true;
                return RenderNodeAction.Skip;
            }

            return RenderNodeAction.Proceed;
        }

        public void OnInstanceEnd(InstanceNode node)
        {
            int id = node.GetSymbolId().IntegerValue;
            if (elements.ContainsKey(id)) return;
            
            Geometry.export();

            Node n = nodeStack.Pop();
            elements.Add(id, n);
            if (n.isEmpty) return;

            //if ((!n.isMesh) && (n.children.Count == 1))
            //{
            //    Node child = n.GetFirstChild();
            //    child.id = n.id;
            //    child.uuid = n.uuid;
            //    child.type = n.type;
            //    child.name = n.shortname;
            //    if ((null != n.transform) && !n.transform.IsIdentity)
            //        child.transform = n.transform.Multiply(child.transform);
            //    currentNode.AddChild(child);
            //    return;
            //}
            gltf.add(n);
            currentNode.AddChild(n);
            
        }

        public void OnLight(LightNode node)
        {
            //throw new NotImplementedException();
        }

        public RenderNodeAction OnLinkBegin(LinkNode node)
        {
            ElementId symbolId = node.GetSymbolId();
            if (elements.ContainsKey(symbolId.IntegerValue))
            {
                if (elements[symbolId.IntegerValue].isEmpty) return RenderNodeAction.Skip;

                Node copied = new Node(elements[symbolId.IntegerValue]);
                copied.transform = node.GetTransform();
                gltf.add(copied.toList());
                currentNode.AddChild(copied);
                return RenderNodeAction.Skip;
            }

            RevitLinkType linkType = doc.GetElement(symbolId) as RevitLinkType;
            Node n = new Node(node.GetSymbolId().IntegerValue, linkType.UniqueId, linkType.Name, NodeType.Link, node.GetTransform());
            nodeStack.Push(n);

            int index = Gltf.Instance.indexOfMesh(symbolId.IntegerValue);
            if (-1 != index)
            {
                n.isMesh = true;
                return RenderNodeAction.Skip;
            }

            return RenderNodeAction.Proceed;
        }

        public void OnLinkEnd(LinkNode node)
        {
            int id = node.GetSymbolId().IntegerValue;
            if (elements.ContainsKey(id)) return;

            Node n = nodeStack.Pop();
            elements.Add(id, n);
            if (n.isEmpty) return;

            //if ((!n.isMesh) && (n.children.Count == 1))
            //{
            //    Node child = n.GetFirstChild();
            //    child.id = n.id;
            //    child.uuid = n.uuid;
            //    child.type = n.type;
            //    child.name = n.shortname;
            //    if ((null != n.transform) && !n.transform.IsIdentity)
            //        child.transform = n.transform.Multiply(child.transform);
            //    currentNode.AddChild(child);
            //    return;
            //}

            gltf.add(n);
            currentNode.AddChild(n);
        }

        public void OnMaterial(MaterialNode node)
        {
            try
            {
                currentMaterial = MaterialExporter.export(doc, node);
            }
            catch
            {
                currentMaterial = -1;
                Console.WriteLine("material export error: " + node.MaterialId.ToString());
            }
            
        }

        public void OnPolymesh(PolymeshTopology node)
        {
            currentNode.isMesh = true;
            gltf.add(new Mesh(currentNode.id));        
            Geometry.addGeometry(currentNode.id, currentMaterial, node);
        }

        public void OnRPC(RPCNode node)
        {
            //throw new NotImplementedException();
        }

        public RenderNodeAction OnViewBegin(ViewNode node)
        {
            View3D view = doc.GetElement(node.ViewId) as View3D;
            view.DetailLevel = ViewDetailLevel.Fine;
            view.DisplayStyle = DisplayStyle.Rendering;
            //Transform transform = Transform.CreateTranslation(view.Origin);
            //Transform transform = new Transform(view.CropBox.Transform);
            //Transform transform = Transform.Identity;
            //transform.BasisX = view.RightDirection;
            //transform.BasisY = view.UpDirection;
            //transform.BasisZ = view.ViewDirection;
            //transform.Origin = new XYZ();

            //Transform transform = new Transform(view.CropBox.Transform);
            ////transform.Origin = view.GetSectionBox().Transform.Origin;
            //Transform ci = transform.Inverse.ScaleBasisAndOrigin(0.3048);
            //currentNode.transform = ci;
            node.LevelOfDetail = this.levelOfDetail;
            Debug.WriteLine("level of detail: {0}", node.LevelOfDetail);
            currentNode.transform = view.GetSectionBox().Transform.Inverse;
            camera.transform = view.CropBox.Transform;
            camera.transform.Origin = camera.transform.Origin.Add(currentNode.transform.Origin);
            Transform transform = Transform.CreateRotation(new XYZ(1, 0, 0), -Math.PI / 2);

            scene.transform = transform.ScaleBasis(0.3048);
            Debug.WriteLine("view begin");
            return RenderNodeAction.Proceed;
        }

        public void OnViewEnd(ElementId elementId)
        {
            Debug.WriteLine("view end");
            //throw new NotImplementedException();
        }

        public bool Start()
        {
            Debug.WriteLine("start");
            gltf.add(scene);
            camera.camera = 0;
            gltf.add(camera);
            scene.AddChild(camera);
            Node node = new Node(-1, null, "root", NodeType.Element);
            int nodeIndex = gltf.add(node);
            scene.AddChild(node);
            nodeStack = new Stack<Node>();
            nodeStack.Push(node);
            return true;
        }
    }
}
