using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;
using FullSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

//Based on original SaveGameJsonSerializer 
public class ExtendedSaveGameJsonSerializer : ISaveGameSerializer
{
    private static List<Type> _customConverterTypes = new List<Type>();
    
    public static void Activate()
    {
        SaveGame.Serializer = new ExtendedSaveGameJsonSerializer();

        foreach (var type in typeof(ExtendedSaveGameJsonSerializer).Assembly.GetTypes())
        {
            if(type.GetCustomAttributes(typeof(CustomConverterAttribute), true).Length > 0)
            {
                _customConverterTypes.Add(type);
            }
        }
    }

    public static void AddConverter<T>() where T: fsBaseConverter
    {
        _customConverterTypes.Add(typeof(T));
    }
    
    public void Serialize<T> ( T obj, Stream stream, Encoding encoding )
    {
#if !UNITY_WSA || !UNITY_WINRT
        try
        {
            StreamWriter writer = new StreamWriter ( stream, encoding );
            fsSerializer serializer = new fsSerializer ();
            foreach (var converter in GetCustomConverters())
            {
                serializer.AddConverter(converter);
            }
            
            fsData data = new fsData ();
            serializer.TrySerialize ( obj, out data );
            writer.Write ( fsJsonPrinter.CompressedJson ( data ) );
            writer.Dispose ();
        }
        catch ( Exception ex )
        {
            Debug.LogException ( ex );
        }
#else
			StreamWriter writer = new StreamWriter ( stream, encoding );
			writer.Write ( JsonUtility.ToJson ( obj ) );
			writer.Dispose ();
#endif
    }

    private List<fsBaseConverter> GetCustomConverters()
    {
        var converters = new List<fsBaseConverter>();
        foreach (Type type in _customConverterTypes)
        {
            converters.Add((fsBaseConverter)Activator.CreateInstance(type));
        }
        return converters;
    }
    
    public T Deserialize<T> ( Stream stream, Encoding encoding )
    {
        T result = default(T);
#if !UNITY_WSA || !UNITY_WINRT
        try
        {
            StreamReader reader = new StreamReader ( stream, encoding );
            fsSerializer serializer = new fsSerializer ();
            foreach (var converter in GetCustomConverters())
            {
                serializer.AddConverter(converter);
            }
            
            fsData data = fsJsonParser.Parse ( reader.ReadToEnd () );
            serializer.TryDeserialize ( data, ref result );
            if ( result == null )
            {
                result = default(T);
            }
            reader.Dispose ();
        }
        catch ( Exception ex )
        {
            Debug.LogException ( ex );
        }
#else
			StreamReader reader = new StreamReader ( stream, encoding );
			result = JsonUtility.FromJson<T> ( reader.ReadToEnd () );
			reader.Dispose ();
#endif
        return result;
    }

}
