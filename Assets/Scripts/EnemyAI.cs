using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agente;
    private Animator anim;

    public float distancia;
    public float distanciaAPerseguir = 3.0f;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);
        if (distancia <= distanciaAPerseguir)
        {
            // NO PARES, MUÉVETE
            agente.isStopped = false;
            // MUEVETE HACIA EL OBJETIVO (PROTAGONISTA)
            agente.SetDestination(player.position);
        }
        else
        {
            // PARA, QUIETO
            agente.isStopped = true;
        }

        // caminar según movimiento del agente
        if (agente.velocity.magnitude > 0.01f)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);
    }
}
