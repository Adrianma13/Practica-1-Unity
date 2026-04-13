using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combate : MonoBehaviour
{
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemigo"))
            {
                collider.GetComponent<Enemigo>().RecibirDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (controladorAtaque == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

}
