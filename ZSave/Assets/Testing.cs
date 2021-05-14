using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;
using ZSave;
using Object = UnityEngine.Object;
using SaveType = ZSave.SaveType;

[Persistent(SaveType.GameObject, ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
    public int num3 = 248;
    public TestingAgain mf;

    private void Start()
    {

       // MeshRenderer mr = GetComponent<MeshRenderer>();
       //
       //
       // foreach (var propertyInfo in typeof(MeshFilter).GetProperties().Where(p=> p.GetCustomAttribute<ObsoleteAttribute>() == null))
       // {
       //     Debug.Log(propertyInfo.Name);
       // }


       // Transform tf = transform;
       // object[] propertyValues = (from ff in (from f in typeof(Transform).GetProperties()
       //             .Where(p => p.GetCustomAttribute<ObsoleteAttribute>() == null && p.CanWrite && p.CanRead)
       //         select f)
       //     select ff.GetValue(tf)).ToArray();



       // foreach (var propertyValue in propertyValues)
       // {
       //     Debug.Log(propertyValue);
       // }


       // FileStream file = File.Create(Application.persistentDataPath + "/test.xml");
       //
       // DataContractSerializer bf = new DataContractSerializer(typeof(object[]));
       // MemoryStream streamer = new MemoryStream();
       //
       // bf.WriteObject(streamer, propertyValues);
       //
       // streamer.Seek(0, SeekOrigin.Begin);
       //
       // file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);
       //
       // file.Close();
       //
       //
       //
       //
       // bf = new DataContractSerializer(
       //     typeof(object[]), typeof(Vector3).Assembly.GetExportedTypes().Where(t => t.IsValueType && !t.IsGenericType));
       // XmlReader reader = new XmlTextReader(Application.persistentDataPath + "/test.xml");
       // object[] datas = (object[]) bf.ReadObject(reader);

       // foreach (var data in datas)
       // {
       //     Debug.Log(data);
       // }

       //IN CASE OF SERIALIZATION EXCEPTION Debug.Log(e.Message.Split(new[] {"data contract"}, StringSplitOptions.None)[0].Split('/').Last());


       // foreach (var data in datas)
       // {
       //     Debug.Log(data);
       // }
    }
}