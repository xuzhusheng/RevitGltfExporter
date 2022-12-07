using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;
#if REVIT2016 || REVIT2017
using Autodesk.Revit.Utility;
#else
using Autodesk.Revit.DB.Visual;
#endif
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace RevitGltfExporter
{
    public partial class RenderingMaterial
    {
        //C:\Program Files(x86)\Common Files\Autodesk Shared\Materials\2016\assetlibrary_base.fbm\Mats\Generic\GenericUI.xml
        int _id;
        string _name;
        double[] _color;
        public double[] diffuseColor;
        public bool colorByObject;
        public Texture diffuseImageTexture;
        public bool backfaceCull = false;
        public double diffuseImageFade = 1;
        public double glossiness = 0;
        public bool isMetal = false;
        public double transparency;
        float reflectivityAt0Deg;
        float reflectivityAt90Deg;
        public float transparencyImageFade;
        public float refractionTranslucencyWeight;
        public float refractionIndex;
        public float cutoutOpacity;
        int cutoutOpacityTexture;
        //类型重新定义为float
        //double[] selfIllumLuminance;
        public float selfIllumLuminance;
        float selfIlluminationDimGlow;
        public float selfIllumColorTemperature;
        public int reflectionGlossySamples;
        public int refractionGlossySamples;
        int bumpMapTexture;
        public float bumpAmount;
        double[] tintColor;
        public string glossinessAssetName;
        public string glossinessAssetBindings;
        public float reflectivityAt0deg;
        public float reflectivityAt90deg;
        public float noiseSize;
        public string bumpMapAssetName;
        public string bumpMapBindingsName;
        public string bumpMapAssetBindings;
        public bool commonTintToggle;
        //public Color diffuse;
        Asset asset;
        //AssetSchema schema;

        static public string TEXTURES_PATH;

        public int id => _id;

        public bool hasTexture => diffuseImageTexture != null;
        string readBitmapPath(Asset asset)
        {
            dynamic property = findPropertyByName(asset, "unifiedbitmap_Bitmap");
            string value = readPropertyValue(property);
            string path = value.Split('|')[0].Trim();

            if (Path.IsPathRooted(path)) return path;
        
            DirectoryInfo dir = new DirectoryInfo(TEXTURES_PATH);
            try
            {
                return dir.GetFiles(path, SearchOption.AllDirectories).First().FullName;
            }
            catch
            {
                Console.WriteLine("missing textures: " + path);
                return null;
            }
            
        }
        Texture readTexture(Asset asset)
        {
            
            string path = readBitmapPath(asset);
            if (null == path) return null;

            Texture texture = new Texture();
            texture.path = path;

            dynamic offsetXProperty = findPropertyByName(asset, "texture_RealWorldOffsetX");
            if (null != offsetXProperty) texture.offsetX = readPropertyValue(offsetXProperty);

            dynamic offsetYProperty = findPropertyByName(asset, "texture_RealWorldOffsetY");
            if (null != offsetYProperty) texture.offsetY = readPropertyValue(offsetYProperty);

            dynamic widthProperty = findPropertyByName(asset, "texture_RealWorldScaleX");
            if (null != widthProperty) texture.width = readPropertyValue(widthProperty);

            dynamic heightProperty = findPropertyByName(asset, "texture_RealWorldScaleY");
            if (null != heightProperty) texture.height = readPropertyValue(heightProperty);

            return texture;
        }

        public RenderingMaterial(int id, string name, double[] color, double transparency)
        {
            this._id = id;
            this._name = name;
            this._color = color;
            this.transparency = transparency;
        }

        public void parseDiffuse(IAssetSchema schema)
        {
            dynamic property = findPropertyByName(schema.diffuseKey);
            if (null == property) return;

            dynamic value = readPropertyValue(property);
            if (value.GetType().IsArray)
            {
                diffuseColor = value;
            }
            //else
            //{
            //    IList<AssetProperty> assets = value;
            //    //cennectedAsset 会处理此分支, 此处无需处理
            //}
            Asset connectedAsset = findConnectedPropertyByName(property, "UnifiedBitmapSchema");
            if (null == connectedAsset) return;
            diffuseImageTexture = readTexture(connectedAsset);

            dynamic diffuseImageFadeProperty = findPropertyByName(schema.diffuseImageFadeKey);
            diffuseImageTexture.imageFade = null != diffuseImageFadeProperty ? readPropertyValue(diffuseImageFadeProperty) : diffuseImageFade;
            Gltf.Instance.add(new Image(diffuseImageTexture.path));
        }

        public void parseSchema(IAssetSchema schema)
        {
            schema.setDefault(this);
            parseDiffuse(schema);

            dynamic backfaceCullProperty = findPropertyByName(schema.backfaceCullKey);
            if (null != backfaceCullProperty) backfaceCull = readPropertyValue(backfaceCullProperty);

            dynamic glossinessProperty = findPropertyByName(schema.glossinessKey);
            if (null != glossinessProperty) glossiness = readPropertyValue(glossinessProperty);

            dynamic isMetalProperty = findPropertyByName(schema.isMetalKey);
            if (null != isMetalProperty) isMetal = readPropertyValue(isMetalProperty);
 
        }
        public void parseAsset(Asset asset)
        {
            this.asset = asset;
            dynamic property = findPropertyByName("BaseSchema");
            if (null == property) return;

            string schemaName = readPropertyValue(property);
            if (string.IsNullOrEmpty(schemaName)) return;

            string typeName = "RevitExporter." + schemaName;
            Type type = Type.GetType(typeName);
            try
            {
                IAssetSchema schema = (IAssetSchema)Activator.CreateInstance(type);
                parseSchema(schema);
            }
            catch (Exception)
            {
                Debug.WriteLine(schemaName + " not supported.");
            }

        }

        //private dynamic readPropertyValue(string name)
        //{
        //    dynamic property = findPropertyByName(name);
        //    if (null == property) return null;

        //    return readPropertyValue(property);
        //}

        static private dynamic readPropertyValue(dynamic property)
        {
            return property.Value;
        }
        //static private int readPropertyValue(AssetPropertyInteger property)
        //{
        //    return property.Value;
        //}
        //static private string readPropertyValue(AssetPropertyString property)
        //{
        //    return property.Value;
        //}

        //static private double readPropertyValue(AssetPropertyDouble property)
        //{
        //    return property.Value;
        //}

        //static private bool readPropertyValue(AssetPropertyBoolean property)
        //{
        //    return property.Value;
        //}

        //static private float readPropertyValue(AssetPropertyFloat property)
        //{
        //    return property.Value;
        //}

        static private IList<AssetProperty> readPropertyValue(AssetPropertyReference property)
        {
            return property.GetAllConnectedProperties();
        }

#if REVIT2016 || REVIT2017
        static private double[] readPropertyValue(AssetPropertyDoubleArray4d property)
        {
            DoubleArray value = property.Value;
            return new double[] { value.get_Item(0), value.get_Item(1), value.get_Item(2), value.get_Item(3) };
        }
#else
        static private double[] readPropertyValue(AssetPropertyDoubleArray4d property)
        {
            return property.GetValueAsDoubles().ToArray();
        }
#endif

#if REVIT2016 || REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020
        static private double readPropertyValue(AssetPropertyDistance property)
        {
            //return UnitUtils.Convert(property.Value, property.DisplayUnitType, DisplayUnitType.DUT_CENTIMETERS);
            return UnitUtils.ConvertToInternalUnits(property.Value, property.DisplayUnitType);
        }
#else
        static private double readPropertyValue(AssetPropertyDistance property)
        {
            //return UnitUtils.Convert(property.Value, property.GetUnitTypeId(), UnitTypeId.Centimeters);
            return UnitUtils.ConvertToInternalUnits(property.Value, property.GetUnitTypeId());
        }
#endif

#if REVIT2016 || REVIT2017 || REVIT2018
        private AssetProperty findPropertyByName(string name)
        {
            return this.asset[name];
        }

        private AssetProperty findPropertyByName(Asset asset, string name)
        {
            return asset[name];
        }

        static private Asset findConnectedPropertyByName(AssetProperty asset, string name)
        {
            return (Asset)asset.GetConnectedProperty(name);
        }
#else
        private AssetProperty findPropertyByName(string name)
        {
            return this.asset.FindByName(name);
        }

        private AssetProperty findPropertyByName(Asset asset, string name)
        {
            return asset.FindByName(name);
        }

        static private Asset findConnectedPropertyByName(AssetProperty asset, string name)
        {
            return (Asset)asset.GetAllConnectedProperties().Where(property => property.Name.Equals(name)).FirstOrDefault();
        }
#endif
    }

    public class Texture
    {
        public string id => path + "&image_fade=" + imageFade;
        public double imageFade
        {
            set;
            private get;
        }
        public string path;
        public double width;
        public double height;
        public double offsetX = 0;
        public double offsetY = 0;
        //#if REVIT2016 || REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020
        //        public static DisplayUnitType unit = DisplayUnitType.DUT_CENTIMETERS;
        //#else
        //        public static ForgeTypeId unit = UnitTypeId.Centimeters;
        //#endif
    }

    public class Image
    {
        [JsonIgnore]
        public string source;

        public string uri
        {
            get
            {
                //return Path.Combine("textures", Path.GetFileName(source));
                return "textures/" + Path.GetFileName(source);  //avoid '\' as directory separator char
            }
        }

        public Image(string uri)
        {
            this.source = uri;
        }
    }
}
