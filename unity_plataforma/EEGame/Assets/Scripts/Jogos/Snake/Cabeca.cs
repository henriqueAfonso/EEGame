using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabeca : MonoBehaviour
{

    public ControladorSnake controlador;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "comida":
                controlador.Comer();
                break;

            case "corpo":
                Time.timeScale = 0;
                break;
        }    
    }

}
