using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KanaFallingObject : MonoBehaviour
{
    [Header("����������")]
    public float fallSpeed = 2f;

    private SpriteRenderer spriteRenderer; // ��� ����������� �����������
    private KanaCharacterData kanaData;
    private bool isActive = true;
    private System.Action<KanaCharacterData> clickCallback;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        if (spriteRenderer == null)
        {
            Debug.LogError("KanaFallingObject: ��������� SpriteRenderer �� ������!");
            isActive = false;
        }
    }

    public void Setup(KanaCharacterData data, System.Action<KanaCharacterData> callback)
    {
        kanaData = data;
        clickCallback = callback;

        if (spriteRenderer != null && data != null && data.image != null)
        {
            spriteRenderer.sprite = data.image;

            if (boxCollider != null && spriteRenderer.sprite != null)
            {
                boxCollider.size = spriteRenderer.sprite.bounds.size;
                boxCollider.offset = spriteRenderer.sprite.bounds.center;
            }
        }
        else
        {
            Debug.LogWarning("KanaFallingObject: �� ������� ���������� �����������! SpriteRenderer ��� ������ ���� �����������.");
            if (spriteRenderer != null) spriteRenderer.sprite = null;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (Camera.main != null && Camera.main.orthographic)
        {
            if (transform.position.y < -Camera.main.orthographicSize - 1f)
            {
                Destroy(gameObject);
            }
        }
        else if (Camera.main == null)
        {
            Debug.LogWarning("KanaFallingObject: �������� ������ �� ������� ��� �������� ������.");
        }
    }

    private void OnMouseDown()
    {
        if (!isActive) return;

        if (GameManager.Instance != null && !GameManager.Instance.IsGameActive())
        {
            return;
        }

        OnClick();
    }

    public void OnClick()
    {
        if (clickCallback != null)
        {
            clickCallback(kanaData);
        }
        else
        {
            Debug.LogWarning("KanaFallingObject: clickCallback �� ����������!");
        }

        isActive = false;
        Destroy(gameObject);
    }

    public void StopFalling()
    {
        isActive = false;
    }
}
