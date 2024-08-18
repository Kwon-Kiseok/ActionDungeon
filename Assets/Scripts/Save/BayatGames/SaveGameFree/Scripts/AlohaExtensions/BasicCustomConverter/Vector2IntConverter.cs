using System;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

// https://github.com/jacobdufault/fullserializer#a-third-converter-example

[CustomConverter]
public class Vector2IntConverter : fsDirectConverter<Vector2Int> 
{
    public override object CreateInstance(fsData data, Type storageType) 
    {
        return new Vector2Int();
    }

    protected override fsResult DoSerialize(Vector2Int model, Dictionary<string, fsData> serialized) 
    {
        serialized["x"] = new fsData(model.x);
        serialized["y"] = new fsData(model.y);

        return fsResult.Success;
    }

    protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref Vector2Int model) 
    {
        var result = fsResult.Success;

        CheckKey(data, "x", out fsData x);
        CheckKey(data, "y", out fsData y);

        model.x = (int)x.AsInt64;
        model.y = (int)y.AsInt64;

        return result;
    }
}
