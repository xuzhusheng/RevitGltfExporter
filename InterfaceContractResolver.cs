using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RevitGltfExporter
{
    class InterfaceContractResolver : DefaultContractResolver
    {
        //private readonly Type[] _interfaceTypes;

        //private readonly ConcurrentDictionary<Type, Type> _typeToSerializeMap;

        private readonly Type _interfaceType;
        public InterfaceContractResolver(Type interfaceType)
        {
            //_interfaceTypes = interfaceTypes;

            //_typeToSerializeMap = new ConcurrentDictionary<Type, Type>();
            this._interfaceType = interfaceType;
        }

        protected override IList<JsonProperty> CreateProperties(
            Type type,
            MemberSerialization memberSerialization)
        {
            //var typeToSerialize = _typeToSerializeMap.GetOrAdd(
            //    type,
            //    t => _interfaceTypes.FirstOrDefault(
            //        it => it.IsAssignableFrom(t)) ?? t);

            if (this._interfaceType.IsAssignableFrom(type))
            {
                return base.CreateProperties(this._interfaceType, memberSerialization);
            }
            return base.CreateProperties(type, memberSerialization);
        }
    }
}
