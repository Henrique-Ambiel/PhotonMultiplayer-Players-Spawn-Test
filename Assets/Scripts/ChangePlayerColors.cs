using UnityEngine;
using Photon.Pun;

public class ChangePlayerColors : MonoBehaviour
{
    //Variáveis declaradas
    private Renderer rendererPlayer;
    private PhotonView view;

    void Start()
    {
        //Captura os componentes do GameObject para alterar a cor do material e e poder enviar e receber RPCs
        rendererPlayer = GetComponent<Renderer>();
        view = GetComponent<PhotonView>();

        //Define a cor inicial de cada player
        if (view.IsMine)
        {
            ChangeColorToRandom();
        }
    }

    
    void Update()
    {
        if (view.IsMine && Input.GetKeyDown(KeyCode.C))
        {
            //Se o player for o local e a tecla "C" for pressionada, muda a cor 
            ChangeColorToRandom();
        }
    }

    //Gera uma cor aleatória
    void ChangeColorToRandom()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        //Chama o RPC para sincronizar a cor com todos os players
        view.RPC("RPC_ChangeColor", RpcTarget.AllBuffered, randomColor);
    }

    [PunRPC]
    //Método irá ser chamado em todos os clientes para mudar de cor
    void RPC_ChangeColor(Color newColor)
    {
        rendererPlayer.material.color = newColor;
    }
}
