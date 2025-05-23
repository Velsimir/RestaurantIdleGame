﻿using UnityEngine;
using UnityEditor;

namespace PolyPerfect
{
    [CustomEditor(typeof(WanderScript))]
    [CanEditMultipleObjects]
    public class WanderScriptEditor : Editor
    {
        static bool AnimationStates, AIControls, DEBUGToggle, StateEvents = false;
        public override void OnInspectorGUI()
        {
            WanderScript myWanderScript = (WanderScript)target;
            serializedObject.Update();

            //Get Lists
            SerializedProperty idleStates = serializedObject.FindProperty("idleStates");
            SerializedProperty movementStates = serializedObject.FindProperty("movementStates");
            SerializedProperty attackingStates = serializedObject.FindProperty("attackingStates");
            SerializedProperty deathStates = serializedObject.FindProperty("deathStates");

            //Death Unity Event
            SerializedProperty deathEvent = serializedObject.FindProperty("deathEvent");
            SerializedProperty attackingEvent = serializedObject.FindProperty("attackingEvent");
            SerializedProperty idleEvent = serializedObject.FindProperty("idleEvent");

            SerializedProperty movementEvent = serializedObject.FindProperty("movementEvent");

            //Get Strings
            SerializedProperty species = serializedObject.FindProperty("species");

            //Get Animal Stats
            SerializedProperty stats = serializedObject.FindProperty("stats");

            //Get AI Controls
            SerializedProperty wanderSize = serializedObject.FindProperty("wanderZone");
            SerializedProperty awareness = serializedObject.FindProperty("awareness");
            SerializedProperty scent = serializedObject.FindProperty("scent");
            SerializedProperty constainedToWanderZone = serializedObject.FindProperty("constainedToWanderZone");
            SerializedProperty nonAgressiveTowards = serializedObject.FindProperty("nonAgressiveTowards");
            SerializedProperty surfaceRotationSpeed = serializedObject.FindProperty("surfaceRotationSpeed");
            SerializedProperty matchSurfaceRotation = serializedObject.FindProperty("matchSurfaceRotation");

            ///DEBUG
            SerializedProperty logChanges = serializedObject.FindProperty("logChanges");
            SerializedProperty showGizmos = serializedObject.FindProperty("showGizmos");
            SerializedProperty drawWanderRange = serializedObject.FindProperty("drawWanderRange");
            SerializedProperty drawScentRange = serializedObject.FindProperty("drawScentRange");
            SerializedProperty drawAwarenessRange = serializedObject.FindProperty("drawAwarenessRange");


            //Load a Texture (Assets/Resources/Textures/texture01.png)
            var mainTexture = Resources.Load<Texture2D>("WanderScriptLogo");

            //Main Image    
            GUILayout.BeginHorizontal();
            GUILayout.Label(mainTexture);
            GUILayout.EndHorizontal();

            //Draw The Species Name
            EditorGUILayout.PropertyField(species);
            EditorGUILayout.PropertyField(stats);


            if (GUILayout.Button("Show Animation States",GUILayout.MaxWidth(Screen.width - 30)))
            {
                AnimationStates = !AnimationStates;
            }

            if (AnimationStates)
            {

                GUILayout.BeginHorizontal();
                //GUILayout.Label(idleTexture);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(idleStates, true);


                GUILayout.BeginHorizontal();
                //GUILayout.Label(movementTexture);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(movementStates, true);


                GUILayout.BeginHorizontal();
                //GUILayout.Label(attackingTexture);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(attackingStates, true);


                GUILayout.BeginHorizontal();
                //GUILayout.Label(deathTexture);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(deathStates, true);

            }

            if (GUILayout.Button("Show State Unity Events", GUILayout.MaxWidth(Screen.width - 30)))
            {
                StateEvents = !StateEvents;
            }

            if (StateEvents)
            {
                var width = GUILayout.MaxWidth(Screen.width);
                EditorGUILayout.PropertyField(idleEvent, width);
                EditorGUILayout.PropertyField(movementEvent, width);
                EditorGUILayout.PropertyField(attackingEvent, width);
                EditorGUILayout.PropertyField(deathEvent, width);
            }

            if (GUILayout.Button("Show AI Controls", GUILayout.MaxWidth(Screen.width - 30)))
            {
                AIControls = !AIControls;
            }

            if (AIControls)
            {
                GUILayout.BeginHorizontal();
                //GUILayout.Label(AITexture);
                GUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(wanderSize);
                EditorGUILayout.PropertyField(awareness);
                EditorGUILayout.PropertyField(scent);

                EditorGUILayout.PropertyField(constainedToWanderZone);
                EditorGUILayout.PropertyField(nonAgressiveTowards, true);
                EditorGUILayout.PropertyField(matchSurfaceRotation, true);
                EditorGUILayout.PropertyField(surfaceRotationSpeed, true);
            }

            if (GUILayout.Button("Show DEBUG Options",GUILayout.MaxWidth(Screen.width - 30)))
            {
                DEBUGToggle = !DEBUGToggle;
            }

            if (DEBUGToggle)
            {
                EditorGUILayout.PropertyField(logChanges);
                EditorGUILayout.PropertyField(showGizmos);
                EditorGUILayout.PropertyField(drawWanderRange);
                EditorGUILayout.PropertyField(drawScentRange);
                EditorGUILayout.PropertyField(drawAwarenessRange, true);
            }

            serializedObject.ApplyModifiedProperties();
            //DrawDefaultInspector();
        }
    }
}
