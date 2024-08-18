using System;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

// https://github.com/jacobdufault/fullserializer#a-third-converter-example

[CustomConverter]
public class Vector3IntConverter : fsDirectConverter<Vector3Int> 
{
    public override object CreateInstance(fsData data, Type storageType) 
    {
        return new Vector3Int();
    }

    protected override fsResult DoSerialize(Vector3Int model, Dictionary<string, fsData> serialized) 
    {
        serialized["x"] = new fsData(model.x);
        serialized["y"] = new fsData(model.y);
        serialized["z"] = new fsData(model.z);

        return fsResult.Success;
    }

    protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Vector3Int model) 
    {
        var result = fsResult.Success;

        CheckKey(data, "x", out fsData x);
        CheckKey(data, "y", out fsData y);
        CheckKey(data, "z", out fsData z);

        model.x = (int)x.AsInt64;
        model.y = (int)y.AsInt64;
        model.z = (int)z.AsInt64;

        return result;
    }
}
