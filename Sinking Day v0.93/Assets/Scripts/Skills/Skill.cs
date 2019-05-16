using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill: MonoBehaviour
{
    public float stillTime = 5;
    public float coolDown = 5;
    public bool isCasting = false;
    protected RaycastHit hit;
    protected float timer;
    

    void Update()
    {
        if (isCasting)
            Casting();
        else
            CoolDown();
    }

    public virtual void StartCast()
    {
        if (PointerEvent.isCasting)
            return;
        if(timer<=0)
        {
            PointerEvent.castingSkill = this;
            isCasting = true;
            PointerEvent.isCasting = true;
        }
    }

    public abstract void Casting();

    public abstract void CastSkill();

    public virtual void FinishCast()
    {
        PointerEvent.isCasting = false;
        isCasting = false;
        timer = coolDown;
        Cursor.visible = true;
        Cursor.SetCursor(UIManager.defaultPointer, Vector2.zero, CursorMode.Auto);
    }

    public virtual void StopCast()
    {
        PointerEvent.isCasting = false;
        isCasting = false;
        Cursor.visible = true;
        Cursor.SetCursor(UIManager.defaultPointer, Vector2.zero, CursorMode.Auto);
    }

    public void CoolDown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.GetChild(0).GetComponent<Image>().fillAmount = timer / coolDown;
        }
    }
}
