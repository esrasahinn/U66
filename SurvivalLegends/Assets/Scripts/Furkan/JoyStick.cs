using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //UI'a eri�mek i�in.


public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform ThumbStickTrans; //UI'daki Stick inputu hareket ettirmek i�in.
    [SerializeField] RectTransform BackgroundTrans; //Joystick background e�itlemesi.
    [SerializeField] RectTransform CenterTrans; //Center girdisi (dokunulan yere joystick ta��mas�.

    public delegate void OnStickInputValueUpdated(Vector2 inputVal);

    public event OnStickInputValueUpdated onStickValueUpdated;


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"O Drag Fired {eventData.position}"); //Ate� at�m�.(etrafa d�nme hareketi)
        Vector2 TouchPos = eventData.position;
        Vector2 centerPos = BackgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(TouchPos - centerPos, BackgroundTrans.sizeDelta.x/2);

        Vector2 inputVal = localOffset / (BackgroundTrans.sizeDelta.x / 2);

        ThumbStickTrans.position = centerPos + localOffset;
        onStickValueUpdated?.Invoke(inputVal); //event ba�lamadan aktifle�sin diye ?. kullan�ld�.

    } //Dokunmatik s�n�f tan�m�.

    public void OnPointerDown(PointerEventData eventData) //A�a�� y�nl� hareket.(basma hareketi) --Bas�lan yerde input alma.
    {
       BackgroundTrans.position = eventData.position;
       ThumbStickTrans.position = eventData.position; 
    } //Dokunmatik s�n�f tan�m�.

    public void OnPointerUp(PointerEventData eventData) //Yukar� y�nl� hareket.(�ekme hareketi)
    {
        BackgroundTrans.position = CenterTrans.position;
        ThumbStickTrans.position = BackgroundTrans.position; //Joystick girdi kalk�nca merkezlemesi
        onStickValueUpdated?.Invoke(Vector2.zero);
    } //Dokunmatik s�n�f tan�m�.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
