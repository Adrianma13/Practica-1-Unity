using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para detectar escenas

public class DestruirEnEscena : MonoBehaviour
{
    [SerializeField] private string nombreDeEscenaDestruccion;

    void OnEnable()
    {
       
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    void OnDisable()
    {
    
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        // Si el nombre de la escena coincide con la escena donde no quieres el objeto
        if (escena.name == "MenuInicio")
        {
            Debug.Log("Escena prohibida detectada. Destruyendo objeto...");
            Destroy(gameObject); 
        }
    }
}