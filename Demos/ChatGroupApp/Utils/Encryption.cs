using Extensions;
using System;
using System.Collections.Generic;

namespace ChatGroupApp
{
    static class Encryption
    {
        public static bool IsEnabled { get; private set; } = false;
        public static string Key { get; private set; }

        public static string TryEncrypt(string data)
        {
            return IsEnabled ? StringCipher.Encrypt(data, Key) : data;
        }

        public static void Enable(string key)
        {
            IsEnabled = true;
            Key = key;
        }
        public static void Disable()
        {
            IsEnabled = false;
            Key = null;
        }


        public static string TryDecrypt(string data, string username)
        {
            if (_userKeys.ContainsKey(username))
            {
                try
                {
                    return StringCipher.Decrypt(data, _userKeys[username]);
                }
                catch //(Exception)
                {
                    return "Can't decrypt message";
                }
            }
            else
            {
                return data;
            }
        }

        private static Dictionary<string, string> _userKeys = new Dictionary<string, string>();
        public static void AddKey(IEnumerable<string> users, string key)
        {
            foreach (var user in users)
                AddKey(user, key);
        }
        public static void AddKey(string username, string key)
        {
            if (_userKeys.ContainsKey(username))
                _userKeys[username] = key;
            else
                _userKeys.Add(username, key);
        }
        public static void RemoveKey(IEnumerable<string> users)
        {
            foreach (var user in users)
                RemoveKey(user);
        }
        public static void RemoveKey(string username)
        {
            if (_userKeys.ContainsKey(username))
                _userKeys.Remove(username);
        }
    }
}
