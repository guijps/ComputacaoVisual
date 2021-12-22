using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static List<DadosCarregados> m;
    public int[] ListaTeste;
    public float a, b, c, d, e, f;
    public GameObject[] ListaProvisoria;
    public static List<GameObject> ListEstruturas= new List<GameObject>();
    public List<string> listaCarregados = new List<string>();
    public static List<ParticleSystem> emissores = new List<ParticleSystem>();
    public List<float> listGeo = new List<float>();
    public GameObject emissor;
    public static float tempMax = 40.0f;
    public static float aqiMaximo = 150f;

    void Start()
    {
        foreach(GameObject obj in ListaProvisoria)
        {
            ListEstruturas.Add(obj);
        }
        
        a = 1570.659789f;
        b = -9134.71889f;
        c = -388491.4357f;
        d=-14890.23194f;
        e =-408.2357243f;
        f=- 370758.1772f;
        List<float> zero = new List<float>();
                zero.Add(0f);
                zero.Add(0f);
        using (StreamReader file = File.OpenText(@"Assets\DataFolder\data_tratada_temp_aletoria.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            m= (List<DadosCarregados>)serializer.Deserialize(file, typeof(List<DadosCarregados>));   
        }
       foreach (DadosCarregados obj in m)
        {
            listaCarregados.Add(obj.name);
            if (obj.geo == null)
            {
                
                Debug.Log("ATENÇÃO! ESTAÇÃO: "+obj.name+ " NÃO TEM LOCALIZAÇÃO");
                obj.geo = zero;
            }
            else
            {
               listGeo.Add(obj.geo[0]);
            }
            
        }
        int i = 0;
        foreach(DadosCarregados d in m)
        {
            
            float[] dd = GetUnityLoc(d.geo[0], d.geo[1]);

            ListEstruturas[i].GetComponent<Transform>().position= new Vector3((float)dd[0],0.0f,(float)(dd[1]));
            GameObject obj = Instantiate(emissor, new Vector3((float)dd[0], 0.0f, (float)(dd[1])), Quaternion.Euler(-90, 0, 0));
            obj.name = "Emissor: "+ d.name ;
            emissores.Add(obj.GetComponent<ParticleSystem>());
            i++;
        }
    }
    
    public float[] GetUnityLoc(float x, float y)
    {
        List<float> lst = new List<float>();
        float rX = a * x + b * y + c;
        float rZ = d * x + e * y + f;
        lst.Add(rX);
        lst.Add(rZ);

        return lst.ToArray();
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

    public Vector2 GetAT(DateTime dt)
    {
        Data objprocurado = data.Find(x => x.date == dt);
        return new Vector2(objprocurado.aqi_max, (float)objprocurado.temp);
    }

}
public class  Data
{
    public DateTime date { get; set; }
    public float aqi_max { get; set; }
    public double temp { get; set; }
   // public int plu { get; set; }
    //public int umid_rel{ get; set; }
   // public int pressao { get; set; }

}