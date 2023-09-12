using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Nome da cena do jogo
    public string cenaDoJogo;

    // Método chamado quando o botão "Começar Jogo" é clicado
    public void ComecarJogo()
    {
        SceneManager.LoadScene(cenaDoJogo); // Carrega a cena do jogo
    }

    // Método chamado quando o botão "Créditos" é clicado
    public void Creditos()
    {
        // Implemente a lógica dos créditos aqui, por exemplo, exibindo um painel de créditos.
        Debug.Log("Mostrar tela de créditos");
    }

    // Método chamado quando o botão "Sair" é clicado
    public void Sair()
    {
        Application.Quit(); // Fecha o aplicativo (funciona apenas em builds standalone)
    }
}
