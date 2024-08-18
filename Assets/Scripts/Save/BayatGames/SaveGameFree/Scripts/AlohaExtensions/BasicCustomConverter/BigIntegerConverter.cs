using System;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using System.Numerics;

// https://github.com/jacobdufault/fullserializer#a-third-converter-example

[CustomConverter]
public class BigIntegerConverter : fsDirectConverter<BigInteger> 
{
    public override object CreateInstance(fsData data, Type storageType) 
    {
        return new BigInteger();
    }

    protected override fsResult DoSerialize(BigInteger model, Dictionary<string, fsData> serialized) 
    {
        serialized["value"] = new fsData(model.ToString("D"));
        return fsResult.Success;
    }

    protected override fsResult DoDeserialize(Dictionary<string, fsData> data, ref BigInteger model) 
    {
        var result = fsResult.Success;

        CheckKey(data, "value", out fsData x);
        model = BigInteger.Parse(x.AsString);

        return result;
    }
}
