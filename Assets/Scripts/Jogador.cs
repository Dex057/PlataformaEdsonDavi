using UnityEngine;
using UnityEngine.InputSystem;

// Movimento lateral e salto do heroi. Usa o Input System novo (o projeto URP
// vem com Active Input Handling = Input System Package), mesma logica do guia.
public class Jogador : MonoBehaviour
{
    [SerializeField] private Transform baseHeroi;
    [SerializeField] private LayerMask mascaraCamada;
    [SerializeField] private float velocidade = 3f;

    private Rigidbody refRigidbody;
    private float entradaEixoHorizontal;
    private bool teclaSaltoFoiPressionada;

    private void Awake()
    {
        refRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var teclado = Keyboard.current;
        if (teclado == null) return;

        entradaEixoHorizontal = 0f;
        if (teclado.rightArrowKey.isPressed || teclado.dKey.isPressed) entradaEixoHorizontal += 1f;
        if (teclado.leftArrowKey.isPressed || teclado.aKey.isPressed) entradaEixoHorizontal -= 1f;

        if (teclado.spaceKey.wasPressedThisFrame)
        {
            teclaSaltoFoiPressionada = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movimento = refRigidbody.linearVelocity;
        movimento.x = entradaEixoHorizontal * velocidade;
        movimento.z = 0f;
        refRigidbody.linearVelocity = movimento;

        bool estaNoChao = Physics.OverlapSphere(
            baseHeroi.position,
            0.2f,
            mascaraCamada,
            QueryTriggerInteraction.Ignore
        ).Length > 0;

        if (teclaSaltoFoiPressionada && estaNoChao)
        {
            refRigidbody.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
        }

        teclaSaltoFoiPressionada = false;
    }
}
