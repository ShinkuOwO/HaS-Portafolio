using System.Collections;
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

    // Configuracion de Movimiento para HP
    public float hpTrembleForce = 5f;
    public float hpTrembleDuration = 0.5f;
    public Vector2 hpTrembleDirection = Vector2.right;

    // Configuracion de Movimiento para EXP
    public float expMoveForce = 2f;
    public float expMoveDuration = 0.5f;
    public Vector2 expMoveDirection = Vector2.up; 

    // Colores para HP
    private Color defaultHPColor;
    public Color yellowHPColor = Color.yellow;
    public Color redHPColor = Color.red;

    void Start()
    {
        HPactual = HPmaximo;
        EXPactual = 0f;
        defaultHPColor = HPTOTAL.color;
        ActualizaHP();
        ActualizaEXP();
    }

    void Update()
    {
        // Para testear HP
        if (Input.GetKeyDown(KeyCode.T))
        {
            SubirHP(5f);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            BajarHP(5f);
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

    // Funciones para testear la vida del jugador
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
        StartCoroutine(TrembleHPBar());
    }

    private void ActualizaHP()
    {
        HPTOTAL.fillAmount = HPactual / HPmaximo;
        UpdateHPColor();
    }

    // Funciones para testear la EXP del jugador
    public void SubirEXP(float amount)
    {
        EXPactual += amount;
        if (EXPactual >= EXPmaximo)
        {
            EXPactual = 0f; // Logica de subir de nivel
            StartCoroutine(GentleEXPBarMovement());
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

    // Coroutine para movimiento de HPBar
    private IEnumerator TrembleHPBar()
    {
        Vector3 originalPosition = HPTOTAL.transform.parent.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < hpTrembleDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * hpTrembleForce * hpTrembleDirection.x;
            float yOffset = Random.Range(-1f, 1f) * hpTrembleForce * hpTrembleDirection.y;

            HPTOTAL.transform.parent.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        HPTOTAL.transform.parent.localPosition = originalPosition;
    }

    // Coroutine para el movimiento de EXPBar
    private IEnumerator GentleEXPBarMovement()
    {
        Vector3 originalPosition = EXPTOTAL.transform.parent.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < expMoveDuration)
        {
            float xOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2) * expMoveForce * expMoveDirection.x;
            float yOffset = Mathf.Sin(elapsedTime * Mathf.PI * 2) * expMoveForce * expMoveDirection.y;

            EXPTOTAL.transform.parent.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        EXPTOTAL.transform.parent.localPosition = originalPosition;
    }

    // Funcion para actualizar el color de la barra de vida
    private void UpdateHPColor()
    {
        float hpPercent = HPactual / HPmaximo;

        if (hpPercent > 0.5f)
        {
            // De color default a amarillo
            float t = (hpPercent - 0.5f) * 2f; 
            HPTOTAL.color = Color.Lerp(yellowHPColor, defaultHPColor, t);
        }
        else
        {
            // De amarillo a rojo
            float t = hpPercent * 2f; 
            HPTOTAL.color = Color.Lerp(redHPColor, yellowHPColor, t);
        }
    }
}