using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Configurations {
    public class ConfigManager<T> : IConfigManager
        where T : struct {

        /// <summary>
        /// The backing file used to store the config. Null means no persistent storage.
        /// </summary>
        protected virtual string Filename { get { return @"game.ini"; } }

        private readonly Dictionary<T, IComparable> configStore = new Dictionary<T, IComparable>();

        public ConfigManager() {

            InitialiseDefaults();
            Load();
        }

        protected virtual void InitialiseDefaults() {
        }

        public void Load() {
            if (string.IsNullOrEmpty(Filename)) return;

            //using (var stream = storage.GetStream(Filename)) {
            using (var stream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read)) {
                if (stream == null)
                    return;

                using (var reader = new StreamReader(stream)) {
                    string line;

                    while ((line = reader.ReadLine()) != null) {
                        int equalsIndex = line.IndexOf('=');

                        if (line.Length == 0 || line[0] == '#' || equalsIndex < 0) continue;

                        string key = line.Substring(0, equalsIndex).Trim();
                        string val = line.Remove(0, equalsIndex + 1).Trim();

                        T lookup;
                        //if (!Enum.TryParse(key, out lookup))
                        //    continue;
                        try {
                            lookup = (T)Enum.Parse(typeof(T), key);
                        } catch (ArgumentException) {
                            continue;
                        }

                        IComparable b;

                        if (configStore.TryGetValue(lookup, out b)) {
                            try {
                                b = float.Parse(val);
                            } catch (Exception e) {
                                throw new Exception(@"Unable to parse config key " + lookup + ": " + e);
                            }
                        }
                        //else if (AddMissingEntries)
                        //    Set(lookup, val);

                        configStore[lookup] = b;
                    }
                }
            }
        }

        public ConfigManager<T> Set(T lookup, int value) {
            configStore[lookup] = value;
            return this;
        }

        public ConfigManager<T> Set(T lookup, float value) {
            configStore[lookup] = value;
            return this;
        }

        public ConfigManager<T> Set(T lookup, bool value) {
            configStore[lookup] = value;
            return this;
        }

        public ConfigManager<T> Set<U>(T lookup, U value)
            where U : IComparable {
            configStore[lookup] = value;
            return this;
        }
        
        public U Get<U>(T lookup) {
            IComparable b;
            configStore.TryGetValue(lookup, out b);
            return (U)b;
        }
    }
}