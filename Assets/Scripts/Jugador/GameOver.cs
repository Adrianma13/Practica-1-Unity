using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public GameObject panelGameOver;
    void Start()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false); // Asegúrate de que el panel esté oculto al inicio
        }
    }
    public void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
    }

}
