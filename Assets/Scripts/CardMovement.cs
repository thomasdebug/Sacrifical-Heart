using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler , IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private Vector3 orignalScale;
    private int currentState = 0;
    private Quaternion originalRotation;   
    private Vector3 originalPosition;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector2 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalRotation = rectTransform.localRotation;
        originalPosition = rectTransform.localPosition;
        orignalScale = rectTransform.localScale;
    }

    void Update()
    {
        switch (currentState) 
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Mouse.current.leftButton.isPressed)
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if (!Mouse.current.leftButton.isPressed)
                {
                    TransitionToState0();
                }
                break;

        }
    }
    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = orignalScale;
        rectTransform.localRotation = originalRotation;
        rectTransform.localPosition = originalPosition;
        glowEffect.SetActive(false);   
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            orignalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Mouse.current.position.ReadValue(), lerpFactor);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, playPosition, lerpFactor);
                }
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = orignalScale * selectScale;
    }

    private void HandleDragState()
    {
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
