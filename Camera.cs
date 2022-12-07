using System;

namespace RevitGltfExporter
{
    public class Camera
    {
        public readonly string name = "camera";
        public readonly string type = "perspective";
        public Perspective perspective => new Perspective();

        public class Perspective
        {
            public readonly float aspectRatio = 1.5F;
            public readonly double yfov = 75 * Math.PI / 180;
            public readonly float znear = 0.01F;
            public readonly float zfar = 1000;
        }
    }
}
