using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones controladorOpciones;
    // Start is called before the first frame update
    void Start()
    {
        controladorOpciones = GameObject.FindGameObjectWithTag("Configuraciones").GetComponent<ControladorOpciones>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void  MostrarMenuOpciones()
    {
        controladorOpciones.menuOpciones.SetActive(true);
        
    }
}
