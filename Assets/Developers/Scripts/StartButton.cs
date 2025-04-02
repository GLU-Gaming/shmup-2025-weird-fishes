using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        rotateClass.Rotate(0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rotateClass.StopRotation();

    }
}
