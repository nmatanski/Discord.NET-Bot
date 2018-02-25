using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UserService.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        [EnumMember(Value = "Visitor")]
        Visitor = 1,
        [EnumMember(Value = "Performer")]
        Performer = 2,
        [EnumMember(Value = "Venue owner")]
        VenueOwner = 3,
        [EnumMember(Value = "Admin")]
        Admin = 4
    }
}
