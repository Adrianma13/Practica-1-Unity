using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public TextMeshProUGUI textoPuntosFinales;
    private int puntosParaGuardar;
    private string rutaArchivo;
    public TextMeshProUGUI textoUiRanking;


    void Start()
    {
        rutaArchivo = Application.persistentDataPath + "/ranking.json";

        // Recuperamos los puntos del PuntuacionManager antes de que se borre
        if (PuntuacionManager.instancia != null)
        {
            puntosParaGuardar = Mathf.FloorToInt(PuntuacionManager.instancia.puntosActuales);
            textoPuntosFinales.text = "" + puntosParaGuardar;
        }
        if (SceneManager.GetActiveScene().name == "PantallaRecords")
        {
            MostrarRanking();
        }
    }

    public void GuardarEnRanking()
    {
        string nombreJugador = inputNombre.text;
        if (string.IsNullOrEmpty(nombreJugador)) nombreJugador = "Anonimo";

        // 1. Leer archivo existente o crear uno nuevo
        ListaRanking miRanking = new ListaRanking();
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            miRanking = JsonUtility.FromJson<ListaRanking>(json);
        }

        // 2. Añadir nueva entrada
        miRanking.lista.Add(new EntradaRanking { nombre = nombreJugador, puntos = puntosParaGuardar });

        // 3. Ordenar por puntos (de mayor a menor) y quedarse con los 10 mejores
        miRanking.lista = miRanking.lista.OrderByDescending(x => x.puntos).Take(10).ToList();

        // 4. Guardar en el disco duro
        string nuevoJson = JsonUtility.ToJson(miRanking);
        File.WriteAllText(rutaArchivo, nuevoJson);

        // 5. Volver al menú
        SceneManager.LoadScene("MenuInicio");
    }
    public void MostrarRanking()
    {
        string ruta = Application.persistentDataPath + "/ranking.json";
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            ListaRanking datos = JsonUtility.FromJson<ListaRanking>(json);

            string textoRanking = "TOP 10 RANKING:\n";
            foreach (var entrada in datos.lista)
            {
                textoRanking += $"{entrada.nombre}: {entrada.puntos}\n";
            }
            textoUiRanking.text = textoRanking;
        }
    }


    public void VolverAlMenu()
    {
        StartCoroutine(LoadRankingSceneAfterDelay());
    }

    private IEnumerator LoadRankingSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MenuInicio");
    }


}