using System;
using Assets.Scripts.Framework;
using Assets.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
#if PLATFORM_ANDROID

#endif

namespace Assets.Scripts
{
    public class PermissionsRationaleDialog
    {
        public static bool IsPermitted(AndroidPermission permission)
        {
#if UNITY_ANDROID
        using (var permissionManager = new AndroidJavaObject("com.inode.dreamcity.SaveImageActivity"))
        {
            return permissionManager.CallStatic<bool>("hasPermission", GetPermissionStr(permission));
        }
#else
            return true;
#endif
        }
public static void RequestPermission(AndroidPermission permission, Action onAllow = null, Action onDeny = null, Action onDenyAndNeverAskAgain = null)
        {
#if UNITY_ANDROID
        using (var permissionManager = new AndroidJavaObject("com.inode.dreamcity.SaveImageActivity"))
        {
            permissionManager.CallStatic("requestPermission", GetPermissionStr(permission));
        }
#else
    Debug.LogWarning("UniAndroidPermission works only on Android Devices.");
#endif
        }

        private static string GetPermissionStr(AndroidPermission permission)
        {
            return "android.permission." + permission.ToString();
        }
    }
    public enum AndroidPermission
    {
        ACCESS_COARSE_LOCATION,
        ACCESS_FINE_LOCATION,
        ADD_VOICEMAIL,
        BODY_SENSORS,
        CALL_PHONE,
        CAMERA,
        GET_ACCOUNTS,
        PROCESS_OUTGOING_CALLS,
        READ_CALENDAR,
        READ_CALL_LOG,
        READ_CONTACTS,
        READ_EXTERNAL_STORAGE,
        READ_PHONE_STATE,
        READ_SMS,
        RECEIVE_MMS,
        RECEIVE_SMS,
        RECEIVE_WAP_PUSH,
        RECORD_AUDIO,
        SEND_SMS,
        USE_SIP,
        WRITE_CALENDAR,
        WRITE_CALL_LOG,
        WRITE_CONTACTS,
        WRITE_EXTERNAL_STORAGE
    }
}