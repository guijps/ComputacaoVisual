using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Json : MonoBehaviour
{
	public TextAsset textJSON;
	
	[System.Serializable]
	public class Data
	{
		public string date;
		public float aqi_max;
		public int temp;
	}
	
	
	
	[System.Serializable]
	public class Estacao
	{
		public Data[] data;
	}
	
	public Estacao Estacao_Cerqueira_Cesar = new Estacao();
	


    void Start()
    {
		Estacao_Cerqueira_Cesar = JsonUtility.FromJson<Estacao>(textJSON.text);
        ParticleSystem ps = GetComponent<ParticleSystem>();
		var emission = ps.emission;
		emission.enabled = true;
		
		
		var asd = Estacao_Cerqueira_Cesar.data;
		string str = asd.ToString();
		int qnt;
		int.TryParse(str, out qnt);
		
		emission.SetBursts(
			new ParticleSystem.Burst[]{
				new ParticleSystem.Burst(1.0f,100,1,1), // 1 seg, 100 particulas, 3 vezes, 1 vez por segundo
				new ParticleSystem.Burst(4.0f,5,1,1),   // 4 seg, 5 particulas, 3 vezes, 1 vez por segundo
				new ParticleSystem.Burst(7.0f,10,1,1)   // 4 seg, 25 particulas, 3 vezes, 1 vez por segundo
			});	
    }

    void Update()
    {

    }

}
