using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventBusNS
{
    public class EventBus
    {
        //Everything related to instances is a singleton implementation
        private static EventBus _instance;//This is a field that stores a single instance of the class. Static provides Uniqueness
        public static EventBus Instance//The property that returns this single instance
        {
            get
            {
                //If we don't have an instance of the class, then we create it
                if (_instance == null)
                {
                    _instance = new EventBus();
                }

                return _instance;
            }
        }

        //Standard Constructor
        private EventBus()
        {
            _events = new();
        }

        //Field with all events
        //The keys are the names of the signals
        private readonly Dictionary<string, List<object>> _events = new Dictionary<string, List<object>>();

        //A method that subscribes an action to an event
        public void Subscribe<T>(Action<T> action)
        {
            string key = typeof(T).Name;//Creating a key by the name of the signal
                                        //If this key already exists, then add the action, if not, then create a list of actions for this key and only then add the action
            if (_events.ContainsKey(key))
            {
                _events[key].Add(action);
            }
            else
            {
                _events.Add(key, new List<object>() { action });
            }
        }

        //A method that unsubscribes an action to an event
        public void Unsubscribe<T>(Action<T> action)
        {
            string key = typeof(T).Name;//Creating a key by the name of the signal
                                        //If this key already exists, then remove the action, if not, display error
            if (_events.ContainsKey(key))
            {
                _events[key].Remove(action);
            }
            else
            {
                Debug.LogError($"Attempt to unsubscribe from an unsubscribed {key} method");
            }
        }

        //A method that invoke an event
        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;//Creating a key by the name of the signal
            if (_events.ContainsKey(key))
            {
                for (int i = 0; i < _events[key].Count; i++)//Go through all the actions on this signal
                {
                    Action<T> callback = _events[key][i] as Action<T>;//Lead to the type of our signal
                    callback.Invoke(signal);
                }
            }
        }
    }
}

