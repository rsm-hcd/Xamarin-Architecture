using Foundation;
using Mobile.Core.Models;
using Newtonsoft.Json;
using Security;

namespace SyncedCare.Mobile.Presentation.iOS.Helpers
{
    public static class KeychainHelper
    {
        // TODO: Rename to something representative of your app.
		private static string KEY = "demo_app";
        /// <summary>
        /// Adds an entry into key chain. If a duplicate is found, it is removed & re-added.
        /// </summary>
        /// <param name="secureValues">Object containing secure values</param>
        /// <returns>Whether the key chain entry was added successfully.</returns>
        public static bool StoreSecureDataInKeychain(KeyStoreModel secureValues)
        {
			var success = false;
            var s = new SecRecord(SecKind.GenericPassword)
            {
                ValueData = NSData.FromString(JsonConvert.SerializeObject(secureValues)),
                Generic = NSData.FromString(KEY),
				AccessGroup = "H29ZBDYNSU.com.andculture.boilerplate" // TODO: Change to your app's bundle id
            };
            var err = SecKeyChain.Add(s);
            if(err == SecStatusCode.DuplicateItem)
            {
                err = SecKeyChain.Remove(s);
				if (err == SecStatusCode.Success)
				{
					return StoreSecureDataInKeychain(secureValues);
				}
            }
            else if(err == SecStatusCode.Success)
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Removes an entry from Keychain
        /// </summary>
        /// <returns>Whether or not the key was removed successfully.</returns>
        public static bool RemoveSecureDataFromKeychain()
        {
            var success = false;
            var s = new SecRecord(SecKind.GenericPassword)
            {
                ValueData = NSData.FromString(string.Empty),
                Generic = NSData.FromString(KEY)
            };
            var err = SecKeyChain.Remove(s);

            if (err == SecStatusCode.Success)
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Gets a string value from keychain for the supplied key.
        /// </summary>
        /// <returns>The value of the entry as a string.</returns>
        public static KeyStoreModel GetSecureDataFromKeychain()
        {
            KeyStoreModel result = null;
            var rec = new SecRecord(SecKind.GenericPassword)
            {
                Generic = NSData.FromString(KEY)
            };
            var match = SecKeyChain.QueryAsRecord(rec, out SecStatusCode res);
            if (match != null && match.ValueData != null)
            {
                result = JsonConvert.DeserializeObject<KeyStoreModel>(match.ValueData.ToString());
            }

            return result;
        }
    }
}
