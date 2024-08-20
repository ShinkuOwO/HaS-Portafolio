using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDControlador : MonoBehaviour
{

    public Image HPTOTAL;
    public Image EXPTOTAL;

    public float HPmaximo = 100f;
    private float HPactual;

    public float EXPmaximo = 1000f;
    private float EXPactual;
    
    void Start()
    {
        HPactual = HPmaximo;
        EXPactual = 0f;
    }


    void Update()
    {
        // Para testear HP
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            SubirHP(10f);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            BajarHP(10f);
        }

        // Para testear EXP
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            SubirEXP(100f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BajarEXP(100f);
        }
    }
    
    //Funciones para testear la vida del jugador
    public void SubirHP(float amount)
    {
        HPactual += amount;
        HPactual = Mathf.Clamp(HPactual, 0, HPmaximo);
        ActualizaHP();
    }
    public void BajarHP(float amount)
    {
        HPactual -= amount;
        HPactual = Mathf.Clamp(HPactual, 0, HPmaximo);
        ActualizaHP();
    }

    private void ActualizaHP()
    {
        HPTOTAL.fillAmount = HPactual / HPmaximo;
    }
    
    //Funciones para testear la EXP del jugador
    public void SubirEXP(float amount)
    {
        EXPactual += amount;
        if (EXPactual >= EXPmaximo)
        {
            EXPactual = 0f; //Logica de subir de nivel
        }
        ActualizaEXP();
    }
    public void BajarEXP(float amount)
    {
        EXPactual -= amount;
        EXPactual = Mathf.Clamp(EXPactual, 0, EXPmaximo);
        ActualizaEXP();
    }

    private void ActualizaEXP()
    {
        EXPTOTAL.fillAmount = EXPactual / EXPmaximo;
    }
}
