using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using System.IO;

public class DataManager : MonoBehaviour
{
    public List<DadosCarregados> m;
   
    void Start()
    {
      using (StreamReader file = File.OpenText(@"Assets/dados.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            m= (List<DadosCarregados>)serializer.Deserialize(file, typeof(List<DadosCarregados>));   
        }
    }
}
public class AreaDeEmissao
{
    public GameObject obj;
    public DadosCarregados dados;
}
public class DadosCarregados
{
    public string name { get; set; }
    public List<float> geo { get; set; }
    public List<Data> data   { get; set; }

}
public class  Data
{
    public DateTime date { get; set; }
    public float aqi_max { get; set; }
    public int temp { get; set; }


}