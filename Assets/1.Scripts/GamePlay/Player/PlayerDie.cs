using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Log"))
            GameManager.Instance.ChangeState(GameState.Ending);
    }
}
