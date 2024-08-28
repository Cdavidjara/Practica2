using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


public struct SMovimiento
{
    public float rotation;
    public float tiempo;
    public float speed;
    public float speedRot;

    public SMovimiento(float pRotation, float pTime, float pSpeed, float pSpeedRot)
    {
        rotation = pRotation;
        tiempo = pTime;
        speed = pSpeed;
        speedRot = pSpeedRot;
    }
}
public class MovPatron : MonoBehaviour
{
    private int cantidadPasos;
    private List<SMovimiento> patron = new List<SMovimiento>();
    private float Tiempo = 0;
    private int indice = 0;
    private Vector3 direction;

    void Start()
    {
        //Creamos el patron 
        patron.Add(new SMovimiento(30, 2, 5, 3));
        patron.Add(new SMovimiento(-30, 2, 5, 2));
        patron.Add(new SMovimiento(0, 3, 5, 0));
        patron.Add(new SMovimiento(0, 2, 2, 0));
        patron.Add(new SMovimiento(15, 5, 0, 5));
        cantidadPasos = patron.Count;

        indice = 0;
    }

    void Update()
    {
        Tiempo += Time.deltaTime;
        if (Tiempo > patron[indice].tiempo)
        {
            //reseteamos tiempo y avanzamos el movimiento 
            Tiempo = 0;
            indice++;
            //Verificamos si es necesario repetir el patron 
            if (indice >= cantidadPasos)
            {
                indice = 0;
            }
        }
        //Calculamos el vector de rotacion 
        direction = Quaternion.AngleAxis(patron[indice].rotation, Vector3.up) * transform.forward;
        Quaternion rotObjetivo = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotObjetivo, patron[indice].speedRot * Time.deltaTime);
        transform.Translate(transform.forward * patron[indice].speed * Time.deltaTime);
    }
}