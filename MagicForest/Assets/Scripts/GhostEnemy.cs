using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] Transform[] puntos;
    [SerializeField] float velocidadPatrulla;
    [SerializeField] private AudioSource audioSourceSlime;
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
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        if (DestinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one*2;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Player"))
        {
           PlayerController pController = elOtro.gameObject.GetComponent<PlayerController>();
            pController.Dead();

        }
        if (elOtro.CompareTag("PlayerBullet"))
        {
            Debug.Log("me da");
            Destroy(gameObject);
            audioSourceSlime.Play();
            Destroy(gameObject, audioSourceSlime.clip.length);
        }
    }
}