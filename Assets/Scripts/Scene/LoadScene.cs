using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneOnTrigger : MonoBehaviour
{
    public string nextSceneName; // Nome da próxima cena a ser carregada

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou em contato é o jogador (você pode usar tags ou outras verificações)
        if (other.CompareTag("Player"))
        {
            // Carrega a próxima cena
            SceneManager.LoadScene(nextSceneName);
        }
    }
}