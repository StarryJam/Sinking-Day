using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Skill))]
public class SkillDisplayScript : Editor {

    private SerializedObject test;
    private SerializedProperty type, coolDown, damage, spellRange, influenceRange, isToEnemy, skillName, skillIcon;

    
    private void OnEnable()
    {
        test = new SerializedObject(target);
        skillName = test.FindProperty("skillName");
        skillIcon = test.FindProperty("skillIcon");
        type = test.FindProperty("type");
        coolDown = test.FindProperty("coolDown");
        damage = test.FindProperty("damage");
        spellRange = test.FindProperty("spellRange");
        influenceRange = test.FindProperty("influenceRange");
        isToEnemy = test.FindProperty("isToEnemy");
    }

    public override void OnInspectorGUI()
    {
        test.Update();
        EditorGUILayout.PropertyField(skillName);
        EditorGUILayout.PropertyField(skillIcon);
        EditorGUILayout.PropertyField(type);
        if (type.enumValueIndex == 0)
        {
        }
        else
        {
            EditorGUILayout.PropertyField(coolDown);
            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(spellRange);
            if (type.enumValueIndex == 1)//To Unit
            {
                EditorGUILayout.PropertyField(isToEnemy);
            }
            else if(type.enumValueIndex == 2)//To Area
            {
                EditorGUILayout.PropertyField(influenceRange);
            }
        }
        
        test.ApplyModifiedProperties();
    }
}
