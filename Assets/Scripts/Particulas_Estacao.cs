using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas_Estacao : MonoBehaviour
{
    public ParticleSystem system;
	
	public int i = 0;
	//public int j = 0;
	
    void Start()
    {
        // A simple particle material with no texture.
        Material particleMaterial = new Material(Shader.Find("Particles/Standard Unlit"));
		
        // Create a yellow Particle System.
        var go = new GameObject("Particle System");
        go.transform.Rotate(90, 0, 0); // Rotate so the system emits upwards.
		go.transform.position = new Vector3(-5.78f, -0.07f, 0.46f);
        system = go.AddComponent<ParticleSystem>();
        go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
        var mainModule = system.main;
        mainModule.startColor = Color.yellow;
        mainModule.startSize = 0.01f;
		
		
        InvokeRepeating("DoEmit", 2.0f, 2.0f);
		//InvokeRepeating("DoEmit2", 2.0f, 2.0f);
    }

    void DoEmit()
    {
		
		int[] array1 = new int[] { 10, 2, 3, 4, 50};
		
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.blue;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, array1[i]);
        system.Play(); // Continue normal emissions
		i++;
		if(i==5){
			i=0;
		}
    }
	
	/*
	void DoEmit2()
    {
		
		int[] array2 = new int[] { 10, 2, 3, 4, 50};
		
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.startColor = Color.red;
        emitParams.startSize = 0.2f;
        system.Emit(emitParams, array2[j]);
        system.Play(); // Continue normal emissions
		j++;
		if(j==5){
			j=0;
		}
    }
	*/
}
