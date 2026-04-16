using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEntreEscenas : MonoBehaviour
{

  private GameObject player;
  private float vidaRestante;
  private void Awake()
  {
    var noDrestruir = FindObjectsOfType<LogicaEntreEscenas>();
   if (noDrestruir.Length > 2)
    {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }
  void Start()
  {
    player = GameObject.FindGameObjectWithTag("Player");
  }
  void Update()
  {
    
  }

  private void setVidaRestante(float vida)
  {
    vidaRestante = vida;
  }

  private float getVidaRestante()
  {
    return vidaRestante;
  }
}
