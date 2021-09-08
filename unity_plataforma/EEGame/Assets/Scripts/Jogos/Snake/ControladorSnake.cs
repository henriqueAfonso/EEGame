using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControladorSnake : MonoBehaviour
{
    public enum Direcoes
    {
        DIREITA, CIMA, BAIXO, ESQUERDA
    }

    public Direcoes direcao;
    public float pausa;
    public float passo;
    public List<Transform> corpo;
    private Vector3 ultimaPosicao;
    public Transform cabeca;
    public Transform comidaPreFab;
    public GameObject corpoPreFab;
    public int linhas = 14;
    public int colunas = 29;
    public Text pontuacao;
    public GameObject panelGameOver;
    public GameObject panelInicioJogo;
    public GameObject btn_Jogar;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direcao != Direcoes.BAIXO)
            direcao = Direcoes.CIMA;
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))&& direcao != Direcoes.DIREITA)
            direcao = Direcoes.ESQUERDA;
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direcao != Direcoes.CIMA)
            direcao = Direcoes.BAIXO;
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direcao != Direcoes.ESQUERDA)
            direcao = Direcoes.DIREITA;
    }

    IEnumerator Movimentacao()
    {
        yield return new WaitForSeconds(pausa);
        Vector3 nextPos = Vector3.zero;

        switch (direcao)
        {
            case Direcoes.BAIXO:
                nextPos = Vector3.down;
                cabeca.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direcoes.CIMA:
                nextPos = Vector3.up;
                cabeca.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direcoes.DIREITA:
                nextPos = Vector3.right;
                cabeca.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direcoes.ESQUERDA:
                nextPos = Vector3.left;
                cabeca.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }

        nextPos *= passo;
        ultimaPosicao = cabeca.position;
        cabeca.position += nextPos;

        foreach (Transform t in corpo)
        {
            Vector3 aux = t.position;
            t.position = ultimaPosicao;
            ultimaPosicao = aux;
            t.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        StartCoroutine("Movimentacao");
    }

    public void Comer()
    {
        Vector3 corpoPos = corpo[corpo.Count - 1].position;
        GameObject temp = Instantiate(corpoPreFab, corpoPos, transform.localRotation);
        corpo.Add(temp.transform);
        pontuacao.text = "pontuacao: " + ((corpo.Count - 2) * 5);
        SetComida();
    }

    private void SetComida()
    {
        int x = Random.Range(((colunas - 1) / 2) * -1, (colunas - 1) / 2);
        int y = Random.Range(((linhas - 1) / 2) * -1, (linhas - 1) / 2);

        foreach(Transform t in corpo)
        {
            if (t.position.x == x && t.position.y == y)
            {
                SetComida();
                break;
            }
        }
        comidaPreFab.position = new Vector2(x * passo, y * passo);
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        StopCoroutine("Movimentacao");
        EventSystem.current.SetSelectedGameObject(btn_Jogar);
    }

    public void Jogar()
    {
        if(corpo.Count > 2)
        {
            for(int i = corpo.Count - 1; i > 1; i--)
            {
                Destroy(corpo[i].gameObject);
                corpo.RemoveAt(i);    
            }
        }
            
        cabeca.position = new Vector3(0, 0, 0);
        corpo[0].position = new Vector3((float)-0.6,0,0);
        corpo[1].position = new Vector3((float)-1.2, 0, 0);
        direcao = Direcoes.DIREITA;
        SetComida();
        pontuacao.text = "Pontuacao: 0";   
        panelGameOver.SetActive(false);
        panelInicioJogo.SetActive(false);
        StartCoroutine("Movimentacao");
    }
}