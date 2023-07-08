using UnityEngine;

public class GameStateChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCollision collision))
        {
            collision.ChangeGameState();
        }
    }
}
