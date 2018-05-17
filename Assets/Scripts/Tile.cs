using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider boxCollider;
    public ParticleSystem particles;
    [HideInInspector] public int ownerID = -10;
    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = new Color(0.5f, 0.85f, 1, 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = new Color(1,1,1,.15f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.EndTurn(eventData.pointerPress.transform.position, this);
        boxCollider.enabled = false;
    }

    internal void Initialize()
    {
        sr.enabled = true;
        sr.color = new Color(1, 1, 1, .15f);
        boxCollider.enabled = true;
        particles.Stop();
    }

    internal void Disable()
    {
        sr.color = new Color(1, 0, 0, .15f);
        boxCollider.enabled = false;
    }
}
