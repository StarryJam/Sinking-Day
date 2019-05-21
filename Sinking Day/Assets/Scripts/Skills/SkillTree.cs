using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillTree : MonoBehaviour {

    public Unit unit;
    public List<SkillTreeLayer> layers;


    private void Start()
    {
        for (int i = 0; i < layers.Count; i++)
        {
            if (i == 1)
                layers[i].SetActive(true);
            if (i < layers.Count - 1)
                layers[i].SetChildLayer(layers[i + 1]);
        }
    }

    public void LearnSkill(int layer, int index)
    {
        //unit.LearnSkill();
    }

    [Serializable]
    public class SkillTreeLayer
    {
        private bool isActive = false;
        public List<SkillTreeNode> Skills;
        private SkillTreeLayer childLayer;

        public void SetChildLayer(SkillTreeLayer layer)
        {
            childLayer = layer;
        }

        public void SetActive(bool _isActive)
        {
            isActive = _isActive;
        }
    }

    [Serializable]
    public class SkillTreeNode
    {
        public bool isActive = false;
        public bool isLearned = false;
        public Skill skill;
    }
}




