using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleManager : MonoBehaviour
{
    public bool acabou = false;
    public int datePointer = 0;
    public float intervalo=0;
    public float fixInterval=5;
    public DateTime inicio, fim,atual;
    public float r, g, b,xx = 0;
    public Text textoExibir;
    public Slider Velocidade;
    public float lifetime = 1;
    public int aqimultiplier= 1;
    private bool pausado=false;
    public GameObject pause;
    public GameObject pivo;
    public Quaternion angle = Quaternion.identity;
    

    void Start()
        {
            inicio = new DateTime(2019, 01, 01);
            fim = new DateTime(2019, 12, 31);
            atual = inicio;

        }
    public void Update()
    {
        if(acabou == false)
        {
            if (intervalo >= fixInterval/Velocidade.value)
            {
            if (atual != fim)
            {
                int k = 0;
                foreach (DadosCarregados d in DataManager.m)
                {
                    Vector2 vec = d.GetAT(atual);
                    float temp = vec.y;
                    float aqi = vec.x;
                    funcao((float)temp, aqi, DataManager.emissores[k]);
                    


                    k++;
                }
                    textoExibir.text = "Data: " + atual.Day + "/" + atual.Month + "/" + atual.Year ;
                atual = atual.AddDays(1);
                intervalo = 0;
            }
            else
            {
                    textoExibir.text = "ACABOU";
                    int k = 0;
                    foreach (DadosCarregados d in DataManager.m)
                    {

                        DataManager.emissores[k].Stop();


                        k++;
                      
                    }
                    acabou = true;
            }
        }
        else
        {
                if (pausado == false)
                {
                    intervalo += Time.deltaTime;
                }
        
        }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotaUp();
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            RotaDown();
        }
        else if(Input.GetKeyDown("space"))
        {
            ResetRota();
        }
    }

    void funcao(float temperatura, float aqi_max, ParticleSystem emit)
    {
      
        int aqi_max_i = (int)aqi_max;
        ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

        Color cor = new Color(temperatura/ DataManager.tempMax, 0, (1 - temperatura/(DataManager.tempMax)),1f);
    
        emitParams.startColor = cor;
        lifetime = 100 * aqi_max_i / DataManager.aqiMaximo;
        emit.startLifetime = 1/Velocidade.value;
        emit.startSpeed = 60;
        emitParams.startSize = 0.2f;
        var emission = emit.emission;
        emission.rateOverTime= ((aqi_max_i))*10;
        
        ParticleSystem.MainModule settings = emit.GetComponent<ParticleSystem>().main;
        settings.startColor = new ParticleSystem.MinMaxGradient(cor);


        
        //emit.Emit(emitParams,);
        //emit.Play();    
    }
    public void IniciaAgain()
    {
        acabou = false;
        atual = inicio;
    }
    public void PausaRetorna()
    {
        pausado = !(pausado);
        pause.SetActive(pausado);
    }
    public void RotaUp()
    {
        angle =angle *Quaternion.AngleAxis(30, Vector3.up);
        pivo.transform.rotation = angle;
    }
    public void RotaDown()
    {
        angle = angle * Quaternion.AngleAxis(-30, Vector3.up);
        pivo.transform.rotation  =(angle);
    }
    public void ResetRota()
    {
        angle = Quaternion.identity;
        pivo.transform.rotation = Quaternion.identity;
    }
}
