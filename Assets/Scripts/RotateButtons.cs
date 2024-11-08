using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public Button left_button;
    public Button right_button;

    public TMP_Text radar_direction_text;

    public GameObject radar;
    public int rotate_speed;

    private bool isLeftHeld = false;
    private bool isRightHeld = false;

    void Start()
    {
        // Add event triggers for PointerDown and PointerUp
        AddEventTrigger(left_button, EventTriggerType.PointerDown, OnLeftButtonDown);
        AddEventTrigger(left_button, EventTriggerType.PointerUp, OnLeftButtonUp);
        AddEventTrigger(right_button, EventTriggerType.PointerDown, OnRightButtonDown);
        AddEventTrigger(right_button, EventTriggerType.PointerUp, OnRightButtonUp);
    }

    void Update()
    {
        // Check if left or right button is held down and call respective methods
        if (isLeftHeld)
        {
            Left_clicked();
        }

        if (isRightHeld)
        {
            Right_clicked();
        }

        DirectionUpdate();
    }

    void Left_clicked()
    {
        radar.transform.Rotate(0, 0, rotate_speed * Time.deltaTime);
    }

    void Right_clicked()
    {
        radar.transform.Rotate(0, 0, -rotate_speed * Time.deltaTime); // Assuming right rotates the other way
    }

    void OnLeftButtonDown(BaseEventData eventData)
    {
        isLeftHeld = true;
    }

    void OnLeftButtonUp(BaseEventData eventData)
    {
        isLeftHeld = false;
    }

    void OnRightButtonDown(BaseEventData eventData)
    {
        isRightHeld = true;
    }

    void OnRightButtonUp(BaseEventData eventData)
    {
        isRightHeld = false;
    }

    void AddEventTrigger(Button button, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(action);
        trigger.triggers.Add(entry);
    }

    void DirectionUpdate()
    {
        float degrees = radar.transform.eulerAngles.z;
        //if (degrees < 0) degrees += 360;
        radar_direction_text.SetText(degrees.ToString("F0") + "°");
    }
}
