using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System;

public static class Json
{
    public static string ConvertToJson<T>(T value)
    {
        return JsonConvert.SerializeObject(value);
    }
    //Type is required for abstract and interfaces
    public static T ConvertFromJson<T>(string value, Type type = null)
    {
        return (T)JsonConvert.DeserializeObject(value, type != null ? type : typeof(T));
    }

}