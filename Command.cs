using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RevitGltfExporter
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        private const int LEVEL_OF_DETAIL = 8;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            SaveFileDialog dialog = new SaveFileDialog() { Filter = "gltf|*.gltf" };

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                if (null == uidoc)
                {
                    message = "No project opened";
                    return Result.Failed;
                }

                Document doc = uidoc.Document;
                if (null == doc)
                {
                    message = "No project opened";
                    return Result.Failed;
                }

                using (View3D view = doc.ActiveView as View3D)
                {
                    if (null == view)
                    {
                        message = "Current view is not a 3D view.";
                        return Result.Failed;
                    }

                    string filename = Path.GetFileNameWithoutExtension(dialog.FileName);
                    string directory = Path.GetDirectoryName(dialog.FileName);

                    RenderingMaterial.TEXTURES_PATH = "C:/Program Files (x86)/Common Files/Autodesk Shared/Materials/Textures"; //read from register

                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = "cmd.exe";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.CreateNoWindow = true;
                        process.Start();
            
                        Console.WriteLine("Eeporting....");
                        ExportGltf(filename, directory, doc, view);
                        Console.WriteLine("Completed.");
                    }
                }
                return Result.Succeeded;
            }

            return Result.Cancelled;
        }

        private static void ExportGltf(string model, string directory, Document document, View3D view)
        {
            Gltf.reset();
            Gltf gltf = Gltf.Instance;
            ExportContext exportContext = new ExportContext(document, gltf, () =>
            {
                gltf.save(directory, model);
                view.Dispose();
                document.Dispose();
                //document.Close(false);    //cause System.AccessViolationException
            }, LEVEL_OF_DETAIL);
            CustomExporter exporter = new CustomExporter(document, exportContext);
            exporter.IncludeGeometricObjects = false;
            exporter.Export(view);
        }
    }
}
