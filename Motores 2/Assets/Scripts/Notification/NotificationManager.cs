using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    /*
     * 
     * ESTO ES PARA CUANDO QUIERO HACER UNA NOTIFICATION EN ALGUN LADO POR EJEMPLO
    [SerializeField] string _titleNotif = "Full Stamina";
    [SerializeField] string _textNotif = "Tenes tu estamina llena, HORA DE ROMPER RECORDS!!!";
    [SerializeField] IconSelector _small = IconSelector.icon_reminder;
    [SerializeField] IconSelector _big = IconSelector.icon_reminderbig;
    TimeSpan timer;
    int id;

    EN EL START
    if(_currentStamina<_maxStamina)
    {
        timer = _nextStaminaTime - Delta.Now;
        id = NotificationManager.Instance.DisplayNotification(_titleNotif,_textNotif,_small,_big,AddDuration(DateTime.Now,((_maxStamina-_currentStamina + 1)* _timeToRecharge)+1+(float)timer.TotalSeconds));
    }

    En donde recharging es falso
    NotificationManager.Instance.CancelNotification(id);

    En el using Stamina

    en el primer if

    NotificationManager.Instance.CancelNotification(id);
    id = NotificationManager.Instance.DisplayNotification(_titleNotif,_textNotif,_small,_big,AddDuration(DateTime.Now,((_maxStamina-_currentStamina + 1)* _timeToRecharge)+1+(float)timer.TotalSeconds));
    */




    public static NotificationManager Instance { get; private set; }
    AndroidNotificationChannel notiChannel;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        notiChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to Play",
            Importance = Importance.High
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notiChannel);
    }

    public int DisplayNotification(string title, string text, IconSelector small, IconSelector big, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = small.ToString();
        notification.LargeIcon = big.ToString();
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, notiChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }

    public void OnApplicationQuit()
    {
        DisplayNotification("NO TE VAYAS", "Necesitas super tus records", IconSelector.icon_reminder, IconSelector.icon_reminderbig, DateTime.Now.AddSeconds(5));
    }
}
//NO USAR MAYUSCULAS NI SMALL NI LARGE
public enum IconSelector
{
    icon_reminder,
    icon_reminderbig
}
