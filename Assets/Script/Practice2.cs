using System.Collections.Generic;
using UnityEngine;

public class Practice2 : MonoBehaviour
{
    private List<SMovimiento> patron = new List<SMovimiento>();
    private float tiempoTranscurrido = 0;
    private int indice = 0;
    private Vector3 direction;
    private bool movimientosListos = false;

    private int botonesPresionados = 0;

    public Transform player;
    public Transform endPoint;


    public Transform cuboVerde;  // Asigna aquí el cubo verde
    public Transform cuboRojo;   // Asigna aquí el cubo rojo

    public GameManager gameManager;

    void Update()
    {
        // Calcular la distancia entre el cubo verde y el cubo rojo
        float distancia = Vector3.Distance(cuboVerde.position, cuboRojo.position);
        //Debug.Log("La distancia entre el cubo verde y el cubo rojo es: " + distancia);

        if (gameManager.gameOver)
        {
            Loser();
        }

        if (Vector3.Distance(player.position, cuboRojo.position) < 1)
        {
            Winner();
            gameManager.win = true;
        }

        if (!movimientosListos || patron.Count == 0) return;

        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido > patron[indice].tiempo)
        {
            // Pasar al siguiente movimiento
            tiempoTranscurrido = 0;
            indice++;

            if (indice >= patron.Count)
            {
                // Al final de todos los movimientos, detener
                movimientosListos = false;
                return;
            }

        }
        if (!gameManager.gameOver)
        {
            // Realizar movimiento si hay alguno
            if (patron[indice].speed > 0 || patron[indice].speedRot > 0)
            {
                ActualizarMovimiento(patron[indice]);
            }
        }

    }

    void ActualizarMovimiento(SMovimiento movimiento)
    {
        // Calcular la rotación y mover el jugador hacia adelante según la dirección
        direction = Quaternion.AngleAxis(movimiento.rotation, Vector3.up) * Vector3.forward;
        Quaternion rotObjetivo = Quaternion.LookRotation(direction);
        // Asegurar una rotación suave usando Lerp
        player.rotation = Quaternion.Lerp(player.rotation, rotObjetivo, movimiento.speedRot * Time.deltaTime);

        // Mover al jugador hacia adelante según la velocidad definida
        player.Translate(direction * movimiento.speed * Time.deltaTime, Space.World);
    }


    public void AgregarMovimiento(SMovimiento nuevoMovimiento)
    {
        patron.Add(nuevoMovimiento);
        botonesPresionados++;

        // Comprobar si ya se han presionado los tres botones
        if (botonesPresionados == 3)
        {
            movimientosListos = true;
            indice = 0;
            tiempoTranscurrido = 0;
        }
    }

    // Métodos que se conectarán a los botones de la UI
    public void Boton1()
    {
        AgregarMovimiento(new SMovimiento(90, 1, 8, 0));
    }

    public void Boton2()
    {
        AgregarMovimiento(new SMovimiento(200, 1, 5.5f, 5));
    }

    public void Boton3()
    {
        AgregarMovimiento(new SMovimiento(120, 1, 5, 0));
    }

    public void Winner()
    {
        Debug.Log("Haz Ganado.");
    }

    public void Loser()
    {
        Debug.Log("Perdiste");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "limite")
        {
            gameManager.gameOver = true;
        }
    }


}
