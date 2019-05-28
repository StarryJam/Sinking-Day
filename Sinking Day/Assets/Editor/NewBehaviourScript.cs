using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Skill))]
public class SkillDisplayScript : Editor {

    private SerializedObject ser;
    private SerializedProperty type, coolDown, damage, spellRange, influenceRange, isToEnemy, skillName, skillIcon, hasProjectile, Projectile;

    
    private void OnEnable()
    {
        ser = new SerializedObject(target);
        skillName = ser.FindProperty("skillName");
        skillIcon = ser.FindProperty("skillIcon");
        type = ser.FindProperty("type");
        coolDown = ser.FindProperty("coolDown");
        damage = ser.FindProperty("damage");
        spellRange = ser.FindProperty("spellRange");
        influenceRange = ser.FindProperty("influenceRange");
        isToEnemy = ser.FindProperty("isToEnemy");
        hasProjectile = ser.FindProperty("hasProjectile");
        Projectile = ser.FindProperty("Projectile");
    }

    public override void OnInspectorGUI()
    {
        ser.Update();
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

            EditorGUILayout.PropertyField(hasProjectile);
            if (hasProjectile.boolValue)
            {
                EditorGUILayout.PropertyField(Projectile);
            }
        }
        
        ser.ApplyModifiedProperties();
    }
}
