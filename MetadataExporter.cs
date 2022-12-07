using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitElement = Autodesk.Revit.DB.Element;

namespace RevitGltfExporter
{
    class MetadataExporter
    {
        static readonly BuiltInParameterGroup[] groupFilter = { BuiltInParameterGroup.PG_TEXT, BuiltInParameterGroup.PG_IDENTITY_DATA };

        static readonly Dictionary<BuiltInParameterGroup, string> groupMap = new Dictionary<BuiltInParameterGroup, string>(){ { BuiltInParameterGroup.PG_TEXT, "文字" }, { BuiltInParameterGroup.PG_IDENTITY_DATA, "标识数据" } };
        static public List<object> exportByGroup(IList<RevitElement> elements, Document doc)
        {
            List<object> ret = new List<object>();

            foreach (var element in elements)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    {"id", element.Id.IntegerValue },
                    {"uuid", element.UniqueId }
                };

                foreach (Parameter p in element.Parameters)
                {
                    if (!groupFilter.Contains(p.Definition.ParameterGroup)) continue;

                    string groupName = groupMap[p.Definition.ParameterGroup];
                    Dictionary<string, Object> group;
                    if (parameters.ContainsKey(groupName))
                    { 
                        group = (Dictionary<string, Object>)parameters[groupName]; 
                    }
                    else { 
                        group = new Dictionary<string, object>();
                    }
                    string key = p.Definition.Name;
                    try
                    {
                        if (group.ContainsKey(key) || !p.HasValue) continue;

                        Object value = null;

                        switch (p.StorageType)
                        {
                            case StorageType.Double:
                                value = p.AsValueString();
                                break;

                            case StorageType.ElementId:
                                ElementId id = p.AsElementId();
                                if (id.IntegerValue < 0)
                                {
                                    value = id.IntegerValue.ToString();
                                }
                                else
                                {
                                    value = doc.GetElement(id).Name;
                                }
                                break;

                            case StorageType.Integer:
#if REVIT2016 || REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020 || REVIT2021
                                if (ParameterType.YesNo == p.Definition.ParameterType)
#else
                        if (SpecTypeId.Boolean.YesNo == p.Definition.GetDataType())
#endif
                                {
                                    if (p.AsInteger() == 0)
                                    {
                                        value = true;
                                    }
                                    else
                                    {
                                        value = false;
                                    }
                                }
                                else
                                {
                                    value = p.AsInteger();
                                }
                                break;

                            case StorageType.String:
                                value = p.AsString();
                                break;

                            default:
                                continue;
                        }
                        if (null != value)
                        {
                            group.Add(key, value);
                            if(!parameters.ContainsKey(groupName)) parameters.Add(groupName, group);
                        }
                    }
                    catch { Debug.WriteLine("parameter error: " + groupName + " " + p.Definition.Name); }

                }
                ret.Add(parameters);
            }

            return ret;
        }
        static public List<Dictionary<string, Object>> export(IList<RevitElement> elements)
        {
            List<Dictionary<string, Object>> data = new List<Dictionary<string, object>>();

            //parameter.definition.parametergroup 分组
            foreach (var element in elements)
            {                
                Dictionary<string, Object> parameters = new Dictionary<string, object>();
                parameters.Add("id", element.Id.IntegerValue);
                parameters.Add("uuid", element.UniqueId);
                Parameter code = element.LookupParameter("专业码");
                if ((code != null) && (null != code.AsString()) && (0 < code.AsString().Length))
                    parameters.Add("专业码", code.AsString());
                Parameter module = element.LookupParameter("模块类");
                if ((module != null) && (null != module.AsString()) && (0 < module.AsString().Length))
                    parameters.Add("模块类", module.AsString());
                Parameter mileage = element.LookupParameter("里程");
                if ((mileage != null) && (null != mileage.AsString()) && (0 < mileage.AsString().Length))
                    parameters.Add("里程", mileage.AsString());

                data.Add(parameters);

            }

            return data;
        }

//        static Dictionary<string, Object> getParameters(ParameterSet parameters, Document doc)
//        {
//            Dictionary<String, Object> ret = new Dictionary<string, object>();

//            foreach (Parameter p in parameters)
//            {
//                if (!p.HasValue)
//                    continue;
//                string name = p.Definition.Name;
//                Object value = null;

//                switch (p.StorageType)
//                {
//                    case StorageType.Double:
//                        value = p.AsValueString();
//                        break;

//                    case StorageType.ElementId:
//                        ElementId id = p.AsElementId();
//                        if (id.IntegerValue < 0)
//                        {
//                            value = id.IntegerValue.ToString();
//                        }
//                        else
//                        {
//                            value = doc.GetElement(id).Name;
//                        }
//                        break;

//                    case StorageType.Integer:
//#if REVIT2016 || REVIT2017 || REVIT2018 || REVIT2019 || REVIT2020 || REVIT2021
//                        if (ParameterType.YesNo == p.Definition.ParameterType)
//#else
//                        if (SpecTypeId.Boolean.YesNo == p.Definition.GetDataType())
//#endif
//                        {
//                            if (p.AsInteger() == 0)
//                            {
//                                value = true;
//                            }
//                            else
//                            {
//                                value = false;
//                            }
//                        }
//                        else
//                        {
//                            value = p.AsInteger();
//                        }
//                        break;

//                    case StorageType.String:
//                        value = p.AsString();
//                        break;

//                    default:
//                        continue;
//                }

//                if (ret.ContainsKey(name))
//                {
//                    continue;
//                }

//                ret.Add(name, value);
//            }

//            return ret;
//        }
    }
}
