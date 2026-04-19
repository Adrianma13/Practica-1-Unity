using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject mainMenu;
    public ControladorOpciones optionsMenu;
    public bool isPaused = false;

    private CerrarOpciones cerrarOpciones;

    void Start()
    {
        optionsMenu = GameObject.FindGameObjectWithTag("Configuraciones").GetComponent<ControladorOpciones>();
        cerrarOpciones = FindObjectOfType<CerrarOpciones>();
        if (cerrarOpciones != null)
        {
            cerrarOpciones.pasarPause(pauseMenu);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }
    public void Pausar()
    {

        pauseMenu.SetActive(true);
        ApplySceneIndex();
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Reanudar()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void SalirMenuPrincipal()
    {
        StartCoroutine(SalirMenuPrincipalAfterDelay());
    }

    private IEnumerator SalirMenuPrincipalAfterDelay()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("MenuInicio");
        isPaused = false;
    }

    public void OpenOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.menuOpciones.SetActive(true);
    }
    public void CloseOptionsMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.menuOpciones.SetActive(false);
    }

    [SerializeField] private Animator anim;

    void Awake()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
    void ApplySceneIndex()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>(); // Re-intento por si acaso
            if (anim == null) return;
        }

        // VERIFICACIÓN CLAVE: Si el animator no tiene el archivo Controller asignado, salimos para evitar el error
        if (anim.runtimeAnimatorController == null)
        {
            Debug.LogError("El Animator en " + anim.gameObject.name + " no tiene un Animator Controller asignado en el Inspector.");
            return;
        }

        SceneManger infoEscena = Object.FindFirstObjectByType<SceneManger>();

        if (infoEscena != null)
        {
            anim.SetInteger("SceneIndex", infoEscena.idDePausa);
        }
    }

}
