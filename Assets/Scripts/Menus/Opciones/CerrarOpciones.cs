using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CerrarOpciones : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;

    public  void CerrarMenuOpciones()
    {
        if(SceneManager.GetActiveScene().name == "MenuInicio")
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        else
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            
        }
    }

    public void pasarPause(GameObject menu)
    {
        pauseMenu= menu;
    }

        public void pasarMainmenu(GameObject menu)
    {
        mainMenu= menu;
    }

    
}
