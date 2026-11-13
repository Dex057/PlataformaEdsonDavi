using UnityEngine;

// Desativa a moeda tocada pelo heroi, conta quantas faltam e,
// ao pegar todas, para o jogo e mostra "FIM DE JOGO" (IMGUI, sem Canvas).
public class ColetarMoedas : MonoBehaviour
{
    private int total;
    private int coletadas;
    private bool fimDeJogo;

    private void Start()
    {
        Time.timeScale = 1f;
        total = GameObject.FindGameObjectsWithTag("moeda").Length;
    }

    private void OnTriggerEnter(Collider outro)
    {
        if (fimDeJogo || !outro.CompareTag("moeda")) return;

        outro.gameObject.SetActive(false);
        coletadas++;

        if (coletadas >= total)
        {
            fimDeJogo = true;
            Time.timeScale = 0f;
        }
    }

    private void OnGUI()
    {
        // Escala a interface pela altura da tela (referencia: 540 px).
        float escala = Screen.height / 540f;
        GUI.matrix = Matrix4x4.Scale(new Vector3(escala, escala, 1f));
        float largura = Screen.width / escala;
        float altura = Screen.height / escala;

        var contador = new GUIStyle(GUI.skin.label) { fontSize = 22, fontStyle = FontStyle.Bold };
        contador.normal.textColor = Color.white;
        GUI.Label(new Rect(16, 10, 300, 36), "Moedas: " + coletadas + " / " + total, contador);

        if (!fimDeJogo) return;

        GUI.Box(new Rect(0, 0, largura, altura), GUIContent.none);

        var titulo = new GUIStyle(GUI.skin.label)
        {
            fontSize = 48,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        titulo.normal.textColor = Color.yellow;
        GUI.Label(new Rect(0, altura / 2f - 35f, largura, 70), "FIM DE JOGO", titulo);
    }
}
