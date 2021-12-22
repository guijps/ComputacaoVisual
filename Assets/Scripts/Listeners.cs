using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Listeners : MonoBehaviour
{ 
    [SerializeField] private Button reset;
    [SerializeField] private Button pause;
    [SerializeField] private Slider vel;
    [SerializeField] private GameObject manager;
    private ParticleManager pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = manager.GetComponent<ParticleManager>();
        reset.onClick.AddListener(() => pm.IniciaAgain() );
        pause.onClick.AddListener(() => pm.PausaRetorna());
      

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
