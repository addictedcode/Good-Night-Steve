using UnityEngine;
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
    public void BuildDefaultNotificationChannel()
    {
        string channel_id = "default";
        string title = "Default Channel";

        Importance importance = Importance.Default;

        string description = "Default channel for this game";

        AndroidNotificationChannel channel = new AndroidNotificationChannel(channel_id, title, description, importance);
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void Awake()
    {
        BuildDefaultNotificationChannel();
    }

    private void CheckIntentData()
    {
        AndroidNotificationIntentData data = AndroidNotificationCenter.GetLastNotificationIntent();
    }

    private void Start()
    {
        CheckIntentData();
        AndroidNotificationCenter.CancelAllScheduledNotifications();
    }

    public void SendNotification(string title, string text, int hours)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = "small_icon";
        notification.LargeIcon = "large_icon";
        notification.FireTime = System.DateTime.Now.AddHours(hours);

        AndroidNotificationCenter.SendNotification(notification, "default");
    }
}
