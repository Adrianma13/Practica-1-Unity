using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public ControladorOpciones optionsMenu;

    public CerrarOpciones cerrarOpciones;
    public TextMeshProUGUI textoUiRanking;

    public AudioSource sonidoBoton;


    void Start()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Configuraciones").GetComponent<ControladorOpciones>();
        cerrarOpciones = FindObjectOfType<CerrarOpciones>();
        if (cerrarOpciones != null)
        {
            cerrarOpciones.pasarMainmenu(mainMenu);
        }
    }
    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.menuOpciones.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        StartCoroutine(LoadStartSceneAfterDelay());
    }

    private IEnumerator LoadStartSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Pantalla Inicio");
    }
    public void OpenRanking()
    {
        StartCoroutine(LoadRankingSceneAfterDelay());
    }

    private IEnumerator LoadRankingSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("PantallaRecords");
    }

    public void ReproducirSonidoBoton()
    {
        if (sonidoBoton != null)
        {
            sonidoBoton.Play();
        }
    }

}
