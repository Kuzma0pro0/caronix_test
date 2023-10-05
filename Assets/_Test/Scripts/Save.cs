using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;


namespace Test
{
    public static class Save
    {
        private static string currentProfileName = "profile";

        #region Profile Saves

        public static void ResetProfile()
        {
            var directory = App.GetProfileDirectory();
            var path = Path.Combine(directory, currentProfileName);
            var backupPath = $"{path}.backup";
            if (Directory.Exists(directory))
            {
                File.Delete(path);
                File.Delete(backupPath);
            }

            App.Profile = null;
        }

        public static void SaveProfile() => SaveProfile(App.Profile, currentProfileName);

        public static void SaveProfile(Profile profile, string profileName)
        {
            if (profile == null)
            {
                throw new InvalidOperationException("Profile not set.");
            }

            if (string.IsNullOrEmpty(profileName))
            {
                throw new InvalidOperationException("Profile name not set.");
            }

            profile.LastSaveTime = DateTime.UtcNow;

            TrySaveInFile(profile, profileName);
        }

        private static (bool success, byte[] saveData) TrySaveInFile(Profile profile, string profileName)
        {
            var directory = App.GetProfileDirectory();
            var path = Path.Combine(directory, profileName);
            var newFilePath = $"{path}.new";
            var backupPath = $"{path}.backup";

            byte[] saveData = null;
            try
            {
                Directory.CreateDirectory(directory);
                if (File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }

                saveData = SerializeProfile(profile);

                File.WriteAllBytes(newFilePath, saveData);

                if (File.Exists(path))
                {
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }

                    File.Move(path, backupPath);
                }

                File.Move(newFilePath, path);
            }
            catch
            {
                return (false, null);
            }

            return (true, saveData);
        }

        private static byte[] SerializeProfile(Profile profile)
        {
            byte[] saveData = null;

            using (var stream = new MemoryStream())
            {
                using (var toBase64Stream = new CryptoStream(stream, new ToBase64Transform(), CryptoStreamMode.Write))
                using (var writer = new StreamWriter(toBase64Stream))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    };

                    JsonSerializer.Create(settings).Serialize(jsonWriter, profile);
                }
                saveData = stream.ToArray();
            }


            return saveData;
        }

        public static Profile LoadLocalProfile() => LoadLocalProfile(currentProfileName);

        public static Profile LoadLocalProfile(string profileName)
        {
            var path = Path.Combine(App.GetProfileDirectory(), profileName);
            var backupPath = $"{path}.backup";

            Profile localProfile = null;
            if (File.Exists(path) || File.Exists(backupPath))
            {
                try
                {
                    localProfile = DeserializeProfile(File.ReadAllBytes(path));
                }
                catch (Exception)
                {
                    Debug.LogWarning($"Trying to load backup after failed to load profile {profileName}");
                    try
                    {
                        localProfile = DeserializeProfile(File.ReadAllBytes(backupPath));
                    }
                    catch (Exception e1)
                    {
                        Debug.LogWarning($"Failed to deserialize profile and its backup.\n{e1.Message}");
                    }
                }
            }

            return localProfile;
        }

        private static Profile DeserializeProfile(byte[] data)
        {
            Profile profile = null;
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }
            using (var stream = new MemoryStream(data))
            {
                Stream cryptoStream = null;
                if (data[0] != '{')
                {
                    cryptoStream = new CryptoStream(stream, new FromBase64Transform(), CryptoStreamMode.Read);
                }
                using (var reader = new StreamReader(cryptoStream ?? stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    var settings = new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };

                    profile = JsonSerializer.Create(settings).Deserialize<Profile>(jsonReader);
                }
                cryptoStream?.Close();
                return profile;
            }
        }

        #endregion      
    }
}
