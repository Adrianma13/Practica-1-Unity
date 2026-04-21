using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 direccionMirado { get; private set; } // Nueva variable accesible
    [HideInInspector] public bool puedeMoverse = true;
    public EntityAudioManager audioManager ;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direccionMirado = Vector2.down; // Dirección inicial por defecto
    }

    void Update()
    {
        if (puedeMoverse)
        {
            rb.velocity = moveInput * moveSpeed;

            // Si después de un ataque no estás tocando nada, que deje de caminar
            if (moveInput == Vector2.zero)
            {
                animator.SetBool("isWalking", false);
            }
            else
            {
                // Si te estás moviendo justo al terminar el ataque, actualiza el Animator
                animator.SetBool("isWalking", true);
                animator.SetFloat("InputX", moveInput.x);
                animator.SetFloat("InputY", moveInput.y);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();
        if (!puedeMoverse) return;


        if (moveInput != Vector2.zero)
        {
            // Solo actualizamos la dirección si el jugador se está moviendo
            direccionMirado = moveInput.normalized;

            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        else if (context.canceled)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void ForzarDesbloqueo()
    {
        puedeMoverse = true;
   
        animator.ResetTrigger("Attack");
    }
    public void ReproducirPaso()
    {
        
        if (audioManager != null)
        {
            audioManager.PlayStepsSound();
        }
    }

}
