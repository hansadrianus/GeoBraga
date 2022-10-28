﻿using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace GeoBraga.Models.Responses;

[Serializable]
public class AreaResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonProperty(ReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
    public Geometry Properties { get; set; }
}