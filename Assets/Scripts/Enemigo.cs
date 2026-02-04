using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AtaqueJugador"))
            Destroy(gameObject);
    }
}
