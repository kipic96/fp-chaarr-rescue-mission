using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChaarrRescueMission.Model.Json
{
    class JsonPropertiesResolver : DefaultContractResolver
    {
        /// <summary>
        /// Return properties that do NOT have the JsonIgnoreSerializationAttribute.
        /// </summary>
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {            
            return objectType.GetProperties()
                             .Where(pi => !Attribute.IsDefined(pi, 
                                typeof(JsonIgnoreSerializationAttribute)))
                             .ToList<MemberInfo>();
        }
    }
}
