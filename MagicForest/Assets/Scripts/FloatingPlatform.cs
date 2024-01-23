using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] Transform[] puntos;
    [SerializeField] float velocidadPatrulla;
    private Vector3 DestinoActual;
    private int indiceActual = 0;

    // Start is called before the first frame update
    void Start()
    {
        DestinoActual = puntos[indiceActual].position;
        StartCoroutine("Patrulla");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Patrulla()
    {
        while (true)
        {


            while (transform.position != DestinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, DestinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }
    }

    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= puntos.Length)
        {
            indiceActual = 0;
        }
        DestinoActual = puntos[indiceActual].position;
       
    }

   

    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            elOtro.transform.SetParent(this.transform);
           
        }
    }
    private void OnCollisionExit2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
            elOtro.transform.SetParent(null);
        }
    }


}
