using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hieroglyph"))
        {
            Destroy(collision.gameObject);
        }
    }
}
