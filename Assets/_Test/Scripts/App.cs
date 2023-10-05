using System.Collections;
using System.IO;
using UnityEngine;

namespace Test
{
    public static class App
    {
        public static Profile Profile { get; set; }

        public static void Create()
        {
            Profile = Save.LoadLocalProfile();
        }

        public static string GetAppDirectory()
        {
#if !UNITY_EDITOR
            return Path.Combine(
                Application.persistentDataPath,
                "Save"
                );
#else
            return
                Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
                    "Save"
                );
#endif
        }

        public static string GetProfileDirectory()
        {
            return
                Path.Combine(
                    GetAppDirectory(),
                    "Profile"
                );
        }
    }
}

