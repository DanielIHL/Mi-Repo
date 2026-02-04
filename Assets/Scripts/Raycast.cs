using UnityEngine;

public class Raycast : MonoBehaviour
{
    // VARIABLES
    private Ray rayo;
    private RaycastHit impactoRayo;
    [SerializeField] float longitudRayo = 10.0f;
    [Header("MASCARA RAYCAST")]
    [SerializeField] LayerMask mascaraRayo;
    private void Update()
    {
        // Definimos el origen y la direccion del rayo
        rayo.origin = transform.position;
        rayo.direction = transform.forward;

        // Lanzamos rayo y comprobamos
        if (Physics.Raycast(rayo, out impactoRayo, longitudRayo, mascaraRayo))
        {
            Debug.Log("He chocado contra algo: " + impactoRayo.collider.name);
            //Debug.Log("Punto de impacto: " + impactoRayo.point);
            //Debug.Log("Distancia: " + impactoRayo.distance);
        }

        // Hacemos Debug para visualizar el rayo
        Debug.DrawRay(rayo.origin, rayo.direction * longitudRayo, Color.blue);
    }
}
