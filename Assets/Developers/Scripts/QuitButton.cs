using UnityEngine;
using UnityEngine.EventSystems;

public class QuitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RotateConfig rotateClass;
    void Start()
    {
        rotateClass = FindFirstObjectByType<RotateConfig>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rotateClass.Rotate(1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rotateClass.StopRotation();

    }
}
