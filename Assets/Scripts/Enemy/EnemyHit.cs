using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Material redMaterial; // Material vermelho.
    private bool isRed = false; // Flag para verificar se o personagem está vermelho.
    private Material originalMaterial; // Material original do personagem.
    public GameObject Enemy; 

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;       
    }
  
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GetComponent<Renderer>().material = redMaterial;     
            if (!isRed)
            {
                GetComponent<Renderer>().material = redMaterial;
                isRed = true;
                StartCoroutine(ResetRedCooldown());
            }    
        }
    }

    private IEnumerator ResetRedCooldown()
    {
        yield return new WaitForSeconds(0.5f); // Espere por 1 segundo (ou o tempo que você desejar).
        // Volta à cor original após o tempo de espera.
        GetComponent<Renderer>().material = originalMaterial;
        isRed = false;
    }
}
