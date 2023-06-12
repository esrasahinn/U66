using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //UI'a eriþmek için.


public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform ThumbStickTrans; //UI'daki Stick inputu hareket ettirmek için.
    [SerializeField] RectTransform BackgroundTrans; //Joystick background eþitlemesi.
    [SerializeField] RectTransform CenterTrans; //Center girdisi (dokunulan yere joystick taþýmasý.

    public delegate void OnStickInputValueUpdated(Vector2 inputVal);

    public event OnStickInputValueUpdated onStickValueUpdated;


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log($"O Drag Fired {eventData.position}"); //Ateþ atýmý.(etrafa dönme hareketi)
        Vector2 TouchPos = eventData.position;
        Vector2 centerPos = BackgroundTrans.position;

        Vector2 localOffset = Vector2.ClampMagnitude(TouchPos - centerPos, BackgroundTrans.sizeDelta.x/2);

        Vector2 inputVal = localOffset / (BackgroundTrans.sizeDelta.x / 2);

        ThumbStickTrans.position = centerPos + localOffset;
        onStickValueUpdated?.Invoke(inputVal); //event baþlamadan aktifleþsin diye ?. kullanýldý.

    } //Dokunmatik sýnýf tanýmý.

    public void OnPointerDown(PointerEventData eventData) //Aþaðý yönlü hareket.(basma hareketi) --Basýlan yerde input alma.
    {
       BackgroundTrans.position = eventData.position;
       ThumbStickTrans.position = eventData.position; 
    } //Dokunmatik sýnýf tanýmý.

    public void OnPointerUp(PointerEventData eventData) //Yukarý yönlü hareket.(çekme hareketi)
    {
        BackgroundTrans.position = CenterTrans.position;
        ThumbStickTrans.position = BackgroundTrans.position; //Joystick girdi kalkýnca merkezlemesi
        onStickValueUpdated?.Invoke(Vector2.zero);
    } //Dokunmatik sýnýf tanýmý.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
